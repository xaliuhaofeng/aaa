namespace Logic
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Data;

    public class TongDao
    {
        public static ArrayList GetAllTongDaoHao()
        {
            ArrayList a = new ArrayList();
            for (int i = 1; i <= 0x10; i++)
            {
                a.Add(i);
            }
            return a;
        }

        public static string[] GetUnUsedTongDao(int fenZhanHao)
        {
            List<string> list = new List<string>();
            for (int i = 1; i < 0x10; i++)
            {
                list.Add(i.ToString());
            }
            DataTable dt = OperateDB.Select("select tongDaoHao from CeDian where fenZhanHao = " + fenZhanHao);
            if (dt != null)
            {
                foreach (DataRow row in dt.Rows)
                {
                    string tongdao = row[0].ToString();
                    if (list.Contains(tongdao))
                    {
                        list.Remove(tongdao);
                    }
                }
                return list.ToArray();
            }
            return new List<string>().ToArray();
        }
    }
}

