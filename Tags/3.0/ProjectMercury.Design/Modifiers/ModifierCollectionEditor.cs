namespace ProjectMercury.Design.Modifiers
{
    using System;
    using System.ComponentModel.Design;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using ProjectMercury.Modifiers;

    public class ModifierCollectionEditor : CollectionEditor
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ModifierCollectionEditor"/> class.
        /// </summary>
        public ModifierCollectionEditor()
            : base(typeof(ModifierCollection)) { }

        /// <summary>
        /// Retrieves the display text for the given list item.
        /// </summary>
        /// <param name="value">The list item for which to retrieve display text.</param>
        /// <returns>
        /// The display text for <paramref name="value"/>.
        /// </returns>
        protected override string GetDisplayText(object value)
        {
            return value.GetType().Name;
        }

        /// <summary>
        /// Indicates whether multiple collection items can be selected at once.
        /// </summary>
        /// <returns>
        /// true if it multiple collection members can be selected at the same time; otherwise, false. By default, this returns true.
        /// </returns>
        protected override bool CanSelectMultipleInstances()
        {
            return false;
        }

        /// <summary>
        /// Gets the data types that this collection editor can contain.
        /// </summary>
        /// <returns>
        /// An array of data types that this collection can contain.
        /// </returns>
        protected override Type[] CreateNewItemTypes()
        {
            return new Type[]
            {
                typeof(ColorModifier),
                typeof(LinearGravityModifier),
                typeof(OpacityModifier),
                typeof(RadialGravityModifier),
                typeof(RandomColourModifier),
                typeof(RotationModifier),
                typeof(ScaleModifier)
            };
        }

        /// <summary>
        /// Gets the data type that this collection contains.
        /// </summary>
        /// <returns>
        /// The data type of the items in the collection, or an <see cref="T:System.Object"/> if no Item property can be located on the collection.
        /// </returns>
        protected override Type CreateCollectionItemType()
        {
            return typeof(ModifierCollection);
        }

        /// <summary>
        /// Creates a new instance of the specified collection item type.
        /// </summary>
        /// <param name="itemType">The type of item to create.</param>
        /// <returns>A new instance of the specified object.</returns>
        protected override object CreateInstance(Type itemType)
        {
            if (itemType == typeof(ColorModifier))
                return new ColorModifier
                {
                    InitialColour = Color.GreenYellow.ToVector3(),
                    UltimateColour = Color.HotPink.ToVector3(),
                };

            if (itemType == typeof(LinearGravityModifier))
                return new LinearGravityModifier
                {
                    Gravity = new Vector2 { X = 0f, Y = 250f }
                };

            if (itemType == typeof(OpacityModifier))
                return new OpacityModifier
                {
                    Initial = 1f,
                    Ultimate = 0f
                };

            if (itemType == typeof(RadialGravityModifier))
                return new RadialGravityModifier
                {
                    Position = new Vector2 { X = 700f, Y = 300f },
                    Radius = 250f,
                    Strength = 250f,
                };

            if (itemType == typeof(RandomColourModifier))
                return new RandomColourModifier
                {
                    Colour = new Vector3 { X = 0.5f, Y = 0.5f, Z = 0.5f },
                    Variation = 0.5f
                };

            if (itemType == typeof(RotationModifier))
                return new RotationModifier
                {
                    RotationRate = MathHelper.Pi
                };

            if (itemType == typeof(ScaleModifier))
                return new ScaleModifier
                {
                    InitialScale = 32f,
                    UltimateScale = 0f
                };

            return null;
        }
    }
}