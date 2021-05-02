using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace plasmaeffect.Engine
{
    public class PlasmaEngine
    {
        public PlasmaEngine()
        {
        }

        public static Texture2D GeneratePlasma(GraphicsDevice device,int width,int height)
        {
            Texture2D rect = new Texture2D(device, width, height);

            Color[] data = new Color[width * height];
            for (int i = 0; i < data.Length; ++i)
            {
                data[i] = Color.Chocolate;
            }
            rect.SetData(data);

            return rect;
        }
    }
}
