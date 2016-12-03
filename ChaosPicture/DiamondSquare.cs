using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoiseGenerator
{
    class DiamondSquare
    {
        public static int ysize = 1025, xsize = ysize * 2 - 1;
        public static float[,] heighmap = new float[xsize, ysize];
        public static float roughness = 2f;      //Определяет разницу высот, чем больше, тем более неравномерная карта высот

        public static void Square(int lx, int ly, int rx, int ry)
        {
            int l = (rx - lx) / 2;

            float a = heighmap[lx, ly];              //  B--------C
            float b = heighmap[lx, ry];              //  |        |
            float c = heighmap[rx, ry];              //  |   ce   |
            float d = heighmap[rx, ly];              //  |        |        
            int cex = lx + l;                        //  A--------D
            int cey = ly + l;

            //heighmap[cex, cey] = (a + b + c + d) / 4 + Random.Range(-l * 2 * roughness / ysize, l * 2 * roughness / ysize);

            Random randomValue = new Random();
            //randomValue.Next()
            heighmap[cex, cey] = (a + b + c + d) / 4 + randomValue.Next((int)Math.Round(-l * 2 * roughness / ysize), (int)Math.Round(l * 2 * roughness / ysize));
        }


    }
}
