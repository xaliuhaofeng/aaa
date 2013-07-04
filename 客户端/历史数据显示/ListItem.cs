using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace 历史数据显示
{
    public class ListItem
    {
        private int id =0;
        private string name = string.Empty;
        public ListItem(string sname, int sid)
        {

            name = sname;
            id = sid;
        }
        public override string ToString()
        {
            return this.name;
        }
        public int ID
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
        public string Name
        {
            get
            {
                return this.name;
            }
            set
            {
                this.name = value;
            }
        } 
    }
}
