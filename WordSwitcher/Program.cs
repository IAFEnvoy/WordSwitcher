using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace WordSwitcher
{
    internal static class Program
    {
        static DateTime[] start = new DateTime[20];
        static List<string> list = new List<string>();
        static string word = @"C:\Program Files\Microsoft Office\Office15\WINWORD.EXE";
        static string wps = @"C:\Users\Administrator\AppData\Local\Kingsoft\WPS Office\ksolaunch.exe";
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new Form1());
            }
            else
            {
                try
                {
                    start[0] = new DateTime(2000, 1, 1, 7, 15, 0);
                    start[1] = new DateTime(2000, 1, 1, 8, 0, 0);
                    start[2] = new DateTime(2000, 1, 1, 9, 10, 0);
                    start[3] = new DateTime(2000, 1, 1, 10, 0, 0);
                    start[4] = new DateTime(2000, 1, 1, 10, 50, 0);
                    start[5] = new DateTime(2000, 1, 1, 11, 30, 0);
                    start[6] = new DateTime(2000, 1, 1, 13, 15, 0);
                    start[7] = new DateTime(2000, 1, 1, 14, 5, 0);
                    start[8] = new DateTime(2000, 1, 1, 15, 0, 0);
                    start[9] = new DateTime(2000, 1, 1, 15, 50, 0);
                    start[10] = new DateTime(2000, 1, 1, 16, 40, 0);

                    StreamReader sr = new StreamReader(Application.StartupPath + @"\data.txt");
                    string line;
                    for (int i = 1; i <= 5; i++)
                    {
                        line = sr.ReadLine();
                        while (line == null)
                            line = sr.ReadLine();
                        string[] time = line.Split(' ');
                        foreach (string s in time)
                            if (!string.IsNullOrEmpty(s))
                                list.Add(i.ToString() + "-" + s);
                    }

                    DateTime now = DateTime.Now;
                    DateTime today = new DateTime(2000, 1, 1, now.Hour, now.Minute, now.Second);
                    for (int i = 0; i <= 10; i++)
                        if (today >= start[i].AddMinutes(-10) && today <= start[i].AddMinutes(40))//提前10分钟
                        {
                            int weekday;
                            switch (now.DayOfWeek)
                            {
                                case DayOfWeek.Sunday: { return; }
                                case DayOfWeek.Monday: { weekday = 1; break; }
                                case DayOfWeek.Tuesday: { weekday = 2; break; }
                                case DayOfWeek.Wednesday: { weekday = 3; break; }
                                case DayOfWeek.Thursday: { weekday = 4; break; }
                                case DayOfWeek.Friday: { weekday = 5; break; }
                                case DayOfWeek.Saturday: { return; }
                                default: { return; }
                            }
                            string key = weekday.ToString() + "-" + i.ToString();
                            if (list.Contains(key))
                            {
                                Process process1 = new Process();
                                process1.StartInfo = new ProcessStartInfo(word, "\"" + args[0] + "\"");
                                process1.Start();
                                return;
                            }
                        }
                    Process process = new Process();
                    process.StartInfo = new ProcessStartInfo(wps, "\"" + args[0] + "\"");
                    process.Start();
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.StackTrace);
                }
            }
        }
    }
}
