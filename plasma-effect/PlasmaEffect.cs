﻿using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using plasmaeffect;
using plasmaeffect.Engine;

namespace plasma_effect
{
    public class PlasmaEffect : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private SpriteFont _defaultFont;
        private Texture2D _plasma;
        private PlasmaEngine _engine;

        public PlasmaEffect()
        {
            this.Window.AllowUserResizing = true;
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            this.TargetElapsedTime = TimeSpan.FromSeconds(1d / Config.FPS_CAP);
        }

        protected override void Initialize()
        {
            this.Window.Title = Config.WINDOW_TITLE;

            this._engine = new PlasmaEngine();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _defaultFont = Content.Load<SpriteFont>("Default");
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            //update plasma
            var shift = gameTime.TotalGameTime.Milliseconds % 256;
            this._plasma = this._engine.GeneratePlasma(
                GraphicsDevice,
                GraphicsDevice.Viewport.Bounds.Width,
                GraphicsDevice.Viewport.Bounds.Height,
                ColorRampEnum.RAINBOW,
                shift,
                Config.PIXEL_RATIO
            );

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            this._spriteBatch.Begin();

            //draw plasma
            this._spriteBatch.Draw(this._plasma, new Vector2(0, 0), Color.White);


            if (Config.DISPLAY_FPS)
            {
                Toolkit.DrawFPSAt(new Vector2(5, 5), _defaultFont, _spriteBatch, 1 / (float)gameTime.ElapsedGameTime.TotalSeconds);
            }

            this._spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
