//
// NProxy is a library for the .NET framework to create lightweight dynamic proxies.
// Copyright © 2012 Martin Tamme
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
using System.Reflection;
using NProxy.Core.Internal.Generators;
using NProxy.Core.Internal.Reflection;

namespace NProxy.Core
{
    /// <summary>
    /// Represents the default interception filter.
    /// </summary>
    internal sealed class DefaultInterceptionFilter : IInterceptionFilter
    {
        #region IInterceptionFilter Members

        /// <inheritdoc/>
        public bool Accept(EventInfo eventInfo)
        {
            return !eventInfo.IsDefined<NonInterceptedAttribute>();
        }

        /// <inheritdoc/>
        public bool Accept(PropertyInfo propertyInfo)
        {
            return !propertyInfo.IsDefined<NonInterceptedAttribute>();
        }

        /// <inheritdoc/>
        public bool Accept(MethodInfo methodInfo)
        {
            if (methodInfo.IsDefined<NonInterceptedAttribute>())
                return false;

            var declaringType = methodInfo.DeclaringType;

            if (declaringType == null)
                return false;

            if (declaringType != typeof (object))
                return true;

            return !String.Equals(methodInfo.Name, "Finalize");
        }

        #endregion
    }
}