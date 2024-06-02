using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassItemLibrary
{
    public class clsUser
    {
        public string _userName { get; set; }
        public string _password { get; set; }
        public int _permissions {  get; set; }

        public bool MarkForDelete = false;
        public clsUser(string userName, string password, int permissions) {
        
            _userName = userName;
            _password = password;
            _permissions = permissions;
        }

        public void addUser()
        {
            List<clsUser> users = LoadDataFromFileToUserList();

            users.Add(this);

            LoadDataFromUsersListToFile(users);
        }

        public void deleteUser()
        {

            List<clsUser> Users = LoadDataFromFileToUserList();

            foreach (clsUser user in Users)
            {
               if(user._userName == _userName)
                {
                    user.MarkForDelete = true;
                    break;
                }

            }

            LoadDataFromUsersListToFile(Users);
        }

        public void updateUser()
        {
            List<clsUser> users = LoadDataFromFileToUserList();

            for (int i = 0; i < users.Count; i++) {

                if (users[i]._userName == _userName)
                {
                    users[i] = this;
                    break;
                }
            }

            LoadDataFromUsersListToFile(users);
        }

        public static clsUser findUser(string userName)
        {
            List<clsUser > users = LoadDataFromFileToUserList();

            foreach (clsUser user in users)
            {
                if(user._userName == userName)
                {
                    return user;
                }
            }

            return null;

        }

        public static clsUser findUser(string userName, string password)
        {
            List<clsUser> users = LoadDataFromFileToUserList();

            foreach (clsUser user in users)
            {
                if (user._userName == userName && user._password == password)
                {
                    return user;
                }
            }

            return null;

        }

        //_________________________________________________________________________
        // Files

        public static clsUser ConvertLineToUser(string line, string separator = "#//#")
        {
            string[] itemFields = line.Split(new string[] { separator }, StringSplitOptions.None);

            return new clsUser(itemFields[0], itemFields[1], Convert.ToInt32(itemFields[2]));
        }

        public static List<clsUser> LoadDataFromFileToUserList()
        {
            List<clsUser> usersRecordList = new List<clsUser>();
            using (StreamReader reader = new StreamReader("Users.txt"))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    // Check if the line is not empty or just whitespace
                    if (!string.IsNullOrWhiteSpace(line))
                    {
                        usersRecordList.Add(ConvertLineToUser(line));
                    }
                }
            }
            return usersRecordList;
        }

        public static string ConvertUserObjectToLine(clsUser user, string separator = "#//#")
        {
            return $"{user._userName}{separator}{user._password}{separator}{user._permissions}";
        }

        public static void LoadDataFromUsersListToFile(List<clsUser> usersRecordList)
        {
            using (StreamWriter writer = new StreamWriter("Users.txt"))
            {
                foreach (clsUser user in usersRecordList)
                {
                    if (!user.MarkForDelete)
                    {
                        writer.WriteLine(ConvertUserObjectToLine(user));
                    }

                }
            }
        }

        //_________________________________________________________________________


    }
}
