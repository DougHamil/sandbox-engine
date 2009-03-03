using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;
using FarseerGames.FarseerPhysics;
using FarseerGames.FarseerPhysics.Collisions;
using FarseerGames.FarseerPhysics.Dynamics;
using FarseerGames.FarseerPhysics.Dynamics.Springs;
using FarseerGames.FarseerPhysics.Factories;
using Sandbox_Engine.Entities;
using Sandbox_Engine.GUI;
using Sandbox_Engine.Screens;


namespace Sandbox_Engine
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        //-Input
        InputHelper inputHelper;
        //-Managers
        ScreenManager screenManager;
        //Visuals
        SpriteFont spriteFont;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            //Initialize Inputs
            inputHelper = new InputHelper();
            this.Services.AddService(typeof(InputHelper), inputHelper);
            this.IsMouseVisible = true;
            //Initialize Managers
            screenManager = new ScreenManager(this);
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            //Load UI
            spriteFont = Content.Load<SpriteFont>("Fonts\\gameFont");
            //Register services
            this.Services.AddService(typeof(SpriteBatch), spriteBatch);
            this.Services.AddService(typeof(SpriteFont), spriteFont);
            MenuScreen ms = new MenuScreen(this);
            screenManager.forceAdd(ms);
            
            LogoScreen ls = new LogoScreen(this, Content.Load<Texture2D>("Media\\Menus\\BirdHeadLogo"));
            screenManager.Add(ls);
            screenManager.forceAdd(ls);
            base.LoadContent();

            
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            //Update Inputs
            inputHelper.Update(GamePad.GetState(PlayerIndex.One), Keyboard.GetState(),Mouse.GetState());
        
            //Update Screen Commands
            checkScreenCommands();
            base.Update(gameTime);
        }

        private void checkScreenCommands()
        {
            string c = screenManager.getCommand();
            if (c == "EDITOR")
            {
                EditorScreen editor = new EditorScreen(this);
                screenManager.Add(editor);
            }
            if (c == "POP")
            {
                screenManager.RemoveScreen();
            }
            if (c == "EXITGAME")
            {
                this.Exit();
            }
        }
        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();
            
            base.Draw(gameTime);
            spriteBatch.End();
        }
    }
}
