using System;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using EjercicioOpenTK.common;
using EjercicioOpenTK.model;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;


namespace EjercicioOpenTK
{
    public partial class Form1 : Form
    {

        private Timer _timer = null!;
        private int interval;
        Game game;

        private const string ROTAR_ESCENARIO = "Rotar escenario";
        private const string AMPLIAR_ESCENARIO = "Ampliar escenario";
        private const string TRASLADAR_ESCENARIO = "Trasladar escenario";
        private const string ROTAR_OBJETO = "Rotar objeto";
        private const string AMPLIAR_OBJETO = "Ampliar objeto";
        private const string TRASLADAR_OBJETO = "Trasladar objeto";
        private const string ROTAR_OBJETO_EJE = "Rotar objeto eje";

        public Form1()
        {
            InitializeComponent();
            
            game = new Game();



        }

        private void glControl1_Load(object sender, EventArgs e)
        {

            glControl.MakeCurrent();
            glControl.VSync = true;
           

            OnLoad1();
            glControl.Resize += glControl_Resize;
            glControl.Paint += glControl_Paint;

            interval = 1000/20;//20 FPS
            _timer = new Timer();
            _timer.Tick += (sender, e) =>
            {
                Render();
            };
            _timer.Interval = interval; ;
            _timer.Start();
        }

        private void glControl_Resize(object? sender, EventArgs e)
        {
            glControl.MakeCurrent();
            if (glControl.ClientSize.Height <= 0)
                return;
            if (glControl.ClientSize.Width <= 0)
                return;
            game.OnResize(glControl.Width, glControl.Height);
        }

        private void glControl_Paint(object sender, PaintEventArgs e)
        {
            Render();
        }

        private void Render()
        {
            glControl.MakeCurrent();

            game.OnRenderFrame();

            glControl.SwapBuffers();
        }

        public void OnLoad1()
        {
            glControl.MakeCurrent();
            game.OnLoad(glControl.Width, glControl.Height);


            cargarObjetosComboBox();
            cargarEscenario();
            cargarObjetosEscenario();
            cargarAcciones();
            
        }

        private void glControl_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            
        }

        private void glControl_KeyDown(object sender, KeyEventArgs e)
        {
            game.OnKeyDown(e, interval);
        }

        private void glControl_MouseMove(object sender, MouseEventArgs e)
        {
            game.onMouseMove(e);
        }


        protected override void OnClosed(EventArgs e)
        {
            game.OnUnload();
            _timer.Stop();
            base.OnClosed(e);
        }


        private int longitudACortar(string t)
        {
            int c = 0;
            for(int i = t.Length - 1; i >= 0; i--)
            {
                if (t.Substring(i, 1).CompareTo("\\") == 0)
                    return c;
                c++;

            }
            return 0;
        }

        private string cortarCadena(string t)
        {
            int c = longitudACortar(t);
            string u = t.Substring(t.Length - c);
            u = u.Substring(0, u.Length - 4);
            return u;
        }

        private void cargarObjetosComboBox()
        {
            string path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase);
            path = path.Remove(0, 6);
            path = path + "..\\..\\..\\..\\files\\";

            string[] array1 = Directory.GetFiles(path);

            foreach (string name in array1)
            {
                comboBox4.Items.Add(cortarCadena(name));
            }

            if (comboBox4.Items.Count > 0)
            {
                comboBox4.Text = comboBox4.Items[0].ToString();
            }
        }

        private void cargarObjetosEscenario()
        {
            comboBox2.Items.Clear();

            if (comboBox1.SelectedItem == null)
                return;
            Escenario escenario = game.getEscenario(comboBox1.SelectedIndex);
            
            for(int i = 0; i < escenario.getCantidad(); i++)
            {
                comboBox2.Items.Add(escenario.getObjeto(i).getNombre());
            }

            if (escenario.getCantidad() > 0)
            {
                comboBox2.Text = comboBox2.Items[0].ToString();
            }
        }

        private void cargarEscenario()
        {
            comboBox1.Items.Clear();
            for (int i = 0; i < game.getCountEscenario(); i++)
            {
                comboBox1.Items.Add("Escenario " + i);
            }

            if (comboBox1.Items.Count > 0)
            {
                comboBox1.Text = comboBox1.Items[0].ToString();
            }
        }

        private void cargarAcciones()
        {
            comboBox3.Items.Add(ROTAR_ESCENARIO);
            comboBox3.Items.Add(AMPLIAR_ESCENARIO);
            comboBox3.Items.Add(TRASLADAR_ESCENARIO);
            comboBox3.Items.Add(ROTAR_OBJETO);
            comboBox3.Items.Add(AMPLIAR_OBJETO);
            comboBox3.Items.Add(TRASLADAR_OBJETO);
            comboBox3.Items.Add(ROTAR_OBJETO_EJE);

            comboBox3.Text = comboBox3.Items[0].ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Escenario escenario = new Escenario();
            game.addEscenario(escenario);
            cargarEscenario();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem == null)
                return;

            if (comboBox4.SelectedItem == null)
                return;

            string key = textBox1.Text;

            if (key.CompareTo("") == 0)
            {
                label9.Text = "Ingrese nombre";
                return;
            }

            int index = comboBox1.SelectedIndex;
            Escenario escenario = game.getEscenario(index);
            string t = TextFile.getFileText(comboBox4.SelectedItem.ToString());

            if (escenario.existeObjeto(key))
            {
                label9.Text = "Ya hay un objeto con ese nombre";
                return;
            }            

            Objeto bsObj = new Objeto(t);
            bsObj.setNombre(key);
            escenario.agregarObjeto(bsObj);

            cargarObjetosEscenario();
        }

        float delta = 0.1f;

        private void button4_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem == null)
                return;
            int index = comboBox1.SelectedIndex;
            Escenario escenario = game.getEscenario(index);
            string t = comboBox3.SelectedItem.ToString();

            if (t == ROTAR_ESCENARIO)
                escenario.rotar(delta, 0, 0);
            if (t == TRASLADAR_ESCENARIO)
                escenario.trasladar(delta, 0, 0);
            if (t == AMPLIAR_ESCENARIO)
                escenario.ampliar(1.0f + delta, 1, 1);


            if (comboBox2.SelectedItem == null)
                return;

            if (t == ROTAR_OBJETO)
            {
                string obj = comboBox2.SelectedItem.ToString();
                Objeto objeto = escenario.buscarObjeto(obj);
                objeto.rotar(delta, 0, 0);
            }
            if (t == TRASLADAR_OBJETO)
            {
                string obj = comboBox2.SelectedItem.ToString();
                Objeto objeto = escenario.buscarObjeto(obj);
                objeto.trasladar(delta, 0, 0);
            }
            if (t == AMPLIAR_OBJETO)
            {
                string obj = comboBox2.SelectedItem.ToString();
                Objeto objeto = escenario.buscarObjeto(obj);
                objeto.ampliar(1.0f + delta, 1, 1);
            }

            if (t == ROTAR_OBJETO_EJE)
            {
                string obj = comboBox2.SelectedItem.ToString();
                Objeto objeto = escenario.buscarObjeto(obj);
                objeto.rotarEje(delta, 0, 0);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem == null)
                return;
            int index = comboBox1.SelectedIndex;
            Escenario escenario = game.getEscenario(index);
            string t = comboBox3.SelectedItem.ToString();

            if (t == ROTAR_ESCENARIO)
                escenario.rotar(-delta, 0, 0);
            if (t == TRASLADAR_ESCENARIO)
                escenario.trasladar(-delta, 0, 0);
            if (t == AMPLIAR_ESCENARIO)
                escenario.ampliar(1.0f - delta, 1, 1);

            if (comboBox2.SelectedItem == null)
                return;

            if (t == ROTAR_OBJETO)
            {
                string obj = comboBox2.SelectedItem.ToString();
                Objeto objeto = escenario.buscarObjeto(obj);
                objeto.rotar(-delta, 0, 0);
            }
            if (t == TRASLADAR_OBJETO)
            {
                string obj = comboBox2.SelectedItem.ToString();
                Objeto objeto = escenario.buscarObjeto(obj);
                objeto.trasladar(-delta, 0, 0);
            }
            if (t == AMPLIAR_OBJETO)
            {
                string obj = comboBox2.SelectedItem.ToString();
                Objeto objeto = escenario.buscarObjeto(obj);
                objeto.ampliar(1.0f - delta, 1, 1);
            }

            if (t == ROTAR_OBJETO_EJE)
            {
                string obj = comboBox2.SelectedItem.ToString();
                Objeto objeto = escenario.buscarObjeto(obj);
                objeto.rotarEje(-delta, 0, 0);
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem == null)
                return;
            int index = comboBox1.SelectedIndex;
            Escenario escenario = game.getEscenario(index);
            string t = comboBox3.SelectedItem.ToString();

            if (t == ROTAR_ESCENARIO)
                escenario.rotar(0, delta, 0);
            if (t == TRASLADAR_ESCENARIO)
                escenario.trasladar(0, delta, 0);
            if (t == AMPLIAR_ESCENARIO)
                escenario.ampliar(1, 1.0f + delta, 1);

            if (comboBox2.SelectedItem == null)
                return;

            if (t == ROTAR_OBJETO)
            {
                string obj = comboBox2.SelectedItem.ToString();
                Objeto objeto = escenario.buscarObjeto(obj);
                objeto.rotar(0, delta, 0);
            }
            if (t == TRASLADAR_OBJETO)
            {
                string obj = comboBox2.SelectedItem.ToString();
                Objeto objeto = escenario.buscarObjeto(obj);
                objeto.trasladar(0, delta, 0);
            }
            if (t == AMPLIAR_OBJETO)
            {
                string obj = comboBox2.SelectedItem.ToString();
                Objeto objeto = escenario.buscarObjeto(obj);
                objeto.ampliar(1, 1.0f + delta, 1);
            }

            if (t == ROTAR_OBJETO_EJE)
            {
                string obj = comboBox2.SelectedItem.ToString();
                Objeto objeto = escenario.buscarObjeto(obj);
                objeto.rotarEje(0, delta, 0);
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem == null)
                return;
            int index = comboBox1.SelectedIndex;
            Escenario escenario = game.getEscenario(index);
            string t = comboBox3.SelectedItem.ToString();

            if (t == ROTAR_ESCENARIO)
                escenario.rotar(0, -delta, 0);
            if (t == TRASLADAR_ESCENARIO)
                escenario.trasladar(0, -delta, 0);
            if (t == AMPLIAR_ESCENARIO)
                escenario.ampliar(1, 1.0f - delta, 1);

            if (comboBox2.SelectedItem == null)
                return;

            if (t == ROTAR_OBJETO)
            {
                string obj = comboBox2.SelectedItem.ToString();
                Objeto objeto = escenario.buscarObjeto(obj);
                objeto.rotar(0, -delta, 0);
            }
            if (t == TRASLADAR_OBJETO)
            {
                string obj = comboBox2.SelectedItem.ToString();
                Objeto objeto = escenario.buscarObjeto(obj);
                objeto.trasladar(0, -delta, 0);
            }
            if (t == AMPLIAR_OBJETO)
            {
                string obj = comboBox2.SelectedItem.ToString();
                Objeto objeto = escenario.buscarObjeto(obj);
                objeto.ampliar(1, 1.0f - delta, 1);
            }

            if (t == ROTAR_OBJETO_EJE)
            {
                string obj = comboBox2.SelectedItem.ToString();
                Objeto objeto = escenario.buscarObjeto(obj);
                objeto.rotarEje(0, -delta, 0);
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem == null)
                return;
            int index = comboBox1.SelectedIndex;
            Escenario escenario = game.getEscenario(index);
            string t = comboBox3.SelectedItem.ToString();

            if (t == ROTAR_ESCENARIO)
                escenario.rotar(0, 0, delta);
            if (t == TRASLADAR_ESCENARIO)
                escenario.trasladar(0, 0, delta);
            if (t == AMPLIAR_ESCENARIO)
                escenario.ampliar(1, 1, 1.0f + delta);

            if (comboBox2.SelectedItem == null)
                return;

            if (t == ROTAR_OBJETO)
            {
                string obj = comboBox2.SelectedItem.ToString();
                Objeto objeto = escenario.buscarObjeto(obj);
                objeto.rotar(0, 0, delta);
            }
            if (t == TRASLADAR_OBJETO)
            {
                string obj = comboBox2.SelectedItem.ToString();
                Objeto objeto = escenario.buscarObjeto(obj);
                objeto.trasladar(0, 0, delta);
            }
            if (t == AMPLIAR_OBJETO)
            {
                string obj = comboBox2.SelectedItem.ToString();
                Objeto objeto = escenario.buscarObjeto(obj);
                objeto.ampliar(1, 1, 1.0f + delta);
            }

            if (t == ROTAR_OBJETO_EJE)
            {
                string obj = comboBox2.SelectedItem.ToString();
                Objeto objeto = escenario.buscarObjeto(obj);
                objeto.rotarEje(0, 0, delta);
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem == null)
                return;
            int index = comboBox1.SelectedIndex;
            Escenario escenario = game.getEscenario(index);
            string t = comboBox3.SelectedItem.ToString();

            if (t == ROTAR_ESCENARIO)
                escenario.rotar(0, 0, -delta);
            if (t == TRASLADAR_ESCENARIO)
                escenario.trasladar(0, 0, -delta);
            if (t == AMPLIAR_ESCENARIO)
                escenario.ampliar(1, 1, 1.0f - delta);

            if (comboBox1.SelectedItem == null)
                return;

            if (t == ROTAR_OBJETO)
            {
                string obj = comboBox2.SelectedItem.ToString();
                Objeto objeto = escenario.buscarObjeto(obj);
                objeto.rotar(0, 0, -delta);
            }
            if (t == TRASLADAR_OBJETO)
            {
                string obj = comboBox2.SelectedItem.ToString();
                Objeto objeto = escenario.buscarObjeto(obj);
                objeto.trasladar(0, 0, -delta);
            }
            if (t == AMPLIAR_OBJETO)
            {
                string obj = comboBox2.SelectedItem.ToString();
                Objeto objeto = escenario.buscarObjeto(obj);
                objeto.ampliar(1, 1, 1.0f - delta);
            }

            if (t == ROTAR_OBJETO_EJE)
            {
                string obj = comboBox2.SelectedItem.ToString();
                Objeto objeto = escenario.buscarObjeto(obj);
                objeto.rotarEje(0, 0, -delta);
            }
        }

        private void comboBox1_SelectionChangeCommitted(object sender, EventArgs e)
        {
            cargarObjetosEscenario();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem == null)
                return;
            int index = comboBox1.SelectedIndex;
            game.eliminarEscenario(index);
            cargarEscenario();
            cargarObjetosEscenario();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem == null)
                return;
            if (comboBox2.SelectedItem == null)
                return;
            int index = comboBox1.SelectedIndex;
            string t = comboBox2.SelectedItem.ToString();
            Escenario escenario = game.getEscenario(index);
            escenario.eliminarObjeto(t);

            cargarObjetosEscenario();
        }
    }
}
