namespace Logic
{
    using System;
    using System.Data;

    public class XianXingZhi
    {
        private int id;
        private float liangCheng0;
        private float liangCheng1;
        private float liangCheng2;
        private float liangCheng3;
        private float liangCheng4;
        private string moNiLiangMingCheng;
        private float value0;
        private float value1;
        private float value2;
        private float value3;
        private float value4;

        public XianXingZhi()
        {
        }

        public XianXingZhi(string mingcheng)
        {
            DataTable dt = OperateDB.Select("select * from XianXingZhi where moNiliangMingCheng='" + mingcheng + "'");
            if ((dt == null) || (dt.Rows.Count == 0))
            {
                Console.WriteLine("不存在限性值类型！");
            }
            else
            {
                this.MoNiLiangMingCheng = dt.Rows[0]["moNiLiangMingCheng"].ToString().Trim();
                this.Id = Convert.ToInt32(dt.Rows[0]["id"]);
                this.LiangCheng0 = Convert.ToSingle(dt.Rows[0]["liangCheng0"]);
                this.LiangCheng1 = Convert.ToSingle(dt.Rows[0]["liangCheng1"]);
                this.LiangCheng2 = Convert.ToSingle(dt.Rows[0]["liangCheng2"]);
                this.LiangCheng3 = Convert.ToSingle(dt.Rows[0]["liangCheng3"]);
                this.LiangCheng4 = Convert.ToSingle(dt.Rows[0]["liangCheng4"]);
                this.Value0 = Convert.ToSingle(dt.Rows[0]["value0"]);
                this.Value1 = Convert.ToSingle(dt.Rows[0]["value1"]);
                this.Value2 = Convert.ToSingle(dt.Rows[0]["value2"]);
                this.Value3 = Convert.ToSingle(dt.Rows[0]["value3"]);
                this.Value4 = Convert.ToSingle(dt.Rows[0]["value4"]);
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

        public float LiangCheng0
        {
            get
            {
                return this.liangCheng0;
            }
            set
            {
                this.liangCheng0 = value;
            }
        }

        public float LiangCheng1
        {
            get
            {
                return this.liangCheng1;
            }
            set
            {
                this.liangCheng1 = value;
            }
        }

        public float LiangCheng2
        {
            get
            {
                return this.liangCheng2;
            }
            set
            {
                this.liangCheng2 = value;
            }
        }

        public float LiangCheng3
        {
            get
            {
                return this.liangCheng3;
            }
            set
            {
                this.liangCheng3 = value;
            }
        }

        public float LiangCheng4
        {
            get
            {
                return this.liangCheng4;
            }
            set
            {
                this.liangCheng4 = value;
            }
        }

        public string MoNiLiangMingCheng
        {
            get
            {
                return this.moNiLiangMingCheng;
            }
            set
            {
                this.moNiLiangMingCheng = value;
            }
        }

        public float Value0
        {
            get
            {
                return this.value0;
            }
            set
            {
                this.value0 = value;
            }
        }

        public float Value1
        {
            get
            {
                return this.value1;
            }
            set
            {
                this.value1 = value;
            }
        }

        public float Value2
        {
            get
            {
                return this.value2;
            }
            set
            {
                this.value2 = value;
            }
        }

        public float Value3
        {
            get
            {
                return this.value3;
            }
            set
            {
                this.value3 = value;
            }
        }

        public float Value4
        {
            get
            {
                return this.value4;
            }
            set
            {
                this.value4 = value;
            }
        }
    }
}

