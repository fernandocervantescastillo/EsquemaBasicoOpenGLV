using Newtonsoft.Json;
using OpenTK.Mathematics;
using PracticaOpenTK.common;
using System;
using System.Collections.Generic;

namespace PracticaOpenTK.model
{
    [Serializable()]
    class Objeto
    {

        public string nombre;
        public float x;
        public float y;
        public float z;

        public List<Cara> list;

        private Matrix4 model;

        public Objeto()
        {
            list = new List<Cara>();
            model = Matrix4.Identity;
            nombre = "";
        }

        public Objeto(string JSON)
        {
            Objeto obj1 = JsonConvert.DeserializeObject<Objeto>(JSON);
            this.list = obj1.list;
            this.x = obj1.x;
            this.y = obj1.y;
            this.z = obj1.z;
            this.nombre = obj1.nombre;
            model = Matrix4.Identity;
        }

        public void setNombre(String nombre)
        {
            this.nombre = nombre;
        }

        public String getNombre()
        {
            return nombre;
        }

        public void rotar(float x, float y, float z)
        {
            model = model * Matrix4.CreateRotationX(x) * Matrix4.CreateRotationY(y) * Matrix4.CreateRotationZ(z);
        }

        public void rotarEje(float x, float y, float z)
        {
            Matrix4 m = Matrix4.Identity;
            m = m * Matrix4.CreateTranslation(-this.x, -this.y, -this.z);
            m = m * Matrix4.CreateRotationX(x) * Matrix4.CreateRotationY(y) * Matrix4.CreateRotationZ(z);
            m = m * Matrix4.CreateTranslation(this.x, this.y, this.z);
            model = m * model;
        }

        public void ampliar(float x, float y, float z)
        {
            model = model * Matrix4.CreateScale(x, y, z);
        }

        public void trasladar(float x, float y, float z)
        {
            model = model * Matrix4.CreateTranslation(x, y, z);
        }

        public void setCenter(float x, float y, float z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        public void addCara(Cara cara)
        {
            list.Add(cara);
        }

        public Cara getCara(int i)
        {
            return list[i];
        }

        public int countCaras()
        {
            return list.Count;
        }

        public void render(Camera _camera, Matrix4 model1)
        {
            Matrix4 m = Matrix4.Identity * model * model1;

            foreach (Cara cara in list)
            {
                cara.render(_camera, m);
            }
        }

        public void dispose()
        {
            foreach (Cara cara in list)
            {
                cara.dispose();
            }

        }

        public string toJSON()
        {
            string text = JsonConvert.SerializeObject(this, Formatting.Indented);
            return text;
        }


    }
}