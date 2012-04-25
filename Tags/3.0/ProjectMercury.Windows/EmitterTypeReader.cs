namespace ProjectMercury
{
    using System.Xml;
    using Microsoft.Xna.Framework.Content;
    using Microsoft.Xna.Framework.Content.Pipeline.Serialization.Intermediate;
    using Emitters;

    public class EmitterTypeReader : ContentTypeReader<Emitter>
    {
        /// <summary>
        /// Reads a strongly typed object from the current stream.
        /// </summary>
        /// <param name="input">The ContentReader used to read the object.</param>
        /// <param name="existingInstance">An existing object to read into.</param>
        /// <returns>The type of object to read.</returns>
        protected override Emitter Read(ContentReader input, Emitter existingInstance)
        {
            using (XmlReader reader = XmlReader.Create(input.BaseStream))
            {
                return IntermediateSerializer.Deserialize<Emitter>(reader, ".\\");
            }
        }
    }
}