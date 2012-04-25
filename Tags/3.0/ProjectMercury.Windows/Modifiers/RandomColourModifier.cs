namespace ProjectMercury.Modifiers
{
    using System.ComponentModel;
    using Microsoft.Xna.Framework;

#if WINDOWS
    [TypeConverter("ProjectMercury.Design.Modifiers.RandomColourModifierTypeConverter, ProjectMercury.Design")]
#endif
    public class RandomColourModifier : Modifier
    {
        public Vector3 Colour;
        public float Variation;

        public override void Process(float totalSeconds, float elapsedSeconds, ref Particle particle, object tag)
        {
            if (particle.Age <= float.Epsilon)
            {
                particle.Colour = new Vector4
                {
                    W = particle.Colour.W,
                    X = RandomHelper.Variation(this.Colour.X + 1f, this.Variation) - 1f,
                    Y = RandomHelper.Variation(this.Colour.Y + 1f, this.Variation) - 1f,
                    Z = RandomHelper.Variation(this.Colour.Z + 1f, this.Variation) - 1f
                };
            }
        }
    }
}