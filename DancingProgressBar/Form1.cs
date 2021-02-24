using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace DancingProgressBar
{
    public partial class Form1 : Form
    {
        delegate void Progress(int value);
        public int countBar = 0;
        public Form1()
        {
            InitializeComponent();
            for (int i = 1; i < 13; i++)
            {
                comboBoxProgressBar.Items.Add(i);
            }
        }

        public Color RandColor()
        {
            Random randGen = new Random();
            KnownColor[] names = (KnownColor[])Enum.GetValues(typeof(KnownColor));
            KnownColor randColorName = names[randGen.Next(names.Length)];
            Color randomColor = Color.FromKnownColor(randColorName);
            Thread.Sleep(20);
            return randomColor;
        }

        public void DeleteBar()
        {
            List<Control> Con = new List<Control>();
            foreach (Control item in this.Controls)
            {
                if ((item as ProgressBar) != null)
                    if ((item as ProgressBar) == item)
                        Con.Add(item);
            }
            foreach (Control item in Con)
            {
                this.Controls.Remove(item);
            }
        }

        public void StartBar(object obj)
        {

            ProgressBar bar = (ProgressBar)obj;

            if (bar.InvokeRequired)
                bar.Invoke(new Action(() => bar.PerformStep()));
        }


        private void buttonStart_Click(object sender, EventArgs e)
        {
            DeleteBar();
            Random rand = new Random();
            int x = 20; int y = 75;
            countBar = Convert.ToInt32(comboBoxProgressBar.Text);
            for (int i = 0; i < countBar; i++)
            {
                ProgressBar bar = new ProgressBar()
                {
                    Location = new Point(x, y),
                    Minimum = 0,
                    Maximum = 100,
                    Value = rand.Next(100),
                    ForeColor = RandColor(),
                    Step = 5,
                    Size = new Size(200, 30),
                    Style = ProgressBarStyle.Continuous
                };
                y += 35;
                this.Controls.Add(bar);
            }
            foreach (Control item in this.Controls)
            {
                if ((item as ProgressBar) != null)
                {
                    Thread thread = new Thread(StartBar);
                    thread.IsBackground = true;
                    thread.Start(item as ProgressBar);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}

