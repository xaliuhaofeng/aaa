using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Printing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;

namespace MAX_CMSS_V2.Curve
{
    
    public class PrintCurve
    {
        private  PrintDocument printDoc = new PrintDocument();
        private  Bitmap printBmp;
        public  void Print_Curve(Bitmap bmp)
        {
            PrintPreviewDialog ppvw;
            PrintDialog pd;
            PageSetupDialog psd;
            printBmp = bmp;
            try
            {
              //  psd = new PageSetupDialog();
             //   PageSettings ps = new PageSettings();
             //   ps.Landscape = true;
                //psd.Document = printDoc;
               // psd.PageSettings = printDoc.DefaultPageSettings;

               // psd.PageSettings = ps;
             //   psd.ShowDialog();

                //psd.PageSettings.Landscape = true;

                printDoc.DefaultPageSettings.Landscape =true;
                //printDoc.DefaultPageSettings.PaperSize = psd.PageSettings.PaperSize;

                ppvw = new PrintPreviewDialog();

                ppvw.Document = printDoc;

                // Showing the Print Preview Page
                printDoc.BeginPrint += new PrintEventHandler(printDoc_BeginPrint);
                printDoc.PrintPage += new PrintPageEventHandler(printDoc_PrintPage);
                ppvw.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {

            }
        }

         void printDoc_PrintPage(object sender, PrintPageEventArgs e)
        {
            try
            {
                int SizeHeight = e.PageSettings.Bounds.Height;
                int SizeWidth = e.PageSettings.Bounds.Width;
                int destHeight = e.MarginBounds.Size.Height;
                int destWidth = e.MarginBounds.Size.Width;
                int sW = 0, sH = 0;
                // 按比例缩放
                int sWidth = printBmp.Width;
                int sHeight = printBmp.Height;

                if (sHeight > destHeight || sWidth > destWidth)
                {
                    if ((sWidth * destHeight) > (sHeight * destWidth))
                    {
                        sW = destWidth;
                        sH = (destWidth * sHeight) / sWidth;
                    }
                    else
                    {
                        sH = destHeight;
                        sW = (sWidth * destHeight) / sHeight;
                    }
                }
                else
                {
                    sW = sWidth;
                    sH = sHeight;
                }
                Bitmap b = new Bitmap(destWidth, destHeight);
                Graphics g = Graphics.FromImage(b);
                // 设置画布的描绘质量
                g.CompositingQuality = CompositingQuality.HighQuality;
                g.SmoothingMode = SmoothingMode.HighQuality;
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                g.DrawImage(printBmp, new Rectangle(0, 0, sW, sH), new Rectangle(0, 0, printBmp.Width, printBmp.Height), GraphicsUnit.Pixel);
                g.Dispose();
                e.Graphics.DrawImage(b, (SizeWidth - destWidth)/ 2, (SizeHeight - destHeight) / 2);
                b.Dispose();
              //  printBmp.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

         void printDoc_BeginPrint(object sender, PrintEventArgs e)
        {
            
        }
    }
}
