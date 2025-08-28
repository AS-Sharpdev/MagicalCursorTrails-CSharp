using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Magical_Cursor_Trails
{
    public partial class Form1 : Form
    {
        private List<Trail> trails = new List<Trail>();
        private Timer timer = new Timer();
        private static readonly Random rnd = new Random();
        public Form1()
        {
            InitializeComponent();
            this.MouseMove += Form1_MouseMove;
            timer.Interval = 16;
            timer.Tick += Timer_Tick;
            timer.Start();

            DoubleBuffered = true;

        }
        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            trails.Add(new Trail
            {
                Position = e.Location,
                Color = Color.FromArgb(rnd.Next(256), rnd.Next(256), rnd.Next(256))
            });
        }
        private void Timer_Tick(object sender, EventArgs e)
        {
            for (int i = trails.Count - 1; i >= 0; i--)
            {
                trails[i].Life--;
                if (trails[i].Life <= 0) trails.RemoveAt(i);
            }
            this.Invalidate();
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            foreach (var t in trails)
            {
                using (Brush b = new SolidBrush(Color.FromArgb(t.Life * 8, t.Color)))
                    e.Graphics.FillEllipse(b, t.Position.X, t.Position.Y, 10, 10);
            }
        }
    }
   
}
