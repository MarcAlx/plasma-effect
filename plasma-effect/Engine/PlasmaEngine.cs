using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace plasmaeffect.Engine
{
    public enum ColorRampEnum {
        GRAY_SCALE,
        RAINBOW
    }

    public class PlasmaEngine
    {
        private Dictionary<ColorRampEnum, Color[]> _colorRamp;

        public PlasmaEngine()
        {
            this._colorRamp = new Dictionary<ColorRampEnum, Color[]>();
            this._colorRamp[ColorRampEnum.GRAY_SCALE] = this.GenerateColorRamp(ColorRampEnum.GRAY_SCALE);
            this._colorRamp[ColorRampEnum.RAINBOW] = this.GenerateColorRamp(ColorRampEnum.RAINBOW);
        }

        /// <summary>
        /// Generate a color ramp according to provided enum
        /// </summary>
        /// <param name="ramp"></param>
        /// <returns></returns>
        private Color[] GenerateColorRamp(ColorRampEnum ramp)
        {
            Color[] res = new Color[256];

            if(ramp == ColorRampEnum.GRAY_SCALE)
            {
                for(int i = 0; i<256; i++)
                {
                    res[i] = new Color(i, i, i);
                }
            }
            else if (ramp == ColorRampEnum.RAINBOW)
            {
                for (int i = 0; i < 256; i++)
                {
                    res[i] = Toolkit.FromHsl((float)i/256f,1f,0.5f);
                    var d = res[i];
                }
            }

            return res;
        }

        /// <summary>
        /// Generate a sin pattern at a given position and scale
        /// </summary>
        /// <param name="val"></param>
        /// <param name="scale"></param>
        /// <returns></returns>
        private int GetSinPatternAt(double pos,double scale = 1.0)
        {
            return (int)(128.0 + (128.0 * Math.Sin(pos / scale)));
        }

        /// <summary>
        /// Generate a square pattern by combining X and Y sin and scale
        /// </summary>
        /// <param name="val"></param>
        /// <param name="scale"></param>
        /// <returns></returns>
        private int GetSquarePatternAt(double x, double y, double scale = 1.0)
        {
            return (this.GetSinPatternAt(x, scale) + this.GetSinPatternAt(y,scale))/2 ;
        }

        /// <summary>
        /// Generate a ripple pattern at x and y 
        /// </summary>
        /// <param name="val"></param>
        /// <param name="scale"></param>
        /// <returns></returns>
        private int GetRipplePatternAt(double x, double y, double scale = 1.0)
        {
            return this.GetSinPatternAt(Math.Sqrt(x * x + y * y), scale);
        }

        /// <summary>
        /// Generate a ripple pattern at x and y 
        /// </summary>
        /// <param name="val"></param>
        /// <param name="scale"></param>
        /// <returns></returns>
        private int GetPlasmaPatternAt(double x, double y,double width,double height)
        {
            return this.GetSquarePatternAt(x, y, 16.0)
                 + this.GetRipplePatternAt(x, y, 8.0)
                 + this.GetRipplePatternAt(x - width / 2, y - width / 2, 8.0)
                 + this.GetSinPatternAt(x, 16)
                 + this.GetSinPatternAt(y, 32);
        }

        /// <summary>
        /// Generate plasma effect on a device (as a Texture2D) with given width and height
        /// </summary>
        /// <param name="device"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <returns></returns>
        public Texture2D GeneratePlasma(GraphicsDevice device,int width,int height,ColorRampEnum colorRamp = ColorRampEnum.GRAY_SCALE, int colorRampShift = 0, int ratio = 50)
        {
            Texture2D rect = new Texture2D(device, width, height);

            Color[] data = new Color[width * height];
            /*for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    var p = this.GetPlasmaPatternAt(x, y, width, height);
                    var color = this._colorRamp[colorRamp][(p + colorRampShift) % 256];
                    data[((y) * width) + (x)] = color;
                }
            }*/

            /*for (int i=0; i < data.Length; i++)
            {
                var p = this.GetPlasmaPatternAt(i%width, i/width, width, height);
                var color = this._colorRamp[colorRamp][(p + colorRampShift) % 256];
                data[i] = color;
            }*/

            var stepX = ratio;
            var stepY = ratio;
            for (int x = 0; x < width;x+=stepX)
            {
                for (int y = 0; y < height;y+=stepY)
                {
                    var p = this.GetPlasmaPatternAt(x, y, width, height);
                    var color = this._colorRamp[colorRamp][(p + colorRampShift) % 256];

                    var topA = Math.Min(x + stepX, width);
                    var topB = Math.Min(y + stepY, height);
                    for (int a = x; a < topA; a++)
                    {
                        for (int b = y; b < topB; b++)
                        {
                            data[((b) * width) + (a)] = color;
                        }
                    }
                }
            }

            rect.SetData(data);

            return rect;
        }
    }
}
