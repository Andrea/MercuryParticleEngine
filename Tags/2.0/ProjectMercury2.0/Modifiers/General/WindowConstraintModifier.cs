//Mercury Particle Engine 2.0 Copyright (C) 2007 Matthew Davey

//This library is free software; you can redistribute it and/or modify it under the terms of
//the GNU Lesser General Public License as published by the Free Software Foundation; either
//version 2.1 of the License, or (at your option) any later version.

//This library is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY;
//without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.
//See the GNU Lesser General Public License for more details.

//You should have received a copy of the GNU Lesser General Public License along with this
//library; if not, write to the Free Software Foundation, Inc., 59 Temple Place, Suite 330,
//Boston, MA 02111-1307 USA 

using System;
using Microsoft.Xna.Framework;

namespace MercuryParticleEngine.Modifiers
{
    public sealed class WindowConstraintModifier : Modifier
    {
        #region [ Private Fields ]

        private float _bounce;
        private Rectangle _rect;

        #endregion

        #region [ Public Interface ]

        /// <summary>
        /// Gets or sets the bounc coefficient.
        /// </summary>
        public float Bounce
        {
            get { return _bounce; }
            set { _bounce = MathHelper.Clamp(value, 0f, 1f); }
        }

        #endregion

        #region [ Constructors & Methods ]

        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="window">Your games window.</param>
        /// <param name="bounce">Bounce coefficient.</param>
        public WindowConstraintModifier(GameWindow window, float bounce)
        {
            _rect = new Rectangle(0, 0, window.ClientBounds.Width, window.ClientBounds.Height);
            _bounce = bounce;
        }

        /// <summary>
        /// Modifies a single Particle.
        /// </summary>
        /// <param name="time">Game timing information.</param>
        /// <param name="particle">Particle to be modified.</param>
        public override void ProcessActiveParticle(GameTime time, Particle particle)
        {
            if (particle.Position.X < (float)_rect.Left)
            {
                particle.Position.X = (float)_rect.Left;
                particle.Momentum.X *= (-1f * _bounce);
            }
            else if (particle.Position.X > (float)_rect.Right)
            {
                particle.Position.X = (float)_rect.Right;
                particle.Momentum.X *= (-1f * _bounce);
            }
            if (particle.Position.Y < (float)_rect.Top)
            {
                particle.Position.Y = (float)_rect.Top;
                particle.Momentum.Y *= (-1f * _bounce);
            }
            else if (particle.Position.Y > (float)_rect.Bottom)
            {
                particle.Position.Y = (float)_rect.Bottom;
                particle.Momentum.Y *= (-1f * _bounce);
            }
        }

        #endregion
    }
}
