using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content.Pipeline;

namespace SEProcessorLib
{
    class RigidEntityContent
    {
        //Fields
        int geomType;
        string textureName;
        float friction;
        float restitution;
        string name;

        public RigidEntityContent(string name, string textureName, float friction, float restitution, int geomType)
        {
            this.name = name;
            this.textureName = textureName;
            this.friction = friction;
            this.restitution = restitution;
            this.geomType = geomType;
        }
    }
}
