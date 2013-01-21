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

using System.Diagnostics;
using System.Reflection;
using NProxy.Core.Test.Performance.Types;
using LinFu.Proxy.Interfaces;
using NUnit.Framework;

namespace NProxy.Core.Test.Performance
{
    [TestFixture]
    [Category("Performance")]
    public sealed class LinFuPerformanceTestFixture
    {
        private static readonly AssemblyName AssemblyName;

        static LinFuPerformanceTestFixture()
        {
            var type = typeof (LinFu.Proxy.ProxyFactory);

            AssemblyName = type.Assembly.GetName();
        }

        [TestFixtureSetUp]
        public void SetUp()
        {
            // Ensure all classes are loaded and initialized.
            var proxyFactory = new LinFu.Proxy.ProxyFactory();
            var interceptor = new LinFuInterceptor(new Method());

            proxyFactory.CreateProxy<IMethod>(interceptor);
        }

        [TestCase(1000)]
        public void CreateProxyFromUnknownTypeTest(int iterations)
        {
            var interceptor = new LinFuInterceptor(new Method());
            var stopwatch = new Stopwatch();

            for (var i = 0; i < iterations; i++)
            {
                var proxyFactory = new LinFu.Proxy.ProxyFactory {Cache = new LinFuProxyCache()};

                stopwatch.Start();

                proxyFactory.CreateProxy<IMethod>(interceptor);

                stopwatch.Stop();
            }

            Report.Instance.WriteValues(AssemblyName, Scenario.CreateProxyFromUnknownType, iterations, stopwatch.Elapsed);
        }

        [TestCase(1000)]
        public void CreateProxyFromUnknownTypeWithGenericMethodTest(int iterations)
        {
            var interceptor = new LinFuInterceptor(new GenericMethod());
            var stopwatch = new Stopwatch();

            for (var i = 0; i < iterations; i++)
            {
                var proxyFactory = new LinFu.Proxy.ProxyFactory {Cache = new LinFuProxyCache()};

                stopwatch.Start();

                proxyFactory.CreateProxy<IGenericMethod>(interceptor);

                stopwatch.Stop();
            }

            Report.Instance.WriteValues(AssemblyName, Scenario.CreateProxyFromUnknownTypeWithGenericMethod, iterations, stopwatch.Elapsed);
        }

        [TestCase(1000000)]
        public void CreateProxyFromKnownTypeTest(int iterations)
        {
            var proxyFactory = new LinFu.Proxy.ProxyFactory();
            var interceptor = new LinFuInterceptor(new Method());
            var stopwatch = new Stopwatch();

            proxyFactory.CreateProxy<IMethod>(interceptor);

            stopwatch.Start();

            for (var i = 0; i < iterations; i++)
            {
                proxyFactory.CreateProxy<IMethod>(interceptor);
            }

            stopwatch.Stop();

            Report.Instance.WriteValues(AssemblyName, Scenario.CreateProxyFromKnownType, iterations, stopwatch.Elapsed);
        }

        [TestCase(1000000)]
        public void CreateProxyFromKnownTypeWithGenericMethodTest(int iterations)
        {
            var proxyFactory = new LinFu.Proxy.ProxyFactory();
            var interceptor = new LinFuInterceptor(new GenericMethod());
            var stopwatch = new Stopwatch();

            proxyFactory.CreateProxy<IGenericMethod>(interceptor);

            stopwatch.Start();

            for (var i = 0; i < iterations; i++)
            {
                proxyFactory.CreateProxy<IGenericMethod>(interceptor);
            }

            stopwatch.Stop();

            Report.Instance.WriteValues(AssemblyName, Scenario.CreateProxyFromKnownTypeWithGenericMethod, iterations, stopwatch.Elapsed);
        }

        [TestCase(10000000)]
        public void InvokeMethodTest(int iterations)
        {
            var proxyFactory = new LinFu.Proxy.ProxyFactory();
            var interceptor = new LinFuInterceptor(new Method());
            var proxy = proxyFactory.CreateProxy<IMethod>(interceptor);
            var stopwatch = new Stopwatch();

            stopwatch.Start();

            for (var i = 0; i < iterations; i++)
            {
                proxy.Invoke(i);
            }

            stopwatch.Stop();

            Report.Instance.WriteValues(AssemblyName, Scenario.InvokeMethod, iterations, stopwatch.Elapsed);
        }

        [TestCase(10000000)]
        public void InvokeGenericMethodTest(int iterations)
        {
            var proxyFactory = new LinFu.Proxy.ProxyFactory();
            var interceptor = new LinFuInterceptor(new GenericMethod());
            var proxy = proxyFactory.CreateProxy<IGenericMethod>(interceptor);
            var stopwatch = new Stopwatch();

            stopwatch.Start();

            for (var i = 0; i < iterations; i++)
            {
                proxy.Invoke(i);
            }

            stopwatch.Stop();

            Report.Instance.WriteValues(AssemblyName, Scenario.InvokeGenericMethod, iterations, stopwatch.Elapsed);
        }
    }
}