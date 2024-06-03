using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassItemLibrary
{
    public static class UserSession
    {
        public static clsUser ActiveUser { get; private set; }

        public static List<clsLog> LoginHistory { get; private set; } = new List<clsLog>();


        private static clsLog currentLog;

        public static void SetActiveUser(clsUser user)
        {
            ActiveUser = user;
            
            RecordLogin();
           

        }

        public static void ClearActiveUser()
        {
            RecordLogout();
            ActiveUser = null;
            
        }

        private static void RecordLogin()
        {
            string dateIn = DateTime.Now.ToString("yyyy-MM-dd");
            string timeIn = DateTime.Now.ToString("HH:mm:ss");
            string userName = ActiveUser._userName;

            currentLog = new clsLog(dateIn, timeIn, null, null, userName);
        }

        private static void RecordLogout()
        {
            if (currentLog != null)
            {
                currentLog._dateOut = DateTime.Now.ToString("yyyy-MM-dd");
                currentLog._timeOut = DateTime.Now.ToString("HH:mm:ss");

                LoginHistory.Add(currentLog);
                //currentLog = null;

                
                clsLog.LoadDataFromLogListToFile(LoginHistory);
            }
        }

    }
}
