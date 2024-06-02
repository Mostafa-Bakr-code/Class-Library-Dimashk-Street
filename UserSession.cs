using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassItemLibrary
{
    public static class UserSession
    {
        public static clsUser ActiveUser { get; private set; }

        public static void SetActiveUser(clsUser user)
        {
            ActiveUser = user;
        }

        public static void ClearActiveUser()
        {
            ActiveUser = null;
        }

    }
}
