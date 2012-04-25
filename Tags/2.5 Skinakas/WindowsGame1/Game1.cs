using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using ProjectMercury;
using ProjectMercury.Controllers;
using ProjectMercury.Emitters;
using ProjectMercury.Modifiers;
using ProjectMercury.Renderers;

namespace WindowsGame1
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager graphics;
        private ContentManager content;

        private ParticleEffect effect;
        private TestController controller;
        private Emitter<Particle> emitter;
        private BasicRenderer renderer;
        private ColorModifier color;
        private OpacityModifier opacity;
        private ScaleModifier scale;

        public Game1()
        {
            this.Window.Title = "Particles Test";

            graphics = new GraphicsDeviceManager(this);
            content = new ContentManager(Services);

            //this.effect = new ParticleEffect(this);

            //this.controller = new TestController(this, .0001f);
            //this.effect.Controllers.Add(this.controller);

            //this.emitter = new CircleEmitter<Particle>(15000, 60f, 300f, true);
            //this.emitter.ReleaseQuantity = 25;
            //this.emitter.ParticleSpeed = 0f;
            //this.emitter.ParticleSpeedVariation = 0f;
            //this.controller.Emitter = this.emitter;
            //this.effect.Emitters.Add(this.emitter);

            //this.color = new ColorModifier(Color.Chartreuse, Color.OrangeRed, .5f, Color.RoyalBlue);
            //this.emitter.Modifiers.Add(this.color);

            //this.opacity = new OpacityModifier(1f, 1f, .9f, 0f);
            //this.emitter.Modifiers.Add(this.opacity);

            //this.scale = new ScaleModifier(32f, 32f);
            //this.emitter.Modifiers.Add(this.scale);

            //this.renderer = new BasicRenderer(this, this.graphics, this.emitter.Budget);
            //this.effect.Renderer = this.renderer;

            ////this.emitter.Modifiers.Add(new RadialGravityModifier(new Vector2(512f, 384f), 5000f, 100f));
            //this.emitter.Modifiers.Add(new VortexModifier(new Vector2(312f, 384f), 150f, 500f, true));
            //this.emitter.Modifiers.Add(new VortexModifier(new Vector2(712f, 384f), 150f, 500f, false));
            //this.emitter.Modifiers.Add(new DampingModifier(.25f));

            this.IsMouseVisible = true;
            this.graphics.PreferredBackBufferWidth = 1024;
            this.graphics.PreferredBackBufferHeight = 768;

            //MercuryLoader.Export(this.effect, "fx.pfx");
            this.effect = MercuryLoader.Import(this, "fx.pfx");
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                this.Exit();

            //this.Window.Title = this.emitter.ActiveParticlesCount.ToString();
            //this.Window.Title = this.effect.UpdateTime.ToString();
            //this.Window.Title = this.effect.RenderTime.ToString();

            base.Update(gameTime);
        }


        protected override void Draw(GameTime gameTime)
        {
            graphics.GraphicsDevice.Clear(Color.Black);

            base.Draw(gameTime);
        }
    }
}
