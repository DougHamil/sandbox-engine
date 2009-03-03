using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Sandbox_Engine.GUI
{
    class GUIButton : GUIObject
    {
        //Fields
        //-Visuals
        private Texture2D texture;

        public GUIButton(Game game, string ID, string com, Texture2D newTexture, Rectangle newRect):base(game,ID,com)
        {
            texture = newTexture;
            rect = newRect;
        }

        public override void  Draw(GameTime gameTime)
        {
            spriteBatch.Draw(texture, rect, Color.White);
            base.Draw(gameTime);
        }
    }
}
