using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace Sandbox_Engine.Entities
{
    
    class Entity
    {
        //Fields
        private string id;
        //-Visuals
        private bool isDrawable;
        private bool isActive;

        #region Properties
        //Properties
        public bool IsActive
        {
            get { return isActive; }
            set { isActive = value; }
        }
        public bool IsDrawable
        {
            get { return isDrawable; }
            set { isDrawable = value; }
        }
        public string ID
        {
            get { return id; }
            set { id = value; }
        }
        #endregion
        public virtual StructRigidEntity toStruct()
        {
            return null;
        }
        public virtual void Save(string fileName)
        {
        }
        public Entity(string id)
        {
            this.id = id;
            isDrawable = true;
            isActive = true;
        }
        public virtual void Update(GameTime gt)
        {
        }
        public virtual void CleanUp()
        {
        }
        public virtual void Draw(SpriteBatch sb)
        {
        }
    }
}
