using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using FarseerGames.FarseerPhysics;
using FarseerGames.FarseerPhysics.Collisions;
using FarseerGames.FarseerPhysics.Controllers;
using FarseerGames.FarseerPhysics.Dynamics;
using FarseerGames.FarseerPhysics.Factories;
using FarseerGames.FarseerPhysics.Dynamics.Springs;

namespace Sandbox_Engine.Entities
{
    class SpringRope : Entity
    {
        //Fields
        //-Visuals
        private Texture2D texture;
        //-Physics
        private LinearSpring[] springs;
        private float mass = 1;
        private float dampningConstant = 1f;
        private Body[] bodies;
        private Geom[] geoms;
        private int nodeCount = 15;
        private int nodeWidth = 10;
        private int nodeHeight = 10;
        private float springConstant = 5f;
        private float springLength = 30;
        private Vector2 position;
        private int collisionGroup;
        private PhysicsSimulator physicsSim;
        private bool isPinnedOn = false;

        //Constructor
        public SpringRope(string id,ref PhysicsSimulator ps, Texture2D texture, Vector2 newPosition):base(id)
        {
            physicsSim = ps;
            nodeWidth = texture.Width;
            nodeHeight = texture.Height;
            springLength = texture.Height + 1;
            collisionGroup = 0;
            //Create the rope
            bodies = new Body[nodeCount];
            geoms = new Geom[nodeCount];
            springs = new LinearSpring[nodeCount - 1];
            this.texture = texture;
            position = newPosition;
            //Get polygon from texture
            uint[] data = new uint[texture.Width*texture.Height];
            texture.GetData(data);
            Vertices verts = Vertices.CreatePolygon(data,texture.Width,texture.Height);
            //Create bodies
            bodies[0] = BodyFactory.Instance.CreatePolygonBody(ps,verts, mass);
            bodies[0].Position = position;
            for (int i = 1; i < nodeCount; i++)
            {
                bodies[i] = BodyFactory.Instance.CreatePolygonBody(ps, verts, mass);
                bodies[i].Position = bodies[i - 1].Position + new Vector2(0, springLength);
            }
            //Create Geometries
            geoms[0] = GeomFactory.Instance.CreatePolygonGeom(ps,bodies[0],verts,0);
            geoms[0].CollisionGroup = collisionGroup;
            for (int i = 1; i < nodeCount; i++)
            {
                geoms[i] = GeomFactory.Instance.CreateGeom(ps, bodies[i],geoms[0]);
            }
            for (int i = 0; i < springs.Length; i++)
            {
                springs[i] = SpringFactory.Instance.CreateLinearSpring(ps, bodies[i], Vector2.Zero, bodies[i + 1], Vector2.Zero, springConstant, dampningConstant);
            }
        }
        public void pinAt(Vector2 pos)
        {
            //Pin 0th body to point
            JointFactory.Instance.CreateFixedRevoluteJoint(physicsSim, bodies[0], pos);
        }
        public void pinOn(Body b,Vector2 pos)
        {
            JointFactory.Instance.CreateRevoluteJoint(physicsSim, bodies[0], b, pos);
            geoms[0].CollisionResponseEnabled = false;
            isPinnedOn = true;
        }
        public bool containsVector2(Vector2 pos)
        {
            for (int i = 0; i < nodeCount; i++)
            {
                if (geoms[i].Collide(pos))
                {
                    return true;
                }
            }
            return false;
        }
        public override void CleanUp()
        {
            //Delete Bodies
            for (int i = 0; i < nodeCount; i++)
            {
                bodies[i].Dispose();
                geoms[i].Dispose();
            }
            base.CleanUp();
        }
        public override void Draw(SpriteBatch sb)
        {
            if (!isPinnedOn)
            {
                for (int i = 0; i < nodeCount; i++)
                {
                    sb.Draw(texture, bodies[i].Position, null, Color.White, bodies[i].Rotation, new Vector2(texture.Width / 2f, texture.Height / 2f), 1.0f, SpriteEffects.None, 0f);
                }
            }
            else
            {
                for (int i = 1; i < nodeCount; i++)
                {
                    sb.Draw(texture, bodies[i].Position, null, Color.White, bodies[i].Rotation, new Vector2(texture.Width / 2f, texture.Height / 2f), 1.0f, SpriteEffects.None, 0f);
                }
            }
            base.Draw(sb);
        }

    }
}
