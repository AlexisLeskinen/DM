using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace DM
{
    public partial class MainForm : Form
    {
        Process cmdProcess;
        ProcessStartInfo myProcessStartInfo;
        private string configFile = "config.txt";
        private Dictionary<string, string> accounts = new Dictionary<string, string>();
        private string adb;

        string thunderPath = @"D:\ChangZhi\dnplayer2\dnplayer.exe";
        Process thunderProcess = new Process();
        DaMo DMThunder;

        string DHXYPath = @"D:\Program Files (x86)\DHXY\XYPCLaunch.exe";


        public MainForm()
        {
            InitializeComponent();
            //设置一下启动参数
            myProcessStartInfo = new ProcessStartInfo();
            myProcessStartInfo.UseShellExecute = false;
            myProcessStartInfo.CreateNoWindow = true;
            myProcessStartInfo.RedirectStandardInput = true;
            myProcessStartInfo.RedirectStandardOutput = true;

            cmdProcess = new Process();
            AutoRegCom("regsvr32 -s dm.dll");

            DMThunder = new DaMo();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            FileStream fs = new FileStream(configFile, FileMode.OpenOrCreate, FileAccess.ReadWrite);
            StreamReader sr = new StreamReader(fs);
            string temp = sr.ReadLine();
            if (null != temp)
            {
                thunderPath = temp;
                DHXYPath = sr.ReadLine();

                path_text_box.Text = thunderPath;
                DHXY_text_box.Text = DHXYPath;

                adb = Path.GetDirectoryName(thunderPath) + @"\adb.exe ";
            }
            sr.Close();
            fs.Close();

        }
        private void start_Click(object sender, EventArgs e)
        {
            RunLightening();
        }

        //注册DM.dll
        private void AutoRegCom(string strCmd)
        {
            myProcessStartInfo.FileName = "cmd.exe";
            cmdProcess.StartInfo = myProcessStartInfo;
            myProcessStartInfo.Arguments = strCmd;
            cmdProcess.Start();
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
            //保持一下路径地址
            FileStream fs = new FileStream(configFile, FileMode.OpenOrCreate, FileAccess.ReadWrite);
            StreamWriter sw = new StreamWriter(fs);
            sw.WriteLine(thunderPath);
            sw.WriteLine(DHXYPath);
            sw.Close();
            fs.Close();
            //关闭雷电模拟器
            //thunderProcess.CloseMainWindow();
            //thunderProcess.Close();
            cmdProcess.Close();
        }

        private void BindThunder()
        {
            if (DMThunder.IsBind()) return;

            while (!DMThunder.BindWindow("LDPlayerMainFrame", "雷电模拟器-1-"))
                Thread.Sleep(2000);

            cmdProcess.StandardInput.WriteLine(adb + "kill-server");
        }

        private bool RunAppVar()
        {
            ClickAppVar();

            PointInfo pointInfo = new PointInfo(20, 70, "3f51b5");
            while (!DMThunder.IFColor(pointInfo))
                Thread.Sleep(1000);                        

            Thread.Sleep(1000);
            ClickDHXY();
            Thread.Sleep(1000);
            ClickAdd();
            Thread.Sleep(500);
            ClickRandom();
            Thread.Sleep(500);
            ClickSave();
            Thread.Sleep(500);
            ClickKillAll();
            Thread.Sleep(500);
            ClickRun();

            return true;
        }

        private void WaitForForm()
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            BindThunder();

            RunAppVar();
        }

        //判断某一区域的特征像素点
        //进行点击并检查
        private bool TurnTo(PicRetangle retangle, string aimColor, PointInfo pointInfo)
        {
            bool success = false;
            if (DMThunder.FindColor(retangle, aimColor))
            {
                DMThunder.MoveToFind();
                DMThunder.Click();

                Thread.Sleep(500);
                if (null == pointInfo || DMThunder.IFColor(pointInfo))
                    success = true;
            }
            return success;
        }
        //调用adb点击，并判断
        private

        //打开应用变量
        private void ClickAppVar()
        {
            //PicRetangle retangle = new PicRetangle(380, 135, 425, 170);
            //PointInfo pointInfo = new PointInfo(20, 70, "3f51b5");
            //return TurnTo(retangle, "1f61ca", pointInfo);
            cmdProcess.StandardInput.WriteLine(adb + 
                "shell am start -n com.sollyu.xposed.hook.model/com.sollyu.xposed.hook.model.MainActivity");
        }
        //点击大话西游
        private bool ClickDHXY()
        {
            //PicRetangle retangle = new PicRetangle(20, 130, 50, 155);
            cmdProcess.StandardInput.WriteLine(adb +
                "shell input tap 33 110");
            PointInfo pointInfo = new PointInfo(741, 453, "ffffff");

            while (!DMThunder.IFColor(pointInfo))
                Thread.Sleep(1000);

            return true;
        }
        //点击加号
        private bool ClickAdd()
        {
            PicRetangle retangle = new PicRetangle(738, 450, 747, 460);
            PointInfo pointInfo = new PointInfo(741, 453, "ffffff");
            return TurnTo(retangle, "ffffff", pointInfo);
        }
        //点击全部随机
        private bool ClickRandom()
        {
            PicRetangle retangle = new PicRetangle(733, 164, 752, 189);
            return TurnTo(retangle, "ffffff", null);
        }
        //点击保存设置
        private bool ClickSave()
        {
            PicRetangle retangle = new PicRetangle(733, 383, 751, 404);
            return TurnTo(retangle, "ffb805", null);
        }
        //点击杀死进程
        private bool ClickKillAll()
        {
            PicRetangle retangle = new PicRetangle(738, 333, 746, 344);
            return TurnTo(retangle, "da4338", null);
        }

        private void path_button_Click(object sender, EventArgs e)
        {
            thunderPath = path_text_box.Text;
            adb = Path.GetDirectoryName(thunderPath) + @"\adb.exe ";
        }

        private void DHXY_button_Click(object sender, EventArgs e)
        {
            DHXYPath = DHXY_text_box.Text;
        }

        //点击启动
        private bool ClickRun()
        {
            PicRetangle retangle = new PicRetangle(736, 224, 746, 237);
            return TurnTo(retangle, "1565c0", null);
        }
        //返回
        private void Back()
        {
            DMThunder.Back();
        }
        //点击用账号密码登录
        private void ClickAccountLogin()
        {
            PicRetangle retangle = new PicRetangle(279, 377, 369, 400);
            if (DMThunder.FindPic(retangle, @"D:\Project\DM\DM\用账号登录.bmp"))
            {
                DMThunder.MoveToFind();
                DMThunder.Click();
            }
            else
            {
                DMThunder.MoveTo(100, 100);
                DMThunder.Click();
                Thread.Sleep(1000);
                ClickAccountLogin();

            }
        }

        private void read_account_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Multiselect = true;//该值确定是否可以选择多个文件
            dialog.Title = "请选择文件夹";
            dialog.Filter = "所有文件(*.*)|*.*";
            string file = null;
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                file = dialog.FileName;
                if (null != file)
                {
                    FileStream fs = new FileStream(file, FileMode.Open, FileAccess.Read);
                    StreamReader sr = new StreamReader(fs);
                    string str = sr.ReadLine();
                    while (null != str && !str.Equals(""))
                    {
                        str = str.Replace("----", " ");
                        string[] array = str.Split(' ');
                        if (!accounts.ContainsKey(array[0]))
                            accounts.Add(array[0], array[1]);
                        str = sr.ReadLine();
                    }

                    sr.Close();
                    fs.Close();

                    //更新账号列表
                    foreach (KeyValuePair<string, string> item in accounts)
                    {
                        account_list.Items.Add(new ListViewItem(new string[] { item.Key, item.Value }));
                    }
                }
            }
        }
    }
}
