using Microsoft.AspNetCore.Mvc;

namespace Tochka.Components
{
    public class LoginViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            string userName = null;
            if (User.Identity.IsAuthenticated)
            {
                userName = User.Identity.Name;
            }

            return View(new LoginViewModel
            {
                UserName = userName
            });
        }
    }

    public class LoginViewModel
    {
        public string UserName { get; set; }
    }
}