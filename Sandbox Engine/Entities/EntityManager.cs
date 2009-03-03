using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Sandbox_Engine.Entities
{
    class EntityManager
    {
        //Fields
        //-Container
        private List<Entity> entities;
        public EntityManager()
        {
            entities = new List<Entity>();
        }
        /*
         * Update Methods
         */
        public void Add(Entity e)
        {
            entities.Add(e);
        }
        public void Remove(Entity e)
        {
            e.CleanUp();
            entities.Remove(e);
        }
        public RigidEntity getRigidEntityAt(Vector2 pos)
        {
            foreach (Entity e in entities)
            {
                if (e is RigidEntity)
                {
                    RigidEntity pe = (RigidEntity)e;
                    if (pe.containsVector2(pos))
                    {
                        return pe;
                    }
                }
            }
            return null;
        }
        public Entity getEntityAt(Vector2 pos)
        {
            foreach (Entity e in entities)
            {
                if (e is RigidEntity)
                {
                    RigidEntity pe = (RigidEntity)e;
                    if (pe.containsVector2(pos))
                        return e;
                }
                else if (e is SpringRope)
                {
                    SpringRope sr = (SpringRope)e;
                    if (sr.containsVector2(pos))
                    {
                        return e;
                    }
                }
            }
            return null;
        }
        public SpringRope getSpringRopeAt(Vector2 pos)
        {
            foreach (Entity e in entities)
            {
                if (e is SpringRope)
                {
                    SpringRope sr = (SpringRope)e;
                    if (sr.containsVector2(pos))
                    {
                        return sr;
                    }
                }
            }
            return null;
        }
        /*
         * Game Methods
         */
        public void Update(GameTime gt)
        {
            foreach (Entity e in entities)
            {
                e.Update(gt);
            }
        }
        public void Draw(SpriteBatch sb)
        {
            foreach (Entity e in entities)
            {
                e.Draw(sb);
            }
        }
    }
}
