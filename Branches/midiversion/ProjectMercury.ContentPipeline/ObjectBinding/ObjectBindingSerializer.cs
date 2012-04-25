/*  
 Copyright © 2010 Project Mercury Team Members (http://mpe.codeplex.com/People/ProjectPeople.aspx)

 This program is licensed under the Microsoft Permissive License (Ms-PL).  You should 
 have received a copy of the license along with the source code.  If not, an online copy
 of the license can be found at http://mpe.codeplex.com/license.
*/

using BindingLibrary;

namespace ProjectMercury.ContentPipeline.ObjectBinding
{
    using System;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Content;
    using Microsoft.Xna.Framework.Content.Pipeline.Serialization.Intermediate;
    using ProjectMercury.Emitters;
    using ProjectMercury.Modifiers;

    [ContentTypeSerializer]
    public class ObjectBindingSerializer : ContentTypeSerializer<ObjectBindingRepository>
    {
        protected override void Serialize(IntermediateWriter output, ObjectBindingRepository value, ContentSerializerAttribute format)
        {
            value.WriteXml(output.Xml);

        }

        protected override ObjectBindingRepository Deserialize(IntermediateReader input, ContentSerializerAttribute format, ObjectBindingRepository existingInstance)
        {
            ObjectBindingRepository value = existingInstance ?? new ObjectBindingRepository();

            value.ReadXml(input.Xml);
       
            return value;
        }
    }
}