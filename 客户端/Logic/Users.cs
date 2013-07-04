namespace Logic
{
    using System;
    using System.Collections.Generic;
    using System.Data;

    public class Users
    {
        private static string CurrentUserName = "";
        private int id = 0;
        private string userName = null;
        private static Logic.UserType userType;

        public static string CountUsers(string Name)
        {
            return ("select Name from USERS where Name = '" + Name + "'");
        }

        public static string CreateUsers(string Name, string Password, string Level)
        {
            return ("insert into USERS (Name,Password, theLevel) values ('" + Name + "', '" + Password + "', '" + Level + "')");
        }

        public static void CreateUsersByZhangJin(string Name, string Password, string Level)
        {
            OperateDB.Execute("insert into USERS (Name,Password, theLevel) values ('" + Name + "', '" + Password + "', '" + Level + "')");
        }

        public static string DelUsers(string Name)
        {
            return ("delete from USERS where Name = '" + Name + "'");
        }

        public static string[] GetAllUserName()
        {
            List<string> list = new List<string>();
            string sql = "select Name from USERS";
            DataTable dt = OperateDB.Select(sql);
            if (dt != null)
            {
                foreach (DataRow row in dt.Rows)
                {
                    list.Add(row["Name"].ToString());
                }
            }
            return list.ToArray();
        }

        public static DataTable GetLevel(string Name)
        {
            return OperateDB.Select("select * from USERS where Name = '" + Name + "'");
        }

        public static DataTable GetUsers()
        {
            string sql = "select Name, Password, theLevel from USERS";
            return OperateDB.Select(sql);
        }

        public static DataTable GetWebUsers()
        {
            string sql = "select id, Name, Password, theLevel from USERS";
            return OperateDB.WebSelect(sql);
        }

        public static string UpdateUsers(string Name, string Password, string Level, string Original)
        {
            return ("update USERS set Name = '" + Name + "', Password = '" + Password + "',theLevel = '" + Level + "' where Name = '" + Original + "'");
        }

        public static string GlobalUserName
        {
            get
            {
                return CurrentUserName;
            }
            set
            {
                CurrentUserName = value;
            }
        }

        public int Id
        {
            get
            {
                return this.id;
            }
            set
            {
                this.id = value;
            }
        }

        public string UserName
        {
            get
            {
                return this.userName;
            }
            set
            {
                this.userName = value;
            }
        }

        public static Logic.UserType UserType
        {
            get
            {
                return userType;
            }
            set
            {
                userType = value;
            }
        }
    }
}

