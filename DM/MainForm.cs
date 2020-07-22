using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;

namespace DM
{
    public partial class MainForm : Form
    {
        ProcessStartInfo myProcessStartInfo;

        String thunderPath = @"D:\ChangZhi\dnplayer2\dnplayer.exe";
        Process thunderProcess = new Process();
        DaMo DMThunder;

        int X = 0, Y = 0;

        public MainForm()
        {
            InitializeComponent();
            myProcessStartInfo = new ProcessStartInfo();
            myProcessStartInfo.UseShellExecute = false;
            myProcessStartInfo.CreateNoWindow = true;
            myProcessStartInfo.RedirectStandardOutput = true;
            AutoRegCom("regsvr32 -s dm.dll");

            DMThunder = new DaMo();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }
        private void start_Click(object sender, EventArgs e)
        {
            RunLightening();
        }

        //注册DM.dll
        private string AutoRegCom(string strCmd)
        {
            string rInfo;


            try
            {
                Process cmdProcess = new Process();
                myProcessStartInfo.FileName = "cmd.exe";
                cmdProcess.StartInfo = myProcessStartInfo;
                myProcessStartInfo.Arguments = "/c " + strCmd;
                cmdProcess.Start();
                StreamReader myStreamReader = thunderProcess.StandardOutput;
                rInfo = myStreamReader.ReadToEnd();
                cmdProcess.Close();
                rInfo = strCmd + "\r\n" + rInfo;

                return rInfo;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        //启动雷电模拟器
        private void RunLightening()
        {
            myProcessStartInfo.FileName = thunderPath;
            thunderProcess.StartInfo = myProcessStartInfo;
            thunderProcess.Start();

            //睡几秒，等模拟器启动
            //然后绑定窗口     
            while (!DMThunder.BindWindow((int)thunderProcess.MainWindowHandle))
                Thread.Sleep(2000);
        }

        private void MainForm_Closed(object sender, FormClosedEventArgs e)
        {
            //关闭雷电模拟器
            //thunderProcess.CloseMainWindow();
            //thunderProcess.Close();
        }

        private void BindThunder()
        {
            if (DMThunder.IsBind()) return;

            while (!DMThunder.BindWindow("LDPlayerMainFrame", "雷电模拟器-1-"))
                Thread.Sleep(2000);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            BindThunder();
            runAppVar();
        }

        //打开应用变量
        private bool runAppVar()
        {
            bool success = false;
            Retangle retangle = new Retangle(380, 135, 425, 170);
            //DMThunder.SearchPic(@"D:\Project\DM\DM\应用变量.bmp")
            //DMThunder.FindColor(retangle, "1f61ca")
            if (DMThunder.FindColor(retangle,"1f61ca"))
            {
                DMThunder.MoveToFind();
                DMThunder.Click();
                Thread.Sleep(500);
                DMThunder.Click();

                if (DMThunder.IFColor(20, 70, "3f51b5"))
                    success = true;
            }

            return success;
        }
        //点击大话西游
        private void selectDHXY()
        {
            Retangle retangle = new Retangle(20, 130, 50, 155);
            if (DMThunder.FindColor(retangle,"fefdee")){
                DMThunder.MoveToFind();
                DMThunder.Click();
            }
        }
    }
}
