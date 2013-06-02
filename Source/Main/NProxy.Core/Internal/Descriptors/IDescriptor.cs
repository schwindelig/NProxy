﻿//
// NProxy is a library for the .NET framework to create lightweight dynamic proxies.
// Copyright © Martin Tamme
//
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU Lesser General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
//
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
// GNU Lesser General Public License for more details.
//
// You should have received a copy of the GNU Lesser General Public License
// along with this program. If not, see <http://www.gnu.org/licenses/>.
//

using System;

namespace NProxy.Core.Internal.Descriptors
{
    /// <summary>
    /// Defines a descriptor.
    /// </summary>
    internal interface IDescriptor
    {
        /// <summary>
        /// Returns the declaring type.
        /// </summary>
        Type DeclaringType { get; }

        /// <summary>
        /// Returns the parent type.
        /// </summary>
        Type ParentType { get; }

        /// <summary>
        /// Dispatches to the specific visit method for each member.
        /// </summary>
        /// <param name="descriptorVisitor">The descriptor visitor.</param>
        void Accept(IDescriptorVisitor descriptorVisitor);

        /// <summary>
        /// Casts an instance to the specified interface type.
        /// </summary>
        /// <typeparam name="TInterface">The interface type.</typeparam>
        /// <param name="instance">The instance.</param>
        /// <returns>The object, of the specified interface type, to which the instance has been casted.</returns>
        TInterface Cast<TInterface>(object instance) where TInterface : class;

        /// <summary>
        /// Creates an instance of the specified type.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="arguments">The constructor arguments.</param>
        /// <returns>The instance.</returns>
        object CreateInstance(Type type, object[] arguments);
    }
}