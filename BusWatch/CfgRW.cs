using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System.IO;

namespace BusWatch
{
    class CfgRW
    {
        private static int Grp = 0;
        private static int PhoneNum = 1;
        private static int Addr = 2;
        private static int BusNum = 3;
        private static int Stopid = 4;
        private static int StopName = 5;
        public static string[,] busstopinfo = new string[10,4];

        public static void Readcfg()
        {
            string path = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            string filename = Path.Combine(path, "cfg.txt");

            string[] filelines = File.ReadAllLines(filename);
            string[] splitline;
            int returnbuscnt = 0;
            int leavebuscnt = 5;
            for(int cnt=0;cnt<filelines.Length;cnt++)
            {
                splitline = filelines[cnt].Split(',');

                if (splitline[Grp] == "Return")
                {
                    //BusWatch.Activity1.lay[returnbuscnt].busnum.Text = splitline[BusNum];
                    //BusWatch.Activity1.lay[returnbuscnt].busstop.Text = splitline[StopName] + ",stop id: " + splitline[Stopid] + "\r\n" + "Next bus: ";
                    busstopinfo[returnbuscnt, 0] = splitline[BusNum];
                    busstopinfo[returnbuscnt, 1] = splitline[Stopid]; 
                    busstopinfo[returnbuscnt, 2] = splitline[StopName];
                    busstopinfo[returnbuscnt, 3] = splitline[PhoneNum];
                    returnbuscnt++;
                }
                else if(splitline[Grp] == "Leave")
                {
                    //BusWatch.Activity1.lay[leavebuscnt].busnum.Text = splitline[BusNum];
                    //BusWatch.Activity1.lay[leavebuscnt].busstop.Text = splitline[StopName] + ",stop id: " + splitline[Stopid] + "\r\n" + "Next bus: ";
                    busstopinfo[leavebuscnt, 0] = splitline[BusNum];
                    busstopinfo[leavebuscnt, 1] = splitline[Stopid];
                    busstopinfo[leavebuscnt, 2] = splitline[StopName];
                    busstopinfo[leavebuscnt, 3] = splitline[PhoneNum];
                    leavebuscnt++;
                }
            }            
        }

        public static void Writecfg()
        {
            string path = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            string filename = Path.Combine(path, "cfg.txt");
            string[] businfo = {    "Return,74000,,15,9189,Fishcreek LRT",
                                    "Return,74000,,15,4795,Shawnessy LRT",
                                    "Return,74000,,15,8232,7-11",
                                    "Leave,74000,,15,8224,To Shawnessy LRT",
                                    "Leave,74000,,12,8228,To Somerset LRT"
                                };

            File.WriteAllLines(filename, businfo);

        }
    }
}