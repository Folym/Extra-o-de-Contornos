using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace ProcessamentoImagens
{
    class Filtros
    {
        //sem acesso direto a memoria
        public static void threshold(Bitmap imageBitmapSrc)
        {
            int width = imageBitmapSrc.Width;
            int height = imageBitmapSrc.Height;
            int r, g, b;
            Int32 gs;

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    //obtendo a cor do pixel
                    Color cor = imageBitmapSrc.GetPixel(x, y);

                    r = cor.R;
                    g = cor.G;
                    b = cor.B;
                    gs = (r + g + b) / 3;
                    if (gs > 127)
                        gs = 255;
                    else
                        gs = 0;

                    //nova cor
                    Color newcolor = Color.FromArgb(gs, gs, gs);
                    imageBitmapSrc.SetPixel(x, y, newcolor);
                }
            }
        }



        public static void countour(Bitmap imageBitmapSrc, Bitmap imageBitmapDest)
        {
            threshold(imageBitmapSrc);
            int width = imageBitmapSrc.Width;
            int height = imageBitmapSrc.Height;
            int x, y, x2, y2;
            bool flag = true;

            Color corB = Color.FromArgb(255,255,255);
            for (y = 0; y < height; y++)
                for (x = 0; x < width; x++)
                    imageBitmapDest.SetPixel(x, y, corB);

            for (y = 0; y < height; y++)
            {
                for (x = 0; x < width-1; x++)
                {
                    //obtendo a cor do pixel
                    Color cor = imageBitmapSrc.GetPixel(x, y);
                    Color cor2 = imageBitmapSrc.GetPixel(x+1, y);
                    if(cor.R == 255 && cor2.R == 0)
                    {
                        x2 = x;
                        y2 = y;
                        do
                        {
                            Color p0, p1, p2, p3, p4, p5, p6, p7, pt0, pt1, pt2, pt3, pt4, pt5, pt6, pt7;
                            imageBitmapDest.SetPixel(x2, y2, cor2);

                            //posições
                            p0 = imageBitmapSrc.GetPixel(x2 + 1, y2);
                            p1 = imageBitmapSrc.GetPixel(x2 + 1, y2 - 1);
                            p2 = imageBitmapSrc.GetPixel(x2, y2 - 1);
                            p3 = imageBitmapSrc.GetPixel(x2 - 1, y2 - 1);
                            p4 = imageBitmapSrc.GetPixel(x2 - 1, y2);
                            p5 = imageBitmapSrc.GetPixel(x2 - 1, y2 + 1);
                            p6 = imageBitmapSrc.GetPixel(x2, y2 + 1);
                            p7 = imageBitmapSrc.GetPixel(x2 + 1, y2 + 1);

                            //posições para comparação
                            pt0 = imageBitmapSrc.GetPixel(x2 + 1, y2 - 1);
                            pt1 = imageBitmapSrc.GetPixel(x2, y2 - 1);
                            pt2 = imageBitmapSrc.GetPixel(x2 - 1, y2 - 1);
                            pt3 = imageBitmapSrc.GetPixel(x2 - 1, y2);
                            pt4 = imageBitmapSrc.GetPixel(x2 - 1, y2 + 1);
                            pt5 = imageBitmapSrc.GetPixel(x2, y2 + 1);
                            pt6 = imageBitmapSrc.GetPixel(x2 + 1, y2 + 1);
                            pt7 = imageBitmapSrc.GetPixel(x2 + 1, y2);

                            if (p0.R == 255 && pt0.R == 0)
                                x2 = x2 + 1;
                            else
                            if (p1.R == 255 && pt1.R == 0)
                            {
                                x2 = x2 + 1;
                                y2 = y2 - 1;
                            }
                            else
                            if (p2.R == 255 && pt2.R == 0)
                            {
                                y2 = y2 - 1;
                            }
                            else
                            if (p3.R == 255 && pt3.R == 0)
                            {
                                x2 = x2 - 1;
                                y2 = y2 - 1;
                            }
                            else
                            if (p4.R == 255 && pt4.R == 0)
                            {
                                x2 = x2 - 1;
                            }
                            else
                            if (p5.R == 255 && pt5.R == 0)
                            {
                                x2 = x2 - 1;
                                y2 = y2 + 1;
                            }
                            else
                            if (p6.R == 255 && pt6.R == 0)
                            {
                                y2 = y2 + 1;
                            }
                            else
                            if (p7.R == 255 && pt7.R == 0)
                            {
                                x2 = x2 + 1;
                                y2 = y2 + 1;
                            }
                        }
                        while (x != x2 || y != y2);
                    }
                }
            }
        }




    }
}
