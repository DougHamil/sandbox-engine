using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Sandbox_Engine.Screens
{
    public enum TransitionState
    {
        In,
        Out,
        None
    }
    class Screen : DrawableGameComponent
    {
        //Fields
        //-Settings
        protected string command;
        //-Batches
        protected SpriteBatch spriteBatch;
        protected SpriteFont spriteFont;
        private InputHelper inputHelper;
        //-States
        private TransitionState transState;
        private TransitionState oldTransState;
        //-Visuals
        private Texture2D shadeTexture;
        private int fadeLevel;
        private int fadeSpeed = 15;
        public bool killFlag = false;

        #region Properties
        public TransitionState TransState
        {
            get { return transState; }
        }
        public InputHelper InputHelper
        {
            get { return inputHelper; }
        }
        public string Command
        {
            get { return command; }
        }
        #endregion

        public void Show()
        {
            this.Visible = true;
            this.Enabled = true;
        }

        public void Hide()
        {
            this.Enabled = false;
            this.Visible = false;
        }
        public void Disable()
        {
            this.Enabled = false;
        }
        public Screen(Game game):base(game)
        {
            game.Components.Add(this);
            this.Enabled = false;
            this.Visible = false;
            killFlag = false;
            transState = TransitionState.None;
            spriteBatch = (SpriteBatch)game.Services.GetService(typeof(SpriteBatch));
            spriteFont = (SpriteFont)game.Services.GetService(typeof(SpriteFont));
            inputHelper = (InputHelper)game.Services.GetService(typeof(InputHelper));
            shadeTexture = Game.Content.Load<Texture2D>("Media\\GUI\\Shade");
            fadeLevel = 0;
        }

        public void clearCommand()
        {
            command = "";
        }
        public void transitionIn()
        {
            transState = TransitionState.In;
            fadeLevel = 255;
            killFlag = false;
        }
        public void transitionOut()
        {
            transState = TransitionState.Out;
            fadeLevel = 0;
            killFlag = true;
        }
        private void UpdateTransition()
        {
            //Update Transition
            oldTransState = transState;
            if (transState == TransitionState.In)
            {
                fadeLevel -= fadeSpeed;
                if (fadeLevel <= 0)
                {
                    fadeLevel = 0;
                    transState = TransitionState.None;
                }
            }
            else if (transState == TransitionState.Out)
            {
                fadeLevel += fadeSpeed;
                if (fadeLevel >= 255)
                {
                    fadeLevel = 255;
                    transState = TransitionState.None;
                }
            }
        }
        public override void Update(GameTime gameTime)
        {
            UpdateTransition();
            base.Update(gameTime);
        }
        public override void Draw(GameTime gameTime)
        {
            //Draw Transition Fade
            if ((transState != TransitionState.None) || (oldTransState == TransitionState.Out && transState == TransitionState.None))
            {
                spriteBatch.Draw(shadeTexture, new Rectangle(0, 0, Game.GraphicsDevice.DisplayMode.Width, Game.GraphicsDevice.DisplayMode.Height), new Color(Color.Black, (byte)fadeLevel));
            }
            base.Draw(gameTime);
        }
    }
}
