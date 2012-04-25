using System.IO;

namespace ProjectMercury.Renderers
{
    using System;
    using System.ComponentModel;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Content;
    using Microsoft.Xna.Framework.Graphics;
    using Emitters;

    /// <summary>
    /// Defines a Renderer which uses hardware point sprites to render Particles.
    /// </summary>
#if WINDOWS
    [TypeConverter("ProjectMercury.Design.Renderers.PointSpriteRendererTypeConverter, ProjectMercury.Design")]
#endif
    public sealed class PointSpriteRenderer : Renderer
    {
        private VertexDeclaration VertexDeclaration;
        private Effect PointSpriteEffect;
        private EffectParameter TextureParameter;
        private EffectParameter WVPParameter;
        private Matrix ScreenSpaceMatrix;

        /// <summary>
        /// The blending mode used when rendering Particles.
        /// </summary>
        public SpriteBlendMode BlendMode;

        /// <summary>
        /// Disposes any unmanaged resources being used by this instance.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (this.VertexDeclaration != null)
                    this.VertexDeclaration.Dispose();

                if (this.PointSpriteEffect != null)
                    this.PointSpriteEffect.Dispose();
            }

            base.Dispose(disposing);
        }

        /// <summary>
        /// Loads any content that is needed by the renderer.
        /// </summary>
        /// <exception cref="System.InvalidOperationException">Thrown if the GraphicsDeviceService has not been set.</exception>
        public override void LoadContent(ContentManager content)
        {
#if DEBUG
            if (base.GraphicsDeviceService == null)
                throw new InvalidOperationException();
#endif
            this.VertexDeclaration = new VertexDeclaration(base.GraphicsDeviceService.GraphicsDevice, Particle.VertexElements);

            if (content.RootDirectory.EndsWith("\\Content"))
                this.PointSpriteEffect = content.Load<Effect>("PointSprite");
            else
                this.PointSpriteEffect = content.Load<Effect>("Content\\PointSprite");

            this.TextureParameter = this.PointSpriteEffect.Parameters["SpriteTexture"];
            this.WVPParameter = this.PointSpriteEffect.Parameters["WVPMatrix"];

            // Create a WVP matrix for the shader...
            Matrix world = Matrix.Identity;

            Matrix view = new Matrix(
                    1.0f, 0.0f, 0.0f, 0.0f,
                    0.0f, -1.0f, 0.0f, 0.0f,
                    0.0f, 0.0f, -1.0f, 0.0f,
                    0.0f, 0.0f, 0.0f, 1.0f);

            Matrix projection = Matrix.CreateOrthographicOffCenter(0, base.GraphicsDeviceService.GraphicsDevice.Viewport.Width, -this.GraphicsDeviceService.GraphicsDevice.Viewport.Height, 0, 0, 1);

            this.ScreenSpaceMatrix = world * view * projection;
        }

        /// <summary>
        /// Renders the specified Emitter.
        /// </summary>
        /// <exception cref="System.InvalidOperationException">Thrown if the GraphicsDeviceService has not been set.</exception>
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
#if DEBUG
            if (base.GraphicsDeviceService == null)
                throw new InvalidOperationException();
#endif
            if (emitter.ParticleTexture != null && emitter.ActiveParticlesCount > 0)
            {
                Matrix tranformationMatrix;
                Matrix.Multiply(ref this.ScreenSpaceMatrix, ref transform, out tranformationMatrix);

                this.WVPParameter.SetValue(tranformationMatrix);

                // Use the Emitter Particle texture...
                this.TextureParameter.SetValue(emitter.ParticleTexture);

                // Set graphics device properties for rendering...
                GraphicsDevice device = base.GraphicsDeviceService.GraphicsDevice;
                device.VertexDeclaration = this.VertexDeclaration;
                device.RenderState.PointSpriteEnable = true;
                device.RenderState.AlphaBlendEnable = true;
                device.RenderState.DepthBufferWriteEnable = false;

                switch (this.BlendMode)
                {
                    case SpriteBlendMode.None:
                        {
                            // I suppose someone might have a use for this...
                            return;
                        }
                    case SpriteBlendMode.Additive:
                        {
                            device.RenderState.SourceBlend = Blend.SourceAlpha;
                            device.RenderState.DestinationBlend = Blend.One;

                            break;
                        }
                    case SpriteBlendMode.AlphaBlend:
                        {
                            device.RenderState.SourceBlend = Blend.SourceAlpha;
                            device.RenderState.DestinationBlend = Blend.InverseSourceAlpha;

                            break;
                        }
                }

                this.PointSpriteEffect.Begin();
                this.PointSpriteEffect.Techniques[0].Passes[0].Begin();
#if XBOX
                // On the XBox we have to split the particle array into more manageable chunks.
                if (emitter.ActiveParticlesCount > 9000)
                {
                    for (int i = 0; i < emitter.ActiveParticlesCount; i += 9000)
                    {
                        int remaining = (emitter.ActiveParticlesCount - i < 9000 ? emitter.ActiveParticlesCount - i : 9000);

                        device.DrawUserPrimitives<Particle>(PrimitiveType.PointList, emitter.Particles, i, remaining);
                    }
                }
                else
                {
                    device.DrawUserPrimitives<Particle>(PrimitiveType.PointList, emitter.Particles, 0, emitter.ActiveParticlesCount);
                }
#elif WINDOWS
                device.DrawUserPrimitives<Particle>(PrimitiveType.PointList, emitter.Particles, 0, emitter.ActiveParticlesCount);
#endif
                this.PointSpriteEffect.Techniques[0].Passes[0].End();
                this.PointSpriteEffect.End();
            }
        }
    }
}