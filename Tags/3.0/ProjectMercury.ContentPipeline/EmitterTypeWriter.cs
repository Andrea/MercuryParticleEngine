namespace ContentPipeline
{
    using System;
    using System.Text;
    using System.Xml;
    using System.Xml.Linq;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Content.Pipeline.Serialization.Compiler;

    [ContentTypeWriter]
    public class EmitterTypeWriter : ContentTypeWriter<XDocument>
    {
        /// <summary>
        /// Compiles a strongly typed object into binary format.
        /// </summary>
        /// <param name="output">The content writer serializing the value.</param>
        /// <param name="value">The value to write.</param>
        protected override void Write(ContentWriter output, XDocument value)
        {
            using (XmlWriter writer = XmlWriter.Create(output.BaseStream, new XmlWriterSettings
            {
                Encoding        = Encoding.UTF8,
                Indent          = true,
                NewLineHandling = NewLineHandling.Entitize
            }))
            {
                if (writer != null)
                {
                    value.WriteTo(writer);

                    writer.Flush();
                    writer.Close();
                }
                else
                {
                    throw new Exception("Could not write to XML stream.");
                }
            }
        }

        /// <summary>
        /// Gets the assembly qualified name of the runtime loader for this type.
        /// </summary>
        /// <param name="targetPlatform">Name of the platform.</param>
        /// <returns>Name of the runtime loader.</returns>
        public override string GetRuntimeReader(TargetPlatform targetPlatform)
        {
            switch (targetPlatform)
            {
                case TargetPlatform.Windows:
                    return "ProjectMercury.EmitterTypeReader, ProjectMercury.Windows";
                
                case TargetPlatform.Xbox360:
                    return "ProjectMercury.EmitterTypeReader, ProjectMercury.Xbox";
            }

            throw new Exception("targetPlatform not supported");
        }
    }
}
