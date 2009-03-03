using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Sandbox_Engine.GUI
{
    class GUITextButton : GUIObject
    {
        //Fields
        //-Visuals
        private string drawString;
        private Texture2D backTexture;
        private Rectangle outlineRect;

        public GUITextButton(Game game, string ID, string com, string newString, int x, int y):base(game,ID,com)
        {
            drawString = newString;
            backTexture = game.Content.Load<Texture2D>("blank");
            rect = new Rectangle(x-3, y, (int)spriteFont.MeasureString(drawString).X+6, (int)spriteFont.MeasureString(drawString).Y);
            outlineRect = new Rectangle(rect.X - 5, rect.Y - 5, rect.Width + 10, rect.Height + 10);
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Draw(backTexture, outlineRect, Color.Black);
            spriteBatch.Draw(backTexture, rect, Color.White);
            spriteBatch.DrawString(spriteFont, drawString, new Vector2((float)rect.X+3, (float)rect.Y+3), Color.Black);
            base.Draw(gameTime);
        }
    }
}
