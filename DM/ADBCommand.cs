using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DM
{
    public static class ADBCommand
    {
        public static string GetFrontActivity = "shell dumpsys window | grep mCurrentFocus";
        public static string StopADB = "kill-server";
        public static string StartADB = "start-server";
        public static string BackHome = "shell input keyevent HOME";
        public static string Back = "shell input keyevent BACK";
    }
}
