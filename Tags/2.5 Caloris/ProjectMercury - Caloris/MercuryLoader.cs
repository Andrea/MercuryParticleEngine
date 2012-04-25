using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Microsoft.Xna.Framework;

namespace ProjectMercury
{
    /// <summary>
    /// Provides static methods which allow loading and saving of particle effects.
    /// </summary>
    static public class MercuryLoader
    {
        /// <summary>
        /// Saves a particle effect to disk.
        /// </summary>
        /// <param name="effect">The particle effect to be saved.</param>
        /// <param name="filename">The name of the file to save to.</param>
        static public void Export(ParticleEffect effect, string filename)
        {
            Stream output = File.OpenWrite(filename);

            BinaryFormatter formatter = new BinaryFormatter();

            formatter.Serialize(output, effect);

            output.Close();
        }

        /// <summary>
        /// Loads a particle effect from disk.
        /// </summary>
        /// <param name="game">A reference to the game object.</param>
        /// <param name="filename">The name of the file to load from.</param>
        /// <returns>The loaded particle effect.</returns>
        static public ParticleEffect Import(Game game, string filename)
        {
            GameReference = game;

            Stream input = File.OpenRead(filename);

            BinaryFormatter formatter = new BinaryFormatter();

            ParticleEffect fx = formatter.Deserialize(input) as ParticleEffect;

            input.Close();

            fx.AfterImport(game);

            return fx;
        }

        /// <summary>
        /// Holds a static reference to the game object. This is a workaround of an XNA game component issue.
        /// </summary>
        internal static Game _gameReference;

        /// <summary>
        /// Gets or sets the static reference to the game object. This is a workaround of an XNA game component issue.
        /// </summary>
        internal static Game GameReference
        {
            get { return _gameReference; }
            private set { _gameReference = value; }
        }
    }
}