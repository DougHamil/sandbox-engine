using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.Xml;
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
using Sandbox_Engine.GUI;
using Microsoft.Xna.Framework.Content.Pipeline.Serialization.Intermediate;

namespace Sandbox_Engine.Screens
{
    class EditorScreen : Screen
    {
        //Fields
        //-Containers
        private EntityManager entityManager;
        private PhysicsSimulator physicsSim;
        private PhysicsSimulatorView simView;
        private GUIManager guiManager;
        //-Settings
        private bool isSimView = false;

        public EditorScreen(Game game):base(game)
        {
            physicsSim = new PhysicsSimulator(new Vector2(0f, 1.0f));
            entityManager = new EntityManager();
            guiManager = new GUIManager();
            simView = new PhysicsSimulatorView(physicsSim);
            simView.LoadContent(Game.GraphicsDevice, Game.Content);
            //Build UI
            GUITextButton exitBtn = new GUITextButton(game, "BTN_EXIT", "POP", "Exit", 10, 10);
            guiManager.addObject(exitBtn);
        }
        /*
         * Private Methods
         */
        private void removeEntityAt(Vector2 position)
        {
            Entity e = entityManager.getEntityAt(position);
            if (e != null)
            {
                entityManager.Remove(e);
            }
        }
        /*
         * Game Methods
         */
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
        public override void Update(GameTime gameTime)
        {
            //Update Input
            if (InputHelper.getLeftMouseClicked())
            {
                RigidEntity re = new RigidEntity("GEAR3", Game.Content.Load<Texture2D>("Media\\Cog"), ref physicsSim, GeomType.Circle);
                re.Position = InputHelper.MouseVector;
                re.IsStatic = true;
                entityManager.Add(re);
            }
            if (InputHelper.getRightMouseClicked())
            {
                StructRigidEntity sr = entityManager.getEntityAt(InputHelper.MouseVector).toStruct();
                XmlWriterSettings xmlSettings = new XmlWriterSettings();
                xmlSettings.Indent = true;
                using (XmlWriter xmlWriter = XmlWriter.Create("test.xml", xmlSettings))
                {
                    IntermediateSerializer.Serialize(xmlWriter, sr, null);
                }
                
            }
            //Update Physics
            physicsSim.Update(gameTime.ElapsedGameTime.Milliseconds * 0.01f);
            if (guiManager.getClickedCommand() == "POP")
            {
                this.command = "POP";
            }
            guiManager.Update(gameTime);
            base.Update(gameTime);
        }
        public override void Draw(GameTime gameTime)
        {
            entityManager.Draw(spriteBatch);
            if (isSimView)
            {
                simView.Draw(spriteBatch);
            }
            guiManager.Draw(gameTime);
            base.Draw(gameTime);
        }
    }
}
