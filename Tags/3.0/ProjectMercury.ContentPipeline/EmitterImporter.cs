namespace ContentPipeline
{
    using System.Xml.Linq;
    using Microsoft.Xna.Framework.Content.Pipeline;

    [ContentImporter(".em", DisplayName = "Emitter Importer")]
    public class EmitterImporter : ContentImporter<XDocument>
    {
        /// <summary>
        /// Called by the framework when importing a game asset.
        /// </summary>
        /// <param name="filename">Name of a game asset file.</param>
        /// <param name="context">Contains information for importing a game asset, such as a logger interface.</param>
        /// <returns>Resulting game asset.</returns>
        public override XDocument Import(string filename, ContentImporterContext context)
        {
            return XDocument.Load(filename);
        }
    }
}
