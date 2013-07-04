namespace Logic
{
    using System;
    using System.Data;

    public class MoNiTuFiles
    {
        public static string getCfgXml()
        {
            string xml = "";
            string sql = "select xmlContent from MoNiTuFiles where fileName='cfg.xml'";
            DataTable dt = OperateDB.Select(sql);
            if ((dt != null) && (dt.Rows.Count > 0))
            {
                xml = dt.Rows[0][0].ToString();
            }
            return xml;
        }

        public static byte[] getImageByName(string fileName)
        {
            DataTable dt = OperateDB.Select("select imageContent from MoNiTuFiles where fileName='" + fileName + "'");
            byte[] ib = null;
            if ((dt != null) && (dt.Rows.Count > 0))
            {
                ib = (byte[]) dt.Rows[0][0];
            }
            return ib;
        }

        public static DataTable getImagesFiles()
        {
            string sql = "select * from MoNiTuFiles where fileName<> 'cfg.xml'";
            return OperateDB.Select(sql);
        }

        public static void insertImageContent(string fileName, byte[] imageContent)
        {
        }

        public static void insertXmlContent(string fileName, string xmlContent)
        {
        }

        public static void trncateMoNiTuFiles()
        {
        }
    }
}

