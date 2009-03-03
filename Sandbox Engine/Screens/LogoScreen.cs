using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace Sandbox_Engine.Screens
{
    class LogoScreen : Screen
    {
        //Fields
        //-Visuals
        private Texture2D logoTexture;
        private int showLength = 500; //Milliseconds to show logo
        private int timer;
        private bool done;

        public LogoScreen(Game game, Texture2D texture)
            : base(game)
        {
            logoTexture = texture; //Load texture
            timer = 0;
            done = false;
        }
        public override void Update(GameTime gameTime)
        {
            timer += gameTime.ElapsedGameTime.Milliseconds;
            if (timer >= showLength && !done)
            {
                done = true;
                command = "POP";
            }
            base.Update(gameTime);
        }
        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Draw(logoTexture, new Rectangle(0, 0, Game.GraphicsDevice.Viewport.Width, Game.GraphicsDevice.Viewport.Height), Color.White);
            base.Draw(gameTime);
        }
    }
}
