//
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

namespace NProxy.Core.Internal.Caching
{
    /// <summary>
    /// Defines a cache factory.
    /// </summary>
    internal interface ICacheFactory
    {
        /// <summary>
        /// Creates a new cache.
        /// </summary>
        /// <param name="kind">The cache kind.</param>
        /// <returns>The cache.</returns>
        /// <typeparam name="TKey">The key type.</typeparam>
        /// <typeparam name="TValue">The value type.</typeparam>
        ICache<TKey, TValue> CreateCache<TKey, TValue>(CacheKind kind);
    }
}