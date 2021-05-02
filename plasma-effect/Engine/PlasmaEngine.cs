using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace plasmaeffect.Engine
{
    public enum ColorRampEnum {
        GRAY_SCALE
    }

    public class PlasmaEngine
    {


        private Dictionary<ColorRampEnum, Color[]> _colorRamp;

        public PlasmaEngine()
        {
            this._colorRamp = new Dictionary<ColorRampEnum, Color[]>();
            this._colorRamp[ColorRampEnum.GRAY_SCALE] = this.GenerateColorRamp(ColorRampEnum.GRAY_SCALE);
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

            return res;
        }

        /// <summary>
        /// Generate a sin pattern at a given value and scale
        /// </summary>
        /// <param name="val"></param>
        /// <param name="scale"></param>
        /// <returns></returns>
        private int GetSinPatternAt(double val,double scale = 1.0)
        {
            return (int)Math.Floor(128.0 + (128.0 * Math.Sin(val / scale)));
        }

        /// <summary>
        /// Generate plasma effect on a device (as a Texture2D) with given width and height
        /// </summary>
        /// <param name="device"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <returns></returns>
        public Texture2D GeneratePlasma(GraphicsDevice device,int width,int height,ColorRampEnum colorRamp = ColorRampEnum.GRAY_SCALE)
        {
            Texture2D rect = new Texture2D(device, width, height);

            Color[] data = new Color[width * height];
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    data[(y*width)+x] = this._colorRamp[ColorRampEnum.GRAY_SCALE][this.GetSinPatternAt(y)];
                }
            }
            rect.SetData(data);

            return rect;
        }
    }
}
