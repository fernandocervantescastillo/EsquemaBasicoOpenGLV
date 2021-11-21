using OpenTK.Mathematics;
using OpenTK.Windowing.Desktop;
using PracticaOpenTK.common;
using PracticaOpenTK.model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PracticaOpenTK
{
    class Escenario
    {
        private Dictionary<string, Objeto> dictionary;
        private Matrix4 model;

        public Escenario()
        {
            dictionary = new Dictionary<string, Objeto>();
            model = Matrix4.Identity;
        }

        public int getCantidad()
        {
            return dictionary.Count;
        }

        public void agregarObjeto(Objeto objeto)
        {
            dictionary.Add(objeto.nombre, objeto);
        }

        public Objeto buscarObjeto(String t)
        {
            return dictionary[t];
        }

        public Objeto getObjeto(int i)
        {
            return dictionary.Values.ElementAt(i);
        }

        public void render(Camera _camera)
        {
            foreach (KeyValuePair<string, Objeto> objeto in dictionary)
            {
                objeto.Value.render(_camera, model);
            }
        }

        public void rotar(float x, float y, float z)
        {
            model = model * Matrix4.CreateRotationX(x) * Matrix4.CreateRotationY(y) * Matrix4.CreateRotationZ(z);
        }

        public void ampliar(float x, float y, float z)
        {
            model = model * Matrix4.CreateScale(x, y, z);
        }

        public void trasladar(float x, float y, float z)
        {
            model = model * Matrix4.CreateTranslation(x, y, z);
        }

        public void clear()
        {
            dictionary.Clear();
        }

        public void dispose()
        {
            foreach (KeyValuePair<string, Objeto> objeto in dictionary)
            {
                objeto.Value.dispose();
            }
        }

    }
}