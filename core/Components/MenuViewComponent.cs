using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Tochka.Models;
using YamlDotNet.RepresentationModel;

namespace Tochka.Components
{
    public class MenuViewComponent : ViewComponent
    {
        private readonly IHostingEnvironment _env;

        public MenuViewComponent(IHostingEnvironment env)
        {
            _env = env;
        }

        public IViewComponentResult Invoke(string viewName = "Default", string section = null)
        {
            YamlMappingNode yaml = GetYaml();
            if (yaml == null)
            {
                return View("Empty");
            }
            
            var model = new Dictionary<string, IEnumerable<MenuItem>>();
            if (section == null)
            {
                foreach (var children in yaml.Children)
                {
                    string nodeName = ((YamlScalarNode) children.Key).Value;
                    IEnumerable<MenuItem> menus = GetMenuItems(yaml, nodeName);
                    if (menus.Any())
                    {
                        model.Add(nodeName, (List<MenuItem>) menus);
                    }
                }
            }
            else
            {
                IEnumerable<MenuItem> menus = GetMenuItems(yaml, section);
                if (menus.Any())
                {
                    model.Add(section, (List<MenuItem>) menus);
                }
            }

            return View(viewName, model);
        }

        private YamlMappingNode GetYaml()
        {
            YamlMappingNode yamlMappingNode;

            string filePath = _env.ContentRootPath + Path.DirectorySeparatorChar + "menu.yml";
            using (StreamReader input = new StreamReader(filePath))
            {
                YamlStream yaml = new YamlStream();
                yaml.Load(input);
                yamlMappingNode = (YamlMappingNode) yaml.Documents[0].RootNode;
            }

            return yamlMappingNode;
        }

        private IEnumerable<MenuItem> GetMenuItems(YamlMappingNode yaml, string nodeName)
        {
            var items = new List<MenuItem>();
            
            bool isAuthenticated = User.Identity.IsAuthenticated;
            // ... current user roles
            // ... yaml.Children[nodeName] isEmpty - throw

            foreach (YamlMappingNode childNode in (YamlSequenceNode) yaml.Children[nodeName])
            {
                bool anonymous = ((YamlScalarNode) childNode.Children[new YamlScalarNode("anonymous")]).Value.ToLower() == "true";
                if (!anonymous && !isAuthenticated)
                {
                    continue;
                }
                
                items.Add(new MenuItem
                {
                    Name = ((YamlScalarNode) childNode.Children[new YamlScalarNode("name")]).Value,
                    Area = ((YamlScalarNode) childNode.Children[new YamlScalarNode("area")]).Value,
                    Controller = ((YamlScalarNode) childNode.Children[new YamlScalarNode("controller")]).Value,
                    Action = ((YamlScalarNode) childNode.Children[new YamlScalarNode("action")]).Value
                });
            }

            return items;
        }
    }
}