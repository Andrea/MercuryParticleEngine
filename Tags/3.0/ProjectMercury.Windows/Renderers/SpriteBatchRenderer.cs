namespace ProjectMercury.Renderers
{
    using System;
    using System.ComponentModel;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Content;
    using Microsoft.Xna.Framework.Graphics;
    using Emitters;

    /// <summary>
    /// Defines a Renderer which uses the standard XNA SpriteBatch class to render Particles.
    /// </summary>
#if WINDOWS
    [TypeConverter("ProjectMercury.Design.Renderers.SpriteBatchRendererTypeConverter, ProjectMercury.Design")]
#endif
    public sealed class SpriteBatchRenderer : Renderer
    {
        private SpriteBatch Batch;
        public SpriteBlendMode BlendMode;

        /// <summary>
        /// Disposes any unmanaged resources being used by the Renderer.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (this.Batch != null)
                    this.Batch.Dispose();
            }

            base.Dispose(disposing);
        }

        /// <summary>
        /// Loads any content required by the renderer.
        /// </summary>
        /// <exception cref="System.InvalidOperationException">Thrown if the GraphicsDeviceManager has not been set.</exception>
        public override void LoadContent(ContentManager content)
        {
            if (base.GraphicsDeviceService == null)
                throw new InvalidOperationException();

            this.Batch = new SpriteBatch(base.GraphicsDeviceService.GraphicsDevice);
        }

        /// <summary>
        /// Renders the specified Emitter.
        /// </summary>
        public override void RenderEmitter(Emitter emitter)
        {
            Matrix ident = Matrix.Identity;

            this.RenderEmitter(emitter, ref ident);
        }

        /// <summary>
        /// Renders the specified Emitter, applying the specified transformation matrix.
        /// </summary>
        /// <param name="emitter"></param>
        /// <param name="transform"></param>
        public override void RenderEmitter(Emitter emitter, ref Matrix transform)
        {
            if (emitter.ParticleTexture != null && emitter.ActiveParticlesCount > 0)
            {
                // Calculate the source rectangle and origin offset of the Particle texture...
                Rectangle source = new Rectangle(0, 0, emitter.ParticleTexture.Width, emitter.ParticleTexture.Height);
                Vector2 origin = new Vector2(source.Width / 2f, source.Height / 2f);

                this.Batch.Begin(this.BlendMode, SpriteSortMode.Deferred, SaveStateMode.None, transform);

                for (int i = 0; i < emitter.ActiveParticlesCount; i++)
                {
                    Particle particle = emitter.Particles[i];

                    float scale = particle.Scale / emitter.ParticleTexture.Width;

                    Batch.Draw(emitter.ParticleTexture, particle.Position, source, new Color(particle.Colour), particle.Rotation, origin, scale, SpriteEffects.None, 0f);
                }

                this.Batch.End();
            }
        }
    }
}