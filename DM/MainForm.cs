using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DM
{
    public partial class MainForm : Form
    {
        Process ADB = new Process();
        private string configFile = "config.txt";
        private Dictionary<string, string> Accounts = new Dictionary<string, string>();

        string thunderPath = @"D:\ChangZhi\dnplayer2\dnplayer.exe";
        Process thunderProcess = new Process();
        DaMo DMThunder = new DaMo();
        private bool thunder = false;

        string DHXYPath = @"D:\Program Files (x86)\DHXY\XYPCLaunch.exe";
        Process DHXYProcess = new Process();
        DaMo DMDHXY = new DaMo();
        private bool dhxy = false;

        string APPath = @"D:\Project\DM\DM\360.txt";
        string RSPath = @".\RS\";
        private bool running = false;

        int SelectedIndex = -1;
        public MainForm()
        {
            InitializeComponent();
        }
        private void MainForm_Load(object sender, EventArgs e)
        {
            ReadConfig();
            //注册dll
            ExecuteCMD("regsvr32 -s dm.dll");
            //设置ADB启动参数
            ADB.StartInfo.UseShellExecute = false;
            ADB.StartInfo.CreateNoWindow = true;
            ADB.StartInfo.RedirectStandardInput = true;
            ADB.StartInfo.RedirectStandardOutput = true;


            CheckForIllegalCrossThreadCalls = false;
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
                APPath = sr.ReadLine();

                TextBox_Thunder_Path.Text = thunderPath;
                TextBox_DHXY_Path.Text = DHXYPath;

                ADB.StartInfo.FileName = Path.GetDirectoryName(thunderPath) + @"\adb.exe ";
                if (null != APPath && !APPath.Equals(""))
                    ReadAP();
            }
            sr.Close();
            fs.Close();
        }
        //启动cmd注册dll
        private void ExecuteCMD(string argumnet)
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
                Arguments = argumnet
            };

            Process CMD = new Process
            {
                StartInfo = PSI
            };
            CMD.Start();
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

        //启动脚本
        private void Button_Start_Click(object sender, EventArgs e)
        {
            if (!running)
            {
                Task.Run(() => LoopScript());
                running = true;
            }
        }
        //启动雷电模拟器
        private void RunLightening()
        {
            thunderProcess.StartInfo.FileName = thunderPath;
            thunderProcess.Start();
            thunder = true;

            BindThunder();

            DMThunder.MoveWindowsRT();
        }
        //启动大话西游
        private void RunDHXY()
        {
            DHXYProcess.StartInfo.FileName = DHXYPath;
            DHXYProcess.StartInfo.WorkingDirectory = Path.GetDirectoryName(DHXYPath);
            DHXYProcess.Start();
            dhxy = true;

            BindDHXY();
            DMDHXY.MoveWindowsLT();
        }

        private void MainForm_Closed(object sender, FormClosedEventArgs e)
        {
            //保持一下路径地址
            FileStream fs = new FileStream(configFile, FileMode.OpenOrCreate, FileAccess.ReadWrite);
            StreamWriter sw = new StreamWriter(fs);
            sw.WriteLine(thunderPath);
            sw.WriteLine(DHXYPath);
            sw.WriteLine(APPath);
            sw.Close();
            fs.Close();

            //关闭雷电模拟器与大话西游
            if (thunder)
                CloseThunder();

            if (dhxy)
                CloseDHXY();

            ADB.Close();
        }
        //绑定雷电模拟器窗口
        private void BindThunder()
        {
            if (DMThunder.IsBind()) return;

            while (!DMThunder.BindWindow("LDPlayerMainFrame", "雷电模拟器-1-"))
                Thread.Sleep(2000);
        }
        //绑定大话西游窗口
        private void BindDHXY()
        {
            if (DMDHXY.IsBind()) return;

            while (!DMDHXY.BindWindow("大话西游手游", "大话西游手游", "normal"))
                Thread.Sleep(2000);
        }
        private bool FrontActivityIs(string activity)
        {
            string reuslt = ExecuteADB(ADBCommand.GetFrontActivity);
            return null != reuslt
                && reuslt.Contains(activity);
        }
        //运行应用变量并设置
        private void RunAppVar()
        {
            StartAppVar();

            //等待进入应用变量主页面
            Thread.Sleep(1000);
            while (!FrontActivityIs("xposed.hook.model"))
                Thread.Sleep(1000);

            //获取当前运行界面
            if (!FrontActivityIs("DetailActivity"))
                ClickDHXY();
            Thread.Sleep(2000);
            ClickAdd();
            Thread.Sleep(800);
            ClickRandom();
            Thread.Sleep(800);
            ClickSave();
            Thread.Sleep(800);
            ClickKillAll();
            Thread.Sleep(800);
            ClickAdd();

            Home();
        }
        //运行大话西游APP
        private void RunDHXYApp(string account, string password)
        {
            StartDHXYApp();
            SkipOP();
            ClickAccountLogin();
            ClickAgree();
            ClickEnter(account, password);
            ClickQR();
        }

        private void Button_Test_Click(object sender, EventArgs e)
        {
            BindThunder();
            BindDHXY();

            Task.Run(() =>
            {
                ClickQR();
                Thread.Sleep(8000);
                //StartGame();

                //Thread.Sleep(12000);
                //PicRetangle retangle = new PicRetangle(428, 594, 576, 628);
                //if (!DMDHXY.FindPic(retangle, RSPath + "确定选择.bmp"))
                //{
                //    Thread.Sleep(1000);
                //    CreateRole();
                //}
            });
        }

        private void LoopScript()
        {
            for (int i = 0; i < Accounts.Count; i++)
            {
                if (!running) break;
                SingleProcess(i);
                Thread.Sleep(4000);
            }
        }

        private void SingleProcess(int Index)
        {
            Task.Run(() => RunLightening());
            Task.Run(() => RunDHXY());

            string account = ListView_Account.Items[Index].SubItems[0].Text;
            string password = ListView_Account.Items[Index].SubItems[1].Text;
            //等待模拟器进入桌面
            while (!FrontActivityIs("launcher3"))
            {
                if (FrontActivityIs("error"))
                    ExecuteADB(ADBCommand.StopADB);
                Thread.Sleep(2000);
                ExecuteADB(ADBCommand.StartADB);
            }

            RunAppVar();
            RunDHXYApp(account, password);

            CloseThunder();
            DMThunder.UnBind();

            Thread.Sleep(8000);
            StartGame();

            Thread.Sleep(12000);
            PicRetangle retangle = new PicRetangle(428, 594, 576, 628);
            if (!DMDHXY.FindPic(retangle, RSPath + "确定选择.bmp"))
            {
                Thread.Sleep(1000);
                CreateRole();
            }

            CloseDHXY();
            DMDHXY.UnBind();

            ListView_Account.Items[Index].SubItems[2].Text = "已完成";
        }

        //调用adb点击，并判断点击后要检查的像素点
        private void Tap(int x, int y, PointInfo checkPoint = null)
        {
            string str = string.Format("shell input tap {0} {1}", x, y);
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
            thunderPath = TextBox_Thunder_Path.Text;
            ADB.StartInfo.FileName = Path.GetDirectoryName(thunderPath) + @"\adb.exe ";
        }
        //设置电脑大话西游路径
        private void Button_Set_DHXY_Click(object sender, EventArgs e)
        {
            DHXYPath = TextBox_DHXY_Path.Text;
        }

        //点击启动
        private void StartDHXYApp()
        {
            ExecuteADB("shell am start -n com.netease.dhxy.qihoo/org.cocos2dx.lua.AppActivity");
        }
        //返回
        private void Home()
        {
            ExecuteADB(ADBCommand.BackHome);
        }
        //跳过动画
        private void SkipOP()
        {
            PicRetangle retangle = new PicRetangle(466, 368, 524, 391);
            while (!DMThunder.FindPic(retangle, RSPath + "快速注册.bmp"))
            {
                Thread.Sleep(1000);
                Tap(45, 50);
            }
        }

        //点击用账号密码登录
        private void ClickAccountLogin()
        {
            PicRetangle retangle = new PicRetangle(280, 377, 367, 399);
            if (DMThunder.FindPic(retangle, RSPath + "用账号登录.bmp"))
            {
                Tap(315, 350);
            }
        }

        private void ClickAgree()
        {
            PicRetangle retangle = new PicRetangle(287, 338, 307, 354);
            if (!DMThunder.FindPic(retangle, RSPath + "同意协议.bmp"))
            {
                Tap(295, 308);
            }
        }

        private void ClickEnter(string account, string password)
        {
            //清空账号密码
            Tap(315, 185);
            Thread.Sleep(500);
            Tap(460, 175);

            Tap(315, 185);
            ExecuteADB("shell input text " + account);

            Tap(315, 215);
            ExecuteADB("shell input text " + password);

            //点击登录
            Tap(400, 270);
        }
        DaMo damo;
        int ScannerHWD = 0;
        private void ClickQR()
        {
            PicRetangle retangle = new PicRetangle(351, 234, 558, 346);
            if (DMThunder.FindPic(retangle, RSPath + "确定扫描登录.bmp"))
                Tap(480, 290);

            Thread.Sleep(1000);

            damo = new DaMo();
            while (!damo.BindWindow("TrayNoticeWindow", ""))
            {
                Thread.Sleep(1000);
                Tap(45, 225);
            }
            damo.MoveTo(100, 110);
            Thread.Sleep(800);
            damo.Click();
            damo.UnBind();
            Thread.Sleep(500);

            damo = new DaMo();
            if (!damo.BindWindow("ldScreenshotWindow", ""))
            {
                ExecuteADB(ADBCommand.Back);
                Thread.Sleep(3000);
                ClickQR();
            }
            else
            {
                ScannerHWD = damo.GetWhd();
                damo.UnBind();
                ScanQR();
            }
        }

        private void ScanQR()
        {
            PicRetangle retangle = new PicRetangle(373, 321, 460, 409);
            if (DMDHXY.FindPic(retangle, RSPath + "二维码.bmp"))
            {
                DMDHXY.ActiveWindows();
                DMDHXY.ScanQR(ScannerHWD);

                retangle = new PicRetangle(351, 234, 558, 346);

                int reset = 10, count = 0;
                while (!DMThunder.FindPic(retangle, RSPath + "确定扫描登录.bmp"))
                {
                    Thread.Sleep(2000);
                    count++;
                    if (count == reset) break;
                }
                DMDHXY.StopScann();

                //等待超时
                if (reset == count)
                {
                    ExecuteADB(ADBCommand.Back);
                    Thread.Sleep(3000);
                    ClickQR();
                }
                else
                    Tap(480, 290);
            }
        }

        private void StartGame()
        {
            PicRetangle retangle = new PicRetangle(402, 612, 670, 680);
            while (!DMDHXY.FindPic(retangle, RSPath + "开始游戏.bmp"))
            {
                Thread.Sleep(1000);
            }
            DMDHXY.MoveToFind();
            DMDHXY.Click();
            Thread.Sleep(200);
            DMDHXY.Click();
        }

        private void CreateRole()
        {

            PicRetangle retangle = new PicRetangle(803, 740, 1009, 786);
            if (DMDHXY.FindPic(retangle, RSPath + "创建角色.bmp"))
            {
                DMDHXY.MoveToFind();
                Thread.Sleep(500);
                DMDHXY.Click();

                retangle = new PicRetangle(623, 291, 801, 473);
                while (!DMDHXY.FindPic(retangle, RSPath + "老玩家.bmp"))
                    Thread.Sleep(1000);
                DMDHXY.MoveToFind();
                Thread.Sleep(500);
                DMDHXY.Click();
            }
        }
        private void CloseThunder()
        {
            thunderProcess.Kill();
            thunderProcess.Close();
            thunder = false;
        }

        private void CloseDHXY()
        {
            DMDHXY.CloseWindows();
            DHXYProcess.Close();
            dhxy = false;
        }

        private void Button_Read_Account_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Multiselect = true;//该值确定是否可以选择多个文件
            dialog.Title = "请选择文件夹";
            dialog.Filter = "所有文件(*.*)|*.*";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                string file = dialog.FileName;
                if (null != file)
                {
                    APPath = file;
                    ReadAP();
                }
            }
        }

        private void ReadAP()
        {
            FileStream fs = new FileStream(APPath, FileMode.Open, FileAccess.Read);
            StreamReader sr = new StreamReader(fs);
            string str = sr.ReadLine();
            while (null != str && !str.Equals(""))
            {
                //分离账号密码
                str = str.Replace("----", " ");
                string[] array = str.Split(' ');
                if (!Accounts.ContainsKey(array[0]))
                    Accounts.Add(array[0], array[1]);
                str = sr.ReadLine();
            }

            sr.Close();
            fs.Close();

            //更新账号列表
            foreach (KeyValuePair<string, string> item in Accounts)
            {
                ListView_Account.Items.Add(new ListViewItem(new string[] { item.Key, item.Value, "" }));
            }
        }

        private void Button_Stop_Click(object sender, EventArgs e)
        {
            running = false;
        }

        private void ListView_Account_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (ListView_Account.SelectedIndices.Count > 0)
                {
                    SelectedIndex = ListView_Account.SelectedIndices[0];
                    contextMenuStrip.Show(MousePosition.X, MousePosition.Y);
                }
            }
        }

        private void TSM_Execute_Click(object sender, EventArgs e)
        {
            if (!running && -1 != SelectedIndex)
            {
                Task.Run(() =>
                {
                    SingleProcess(SelectedIndex);
                    SelectedIndex = -1;
                });
                running = true;
            }
        }
    }
}
