//Copyright (C) 2006 Matt Davey

//This library is free software; you can redistribute it and/or
//modify it under the terms of the GNU Lesser General Public
//License as published by the Free Software Foundation; either
//version 2.1 of the License, or any later version.

//This library is distributed in the hope that it will be useful,
//but WITHOUT ANY WARRANTY; without even the implied warranty of
//MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
//Lesser General Public License for more details.

//You should have received a copy of the GNU Lesser General Public
//License along with this library; if not, write to the Free Software
//Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston, MA  02110-1301  USA

using System;
using Microsoft.Xna.Framework;
using MercuryParticleEngine.Emitters;

namespace MercuryParticleEngine.Modifiers
{
    public sealed class WindowBounceModifier : Modifier
    {
        private Rectangle _rect;
        private float _bounce;
        private Vector2 _momentum;

        public WindowBounceModifier(GameWindow window, float bounce)
        {
            _rect = new Rectangle(0, 0, window.ClientBounds.Width, window.ClientBounds.Height);
            _bounce = MathHelper.Clamp(bounce, 0f, 1f);
            _momentum = new Vector2();
        }
        public override void UpdateParticle(GameTime time, Particle spawn)
        {
            _momentum.X = (float)Math.Cos(spawn.Orientation) * spawn.Speed;
            _momentum.Y = (float)Math.Sin(spawn.Orientation) * spawn.Speed;

            if (spawn.Position.X < (float)_rect.X)
            {
                spawn.Position.X = (float)_rect.X;
                _momentum.X *= -1f; _momentum.X *= _bounce;
            }
            else if (spawn.Position.X > (float)_rect.Width)
            {
                spawn.Position.X = (float)_rect.Width;
                _momentum.X *= -1f; _momentum.X *= _bounce;
            }

            if (spawn.Position.Y < (float)_rect.Y)
            {
                spawn.Position.Y = (float)_rect.Y;
                _momentum.Y *= -1f; _momentum.Y *= _bounce;
            }
            else if (spawn.Position.Y > (float)_rect.Height)
            {
                spawn.Position.Y = (float)_rect.Height;
                _momentum.Y *= -1f; _momentum.Y *= _bounce;
            }

            spawn.Orientation = (float)Math.Atan2(_momentum.Y, _momentum.X);
            spawn.Speed = _momentum.Length();
        }
    }
}