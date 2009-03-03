using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Sandbox_Engine.GUI
{
    class GUIManager
    {
        //Fields
        //-General
        private List<GUIObject> objects;

        public GUIManager()
        {
            objects = new List<GUIObject>();
        }
        public void Clear()
        {
            objects.Clear();
        }
        public string getClickedCommand()
        {
            foreach (GUIObject o in objects)
            {
                if (o.IsClicked)
                {
                    return o.Command;
                }
            }
            return "";
        }
        public void addObject(GUIObject o)
        {
            objects.Add(o);
        }

        public void removeObject(GUIObject o)
        {
            objects.Remove(o);
        }
        public void Draw(GameTime gt)
        {
            foreach (GUIObject o in objects)
            {
                o.Draw(gt);
            }
        }
        public void Update(GameTime gt)
        {
            foreach (GUIObject o in objects)
            {
                o.Update(gt);
            }
        }
    }
}
