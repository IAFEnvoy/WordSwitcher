using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace WordSwitcher
{
    public partial class Form1 : Form
    {
        string datapath = Application.StartupPath + @"\data.txt";
        CheckBox[,] data = new CheckBox[20, 20];
        public Form1()
        {
            InitializeComponent();
            for (int i = 1; i <= 5; i++)
            {
                for (int j = 0; j <= 10; j++)
                {
                    data[i, j] = new CheckBox()
                    {
                        Location = new Point((i - 1) * 50 + 10, j * 20 + 5),
                        Size = new Size(50, 20),
                        Text = j.ToString()
                    };
                    this.Controls.Add(data[i, j]);
                }
            }
            this.Width = 5 * 50 + 20;
            this.Height = 10 * 25 + 20;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if (File.Exists(datapath))
            {
                StreamReader sr = new StreamReader(datapath);
                string line;
                for (int i = 1; i <= 5; i++)
                {
                    line = sr.ReadLine();
                    while (line == null)
                        line = sr.ReadLine();
                    string[] time = line.Split(' ');
                    foreach (string s in time)
                        if (!string.IsNullOrEmpty(s))
                            data[i, int.Parse(s)].Checked = true;
                }
                sr.Close();
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            StreamWriter sw = new StreamWriter(datapath);
            for (int i = 1; i <= 5; i++)
            {
                string line = "";
                for (int j = 0; j <= 10; j++)
                    if (data[i, j].Checked)
                        line += j.ToString() + " ";
                sw.WriteLine(line);
            }
            sw.Close();
        }
    }
}
