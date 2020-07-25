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
            DMObeject = new Dm.dmsoft(); ;
            WindowHandle = 0;
        }
        //判断该对象是否已经绑定窗口
        public bool IsBind()
        {
            return WindowHandle != 0;
        }
        //根据进程ID绑定窗口
        public bool BindWindow(int processID)
        {
            if (0 == processID) return false;

            int result = DMObeject.BindWindow(processID, "normal", "normal", "normal", 0);
            if (0 == result) return false;
            WindowHandle = DMObeject.FindWindowByProcessId(processID, "", "");
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
            Thread.Sleep(100);
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

       public void ActiveWindows()
        {
            DMObeject.SetWindowState(WindowHandle, 1);
        }

        public void ScanQR(DaMo dm)
        {
            DMObeject.GetWindowRect(WindowHandle,
                out object IntX0, out object IntY0, out object IntX1, out object IntY1);

            int aimX = (int)IntX0+340;
            int aimY = (int)IntY0+250;
            DMObeject.MoveWindow(dm.WindowHandle, aimX, aimY);

            Thread.Sleep(500);
            dm.MoveTo(10, 10);
            dm.DMObeject.RightDown();
            dm.MoveTo(10, 9);
            dm.DMObeject.RightUp();

            //dm.DMObeject.SetWindowSize(dm.WindowHandle, 400, 400);
        }
       
    }
}
