using EjercicioOpenTK.common;
using EjercicioOpenTK.model;
using OpenTK;
using System;
using System.Collections.Generic;
using System.Text;
using OpenTK.Graphics.OpenGL4;
using System.Windows.Forms;

namespace EjercicioOpenTK
{
    class Game
    {
        private Camera _camera;
        private bool _firstMove = true;
        private Vector2 _lastPos;

        const float sensitivity = 0.2f; //Mouse wheel
        const float cameraSpeed = 1.5f; //Velocidad de movimiento asdw

        private List<Escenario> escenarios;

        public Game()
        {

        }


        public void initEscenario()
        {
            Escenario escenario;
            escenario = new Escenario();
            float x = 0;
            float y = 0;
            float z = 0;

            Objeto cubo1 = ObjetoShape.initCubo(1f, 1f, 1f, x, y, z, "Cubo1");
            Objeto piramide1 = ObjetoShape.initPiramide(1, 1, 1, x, y, z, "Piramide1");
            Objeto piramide2 = ObjetoShape.initPiramide(1f, 1f, 1f, x, y, z, "Piramide2");

            escenario.agregarObjeto(cubo1);
            escenario.agregarObjeto(piramide1);
            escenario.agregarObjeto(piramide2);

            string t;
            Objeto obj;
            for (int i = 0; i < escenario.getCantidad(); i++)
            {
                obj = escenario.getObjeto(i);
                t = obj.toJSON();
                TextFile.saveFileText(t, obj.nombre);
            }
            escenario.clear();


            //Lee y agrega los objetos
            string t1 = TextFile.getFileText("Cubo1");
            string t2 = TextFile.getFileText("Piramide1");
            string t3 = TextFile.getFileText("Piramide2");

            Objeto bsObj1 = new Objeto(t1);
            Objeto bsObj2 = new Objeto(t2);
            bsObj2.trasladar(0, 0, 1.5f);
            Objeto bsObj3 = new Objeto(t3);
            bsObj3.trasladar(1.5f, 0, 1.5f);

            escenario.agregarObjeto(bsObj1);
            escenario.agregarObjeto(bsObj2);
            escenario.agregarObjeto(bsObj3);

            addEscenario(escenario);

        }

        public void OnLoad(int width, int height)
        {
            GL.ClearColor(0f, 0f, 0f, 1.0f);
            GL.Enable(EnableCap.DepthTest);


            escenarios = new List<Escenario>();
            initEscenario();

            _camera = new Camera(Vector3.UnitZ * 3, width / (float)height);
            //CursorGrabbed = true;
        }

        public void OnRenderFrame()
        {

            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            for (int i = 0; i < getCountEscenario(); i++)
            {
                Escenario escenario = getEscenario(i);
                //Rotamos el escenario
                escenario.rotar(0, 0.005f, 0);

                escenario.render(_camera);
            }
        }

        public void OnKeyDown(KeyEventArgs e, int time)
        {
           
            float t = time/(float)1000;

            if (e.KeyCode == Keys.W)
            {
                _camera.Position += _camera.Front * cameraSpeed * t; // Adelante
            }

            if (e.KeyCode == Keys.S)
            {
                _camera.Position -= _camera.Front * cameraSpeed * t; // Atras
            }
            if (e.KeyCode == Keys.A)
            {
                _camera.Position -= _camera.Right * cameraSpeed * t; // Izquierda
            }
            if (e.KeyCode == Keys.D)
            {
                _camera.Position += _camera.Right * cameraSpeed * t; // Derecha
            }
            if (e.KeyCode == Keys.Space)
            {
                _camera.Position += _camera.Up * cameraSpeed * t; // Arriba
            }
            if (e.KeyCode == Keys.Q)
            {
                _camera.Position -= _camera.Up * cameraSpeed * t; // Abajo
            }
        }


        public void onMouseMove(MouseEventArgs e)
        {
            if (false)
            {
                if (_firstMove)
                {
                    _lastPos = new Vector2(e.X, e.Y);
                    _firstMove = false;
                }
                else
                {
                    var deltaX = e.X - _lastPos.X;
                    var deltaY = e.Y - _lastPos.Y;
                    _lastPos = new Vector2(e.X, e.Y);

                    _camera.Yaw += deltaX * sensitivity;
                    _camera.Pitch -= deltaY * sensitivity;
                }
            }
        }




        public void OnMouseWheel(System.Windows.Forms.MouseEventArgs e)
        {
            _camera.Fov -= e.Delta;
        }

        public void OnResize(int width, int height)
        {
            GL.Viewport(0, 0, width, height);
            _camera.AspectRatio = width / (float)height;
        }


        public int getCountEscenario()
        {
            return escenarios.Count;
        }

        public Escenario getEscenario(int index)
        {
            return escenarios[index];
        }

        public void addEscenario(Escenario escenario)
        {
            escenarios.Add(escenario);
        }

        public void eliminarEscenario(int index)
        {
            escenarios.RemoveAt(index);
        }

        public void OnUnload()
        {
            for (int i = 0; i < getCountEscenario(); i++)
            {
                Escenario escenario = getEscenario(i);
                escenario.dispose();
            }
        }
    }
}