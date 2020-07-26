using System.Threading;

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
            int result = DMObeject.BindWindow(hwd, "normal", "normal", "normal", 0);
            if (0 == result) return false;
            WindowHandle = hwd;
            SetHW();
            return true;
        }
        public bool BindWindow(string className, string windowsName)
        {
            WindowHandle = DMObeject.FindWindow(className, windowsName);
            if (0 == WindowHandle) return false;
            int result = DMObeject.BindWindow(WindowHandle, "normal", "normal", "normal", 0);
            SetHW();
            return 1 == result;
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
        //扫描二维码
        public void ScanQR(DaMo dm)
        {
            DMObeject.GetWindowRect(WindowHandle,
                out object IntX0, out object IntY0, out object IntX1, out object IntY1);

            int aimX = (int)IntX0 + 340;
            int aimY = (int)IntY0 + 250;
            dm.ActiveWindows();
            DMObeject.MoveWindow(dm.WindowHandle, aimX, aimY);


            dm.ActiveWindows();
            dm.UnBind();
            Thread.Sleep(500);

            DMObeject.MoveTo(aimX + 240, aimY + 15);
            DMObeject.LeftDown();
            for (int i = 1; i < 30; i++)
            {
                Thread.Sleep(80);
                DMObeject.MoveR(-1, 0);
            }
            DMObeject.LeftUp();

        }

        public void MoveWindowsLT()
        {
            Dm.dmsoft dm = new Dm.dmsoft();
            dm.MoveWindow(WindowHandle, 0, 0);
        }

        public void MoveWindowsRT()
        {
            Dm.dmsoft dm = new Dm.dmsoft();
            dm.MoveWindow(WindowHandle, 1920 - width, 0);
        }

        public void CloseWindows()
        {
            //强制结束窗口所在进程
            DMObeject.SetWindowState(WindowHandle, 13);
            //窗口句柄重置
            WindowHandle = 0;
        }

        public void SetWindowsSize(int width, int heigth)
        {
            DMObeject.SetWindowSize(WindowHandle, width, heigth);
            SetHW();
        }

        public void UnBind()
        {
            DMObeject.UnBindWindow();
        }
    }
}
