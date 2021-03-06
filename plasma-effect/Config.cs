using System;
namespace plasmaeffect
{
    public static class Config
    {
        /// <summary>
        /// Window title
        /// </summary>
        public static readonly string WINDOW_TITLE = "Plasma effect";

        /// <summary>
        /// Tells if FPS should be displayed on screen
        /// </summary>
        public static readonly bool DISPLAY_FPS = true;

        /// <summary>
        /// Tells if moving random point should be drawn
        /// </summary>
        public static readonly bool DRAW_MOVINGPOINT = false;

        /// <summary>
        /// FPS cap
        /// </summary>
        public static readonly int FPS_CAP = 60;

        /// <summary>
        /// Pixel ratio
        /// </summary>
        public static readonly int PIXEL_RATIO = 5;
    }
}
