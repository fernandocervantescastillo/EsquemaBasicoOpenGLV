using PracticaOpenTK.model;
using System;
using System.Collections.Generic;
using System.Text;

namespace PracticaOpenTK.common
{
    class ObjetoShape
    {

        public static Objeto initCubo(float l, float h, float a, float x, float y, float z, string name)
        {
            Objeto cubo = new Objeto();
            List<float> puntos = new List<float>();
            puntos.Add(l); puntos.Add(0); puntos.Add(0);
            puntos.Add(0); puntos.Add(0); puntos.Add(0);
            puntos.Add(0); puntos.Add(h); puntos.Add(0);
            puntos.Add(l); puntos.Add(h); puntos.Add(0);
            Cara cara1 = new Cara(puntos, x, y, z, 0f, 1f, 1f, 1f);

            puntos.Clear();
            puntos.Add(l); puntos.Add(0); puntos.Add(a);
            puntos.Add(l); puntos.Add(0); puntos.Add(0);
            puntos.Add(l); puntos.Add(h); puntos.Add(0);
            puntos.Add(l); puntos.Add(h); puntos.Add(a);
            Cara cara2 = new Cara(puntos, x, y, z, 1f, 0f, 1f, 1f);

            puntos.Clear();
            puntos.Add(0); puntos.Add(0); puntos.Add(0);
            puntos.Add(0); puntos.Add(0); puntos.Add(a);
            puntos.Add(0); puntos.Add(h); puntos.Add(a);
            puntos.Add(0); puntos.Add(h); puntos.Add(0);
            Cara cara3 = new Cara(puntos, x, y, z, 1f, 1f, 0f, 1f);

            puntos.Clear();
            puntos.Add(0); puntos.Add(0); puntos.Add(0);
            puntos.Add(l); puntos.Add(0); puntos.Add(0);
            puntos.Add(l); puntos.Add(0); puntos.Add(a);
            puntos.Add(0); puntos.Add(0); puntos.Add(a);
            Cara cara4 = new Cara(puntos, x, y, z, 0f, 0f, 1f, 1f);

            puntos.Clear();
            puntos.Add(0); puntos.Add(0); puntos.Add(a);
            puntos.Add(l); puntos.Add(0); puntos.Add(a);
            puntos.Add(l); puntos.Add(h); puntos.Add(a);
            puntos.Add(0); puntos.Add(h); puntos.Add(a);
            Cara cara5 = new Cara(puntos, x, y, z, 0f, 1f, 0f, 1f);

            puntos.Clear();
            puntos.Add(0); puntos.Add(h); puntos.Add(a);
            puntos.Add(l); puntos.Add(h); puntos.Add(a);
            puntos.Add(l); puntos.Add(h); puntos.Add(0);
            puntos.Add(0); puntos.Add(h); puntos.Add(0);
            Cara cara6 = new Cara(puntos, x, y, z, 1f, 0f, 0f, 1f);

            cubo.addCara(cara1);
            cubo.addCara(cara2);
            cubo.addCara(cara3);
            cubo.addCara(cara4);
            cubo.addCara(cara5);
            cubo.addCara(cara6);

            cubo.setNombre(name);
            return cubo;
        }

        public static Objeto initPiramide(float l, float h, float a, float x, float y, float z, string name)
        {
            Objeto piramide = new Objeto();
            List<float> puntos = new List<float>();
            puntos.Add(0); puntos.Add(0); puntos.Add(0);
            puntos.Add(l); puntos.Add(0); puntos.Add(0);
            puntos.Add(l); puntos.Add(0); puntos.Add(a);
            puntos.Add(0); puntos.Add(0); puntos.Add(a);
            Cara cara1 = new Cara(puntos, x, y, z, 0f, 1f, 1f, 1f);

            puntos.Clear();
            puntos.Add(l); puntos.Add(0); puntos.Add(0);
            puntos.Add(0); puntos.Add(0); puntos.Add(0);
            puntos.Add(l / 2.0f); puntos.Add(h); puntos.Add(a / 2.0f);
            Cara cara2 = new Cara(puntos, x, y, z, 1f, 0f, 1f, 1f);

            puntos.Clear();
            puntos.Add(l); puntos.Add(0); puntos.Add(a);
            puntos.Add(l); puntos.Add(0); puntos.Add(0);
            puntos.Add(l / 2.0f); puntos.Add(h); puntos.Add(a / 2.0f);
            Cara cara3 = new Cara(puntos, x, y, z, 1f, 1f, 0f, 1f);

            puntos.Clear();
            puntos.Add(0); puntos.Add(0); puntos.Add(0);
            puntos.Add(0); puntos.Add(0); puntos.Add(a);
            puntos.Add(l / 2.0f); puntos.Add(h); puntos.Add(a / 2.0f);
            Cara cara4 = new Cara(puntos, x, y, z, 0f, 0f, 1f, 1f);

            puntos.Clear();
            puntos.Add(0); puntos.Add(0); puntos.Add(a);
            puntos.Add(l); puntos.Add(0); puntos.Add(a);
            puntos.Add(l / 2.0f); puntos.Add(h); puntos.Add(a / 2.0f);
            Cara cara5 = new Cara(puntos, x, y, z, 0f, 1f, 0f, 1f);


            piramide.addCara(cara1);
            piramide.addCara(cara2);
            piramide.addCara(cara3);
            piramide.addCara(cara4);
            piramide.addCara(cara5);

            piramide.setNombre(name);

            return piramide;
        }
    }
}
