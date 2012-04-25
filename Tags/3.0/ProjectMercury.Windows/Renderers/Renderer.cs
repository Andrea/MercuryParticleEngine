namespace ProjectMercury.Renderers
{
    using System;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Content;
    using Microsoft.Xna.Framework.Graphics;
    using Emitters;

    /// <summary>
    /// Defines the abstract base class for a Renderer.
    /// </summary>
    public abstract class Renderer : IDisposable
    {
        public IGraphicsDeviceService GraphicsDeviceService;

        protected virtual void Dispose(bool disposing) { }

        /// <summary>
        /// Disposes any unmanaged resources being used by this instance.
        /// </summary>
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~Renderer()
        {
            this.Dispose(false);
        }

        /// <summary>
        /// Loads any content needed by the Renderer.
        /// </summary>
        public virtual void LoadContent(ContentManager content) { }

        /// <summary>
        /// Renders the specified Emitter.
        /// </summary>
        public abstract void RenderEmitter(Emitter emitter);

        /// <summary>
        /// Renders the specified Emitter, applying the specified transformation matrix.
        /// </summary>
        public abstract void RenderEmitter(Emitter emitter, ref Matrix transform);
    }
}