namespace MAX_CMSS_V2
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    internal class GridView : DataGridView
    {
        private static Color _BeforCellSelect;
        private static Color _BeforInCellsColor;
        private static Color _MouseOverHighColor = Color.SkyBlue;
        private bool _UseMouseOverStyle = false;

        protected override void OnCellClick(DataGridViewCellEventArgs e)
        {
            base.OnCellClick(e);
            _BeforCellSelect = _BeforInCellsColor;
        }

        protected override void OnCellMouseEnter(DataGridViewCellEventArgs e)
        {
            base.OnCellMouseEnter(e);
            if ((this.UseMouseOverStyle && ((e.RowIndex > -1) && (e.ColumnIndex > -1))) && !base.Rows[e.RowIndex].Selected)
            {
                _BeforInCellsColor = base.Rows[e.RowIndex].DefaultCellStyle.BackColor;
                base.Rows[e.RowIndex].DefaultCellStyle.BackColor = _MouseOverHighColor;
            }
        }

        protected override void OnCellMouseLeave(DataGridViewCellEventArgs e)
        {
            base.OnCellMouseLeave(e);
            if ((this.UseMouseOverStyle && ((e.RowIndex > -1) && (e.ColumnIndex > -1))) && !base.Rows[e.RowIndex].Selected)
            {
                base.Rows[e.RowIndex].DefaultCellStyle.BackColor = _BeforInCellsColor;
            }
        }

        protected override void OnDataBindingComplete(DataGridViewBindingCompleteEventArgs e)
        {
            base.OnDataBindingComplete(e);
            if (base.Rows.Count > 0)
            {
                for (int i = 0; i < base.Rows.Count; i++)
                {
                    base.Rows[i].HeaderCell.Value = (i + 1).ToString();
                }
                if (base.SelectedRows.Count > 0)
                {
                    base.SelectedRows[0].Selected = false;
                }
            }
        }

        protected override void OnRowLeave(DataGridViewCellEventArgs e)
        {
            base.OnRowLeave(e);
            base.Rows[e.RowIndex].DefaultCellStyle.BackColor = _BeforCellSelect;
        }

        [Category("OtherStyle"), DefaultValue(typeof(Color), "SkyBlue"), Browsable(true), Description("鼠标经过时的高亮颜色")]
        public Color MouseOverHighColor
        {
            get
            {
                return _MouseOverHighColor;
            }
            set
            {
                _MouseOverHighColor = value;
            }
        }

        [DefaultValue(false), Browsable(true), Category("OtherStyle"), Description("是否启用鼠标经过时颜色高亮，如启用，SelectionMode也会被设为FullRowSelect")]
        public bool UseMouseOverStyle
        {
            get
            {
                return this._UseMouseOverStyle;
            }
            set
            {
                if (value)
                {
                    base.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                }
                else
                {
                    base.SelectionMode = DataGridViewSelectionMode.RowHeaderSelect;
                }
                this._UseMouseOverStyle = value;
            }
        }
    }
}

