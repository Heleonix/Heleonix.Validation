/*
The MIT License (MIT)

Copyright (c) 2015 Heleonix.Validation - Hennadii Lutsyshyn (Heleonix)

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
*/

using System.Reflection;
using Moq;

namespace Heleonix.Validation.Tests.Common
{
    /// <summary>
    /// Extends the <see cref="IMock{T}"/>.
    /// </summary>
    public static class MockExtensions
    {
        #region Methods

        /// <summary>
        /// Invokes the specified <paramref name="methodName"/>.
        /// </summary>
        /// <typeparam name="TMock">A type of a mock.</typeparam>
        /// <param name="mock">A mock.</param>
        /// <param name="methodName">A name of a method.</param>
        /// <param name="parameters">Parameters for a method.</param>
        public static void Invoke<TMock>(this IMock<TMock> mock, string methodName, params object[] parameters)
            where TMock : class
        {
            try
            {
                mock.Object.GetType().GetMethod(methodName,
                    BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)
                    .Invoke(mock.Object, parameters);
            }
            catch (TargetInvocationException ex)
            {
                throw ex.InnerException;
            }
        }

        /// <summary>
        /// Invokes the specified <paramref name="methodName"/>.
        /// </summary>
        /// <typeparam name="TMock">A type of a mock.</typeparam>
        /// <param name="mock">A mock.</param>
        /// <param name="methodName">A name of a method.</param>
        /// <param name="parameters">Parameters for a method.</param>
        /// <returns>A value from a method.</returns>
        public static object InvokeVoid<TMock>(this IMock<TMock> mock, string methodName, params object[] parameters)
            where TMock : class
        {
            try
            {
                return mock.Object.GetType().GetMethod(methodName,
                    BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)
                    .Invoke(mock.Object, parameters);
            }
            catch (TargetInvocationException ex)
            {
                throw ex.InnerException;
            }
        }

        /// <summary>
        /// Invokes the specified property getter.
        /// </summary>
        /// <typeparam name="TMock">A type of a mock.</typeparam>
        /// <param name="mock">A mock.</param>
        /// <param name="propertyName">A name of a property.</param>
        /// <returns>A value from a property.</returns>
        public static object InvokeGetter<TMock>(this IMock<TMock> mock, string propertyName)
            where TMock : class
        {
            try
            {
                return mock.Object.GetType().GetProperty(propertyName,
                    BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic).GetValue(mock.Object);
            }
            catch (TargetInvocationException ex)
            {
                throw ex.InnerException;
            }
        }

        /// <summary>
        /// Invokes the specified property setter.
        /// </summary>
        /// <typeparam name="TMock">A type of a mock.</typeparam>
        /// <param name="mock">A mock.</param>
        /// <param name="propertyName">A name of a property.</param>
        /// <param name="value">A value to set.</param>
        public static void InvokeSetter<TMock>(this IMock<TMock> mock, string propertyName, object value)
            where TMock : class
        {
            try
            {
                mock.Object.GetType().GetProperty(propertyName,
                    BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic).SetValue(mock.Object, value);
            }
            catch (TargetInvocationException ex)
            {
                throw ex.InnerException;
            }
        }

        #endregion
    }
}