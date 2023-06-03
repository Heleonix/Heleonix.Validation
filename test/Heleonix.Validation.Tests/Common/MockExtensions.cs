// <copyright file="MockExtensions.cs" company="Heleonix - Hennadii Lutsyshyn">
// Copyright (c) Heleonix - Hennadii Lutsyshyn. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the repository root for full license information.
// </copyright>

namespace Heleonix.Validation.Tests.Common
{
    using System.Reflection;
    using Moq;

    /// <summary>
    /// Extends the <see cref="IMock{T}"/>.
    /// </summary>
    public static class MockExtensions
    {
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
                mock.Object.GetType().GetMethod(
                    methodName,
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
                return mock.Object.GetType().GetMethod(
                    methodName,
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
                return mock.Object.GetType().GetProperty(
                    propertyName,
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
                mock.Object.GetType().GetProperty(
                    propertyName,
                    BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic).SetValue(mock.Object, value);
            }
            catch (TargetInvocationException ex)
            {
                throw ex.InnerException;
            }
        }
    }
}