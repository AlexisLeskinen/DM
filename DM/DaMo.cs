using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
            int result = DMObeject.BindWindow(processID, "normal", "normal", "normal", 0);
            WindowHandle = DMObeject.FindWindowByProcessId(processID, "", "");
            SetHW();
            return 1 == result;
        }
        public bool BindWindow(string className, string windowsName)
        {
            WindowHandle = DMObeject.FindWindow(className, windowsName);
            int result = DMObeject.BindWindow(WindowHandle, "normal", "normal", "normal", 0);
            SetHW();
            return 1 == result;
        }
        private void SetHW()
        {
            int result = DMObeject.GetWindowRect(WindowHandle, out object IntX0, out object IntY0, out object IntX1, out object IntY1);
            if (1 == result)
            {
                height = (int)IntY1 - (int)IntY0;
                width = (int)IntX1 - (int)IntX0;
            }
        }

        public bool FindPic(Retangle retangle, string picPath)
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
            Retangle retangle = new Retangle(380, 135, 425, 170);
            return FindPic(retangle, picPath);

        }
        public bool FindColor(Retangle retangle, string rgbColor)
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

        public bool IFColor(int x, int y, string rgbColor)
        {
            rgbColor.ToLower();
            return rgbColor.Equals(DMObeject.GetColor(x, y).ToLower());
        }

        public void Click()
        {
            DMObeject.LeftDown();
            Thread.Sleep(100);
            DMObeject.LeftUp();
        }

        public void MoveTo(int x, int y)
        {
            DMObeject.MoveTo(x, y);
        }
        public void MoveToFind()
        {
            DMObeject.MoveTo(FindX, FindY);
        }
    }
}
