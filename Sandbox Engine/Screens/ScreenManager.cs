using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace Sandbox_Engine.Screens
{
    class ScreenManager : GameComponent
    {
        //Fields
        //-Containers
        private Stack<Screen> screens;
        private Screen screenToAdd;

        public Stack<Screen> Screens
        {
            get { return screens; }
        }
        //Constructor
        public ScreenManager(Game game):base(game)
        {
            game.Components.Add(this);
            screens = new Stack<Screen>();
        }
        /*
         * Public Methods
         */
        public void Add(Screen s)
        {
            if (screens.Count > 0)
            {
                s.Hide();
                screens.Peek().clearCommand(); //Clear command of current screen
                screens.Peek().transitionOut(); //transition out current screen
                screens.Peek().killFlag = false; //Don't kill
                screenToAdd = s; //Store screen temporarily
            }
            else
            {
                s.transitionIn(); //Transition
                s.Show();
                screens.Push(s); //Add to stack
            }
        }
        public void forceAdd(Screen s)
        {
            if (screens.Count > 0)
            {
                screens.Peek().clearCommand();
                screens.Peek().Hide(); //Hide current screen
            }
            s.transitionIn();  //Invoke transition
            s.Show();
            screens.Push(s);
            screenToAdd = null;
        }
        public void RemoveScreen()
        {
            screens.Peek().transitionOut();
            screens.Peek().clearCommand();
        }
        /*
         * Private Methods
         */
        public void Pop()
        {
            Screen s = screens.Pop();
            s.Hide();
            s.Dispose(); //Delete screen
            if (screens.Count > 0)
            {
                screens.Peek().transitionIn();
                screens.Peek().Show();
            }
        }
        /*
         * Public Methods
         */
        public string getCommand()
        {
            if (screens.Count > 0)
            {
                return screens.Peek().Command;
            }
            return "";
        }
        /*
         * Game Methods
         */
        public override void Update(GameTime gameTime)
        {
            //Check for transition over
            if (screens.Peek().TransState == TransitionState.None && screens.Peek().killFlag)
            {
                //remove screen
                Pop();
            }
            if (screens.Peek().TransState == TransitionState.None && screenToAdd != null)
            {
                //Add the new screen
                forceAdd(screenToAdd);
            }

            base.Update(gameTime);
        }

    }
}
