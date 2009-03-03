using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;
using System.Xml;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content.Pipeline;
using Microsoft.Xna.Framework.Content.Pipeline.Graphics;

// TODO: replace this with the type you want to import.
using TImport = System.String;

namespace SEProcessorLib
{
    /// <summary>
    /// This class will be instantiated by the XNA Framework Content Pipeline
    /// to import a file from disk into the specified type, TImport.
    /// 
    /// This should be part of a Content Pipeline Extension Library project.
    /// 
    /// TODO: change the ContentImporter attribute to specify the correct file
    /// extension, display name, and default processor for this importer.
    /// </summary>
    [ContentImporter(".ret", DisplayName = "Rigid Entity Importer", DefaultProcessor = "RigidEntityProcessor")]
    public class RigidEntityImporter : ContentImporter<RigidEntityContent>
    {
        public override RigidEntityContent Import(string filename, ContentImporterContext context)
        {
            XmlSerializer xml = new XmlSerializer(typeof(RigidEntityContent));
            Stream stream = File.OpenRead(filename);
            RigidEntityContent re = (RigidEntityContent)xml.Deserialize(stream);
            // TODO: read the specified file into an instance of the imported type.
            return re;
        }
    }
}
