using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace MAX_CMSS_V2.Curve
{
	public class ImageCapture
	{
		public static Bitmap GetPartScreen(Graphics g, int width, int height)
		{
			Bitmap bmp = new Bitmap(width, height, g); 
			Graphics g2 = Graphics.FromImage(bmp);
			IntPtr dc1 = g.GetHdc();//获得窗体的上下文设备 
			IntPtr dc2 = g2.GetHdc();//获得位图文件的上下文设备 
			BitBlt(dc2, 0, 0, width, height, dc1, 0, 0, (UInt32)0xcc0020);//写入到位图 
			g.ReleaseHdc(dc1);
			g2.ReleaseHdc(dc2);
			return bmp;
		}

		public static Bitmap GetFullScreen()
		{
			IntPtr dc1 = CreateDC("DISPLAY", null, null, (IntPtr)null);
			int width = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width;
			int height = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height;
			Graphics g1 = Graphics.FromHdc(dc1);
			Bitmap bmp = new Bitmap(width, height, g1);
			Graphics g2 = Graphics.FromImage (bmp) ;
			//获得屏幕的句柄
			IntPtr dc3 = g1.GetHdc() ;
			//获得位图的句柄
			IntPtr dc2 = g2.GetHdc() ;
			//把当前屏幕捕获到位图对象中
			BitBlt(dc2, 0, 0, width, height, dc1, 0, 0, (UInt32)0xcc0020);//写入到位图 
			//释放屏幕句柄
			g1.ReleaseHdc (dc3) ;
			g2.ReleaseHdc (dc2) ;
			return bmp;
		}

		public static Bitmap GetWindow(IntPtr hWnd)
		{
			IntPtr hscrdc = GetWindowDC(hWnd);
            System.Windows.Forms.Control control = System.Windows.Forms.Control.FromHandle(hWnd);
			IntPtr hbitmap = CreateCompatibleBitmap(hscrdc, control.Width, control.Height);
			IntPtr hmemdc = CreateCompatibleDC(hscrdc);
			SelectObject(hmemdc, hbitmap);
			PrintWindow(hWnd, hmemdc, 0);
			Bitmap bmp = Bitmap.FromHbitmap(hbitmap);
			DeleteDC(hscrdc);//删除用过的对象
			DeleteDC(hmemdc);//删除用过的对象
			return bmp;
		}

		public static Guid GetEncoderClsid(string format)
		{
			Guid picGUID = new Guid();
			ImageCodecInfo[] pImageCodecInfo;
			//获取编码器信息
			pImageCodecInfo = ImageCodecInfo.GetImageEncoders();
			//查找指定格式文件的编码器信息
			for(int i = 0; i < pImageCodecInfo.GetLength(0); ++i)
			{
				//MimeType:编码方式的具体描述
				if(format.CompareTo(pImageCodecInfo[i].MimeType.ToString()) == 0)
				{
					picGUID = pImageCodecInfo[i].Clsid;
				}
			}
			return picGUID;
		}

		public static ImageCodecInfo GetEncoderInfo(string mimeType)
		{
			ImageCodecInfo[] encoders = ImageCodecInfo.GetImageEncoders();
			for(int i = 0; i < encoders.Length; ++i)
			{
				if(encoders[i].MimeType == mimeType)
					return encoders[i];
			}
			return null;
		}

		public static bool SaveJPEG(string fileName, Bitmap bmp, long quality)
		{
			ImageCodecInfo myImageCodecInfo = GetEncoderInfo("image/jpeg");
			Encoder myEncoder= Encoder.Quality;
			EncoderParameter myEncoderParameter = new EncoderParameter(myEncoder, quality);
			EncoderParameters myEncoderParameters = new EncoderParameters(1);
			myEncoderParameters.Param[0] = myEncoderParameter;
			try
			{
				bmp.Save(fileName, myImageCodecInfo, myEncoderParameters);
				return true;
			}
			catch
			{
				return false;
			}
		}

		[DllImport("gdi32.dll")]
		public static extern IntPtr CreateDC(
			string lpszDriver,        // driver name驱动名
			string lpszDevice,        // device name设备名
			string lpszOutput,        // not used; should be NULL
			IntPtr lpInitData  // optional printer data
			);
		[DllImport("gdi32.dll")]
		public static extern int BitBlt(
			IntPtr hdcDest, // handle to destination DC目标设备的句柄
			int nXDest,  // x-coord of destination upper-left corner目标对象的左上角的X坐标
			int nYDest,  // y-coord of destination upper-left corner目标对象的左上角的Y坐标
			int nWidth,  // width of destination rectangle目标对象的矩形宽度
			int nHeight, // height of destination rectangle目标对象的矩形长度
			IntPtr hdcSrc,  // handle to source DC源设备的句柄
			int nXSrc,   // x-coordinate of source upper-left corner源对象的左上角的X坐标
			int nYSrc,   // y-coordinate of source upper-left corner源对象的左上角的Y坐标
			UInt32 dwRop  // raster operation code光栅的操作值
			);

		[DllImport("gdi32.dll")]
		public static extern IntPtr CreateCompatibleDC(
			IntPtr hdc // handle to DC
			);

		[DllImport("gdi32.dll")]
		public static extern IntPtr CreateCompatibleBitmap(
			IntPtr hdc,        // handle to DC
			int nWidth,     // width of bitmap, in pixels
			int nHeight     // height of bitmap, in pixels
			);

		[DllImport("gdi32.dll")]
		public static extern IntPtr SelectObject(
			IntPtr hdc,          // handle to DC
			IntPtr hgdiobj   // handle to object
			);

		[DllImport("gdi32.dll")]
		public static extern int DeleteDC(
			IntPtr hdc          // handle to DC
			);

		[DllImport("user32.dll")]
		public static extern bool PrintWindow(
			IntPtr hwnd,               // Window to copy,Handle to the window that will be copied. 
			IntPtr  hdcBlt,             // HDC to print into,Handle to the device context. 
			UInt32 nFlags              // Optional flags,Specifies the drawing options. It can be one of the following values. 
			);

		[DllImport("user32.dll")]
		public static extern IntPtr GetWindowDC(
			IntPtr hwnd
			);
	}
}
