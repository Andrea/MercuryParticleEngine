namespace ProjectMercury.Emitters
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Content;

    /// <summary>
    /// Emits particles in the shape of a polygon defined with the Points property.
    /// </summary>
#if WINDOWS
    [TypeConverter("ProjectMercury.Design.Emitters.PolygonEmitterTypeConverter, ProjectMercury.Design")]
#endif
    public class PolygonEmitter : Emitter
    {
        private Matrix RotationMatrix = Matrix.Identity;
        private Matrix ScaleMatrix = Matrix.Identity;

        /// <summary>
        /// Close the polygon?
        /// </summary>
        public bool Close;

        /// <summary>
        /// Polygon points.
        /// </summary>
        public PolygonPointCollection Points { get; set; }

        public PolygonOrigin Origin
        {
            get { return this.Points.Origin; }
            set { this.Points.Origin = value; }
        }

        /// <summary>
        /// Polygon rotation.
        /// </summary>
        public float Rotation
        {
            get { return (float)Math.Atan2(this.RotationMatrix.M12, this.RotationMatrix.M11); }
            set { this.RotationMatrix = Matrix.CreateRotationZ(value); }
        }

        /// <summary>
        /// Polygon scale.
        /// </summary>
        public float Scale
        {
            get { return this.ScaleMatrix.M11; }
            set { this.ScaleMatrix = Matrix.CreateScale(value); }
        }

        public PolygonEmitter() : base()
        {
            this.Close    = true;
            this.Points   = new PolygonPointCollection();
            this.Scale    = 1.0f;
        }

        protected override void GenerateParticleOffset(float totalSeconds, ref Vector2 triggerPosition, out Vector2 offset)
        {
            if      (this.Points.Count == 0) { offset = Vector2.Zero; }
            else if (this.Points.Count == 1) { offset = this.Points[0]; }
            else if (this.Points.Count == 2) { offset = Vector2.Lerp(this.Points[0], this.Points[1], RandomHelper.NextFloat()); }
            else
            {
                int i = this.Close ? RandomHelper.NextInt(0, this.Points.Count)
                                   : RandomHelper.NextInt(0, this.Points.Count - 1);

                offset = Vector2.Lerp(this.Points[i], this.Points[(i + 1) % this.Points.Count], RandomHelper.NextFloat());
            }

            // Apply any necessary transformation to the resultant offset vector...
            Matrix transform;

            Matrix.Multiply(ref this.Points.TranslationMatrix, ref this.RotationMatrix, out transform);

            Matrix.Multiply(ref this.ScaleMatrix, ref transform, out transform);

            Vector2.Transform(ref offset, ref transform, out offset);
        }
    }

    /// <summary>
    /// Collection of points to generate a polygon.
    /// </summary>
    /// <remarks>By implementing the IList interface explicitly, we can effectively override certain methods of the
    /// base class without them being declared as virtual.</remarks>
    public class PolygonPointCollection : List<Vector2>, IList
    {
        private PolygonOrigin _origin;

        [ContentSerializerIgnore]
        public PolygonOrigin Origin
        {
            get { return this._origin; }
            set
            {
                if (this.Origin != value)
                {
                    this._origin = value;

                    this.RecalculateTranslation();
                }
            }
        }

        [ContentSerializerIgnore]
        public Matrix TranslationMatrix = Matrix.Identity;

        private void RecalculateTranslation()
        {
            switch (this.Origin)
            {
                case PolygonOrigin.Default:
                    {
                        this.GetDefaultTranslation(out this.TranslationMatrix);

                        break;
                    }
                case PolygonOrigin.Center:
                    {
                        this.GetCenterTranslation(out this.TranslationMatrix);

                        break;
                    }
                case PolygonOrigin.Origin:
                    {
                        this.GetOriginTranslation(out this.TranslationMatrix);

                        break;
                    }
            }
        }

        private void GetCenterTranslation(out Matrix matrix)
        {
            // Creates a rectangle around the polygon and use its center as the polygon center.
            float left   = base[0].X,
                  right  = base[0].X,
                  top    = base[0].Y,
                  bottom = base[0].Y;

            // Check all the points to make the rectangle surround the entire polygon.
            for (int i = 1; i < base.Count; i++)
            {
                left   = base[i].X < left   ? base[i].X : left;
                right  = base[i].X > right  ? base[i].X : right;
                top    = base[i].Y < top    ? base[i].Y : top;
                bottom = base[i].Y > bottom ? base[i].Y : bottom;
            }

            // Apply the translation offset.
            matrix = Matrix.CreateTranslation(-((right - left) / 2 + left), -((bottom - top) / 2 + top), 0f);
        }

        private void GetOriginTranslation(out Matrix matrix)
        {
            Vector2 point = Vector2.Zero;

            if (base.Count > 0)
                point = base[0];

            matrix = Matrix.CreateTranslation(-point.X, -point.Y, 0f);
        }

        private void GetDefaultTranslation(out Matrix matrix)
        {
            matrix = Matrix.Identity;
        }

        int IList.Add(object value)
        {
            Vector2 v = (Vector2)value;

            base.Add(v);

            this.RecalculateTranslation();

            return base.IndexOf(v);
        }

        void IList.Clear()
        {
            base.Clear();

            this.TranslationMatrix = Matrix.Identity;
        }

        void IList.Remove(object value)
        {
            Vector2 v = (Vector2)value;

            if (base.Contains(v))
                base.Remove(v);

            this.RecalculateTranslation();
        }

        void IList.RemoveAt(int index)
        {
            base.RemoveAt(index);

            this.RecalculateTranslation();
        }

        object IList.this[int index]
        {
            get { return base[index]; }
            set
            {
                Vector2 v = (Vector2)value;

                base[index] = v;

                this.RecalculateTranslation();
            }
        }
    }

    public enum PolygonOrigin : byte
    {
        Default = 0,
        Center = 1,
        Origin = 2
    }
}
