using System;
using System.Xml.Serialization;
using System.IO;
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
using Microsoft.Xna.Framework.Storage;

namespace Sandbox_Engine.Entities
{
    public enum GeomType
    {
        Circle,
        Rectangle,
        Ellipse,
        Polygon
    }

    public class StructRigidEntity
    {
        public string textureName;
        public GeomType gType;
        public float friction;
        public float restitution;
        public string name;
    }

    class RigidEntity : Entity
    {
        //Struct for Entity loading/Saving

        //Fields
        //-Physics
        public Body body;
        private Geom geom;
        private GeomType geomType;
        private PhysicsSimulator physicsSim;
        private bool isFrozen;
        //-Visuals
        private Texture2D texture;
        private Rectangle drawRectangle;
        private Vector2 origin;
        private Color color;

        #region Properties
        public GeomType GeomType
        {
            get { return geomType; }
            set { geomType = value; }
        }
        public Vector2 Origin
        {
            get { return origin; }
        }
        public bool IsFrozen
        {
            get { return isFrozen; }
            set { isFrozen = value; }
        }
        public float Friction
        {
            get { return geom.FrictionCoefficient; }
            set { geom.FrictionCoefficient = value; }
        }
        public float Restitution
        {
            get { return geom.RestitutionCoefficient; }
            set { geom.RestitutionCoefficient = value; }
        }
        public Vector2 Position
        {
            get { return body.Position; }
            set { body.Position = value; }
        }
        public bool IsStatic
        {
            get { return body.IsStatic; }
            set { body.IsStatic = value; }
        }
        #endregion

        /*
         * Constructors
         */
        public RigidEntity(string id, Texture2D newTexture,ref PhysicsSimulator ps, GeomType newGeomType)
            : base(id)
        {
            texture = newTexture;
            geomType = newGeomType;
            preparePhysicsEntity(ps);
            physicsSim = ps;
            IsDrawable = true;
        }
        public RigidEntity(ContentManager cm, ref PhysicsSimulator phs, string xmlFileName):base("FILE")
        {
            physicsSim = phs;
            //Stream stream = File.OpenRead(System.IO.Directory.GetCurrentDirectory()+"\\"+xmlFileName);
            //XmlSerializer xml = new XmlSerializer(typeof(StructRigidEntity));
            //StructRigidEntity re = (StructRigidEntity)xml.Deserialize(stream);
            StructRigidEntity re = new StructRigidEntity();
            re = cm.Load<StructRigidEntity>(xmlFileName);
            texture = cm.Load<Texture2D>(re.textureName);
            geomType = re.gType;
            preparePhysicsEntity(phs);
            IsDrawable = true;
            Friction = re.friction;
            Restitution = re.restitution;
            ID = re.name;
            //stream.Close();
        }
        public RigidEntity(string id, ref PhysicsSimulator ps, Vector2 position, int width, int height):base(id)
        {
            //Create an invisible rectangle phys object
            geomType = GeomType.Rectangle;
            body = BodyFactory.Instance.CreateRectangleBody(ps,(float)width, (float)height,1);
            geom = GeomFactory.Instance.CreateRectangleGeom(ps,body, (float)width, (float)height);
            Position = position;
            IsStatic = true;
            IsDrawable = false;
        }
        /*
         * Public Methods
         */
        public override void  Save(string fileName)
        {
            StructRigidEntity sr = new StructRigidEntity();
            sr.friction = Friction;
            sr.restitution = Restitution;
            sr.gType = geomType;
            sr.name = ID;
            sr.textureName = texture.Name;
            Stream s = File.OpenWrite(System.IO.Directory.GetCurrentDirectory() + "\\EntitySaves\\" + sr.name + ".xml");
            XmlSerializer xml = new XmlSerializer(typeof(StructRigidEntity));
            xml.Serialize(s, sr);
            s.Close();
        }
        public bool containsVector2(Vector2 pos)
        {
            return geom.Collide(pos);
        }
        public override StructRigidEntity toStruct()
        {
            StructRigidEntity sr = new StructRigidEntity();
            sr.friction = Friction;
            sr.restitution = Restitution;
            sr.gType = geomType;
            sr.name = ID;
            sr.textureName = texture.Name;
            return sr;
        }
        /*
         * Private Methods
         */
        private void preparePhysicsEntity(PhysicsSimulator ps)
        {
            
            if (geomType == GeomType.Circle)
            {
                body = BodyFactory.Instance.CreateCircleBody(ps,(float)texture.Width / 2, 20.0f);
                geom = GeomFactory.Instance.CreateCircleGeom(ps,body, (float)texture.Width / 2, 50);
                geom.RestitutionCoefficient = 0.5f;
                origin = new Vector2((float)texture.Width / 2, (float)texture.Height / 2);
            }
            if (geomType == GeomType.Rectangle)
            {
                body = BodyFactory.Instance.CreateRectangleBody(ps,(float)texture.Width, (float)texture.Height, 50.0f);
                origin = new Vector2((float)texture.Width / 2, (float)texture.Height / 2);
                geom = GeomFactory.Instance.CreateRectangleGeom(ps,body, (float)texture.Width, (float)texture.Height,20);
            }
            if (geomType == GeomType.Polygon)
            {
                //Create bit array
                uint[] bits = new uint[texture.Width*texture.Height];
                texture.GetData(bits);  //Store texture data
                Vertices verts = Vertices.CreatePolygon(bits,texture.Width,texture.Height); //Create verts from data
                origin = verts.GetCentroid(); //Get origin
                body = BodyFactory.Instance.CreatePolygonBody(ps, verts, 20.0f);
                geom = GeomFactory.Instance.CreatePolygonGeom(ps,body, verts, 0);
            }
        }
        /*
         * Game Methods
         */
        public override void CleanUp()
        {
            geom.Dispose();
            body.Dispose();
            base.CleanUp();
        }
        public override void Update(GameTime gt)
        {
            //Update draw rectangle
            if(IsDrawable)
                drawRectangle = new Rectangle((int)(body.Position.X), (int)body.Position.Y, texture.Width, texture.Height);
            base.Update(gt);
        }
        public override void Draw(SpriteBatch sb)
        {
            base.Draw(sb);

            if (IsDrawable)
            {
                if (body.Enabled)
                {
                    color = Color.White;
                }
                else
                {
                    color = Color.Tomato;
                }

                sb.Draw(texture, body.Position, null, color, body.Rotation, origin, 1.0f, SpriteEffects.None, 0.0f);
            }
                
        }
    }


}
