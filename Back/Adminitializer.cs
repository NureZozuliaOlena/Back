using Back.Models;
using System.Linq;

namespace Back
{
    public static class AdminInitializer
    {
        public static void InitAdmins(AppContext appContext)
        {
            var admin = new Admin()
            {
                Name = "MainAdmin",
                Email = "admin@gmail.com",
                Password = "admin123456",
            };

            if (appContext.Admins.Any(c => c.Email == admin.Email))
                return;

            appContext.Admins.Add(admin);
            appContext.SaveChanges();
        }
    }
}
