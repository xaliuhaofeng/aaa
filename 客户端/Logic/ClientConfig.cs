namespace Logic
{
    using System;
    using System.Collections;
    using System.Data;

    public class ClientConfig
    {
        private static ClientConfig common;
        private Hashtable table = new Hashtable();

        private ClientConfig()
        {
            string s = "select * from HashTable";
            DataTable data = OperateDBAccess.Select(s);
            foreach (DataRow row in data.Rows)
            {
                this.table.Add(row["sysKey"], row["theValue"]);
            }
        }

        public void add(string key, string value)
        {
            if (this.table.ContainsKey(key))
            {
                this.set(key, value);
            }
            else
            {
                this.table.Add(key, value);
                OperateDBAccess.Execute("insert into HashTable(sysKey, theValue) values('" + key + "', '" + value + "')");
            }
        }

        public static ClientConfig CreateCommon()
        {
            if (common == null)
            {
                common = new ClientConfig();
            }
            return common;
        }

        public string get(string key)
        {
            if (this.table.ContainsKey(key))
            {
                return (string) this.table[key];
            }
            return null;
        }

        public static void main()
        {
            ClientConfig common = CreateCommon();
            common.add("color", "red");
            Console.WriteLine(common.get("color"));
        }

        public void set(string key, string value)
        {
            this.table[key] = value;
            OperateDBAccess.Execute("update HashTable set theValue = '" + value + "' where sysKey = '" + key + "'");
        }
    }
}

