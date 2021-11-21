using OpenTK.Windowing.Common;
using OpenTK.Windowing.GraphicsLibraryFramework;
using OpenTK.Windowing.Desktop;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using PracticaOpenTK.common;
using PracticaOpenTK.model;

namespace PracticaOpenTK
{
    class Game : GameWindow
    {
        private Camera _camera;
        private double _time;
        private bool _firstMove = true;
        private Vector2 _lastPos;

        Escenario escenario;

        public Game(GameWindowSettings gameWindowSettings, NativeWindowSettings nativeWindowSettings)
            : base(gameWindowSettings, nativeWindowSettings)
        {
            this.CenterWindow();
        }


        public void initEscenario()
        {
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
            for(int i = 0; i < escenario.getCantidad(); i++)
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
            
        }

        protected override void OnLoad()
        {
            GL.ClearColor(0f, 0f, 0f, 1.0f);
            GL.Enable(EnableCap.DepthTest);

            escenario = new Escenario();
            initEscenario();

            _camera = new Camera(Vector3.UnitZ * 3, Size.X / (float)Size.Y);
            CursorGrabbed = true;

            base.OnLoad();
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            _time += 0 * e.Time;

            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            escenario.rotar(0, 0.005f, 0);
            //escenario.ampliar(1.0001f, 1.0001f, 1.0001f);
            escenario.getObjeto(0).rotarEje(0.001f,0,0);
            escenario.getObjeto(1).rotarEje(0.001f, 0, 0);
            escenario.getObjeto(2).rotarEje(0.001f, 0, 0);
            

            escenario.render(_camera);

            SwapBuffers();
            base.OnRenderFrame(e);
        }

        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            if (!IsFocused)
            {
                return;
            }

            var input = KeyboardState;

            if (input.IsKeyDown(Keys.Escape))
            {
                Close();
            }

            const float cameraSpeed = 1.5f;
            const float sensitivity = 0.2f;

            if (input.IsKeyDown(Keys.W))
            {
                _camera.Position += _camera.Front * cameraSpeed * (float)e.Time; // Adelante
            }

            if (input.IsKeyDown(Keys.S))
            {
                _camera.Position -= _camera.Front * cameraSpeed * (float)e.Time; // Atras
            }
            if (input.IsKeyDown(Keys.A))
            {
                _camera.Position -= _camera.Right * cameraSpeed * (float)e.Time; // Izquierda
            }
            if (input.IsKeyDown(Keys.D))
            {
                _camera.Position += _camera.Right * cameraSpeed * (float)e.Time; // Derecha
            }
            if (input.IsKeyDown(Keys.Space))
            {
                _camera.Position += _camera.Up * cameraSpeed * (float)e.Time; // Arriba
            }
            if (input.IsKeyDown(Keys.LeftShift))
            {
                _camera.Position -= _camera.Up * cameraSpeed * (float)e.Time; // Abajo
            }

            var mouse = MouseState;

            if (_firstMove)
            {
                _lastPos = new Vector2(mouse.X, mouse.Y);
                _firstMove = false;
            }
            else
            {
                var deltaX = mouse.X - _lastPos.X;
                var deltaY = mouse.Y - _lastPos.Y;
                _lastPos = new Vector2(mouse.X, mouse.Y);

                _camera.Yaw += deltaX * sensitivity;
                _camera.Pitch -= deltaY * sensitivity;
            }

            base.OnUpdateFrame(e);
        }

        protected override void OnMouseWheel(MouseWheelEventArgs e)
        {
            _camera.Fov -= e.OffsetY;
            base.OnMouseWheel(e);
        }

        protected override void OnResize(ResizeEventArgs e)
        {
            GL.Viewport(0, 0, Size.X, Size.Y);
            _camera.AspectRatio = Size.X / (float)Size.Y;
            base.OnResize(e);
        }

        protected override void OnUnload()
        {
            base.OnUnload();
        }
    }
}