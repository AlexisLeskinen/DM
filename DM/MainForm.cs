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
        Process ADB = new Process();
        private string configFile = "config.txt";
        private Dictionary<string, string> accounts = new Dictionary<string, string>();

        string thunderPath = @"D:\ChangZhi\dnplayer2\dnplayer.exe";
        Process thunderProcess = new Process();
        DaMo DMThunder = new DaMo();

        string DHXYPath = @"D:\Program Files (x86)\DHXY\XYPCLaunch.exe";

        public MainForm()
        {
            InitializeComponent();
        }
        private void MainForm_Load(object sender, EventArgs e)
        {
            ReadConfig();
            //注册dll
            RegisterDLL();
            //设置ADB启动参数
            ADB.StartInfo.UseShellExecute = false;
            ADB.StartInfo.CreateNoWindow = true;
            ADB.StartInfo.RedirectStandardInput = true;
            ADB.StartInfo.RedirectStandardOutput = true;
        }
        //读取配置文件
        private void ReadConfig()
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

                ADB.StartInfo.FileName = Path.GetDirectoryName(thunderPath) + @"\adb.exe ";
            }
            sr.Close();
            fs.Close();
        }
        //启动cmd注册dll
        private void RegisterDLL()
        {
            //设置一cmd下启动参数
            ProcessStartInfo PSI = new ProcessStartInfo
            {
                UseShellExecute = false,
                CreateNoWindow = true,
                RedirectStandardInput = true,
                RedirectStandardOutput = true,
                FileName = "cmd.exe",
                //注册dm.dll
                Arguments = "regsvr32 -s dm.dll"
            };

            Process CMD = new Process
            {
                StartInfo = PSI
            };
            CMD.Start();
            //CMD.WaitForExit();
            CMD.Close();
        }
        //执行单条ADB语句
        private string ExecuteADB(string commad)
        {
            ADB.StartInfo.Arguments = commad;
            ADB.Start();
            string result = ADB.StandardOutput.ReadLine();
            ADB.Close();
            return result;
        }


        private void start_Click(object sender, EventArgs e)
        {
            RunLightening();
        }


        //启动雷电模拟器
        private void RunLightening()
        {
            thunderProcess.StartInfo.FileName = thunderPath;
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
            ADB.Close();
        }

        private void BindThunder()
        {
            if (DMThunder.IsBind()) return;

            while (!DMThunder.BindWindow("LDPlayerMainFrame", "雷电模拟器-1-"))
                Thread.Sleep(2000);

            //ExecuteADB("kill-server");
            //ExecuteADB("start-server");
        }

        private void RunAppVar()
        {
            StartAppVar();

            //等待进入应用变量主页面
            Thread.Sleep(1000);
            string frontActivity = ExecuteADB(ADBCommand.GetFrontActivity);
            while (!frontActivity.Contains("xposed.hook.model"))
            {
                Thread.Sleep(1000);
                frontActivity = ExecuteADB(ADBCommand.GetFrontActivity);
            }

            //获取当前运行界面
            frontActivity = ExecuteADB(ADBCommand.GetFrontActivity);
            if (!frontActivity.Contains("DetailActivity"))
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
            ClickAdd();

            Home();
        }

        private void WaitForForm()
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            BindThunder();

            //RunAppVar();
            StartDHXY();
            ClickAccountLogin();
            InputAccPw("bgippg4112", "ypbx0610");
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
        //调用adb点击，并判断点击后要检查的像素点
        private void Tap(int x, int y, PointInfo checkPoint = null)
        {
            string str = "shell input tap " + x.ToString() + " " + y.ToString();
            ExecuteADB(str);

            Thread.Sleep(500);
            if (null != checkPoint)
                while (!DMThunder.IFColor(checkPoint))
                    Thread.Sleep(1000);
        }

        //打开应用变量
        private void StartAppVar()
        {
            //通过adb命令启动app
            //需要花费几秒
            ExecuteADB("shell am start -n com.sollyu.xposed.hook.model/com.sollyu.xposed.hook.model.MainActivity");
        }
        //点击大话西游
        private void ClickDHXY()
        {
            //PicRetangle retangle = new PicRetangle(20, 130, 50, 155);
            PointInfo pointInfo = new PointInfo(741, 453, "ffffff");
            Tap(33, 100, pointInfo);
        }
        //点击加号
        private void ClickAdd()
        {
            Tap(740, 420);
        }
        //点击全部随机
        private void ClickRandom()
        {
            Tap(740, 120);
        }
        //点击保存设置
        private void ClickSave()
        {
            Tap(740, 360);
        }
        //点击杀死进程
        private void ClickKillAll()
        {
            Tap(740, 300);
        }
        //设置雷电模拟器路径
        private void path_button_Click(object sender, EventArgs e)
        {
            thunderPath = path_text_box.Text;
            ADB.StartInfo.FileName = Path.GetDirectoryName(thunderPath) + @"\adb.exe ";
        }
        //设置电脑大话西游路径
        private void DHXY_button_Click(object sender, EventArgs e)
        {
            DHXYPath = DHXY_text_box.Text;
        }

        //点击启动
        private void StartDHXY()
        {
            ExecuteADB("shell am start -n com.netease.dhxy.qihoo/org.cocos2dx.lua.AppActivity");
        }
        //返回
        private void Home()
        {
            ExecuteADB("shell input keyevent HOME");
        }        

        //跳过动画直到登录界面,点击用账号密码登录
        private void ClickAccountLogin()
        {
            PicRetangle retangle = new PicRetangle(279, 377, 369, 400);
            if (DMThunder.FindPic(retangle, @"D:\Project\DM\DM\用账号登录.bmp"))
            {
                Tap(315, 350);
            }
            else
            {
                Thread.Sleep(1000);
                Tap(315, 185);
                ClickAccountLogin();
            }
        }

        private void InputAccPw(string account,string password)
        {
            Tap(315, 185);
            ExecuteADB("shell input text "+account);

            Tap(315, 215);
            ExecuteADB("shell input text "+password);

            //点击登录
            Tap(400, 270);
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
