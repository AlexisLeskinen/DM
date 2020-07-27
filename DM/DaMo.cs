using System.Threading;
using System.Threading.Tasks;

namespace DM
{
    class DaMo
    {
        //大漠插件对象
        private Dm.dmsoft DMObeject;
        //该对象绑定的窗口
        private int WindowHandle;
        //该窗口的大小
        private int height, width;

        //保持Find到的坐标
        public int FindX = 0;
        public int FindY = 0;

        bool scanning = false;
        public DaMo()
        {
            DMObeject = new Dm.dmsoft();
            WindowHandle = 0;
        }
        //判断该对象是否已经绑定窗口
        public bool IsBind()
        {
            return WindowHandle != 0;
        }
        //根据进程ID绑定窗口
        public bool BindWindow(int hwd)
        {
            if (0 == hwd) return false;
            int result = DMObeject.BindWindow(hwd, "normal", "windows", "windows", 0);
            if (0 == result) return false;
            WindowHandle = hwd;
            SetHW();
            return true;
        }
        public bool BindWindow(string className, string windowsName, string mouseMode = "windows")
        {
            WindowHandle = DMObeject.FindWindow(className, windowsName);
            if (0 == WindowHandle) return false;
            int result = DMObeject.BindWindow(WindowHandle, "normal", mouseMode, "windows", 0);
            SetHW();
            return 1 == result;
        }
        public int GetWhd()
        {
            return WindowHandle;
        }
        //获取当前窗口的宽高信息
        private void SetHW()
        {
            int result = DMObeject.GetWindowRect(WindowHandle,
                out object IntX0, out object IntY0, out object IntX1, out object IntY1);
            if (1 == result)
            {
                height = (int)IntY1 - (int)IntY0;
                width = (int)IntX1 - (int)IntX0;
            }
        }

        public bool FindPic(PicRetangle retangle, string picPath)
        {
            int result = DMObeject.FindPic(retangle.x0, retangle.y0, retangle.x1, retangle.y1,
                picPath, "000000", 0.9, 0, out object IntX, out object IntY);
            if (-1 != result)
            {
                FindX = (int)IntX;
                FindY = (int)IntY;
                return true;
            }
            else
                return false;
        }

        public bool SearchPic(string picPath)
        {
            //Retangle retangle = new Retangle(0, 0, width, height);
            PicRetangle retangle = new PicRetangle(380, 135, 425, 170);
            return FindPic(retangle, picPath);

        }
        public bool FindColor(PicRetangle retangle, string rgbColor)
        {

            int result = DMObeject.FindColor(retangle.x0, retangle.y0, retangle.x1, retangle.y1,
                rgbColor, 0.9, 0, out object IntX, out object IntY);
            if (-0 != result)
            {
                FindX = (int)IntX;
                FindY = (int)IntY;
                return true;
            }
            else
                return false;
        }

        public bool IFColor(PointInfo pointInfo)
        {
            pointInfo.color.ToLower();
            return pointInfo.color.Equals(DMObeject.GetColor(pointInfo.x, pointInfo.y).ToLower());
        }

        public void Click()
        {
            DMObeject.LeftDown();
            Thread.Sleep(10);
            DMObeject.LeftUp();
        }

        public void MoveTo(int x, int y)
        {
            ActiveWindows();
            Thread.Sleep(200);
            DMObeject.MoveTo(x, y);
        }
        public void MoveToFind()
        {
            ActiveWindows();
            Thread.Sleep(200);
            DMObeject.MoveTo(FindX, FindY);
        }
        //激活当前窗口
        public void ActiveWindows()
        {
            DMObeject.SetWindowState(WindowHandle, 1);
        }

        [System.Runtime.InteropServices.DllImport("user32")]
        private static extern int mouse_event(int dwFlags, int dx, int dy, int dwData, int dwExtraInfo);
        //移动鼠标 
        const int MOUSEEVENTF_MOVE = 0x0001;
        //模拟鼠标左键按下 
        const int MOUSEEVENTF_LEFTDOWN = 0x0002;
        //模拟鼠标左键抬起 
        const int MOUSEEVENTF_LEFTUP = 0x0004;
        //标示是否采用绝对坐标 
        const int MOUSEEVENTF_ABSOLUTE = 0x8000;

        //扫描二维码
        public void ScanQR(int ScannerHWD)
        {
            DMObeject.GetWindowRect(WindowHandle,
                out object IntX0, out object IntY0, out object IntX1, out object IntY1);

            int aimX = (int)IntX0 + 250;
            int aimY = (int)IntY0 + 250;
            ActiveWindows();
            DMObeject.MoveWindow(ScannerHWD, aimX, aimY);

            Thread.Sleep(800);
            DMObeject.SetWindowState(ScannerHWD, 1);
            DMObeject.MoveTo(aimX + 240, aimY + 15);
            //DMObeject.MoveTo(240, 15);

            scanning = true;

            Task.Run(() =>
            {
                int c = 1, max = 90;
                while (scanning)
                {
                    //DMObeject.SetWindowState(ScannerHWD, 1);
                    DMObeject.LeftDown();
                   // mouse_event(MOUSEEVENTF_LEFTDOWN, 0, 0, 0, 0);
                    for (int i = 0; (i <= max) && scanning; i++)
                    {

                        //DMObeject.MoveTo(aimX + 240 + c * i, aimY + 15);
                        Thread.Sleep(5);
                        //mouse_event(MOUSEEVENTF_MOVE, c, 0, 0, 0);
                        DMObeject.MoveR(c, 0);
                        //DMObeject.MoveWindow(WindowHandle, i, 0);
                    }
                    c = -c;
                    //mouse_event(MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);
                    DMObeject.LeftUp();
                }
                MoveWindowsLT();
            });
        }
        public void StopScann()
        {
            scanning = false;
        }

        public void MoveWindowsLT()
        {
            Dm.dmsoft dm = new Dm.dmsoft();
            dm.MoveWindow(WindowHandle, 0, 0);
        }

        public void MoveWindowsRT()
        {
            Dm.dmsoft dm = new Dm.dmsoft();
            dm.MoveWindow(WindowHandle, dm.GetScreenWidth() - width, 0);
        }

        public void CloseWindows()
        {
            //强制结束窗口所在进程
            DMObeject.SetWindowState(WindowHandle, 13);
            //窗口句柄重置
            WindowHandle = 0;
        }

        public void UnBind()
        {
            DMObeject.UnBindWindow();
        }
    }
}
