using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OpenTK.Graphics.OpenGL4;

namespace EscritorioOpenTK
{
    public partial class Form1 : Form
    {

        Timer timer;
        int interval;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            onLoad1();
            interval = 200;
            var timer = new Timer();
            timer.Tick += GameLoop;
            timer.Interval = interval;
            timer.Start();
        }



        private void GameLoop(object sender, System.EventArgs e)
        {
            onRender1();
            glControl.Invalidate();
        }

        private void glControl_Resize(object sender, EventArgs e)
        {
            onResize1();
        }



        private void onLoad1()
        {
            glControl.MakeCurrent();
            GL.ClearColor(1f, 0f, 0f, 1f);
            GL.Viewport(0, 0, glControl.Width, glControl.Height);


        }

        private void onRender1()
        {
            glControl.MakeCurrent();

            GL.Clear(ClearBufferMask.ColorBufferBit);
        }

        private void onResize1()
        {
            glControl.MakeCurrent();
            GL.Viewport(0, 0, glControl.Width, glControl.Height);
        }
    }
}
