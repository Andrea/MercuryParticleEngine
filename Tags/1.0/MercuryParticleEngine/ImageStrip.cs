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
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MercuryParticleEngine
{
    public class ImageStrip
    {
        //==============================================================[ Private Fields ]
        private string _name;
        private string _asset;
        private byte _frames;
        private Vector2 _size;
        private Texture2D _texture;

        //============================================================[ Public Interface ]
        public string Name
        {
            get { return _name; }
        }
        public string Asset
        {
            get { return _asset; }
            set { _asset = value; }
        }
        public byte Frames
        {
            get { return _frames; }
            set { _frames = value; }
        }
        public int FrameWidth
        {
            get { return (int)_size.X; }
            set { _size.X = (float)value; }
        }
        public int FrameHeight
        {
            get { return (int)_size.Y; }
            set { _size.Y = (float)value; }
        }
        internal Texture2D Texture
        {
            get { return _texture; }
        }

        //======================================================[ Constructors & Methods ]
        public ImageStrip(string name)
        {
            _name = name;
            _size = Vector2.Zero;
        }
        internal void Load(ContentManager content)
        {
            try
            {
                _texture = content.Load<Texture2D>(_asset);
            }
            catch (Exception e)
            {
                throw new Exception("Could not load asset!", e);
            }

            //The following code was contributed by Stas Kravets.
            // Auto-detect frame height if its zero initially
            if (_size.Y == 0)
                _size.Y = _texture.Height;

            // Auto-detect frame width if it is zero
            if (_size.X == 0)

                // If number of frames is calculated automatically we assume we deal with a square and use frame height,
                // otherwise we use number of frames to calculate the width from the total texture width
                _size.X = (_frames == 0) ? _size.Y : _texture.Width / _frames;

            // Auto-detect number of frames using width of texture and width of frame
            if (_frames == 0)
                _frames = (byte)Math.Truncate(((double)_texture.Width) / (double)(_size.X));
            //End of Stas Kravets contribution.
        }
        internal void UnLoad()
        {
            _texture.Dispose();
        }
        internal void Query(byte frame, ref Rectangle rect, ref Vector2 origin)
        {
            if (frame > _frames) { frame = 1; }

            rect.X = (int)_size.X * (frame - 1);
            rect.Y = 0;
            rect.Width = (int)_size.X;
            rect.Height = (int)_size.Y;

            origin.X = _size.X / 2f;
            origin.Y = _size.Y / 2f;
        }
    }
}
