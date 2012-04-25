namespace ProjectMercury.Editor
{
    using System.Xml;
    using Microsoft.Xna.Framework.Content.Pipeline.Serialization.Intermediate;
    using ProjectMercury.Emitters;

    public static class EmitterSerializer
    {
        static public void Serialize(Emitter emitter, string filePath)
        {
            using (XmlWriter writer = XmlWriter.Create(filePath, new XmlWriterSettings
                {
                    Indent          = true,
                    NewLineHandling = NewLineHandling.Entitize
                }))
            {
                IntermediateSerializer.Serialize<Emitter>(writer, emitter, ".\\");
            }
        }

        static public Emitter Deserialize(string filePath)
        {
            using (XmlReader reader = XmlReader.Create(filePath))
            {
                return IntermediateSerializer.Deserialize<Emitter>(reader, ".\\");
            }
        }
    }
}