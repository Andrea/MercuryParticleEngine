﻿/*
 * Copyright © 2010 Project Mercury Team Members (http://mpe.codeplex.com/People/ProjectPeople.aspx)
 * 
 * This program is licensed under the Microsoft Permissive License (Ms-PL). You should
 * have received a copy of the license along with the source code. If not, an online copy
 * of the license can be found at http://mpe.codeplex.com/license.
 */

namespace ProjectMercury.Design.Emitters
{
    using System;
    using System.ComponentModel;
    using ProjectMercury;
    using ProjectMercury.Emitters;

    /// <summary>
    /// Defines a factory class for getting emitter type descriptors.
    /// </summary>
    public sealed class TypeDescriptorFactory : TypeDescriptionProvider
    {
        /// <summary>
        /// Gets a custom type descriptor for the given type and object.
        /// </summary>
        /// <param name="objectType">The type of object for which to retrieve the type descriptor.</param>
        /// <param name="instance">An instance of the type. Can be null if no instance was passed to the <see cref="T:System.ComponentModel.TypeDescriptor"/>.</param>
        /// <returns>
        /// An <see cref="T:System.ComponentModel.ICustomTypeDescriptor"/> that can provide metadata for the type.
        /// </returns>
        public override ICustomTypeDescriptor GetTypeDescriptor(Type objectType, Object instance)
        {
            if (objectType == typeof(PointEmitter))
                return new PointEmitterTypeDescriptor();

            if (objectType == typeof(BoxEmitter))
                return new BoxEmitterTypeDescriptor();

            if (objectType == typeof(CircleEmitter))
                return new CircleEmitterTypeDescriptor();

            if (objectType == typeof(CylinderEmitter))
                return new CylinderEmitterTypeDescriptor();

            if (objectType == typeof(LineEmitter))
                return new LineEmitterTypeDescriptor();

            if (objectType == typeof(SphereEmitter))
                return new SphereEmitterTypeDescriptor();

            return base.GetTypeDescriptor(objectType, instance);
        }
    }
}