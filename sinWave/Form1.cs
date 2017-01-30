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

namespace sinWave
{
    public partial class Form1 : Form
    {
        float[] buffer = new float[8000];
        float halfY;
        float amplitude = 100;
        float frequency = 50;
        float x1 = 0;
        float y1 = 0;
        float t;
        public Form1()
        {
            InitializeComponent();
        }

        private void btnPlay_Click(object sender, EventArgs e)
        {
            Thread t = new Thread(ExecutePlay);
            t.Start();
        }
        public void ExecutePlay()
        {
            Graphics DrawWave = picBox.CreateGraphics();
            Pen pen = new Pen(Brushes.Red, 2);
            t = Convert.ToInt32(txtTime.Text);// time or length 
            halfY = picBox.Height / 2;
            float nextDraw = 0;
            x1 = 0;
            y1 = 0;
            
        Drowagain:
            
            for (int x=0; x < t; x ++)
                {
                if(x > 900)
                {
                    break;
                }
                buffer[x] = (float)(amplitude * Math.Sin(2 * Math.PI * frequency * ((x + nextDraw) / 11025)));
                DrawWave.DrawLine(pen, (x1), (y1 + halfY), (x), (buffer[x] + halfY));// draw sin wave
                x1 = x;
                y1 = buffer[x];
                }

            x1 = 0;
            nextDraw = nextDraw + 900;
            float rest = t - 900;
            t = rest;
            if (t > 0)
            {
                DrawWave.Clear(Color.Black);
                goto Drowagain;
            }
        }

        private void picBox_Click(object sender, EventArgs e)
        {
                Point point = picBox.PointToClient(Cursor.Position);
                float x = point.X - 100;
                float y = point.Y - 100;
                float X = x + 290;

                // draw rectangle
                Graphics DrawWave = picBox.CreateGraphics();
                Pen penzoom = new Pen(Brushes.White, 3);
                DrawWave.DrawRectangle(penzoom, x, y, 300, 300);
                //DrawWave.DrawRectangle(penzoom, x, 0, 300, 550);
                //DrawWave.Clear(Color.Black);
                DrawWave.DrawRectangle(penzoom, 1, 1, 898, 510);
                x1 = 0;
                y1 = 0;
                float y2 = 0;
                for (int j = 0; j < 1000; j++)
                {
                    if (x > X)
                    {
                        break;
                    }
                    y2 = (float)(amplitude * Math.Sin(2 * Math.PI * frequency * (x / 11025)));
                    DrawWave.DrawLine(penzoom, (x1 * 3.08f), ((y1 * 2) + halfY), (j * 3.08f), ((y2 * 2) + halfY));
                    x1 = j;
                    y1 = y2;
                    x++;
                }
        }
    }
}

