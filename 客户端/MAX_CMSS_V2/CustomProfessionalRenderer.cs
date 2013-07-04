namespace MAX_CMSS_V2
{
    using System;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Windows.Forms;

    public class CustomProfessionalRenderer : ToolStripProfessionalRenderer
    {
        private Color _color;

        public CustomProfessionalRenderer()
        {
            this._color = Color.Red;
        }

        public CustomProfessionalRenderer(Color color)
        {
            this._color = Color.Red;
            this._color = color;
        }

        public static GraphicsPath GetRoundedRectPath(Rectangle rect, int radius)
        {
            int diameter = radius;
            Rectangle arcRect = new Rectangle(rect.Location, new Size(diameter, diameter));
            GraphicsPath path = new GraphicsPath();
            path.AddArc(arcRect, 180f, 90f);
            arcRect.X = rect.Right - diameter;
            path.AddArc(arcRect, 270f, 90f);
            arcRect.Y = rect.Bottom - diameter;
            path.AddArc(arcRect, 0f, 90f);
            arcRect.X = rect.Left;
            path.AddArc(arcRect, 90f, 90f);
            path.CloseFigure();
            return path;
        }

        protected override void OnRenderArrow(ToolStripArrowRenderEventArgs e)
        {
            e.ArrowColor = this._color;
            base.OnRenderArrow(e);
        }

        protected override void OnRenderImageMargin(ToolStripRenderEventArgs e)
        {
        }

        protected override void OnRenderMenuItemBackground(ToolStripItemRenderEventArgs e)
        {
            LinearGradientBrush lgbrush;
            GraphicsPath gp;
            Graphics g = e.Graphics;
            ToolStripItem item = e.Item;
            ToolStrip toolstrip = e.ToolStrip;
            if (toolstrip is MenuStrip)
            {
                lgbrush = new LinearGradientBrush(new Point(0, 0), new Point(0, item.Height), Color.FromArgb(100, Color.White), Color.FromArgb(0, Color.White));
                SolidBrush brush = new SolidBrush(Color.FromArgb(0xff, Color.White));
                if (e.Item.Selected)
                {
                    gp = GetRoundedRectPath(new Rectangle(new Point(0, 0), item.Size), 5);
                    g.FillPath(lgbrush, gp);
                }
                if (item.Pressed)
                {
                    g.FillRectangle(Brushes.White, new Rectangle(Point.Empty, item.Size));
                }
            }
            else if (toolstrip is ToolStripDropDown)
            {
                g.SmoothingMode = SmoothingMode.HighQuality;
                lgbrush = new LinearGradientBrush(new Point(0, 0), new Point(item.Width, 0), Color.FromArgb(200, this._color), Color.FromArgb(0, Color.White));
                if (item.Selected)
                {
                    gp = GetRoundedRectPath(new Rectangle(0, 0, item.Width, item.Height), 10);
                    g.FillPath(lgbrush, gp);
                }
            }
            else
            {
                base.OnRenderMenuItemBackground(e);
            }
        }

        protected override void OnRenderSeparator(ToolStripSeparatorRenderEventArgs e)
        {
            Graphics g = e.Graphics;
            LinearGradientBrush lgbrush = new LinearGradientBrush(new Point(0, 0), new Point(e.Item.Width, 0), this._color, Color.FromArgb(0, this._color));
            g.FillRectangle(lgbrush, new Rectangle(3, e.Item.Height / 2, e.Item.Width, 1));
        }

        protected override void OnRenderToolStripBackground(ToolStripRenderEventArgs e)
        {
            int diameter;
            GraphicsPath path;
            Rectangle rect;
            Rectangle arcRect;
            ToolStrip toolStrip = e.ToolStrip;
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.HighQuality;
            Rectangle bounds = e.AffectedBounds;
            LinearGradientBrush lgbrush = new LinearGradientBrush(new Point(0, 0), new Point(0, toolStrip.Height), Color.FromArgb(0xff, Color.White), Color.FromArgb(150, this._color));
            if (toolStrip is MenuStrip)
            {
                diameter = 10;
                path = new GraphicsPath();
                rect = new Rectangle(Point.Empty, toolStrip.Size);
                arcRect = new Rectangle(rect.Location, new Size(diameter, diameter));
                path.AddLine(0, 0, 10, 0);
                arcRect.X = rect.Right - diameter;
                path.AddArc(arcRect, 270f, 90f);
                arcRect.Y = rect.Bottom - diameter;
                path.AddArc(arcRect, 0f, 90f);
                arcRect.X = rect.Left;
                path.AddArc(arcRect, 90f, 90f);
                path.CloseFigure();
                toolStrip.Region = new Region(path);
                g.FillPath(lgbrush, path);
            }
            else if (toolStrip is ToolStripDropDown)
            {
                diameter = 10;
                path = new GraphicsPath();
                rect = new Rectangle(Point.Empty, toolStrip.Size);
                arcRect = new Rectangle(rect.Location, new Size(diameter, diameter));
                path.AddLine(0, 0, 10, 0);
                arcRect.X = rect.Right - diameter;
                path.AddArc(arcRect, 270f, 90f);
                arcRect.Y = rect.Bottom - diameter;
                path.AddArc(arcRect, 0f, 90f);
                arcRect.X = rect.Left;
                path.AddArc(arcRect, 90f, 90f);
                path.CloseFigure();
                toolStrip.Region = new Region(path);
                g.FillPath(lgbrush, path);
            }
            else
            {
                base.OnRenderToolStripBackground(e);
            }
        }

        protected override void OnRenderToolStripBorder(ToolStripRenderEventArgs e)
        {
        }
    }
}

