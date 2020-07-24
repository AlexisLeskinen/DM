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
    }
}
