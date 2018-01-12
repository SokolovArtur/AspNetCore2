using System.Collections.Generic;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Tochka.Models;
using YamlDotNet.RepresentationModel;

namespace Tochka.Components
{
    public class SideNavViewComponent : ViewComponent
    {
        private readonly IHostingEnvironment _env;

        public SideNavViewComponent(IHostingEnvironment env)
        {
            _env = env;
        }

        public IViewComponentResult Invoke()
        {
            YamlMappingNode mapping;

            using (StreamReader input =
                new StreamReader(_env.ContentRootPath + Path.DirectorySeparatorChar + "menu.yml"))
            {
                YamlStream yaml = new YamlStream();
                yaml.Load(input);
                mapping = (YamlMappingNode) yaml.Documents[0].RootNode;
            }

            if (mapping == null)
            {
                return View("Empty");
            }

            bool isAuthenticated = User.Identity.IsAuthenticated;
            // ... current user roles

            var model = new Dictionary<string, IEnumerable<MenuItem>>();
            foreach (var section in mapping.Children)
            {
                var menus = new List<MenuItem>();
                string sectionName = ((YamlScalarNode) section.Key).Value;

                YamlSequenceNode sectionItems = (YamlSequenceNode) mapping.Children[sectionName];
                foreach (YamlMappingNode sectionItem in sectionItems)
                {
                    bool anonymous = ((YamlScalarNode) sectionItem.Children[new YamlScalarNode("anonymous")]).Value.ToLower() == "true";
                    if (!anonymous && !isAuthenticated)
                    {
                        continue;
                    }
                    
                    menus.Add(new MenuItem
                    {
                        Name = ((YamlScalarNode) sectionItem.Children[new YamlScalarNode("name")]).Value,
                        Area = ((YamlScalarNode) sectionItem.Children[new YamlScalarNode("area")]).Value,
                        Controller = ((YamlScalarNode) sectionItem.Children[new YamlScalarNode("controller")]).Value,
                        Action = ((YamlScalarNode) sectionItem.Children[new YamlScalarNode("action")]).Value
                    });
                }

                if (menus.Count > 0)
                {
                    model.Add(sectionName, menus);
                }
            }

            return View(model);
        }
    }
}