using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using MercuryParticleEngine.Emitters;

namespace MercuryParticleEngine.Modifiers
{
    public sealed class GroupMouseController : Modifier
    {
        private bool _click;

        public GroupMouseController(bool triggerOnClick)
        {
            _click = triggerOnClick;
        }
        public override void UpdateEmitterGroup(GameTime time, EmitterGroup group)
        {
            MouseState state = Mouse.GetState();

            group.Position.X = (float)state.X;
            group.Position.Y = (float)state.Y;

            if (_click)
            {
                if (state.LeftButton == ButtonState.Pressed)
                {
                    group.Trigger();
                }
            }
        }
    }
}