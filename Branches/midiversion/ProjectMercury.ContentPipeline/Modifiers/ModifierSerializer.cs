/*  
 Copyright © 2010 Project Mercury Team Members (http://mpe.codeplex.com/People/ProjectPeople.aspx)

 This program is licensed under the Microsoft Permissive License (Ms-PL).  You should 
 have received a copy of the license along with the source code.  If not, an online copy
 of the license can be found at http://mpe.codeplex.com/license.
*/

using System.Xml;
using ProjectMercury.Modifiers;

namespace ProjectMercury.ContentPipeline.ModifiersEmitters
{
    using Microsoft.Xna.Framework.Content;
    using Microsoft.Xna.Framework.Content.Pipeline.Serialization.Intermediate;
    using ProjectMercury.Emitters;
    using System;

    [ContentTypeSerializer]
    public class ModifierSerializer : ContentTypeSerializer<Modifier>
    {
        protected override void Serialize(IntermediateWriter output, Modifier value, ContentSerializerAttribute format)
        {
            if (value == null) return;

            output.Xml.WriteElementString("BindingId", value.BindingId);
        }

        protected override Modifier Deserialize(IntermediateReader input, ContentSerializerAttribute format, Modifier value)
        {
  

            if (value == null) return null;


            if (input.Xml.MoveToContent() == XmlNodeType.Element && input.Xml.LocalName == "BindingId")
            {
                value.BindingId = input.Xml.ReadElementString("BindingId");
            }
            

            return value;
        }
    }
}