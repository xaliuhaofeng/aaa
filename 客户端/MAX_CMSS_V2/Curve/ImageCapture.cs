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
			IntPtr dc1 = g.GetHdc();//��ô�����������豸 
			IntPtr dc2 = g2.GetHdc();//���λͼ�ļ����������豸 
			BitBlt(dc2, 0, 0, width, height, dc1, 0, 0, (UInt32)0xcc0020);//д�뵽λͼ 
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
			//�����Ļ�ľ��
			IntPtr dc3 = g1.GetHdc() ;
			//���λͼ�ľ��
			IntPtr dc2 = g2.GetHdc() ;
			//�ѵ�ǰ��Ļ����λͼ������
			BitBlt(dc2, 0, 0, width, height, dc1, 0, 0, (UInt32)0xcc0020);//д�뵽λͼ 
			//�ͷ���Ļ���
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
			DeleteDC(hscrdc);//ɾ���ù��Ķ���
			DeleteDC(hmemdc);//ɾ���ù��Ķ���
			return bmp;
		}

		public static Guid GetEncoderClsid(string format)
		{
			Guid picGUID = new Guid();
			ImageCodecInfo[] pImageCodecInfo;
			//��ȡ��������Ϣ
			pImageCodecInfo = ImageCodecInfo.GetImageEncoders();
			//����ָ����ʽ�ļ��ı�������Ϣ
			for(int i = 0; i < pImageCodecInfo.GetLength(0); ++i)
			{
				//MimeType:���뷽ʽ�ľ�������
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
			string lpszDriver,        // driver name������
			string lpszDevice,        // device name�豸��
			string lpszOutput,        // not used; should be NULL
			IntPtr lpInitData  // optional printer data
			);
		[DllImport("gdi32.dll")]
		public static extern int BitBlt(
			IntPtr hdcDest, // handle to destination DCĿ���豸�ľ��
			int nXDest,  // x-coord of destination upper-left cornerĿ���������Ͻǵ�X����
			int nYDest,  // y-coord of destination upper-left cornerĿ���������Ͻǵ�Y����
			int nWidth,  // width of destination rectangleĿ�����ľ��ο��
			int nHeight, // height of destination rectangleĿ�����ľ��γ���
			IntPtr hdcSrc,  // handle to source DCԴ�豸�ľ��
			int nXSrc,   // x-coordinate of source upper-left cornerԴ��������Ͻǵ�X����
			int nYSrc,   // y-coordinate of source upper-left cornerԴ��������Ͻǵ�Y����
			UInt32 dwRop  // raster operation code��դ�Ĳ���ֵ
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
