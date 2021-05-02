using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace plasmaeffect.Engine
{
    /// <summary>
    /// Contains general purpose methods
    /// </summary>
    public class Toolkit
    {
        public static void DrawFPSAt(Vector2 location, SpriteFont font, SpriteBatch spriteBatch, float fps)
        {
            spriteBatch.DrawString(font, String.Format("{0:00.00} fps",fps), location, Color.Black);
        }
    }
}
