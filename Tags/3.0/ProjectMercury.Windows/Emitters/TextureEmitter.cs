namespace ProjectMercury.Emitters
{
    using System;
    using System.Collections.Generic;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using ProjectMercury.Modifiers;

    public class TextureEmitter : Emitter
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TextureEmitter"/> class.
        /// </summary>
        public TextureEmitter()
        {
            this.Rotation = 0f;
            this.Scale = 1f;
            this.PixelOffsets = new Vector2[0];
            this.Threshold = 0.5f;
            this.PixelColourModifier = new ColourQueueModifier();
        }

        private Matrix RotationMatrix;

        /// <summary>
        /// Gets or sets the rotation of the texture (in radians).
        /// </summary>
        public float Rotation
        {
            get { return (float)Math.Atan2(this.RotationMatrix.M12, this.RotationMatrix.M11); }
            set { this.RotationMatrix = Matrix.CreateRotationZ(value); }
        }
        
        private Matrix ScaleMatrix;

        /// <summary>
        /// Gets or sets the scale factor of the texture (in screen space).
        /// </summary>
        public float Scale
        {
            get { return this.ScaleMatrix.M11; }
            set { this.ScaleMatrix = Matrix.CreateScale(value); }
        }

        private Vector2 TextureOrigin;
        
        private Vector2[] PixelOffsets;
        
        private Vector4[] PixelColours;
        
        private ColourQueueModifier PixelColourModifier;

        private Texture2D _texture;

        /// <summary>
        /// Gets or sets the texture used to lookup particle release offsets.
        /// </summary>
        /// <value>The texture.</value>
        public Texture2D Texture
        {
            get { return this._texture; }
            set
            {
                if (this.Texture != value)
                {
                    this._texture = value;

                    this.CalculateEmissionPoints();
                }
            }
        }

        private bool _applyPixelColours;

        /// <summary>
        /// Gets or sets a value indicating whether particles should assume the colour of the underlying
        /// pixel in the texture
        /// </summary>
        public bool ApplyPixelColours
        {
            get { return this._applyPixelColours; }
            set
            {
                if (this.ApplyPixelColours != value)
                {
                    this._applyPixelColours = value;

                    if (value == true)
                        this.Modifiers.Add(this.PixelColourModifier);
                    
                    else
                        this.Modifiers.Remove(this.PixelColourModifier);
                }
            }
        }

#if DEBUG
        private float _threshold;

        /// <summary>
        /// Gets or sets the threshold over which pixels will trigger the release of particles.
        /// </summary>
        public float Threshold
        {
            get { return this._threshold; }
            set
            {
                this._threshold = MathHelper.Clamp(value, 0f, 1f);
            }
        }
#else
        public float Threshold;
#endif

        private void CalculateEmissionPoints()
        {
            if (this.Texture == null)
            {
                this.TextureOrigin = Vector2.Zero;
                
                Array.Resize(ref this.PixelOffsets, 0);
                Array.Resize(ref this.PixelColours, 0);
                
                return;
            }

            this.TextureOrigin = new Vector2(this.Texture.Width / 2, this.Texture.Height / 2);

            List<Vector2> offsets = new List<Vector2>();
            List<Vector4> colours = new List<Vector4>();

            Color[] pixels = new Color[this.Texture.Width * this.Texture.Height];

            this.Texture.GetData(pixels);

            int sourceIndex, destIndex = 0;

            byte minOpacity = Convert.ToByte(this.Threshold * 255f);

            for (int x = 0; x < this.Texture.Width; x++)
            {
                for (int y = 0; y < this.Texture.Height; y++)
                {
                    sourceIndex = this.Texture.Width * y + x;

                    if (pixels[sourceIndex].A > minOpacity)
                    {
                        offsets.Add(new Vector2 { X = x - this.TextureOrigin.X, Y = y - this.TextureOrigin.Y });
                        colours.Add(pixels[sourceIndex].ToVector4());

                        destIndex++;
                    }
                }
            }

            this.PixelOffsets = offsets.ToArray();
            this.PixelColours = colours.ToArray();
        }

        protected override void GenerateParticleOffset(float totalSeconds, ref Vector2 triggerPosition, out Vector2 offset)
        {
            Matrix transform;
            Matrix.Multiply(ref this.RotationMatrix, ref this.ScaleMatrix, out transform);

            int index = RandomHelper.NextInt(this.PixelOffsets.Length);

            Vector2.Transform(ref this.PixelOffsets[index], ref transform, out offset);

            if (this.ApplyPixelColours)
                this.PixelColourModifier.PixelColours.Enqueue(this.PixelColours[index]);
        }

        private class ColourQueueModifier : Modifier
        {
            public Queue<Vector4> PixelColours;

            public ColourQueueModifier()
            {
                this.PixelColours = new Queue<Vector4>();
            }

            public override void Process(float totalSeconds, float elapsedSeconds, ref Particle particle, object tag)
            {
                if (particle.Age <= float.Epsilon)
                    particle.Colour = this.PixelColours.Dequeue();
            }
        }
    }
}