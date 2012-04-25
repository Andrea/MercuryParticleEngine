using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using ProjectMercury.Emitters;

namespace ProjectMercury.Renderers
{
    /// <summary>
    /// Represents a basic particle effect renderer.
    /// </summary>
    [Serializable]
    public class BasicRenderer : Renderer
    {
        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="game">A reference to the game.</param>
        /// <param name="graphics">A reference to the games graphics device service.</param>
        /// <param name="budget">The number of particles that the renderer will be
        /// able to draw for each emitter. This is necessary to avoid resizing the
        /// vertex buffer at runtime.</param>
        public BasicRenderer(Game game, IGraphicsDeviceService graphics, int budget)
            : base(game, graphics)
        {
            this._budget = budget;
            this._verts = new MercuryVertex[this._budget];
        }

        /// <summary>
        /// Called after the particle effect is imported.
        /// </summary>
        /// <param name="game">A reference to the game object.</param>
        public override void AfterImport(Game game)
        {
            this._verts = new MercuryVertex[this._budget];

            base.AfterImport(game);
        }

        private int _budget;                    //Number of particle available to draw for each emitter.

        [NonSerialized]
        private VertexDeclaration _vDec;        //Vertex declaration.

        [NonSerialized]
        private MercuryVertex[] _verts;         //Array of verts too be drawn.

        [NonSerialized]
        private Effect _pointSpriteEffect;      //Shader effect that will draw the point sprites.

        [NonSerialized]
        private EffectParameter _wvpParam;      //The world * view * projection matrix parameter of the shader.

        [NonSerialized]
        private EffectParameter _textureParam;  //The texture parameter of the shader.

        [NonSerialized]
        private int _drawIndex;                 //The current index of the particle being drawn.

        private bool _shader2;                  //True to use shader model 2.

        /// <summary>
        /// True to enable rotation of point sprites, require shader model 2.
        /// </summary>
        public bool EnableRotatedPointSprites
        {
            get { return this._shader2; }
            set { this._shader2 = value; }
        }

        /// <summary>
        /// Loads content required by the renderer.
        /// </summary>
        public override void LoadContent()
        {
            this._vDec = new VertexDeclaration(base.Graphics.GraphicsDevice, MercuryVertex.VertexElements);
            this._pointSpriteEffect = base.Content.Load<Effect>("PointSprite");
            this._wvpParam = this._pointSpriteEffect.Parameters["WVPMatrix"];
            this._textureParam = this._pointSpriteEffect.Parameters["SpriteTexture"];

            Matrix world = Matrix.Identity;
            Matrix view = new Matrix(
                    1.0f, 0.0f, 0.0f, 0.0f,
                    0.0f, -1.0f, 0.0f, 0.0f,
                    0.0f, 0.0f, -1.0f, 0.0f,
                    0.0f, 0.0f, 0.0f, 1.0f);
            Matrix projection = Matrix.CreateOrthographicOffCenter(0, base.Graphics.GraphicsDevice.Viewport.Width, -base.Graphics.GraphicsDevice.Viewport.Height, 0, 0, 1);

            this._wvpParam.SetValue(world * view * projection);

            base.LoadContent();
        }

        /// <summary>
        /// Prepares the renderer for rendering.
        /// </summary>
        /// <param name="totalTime">Total game time in whole and fractional seconds.</param>
        /// <param name="emitter">The emitter which is about to be rendered.</param>
        protected override void Prepare(float totalTime, Emitter<Particle> emitter)
        {
            this._drawIndex = 0;

            Texture2D texture = (emitter.ParticleTexture != null ? emitter.ParticleTexture : base.DefaultTexture);

            this._textureParam.SetValue(texture);
        }

        /// <summary>
        /// Renders a particle.
        /// </summary>
        /// <param name="totalTime">Total game time in whole and fractional seconds.</param>
        /// <param name="particle">The particle to be rendered.</param>
        protected override void RenderParticle(float totalTime, IParticle particle)
        {
            if (this._drawIndex > this._verts.Length) { throw new InvalidOperationException(); }

            this._verts[this._drawIndex].Position = particle.Position;
            this._verts[this._drawIndex].Color = new Vector4(particle.Color, particle.Opacity);
            this._verts[this._drawIndex].Size = particle.Scale;
            this._verts[this._drawIndex].Rotation = particle.Rotation;

            this._drawIndex++;
        }

        /// <summary>
        /// Finishes rendering an emitter.
        /// </summary>
        /// <param name="totalTime">Total game time in whole and fractional seconds.</param>
        protected override void PostRender(float totalTime)
        {
            if (this._drawIndex > 0)
            {
                this.Graphics.GraphicsDevice.VertexDeclaration = this._vDec;

                this.Graphics.GraphicsDevice.RenderState.AlphaBlendEnable = true;
                this.Graphics.GraphicsDevice.RenderState.SourceBlend = Blend.SourceAlpha;
                this.Graphics.GraphicsDevice.RenderState.DestinationBlend = Blend.One;

                this.Graphics.GraphicsDevice.RenderState.PointSpriteEnable = true;
                this.Graphics.GraphicsDevice.RenderState.DepthBufferWriteEnable = false;

                this._pointSpriteEffect.CurrentTechnique = this._pointSpriteEffect.Techniques[(this._shader2 ? 0 : 1)];

                this._pointSpriteEffect.Begin();

                foreach (EffectPass pass in this._pointSpriteEffect.CurrentTechnique.Passes)
                {
                    pass.Begin();

                    base.Graphics.GraphicsDevice.DrawUserPrimitives<MercuryVertex>(PrimitiveType.PointList, this._verts, 0, this._drawIndex);

                    pass.End();
                }

                this._pointSpriteEffect.End();
            }
        }
    }
}