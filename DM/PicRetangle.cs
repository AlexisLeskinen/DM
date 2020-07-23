namespace DM
{
    class PicRetangle
    {
        //x0 整形数:区域的左上X坐标
        //y0 整形数:区域的左上Y坐标
        //x1 整形数:区域的右下X坐标
        //y1 整形数:区域的右下Y坐标

        public int x0;
        public int y0;
        public int x1;
        public int y1;

        public PicRetangle(int x0, int y0, int x1, int y1)
        {
            this.x0 = x0;
            this.y0 = y0;
            this.x1 = x1;
            this.y1 = y1;
        }

        public PicRetangle()
        {
            x0 = 0;
            y0 = 0;
            x1 = 0;
            y1 = 0;
        }
    }

    class PointInfo
    {
        public int x;
        public int y;
        public string color;

        public PointInfo(int x, int y, string color)
        {
            this.x = x;
            this.y = y;
            this.color = color;
        }
    }
}
