namespace Logic
{
    using System;
    using System.Data;

    public class ComData
    {
        public static DataTable JoinTable(DataTable DataTable1, DataTable DataTable2)
        {
            object[] obj = new object[DataTable1.Columns.Count];
            for (int i = 0; i < DataTable2.Rows.Count; i++)
            {
                DataTable2.Rows[i].ItemArray.CopyTo(obj, 0);
                DataTable1.Rows.Add(obj);
            }
            return DataTable1;
        }

        public static DataTable ListData()
        {
            DataTable JoinTable = new DataTable("List");
            DataColumn DiDian = new DataColumn("DiDian", Type.GetType("System.String"));
            JoinTable.Columns.Add(DiDian);
            DataColumn DianHao = new DataColumn("DianHao", Type.GetType("System.String"));
            JoinTable.Columns.Add(DianHao);
            return JoinTable;
        }
    }
}

