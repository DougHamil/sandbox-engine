using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
using FarseerGames.FarseerPhysics.Controllers;
using FarseerGames.FarseerPhysics.Dynamics;
using FarseerGames.FarseerPhysics.Factories;
using Sandbox_Engine.Entities;

namespace Sandbox_Engine.Screens
{
    class MenuScreen : Screen
    {
        //Fields
        //-Containers
        private EntityManager entityManager;
        private PhysicsSimulator physicsSim;
        private bool isChanging;
        private string tempCommand;
        private Texture2D shadeTexture;
        private Rectangle screenRect;
        private int deltaTime;
        private int shadeOpacity;

        //Constructor
        public MenuScreen(Game game):base(game)
        {
            screenRect = new Rectangle(0, 0, 800, 600);
            shadeTexture = Game.Content.Load<Texture2D>("Media\\GUI\\Shade");
            physicsSim = new PhysicsSimulator(new Vector2(0.0f, 2.2f));
            //Load Menu screen content
            RigidEntity menuBottom = new RigidEntity("MenuBottom", Game.Content.Load<Texture2D>("Media\\Menus\\MainMenuBottom"), ref physicsSim, GeomType.Polygon);
            menuBottom.body.IsStatic = true;
            menuBottom.body.Position = new Vector2(menuBottom.Origin.X, 200+menuBottom.Origin.Y);
            RigidEntity EditorButton = new RigidEntity("BTN_Editor", Game.Content.Load<Texture2D>("Media\\Menus\\EditorButton"), ref physicsSim, GeomType.Circle);
            RigidEntity quitButton = new RigidEntity("BTN_Quit", Game.Content.Load<Texture2D>("Media\\Menus\\QuitButton"), ref physicsSim, GeomType.Circle);
            RigidEntity w1 = new RigidEntity("WALL1", ref physicsSim, new Vector2(-2.5f,300f), 5, 600);
            RigidEntity w2 = new RigidEntity("WALL2", ref physicsSim, new Vector2(802.5f, 300f), 5, 600);
            EditorButton.body.Position = new Vector2(200.0f, -220.0f);
            EditorButton.Friction = 1.0f;
            EditorButton.Restitution = 0.8f;
            menuBottom.Restitution = 0.3f;
            menuBottom.Friction = 0.8f;
            quitButton.body.Position = new Vector2(400.0f,-250.0f);
            quitButton.Friction = 1.0f;
            quitButton.Restitution = 0.4f;
            entityManager = new EntityManager();
            entityManager.Add(menuBottom);
            entityManager.Add(EditorButton);
            entityManager.Add(quitButton);
            entityManager.Add(w1);
            entityManager.Add(w2);
            shadeOpacity = 0;
        }

        public override void Update(GameTime gameTime)
        {
            //Clear command
            command = "";
            if (InputHelper.getLeftMouseClicked())
            {
                RigidEntity pe = entityManager.getRigidEntityAt(InputHelper.MouseVector);
                if (pe != null)
                {
                    if (pe.ID == "BTN_Quit")
                    {
                        tempCommand = "EXITGAME";
                        isChanging = true;
                    }
                    else if (pe.ID == "BTN_Editor")
                    {
                       tempCommand = "EDITOR";
                       isChanging = true;
                        
                    }
                   
                    deltaTime = 0;
                }
            }
            //Update Transition
            if (isChanging)
            {
                deltaTime += gameTime.ElapsedGameTime.Milliseconds;
                if (deltaTime >= 2)
                {
                    shadeOpacity += 15;
                   
                    if (shadeOpacity >= 255)
                    {
                        shadeOpacity = 255;
                        isChanging = false;
                        command = tempCommand;
                    }
                    deltaTime = 0;
                }
            }
            //Update Manager
            entityManager.Update(gameTime);
            //Update Physics
            physicsSim.Update(gameTime.ElapsedGameTime.Milliseconds * 0.01f);
            base.Update(gameTime);
        }
        public override void Draw(GameTime gameTime)
        {
            entityManager.Draw(spriteBatch);
            base.Draw(gameTime);
        }

    }
}
