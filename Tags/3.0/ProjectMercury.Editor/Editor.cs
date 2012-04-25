using System.Windows.Forms;

namespace ProjectMercury.Editor
{
    using System;
    using System.IO;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Content;
    using Microsoft.Xna.Framework.Graphics;
    using Microsoft.Xna.Framework.Input;
    using ProjectMercury.Editor.UI;
    using ProjectMercury.Emitters;
    using ProjectMercury.Renderers;
    using Point = System.Drawing.Point;
    using Size = System.Drawing.Size;

    public class Editor : Game
    {
        private GraphicsDeviceManager GraphicsDeviceManager;
        private ControlPanel ControlPanel;
        private Emitter Emitter;
        private Renderer Renderer;
        private Texture2D ParticleTexture;
        private Vector2 Position;

        public Editor()
        {
            this.GraphicsDeviceManager = new GraphicsDeviceManager(this);
            this.GraphicsDeviceManager.PreferredBackBufferWidth = 1024;
            this.GraphicsDeviceManager.PreferredBackBufferHeight = 600;
            base.IsMouseVisible = true;
            base.Window.Title = "Mercury Particle Engine";

            this.Content = new ContentManager(base.Services);

            this.ControlPanel = new ControlPanel();

            this.ControlPanel.CreateEmitterCallback = new CreateEmitterCallback(this.CreateEmitter);
            this.ControlPanel.LoadEmitterCallback = new FileOperation(this.LoadEmitter);
            this.ControlPanel.LoadTextureCallback = new FileOperation(this.LoadTexture);
            this.ControlPanel.SaveEmitterCallback = new FileOperation(this.SaveEmitter);
            this.ControlPanel.SwitchRendererCallback = new SwitchRendererCallback(this.SwitchRenderer);
            this.ControlPanel.TriggerCallback = new TriggerCallback(this.Trigger);
        }

        private void CreateEmitter(string type, int budget, float term)
        {
            var oldColour = this.Emitter.ReleaseColour;
            var oldOpacity = this.Emitter.ReleaseOpacity;
            var oldQuantity = this.Emitter.ReleaseQuantity;
            var oldRotation = this.Emitter.ReleaseRotation;
            var oldScale = this.Emitter.ReleaseScale;
            var oldSpeed = this.Emitter.ReleaseSpeed;
            var oldModifier = this.Emitter.Modifiers;

            switch (type)
            {
                case "Emitter":
                    {
                        this.Emitter = new Emitter();

                        break;
                    }
                case "CircleEmitter":
                    {
                        this.Emitter = new CircleEmitter
                        {
                            Radius = 50f,
                            Ring = false,
                            Radiate = true
                        };

                        break;
                    }
                case "ConeEmitter":
                    {
                        this.Emitter = new ConeEmitter
                        {
                            Direction = 0f,
                            ConeAngle = MathHelper.ToRadians(40f)
                        };

                        break;
                    }
                case "LineEmitter":
                    {
                        this.Emitter = new LineEmitter
                        {
                            Length = 100
                        };

                        break;
                    }
                case "PolygonEmitter":
                    {
                        this.Emitter = new PolygonEmitter
                        {
                            Points = new PolygonPointCollection
                            {
                                new Vector2 { X = -100f, Y =  100f },
                                new Vector2 { X =  0f,   Y = -100f },
                                new Vector2 { X =  100f, Y =  100f }
                            }
                        };

                        break;
                    }
                case "RectEmitter":
                    {
                        this.Emitter = new RectEmitter
                        {
                            Width = 100,
                            Height = 100
                        };

                        break;
                    }
            }

            this.Emitter.Budget = budget;
            this.Emitter.ParticleTexture = this.ParticleTexture;
            this.Emitter.ReleaseColour = oldColour;
            this.Emitter.ReleaseOpacity = oldOpacity;
            this.Emitter.ReleaseQuantity = oldQuantity;
            this.Emitter.ReleaseRotation = oldRotation;
            this.Emitter.ReleaseScale = oldScale;
            this.Emitter.ReleaseSpeed = oldSpeed;
            this.Emitter.Modifiers = oldModifier;
            this.Emitter.Term = term;

            this.Emitter.Initialize();

            this.ControlPanel.EmitterPropertyGridWrapper = this.Emitter;
        }
        
        private void LoadEmitter(string filePath)
        {
            this.Emitter = EmitterSerializer.Deserialize(filePath);

            this.Emitter.Initialize();

            this.Emitter.ParticleTexture = this.ParticleTexture;

            this.ControlPanel.EmitterPropertyGridWrapper = this.Emitter;
        }
        
        private void SaveEmitter(string filePath)
        {
            EmitterSerializer.Serialize(this.Emitter, filePath);
        }
        
        private void LoadTexture(string filePath)
        {
            this.ParticleTexture = Texture2D.FromFile(base.GraphicsDevice, filePath);

            this.Emitter.ParticleTexture = this.ParticleTexture;
            this.Emitter.ParticleTextureAssetName = "Content\\" + Path.GetFileNameWithoutExtension(filePath);
        }

        private void SwitchRenderer(string type)
        {
            switch (type)
            {
                case "PointSpriteRenderer":
                    {
                        this.Renderer = new PointSpriteRenderer
                        {
                            BlendMode = SpriteBlendMode.Additive,
                            GraphicsDeviceService = this.GraphicsDeviceManager
                        };

                        this.Renderer.LoadContent(base.Content);

                        break;
                    }
                case "SpriteBatchRenderer":
                    {
                        this.Renderer = new SpriteBatchRenderer
                        {
                            BlendMode = SpriteBlendMode.Additive,
                            GraphicsDeviceService = this.GraphicsDeviceManager
                        };

                        this.Renderer.LoadContent(base.Content);

                        break;
                    }
            }

            this.ControlPanel.RendererPropertyGridWrapper = this.Renderer;
        }
        
        private void Trigger()
        {
            this.Emitter.Trigger(this.Position);
        }

        protected override void Initialize()
        {
            base.Initialize();

            this.LoadEmitter("Content\\DefaultEmitter.em");
            this.SwitchRenderer("PointSpriteRenderer");
            this.LoadTexture("Content\\FlowerBurst.bmp");

            var winForm = Control.FromHandle(base.Window.Handle);

            winForm.LocationChanged += delegate
            {
                this.ControlPanel.Location = new Point
                {
                    X = winForm.Location.X + 3,
                    Y = winForm.Location.Y + 29
                };
            };

            winForm.Resize += delegate
            {
                this.ControlPanel.Size = new Size
                {
                    Width = this.ControlPanel.Width,
                    Height = this.Window.ClientBounds.Height
                };
            };

            this.ControlPanel.Show(winForm);

            this.ControlPanel.Location = new Point
            {
                X = winForm.Location.X + 3,
                Y = winForm.Location.Y + 28
            };

            this.ControlPanel.Size = new Size
            {
                Width = this.ControlPanel.Width,
                Height = this.Window.ClientBounds.Height
            };
        }

        protected override void Update(GameTime gameTime)
        {
            var totalSeconds = (float)gameTime.TotalGameTime.TotalSeconds;
            var elpsedSeconds = (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                base.Exit();

            var state = Mouse.GetState();

            if (state.LeftButton == ButtonState.Pressed)
            {
                if (state.X > this.ControlPanel.Size.Width && state.X < this.GraphicsDevice.Viewport.Width)
                {
                    if (state.Y > 0 && state.Y < this.GraphicsDevice.Viewport.Height)
                    {
                        this.Position = new Vector2(state.X, state.Y);

                        this.Emitter.Trigger(this.Position);
                    }
                }
            }

            if (this.Position.X < 0) this.Position.X = 0;
            if (this.Position.X > this.GraphicsDevice.Viewport.Width) this.Position.X = this.GraphicsDevice.Viewport.Width;

            if (this.Position.Y < 0) this.Position.Y = 0;
            if (this.Position.Y > this.GraphicsDevice.Viewport.Height) this.Position.Y = this.GraphicsDevice.Viewport.Height;

            this.Emitter.Update(totalSeconds, elpsedSeconds);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            this.GraphicsDevice.Clear(Color.Black);

            this.Renderer.RenderEmitter(this.Emitter);
        }
    }
}