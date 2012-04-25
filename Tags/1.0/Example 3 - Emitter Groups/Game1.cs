using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;

using MercuryParticleEngine;
using MercuryParticleEngine.Emitters;
using MercuryParticleEngine.Modifiers;

namespace Example3
{
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        ContentManager content;

        ParticleSystem system;

        //The main purpose of this example is to intoduce the EmitterGroup class. An
        //EmitterGroup allows you to group Emitters together, and control them as one.
        //When you add an Emitter to an EmitterGroup, the position of the Emitter is relative
        //to the position of the EmitterGroup, and not the origin. EmittersGroups also allow
        //you to apply modifiers to many Emitters at once. Applying a Modifier to an
        //EmitterGroup will also apply the Modifier to each Emitter within the group.
        //Finally, EmitterGroups allow you to change the blending mode used when rendering
        //Particles. You could create an Emitter group and change its blending mode to
        //SpriteBlendMode.AlphaBlend, and each Emitter added to the EmitterGroup will render
        //in this way.
        EmitterGroup group;

        SprayEmitter emitter1;
        SprayEmitter emitter2;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            content = new ContentManager(Services);

            system = new ParticleSystem(this);
        }

        protected override void Initialize()
        {
            //Creating the EmitterGroup, it must be given a name.
            group = new EmitterGroup("test");

            //Create two simple spray emitters.
            emitter1 = new SprayEmitter(1000, -MathHelper.PiOver2, MathHelper.Pi);
            emitter1.ParticleSpeed = 100f;
            emitter1.ParticleColor = Color.SteelBlue;
            emitter1.Position.Y = -50f;

            emitter2 = new SprayEmitter(1000, MathHelper.PiOver2, MathHelper.Pi);
            emitter2.ParticleSpeed = 100f;
            emitter2.ParticleColor = Color.Tomato;
            emitter2.Position.Y = 50f;

            //We add the Emitters to the EmitterGroup (instead of adding them to the
            //ParticleSystem).
            group.AddEmitter(emitter1);
            group.AddEmitter(emitter2);

            //Now we can apply Modifiers to the EmitterGroup. Notice that there is a
            //seperate Modifier for GroupMouseController. As the position of Emitters is
            //relative to the position of the EmitterGroup, applying a regalar
            //EmitterMouseController would result in undesired results... This modifier
            //adjusts the position of the EmitterGroup, and not the Emitters.
            group.ApplyModifier(new RandomScaleModifier(.1f, 1f));
            group.ApplyModifier(new GroupMouseController(true));

            //Finally we add the EmitterGroup to the ParticleSystem.
            system.AddEmitterGroup(group);

            //Of course you can choose to ignore EmitterGroups, and add Emitters to the
            //ParticleSystem directly instead. However you should take care to never
            //add an Emitter to more than one container.

            base.Initialize();
        }

        protected override void Update(GameTime gameTime)
        {
            // Allows the default game to exit on Xbox 360 and Windows
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            graphics.GraphicsDevice.Clear(Color.Black);

            base.Draw(gameTime);
        }
    }
}