// <copyright file="Throw.cs" company="Heleonix - Hennadii Lutsyshyn">
// Copyright (c) Heleonix - Hennadii Lutsyshyn. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the repository root for full license information.
// </copyright>

namespace Heleonix.Validation.Internal
{
    /// <summary>
    /// Represents the helper to raise exceptions.
    /// </summary>
    /// <typeparam name="TException">A type of an exception to throw.</typeparam>
    internal static class Throw<TException>
        where TException : Exception, new()
    {
        /// <summary>
        /// Throws the <typeparamref name="TException"/> if <paramref name="condition"/> is <see langword="true"/>.
        /// </summary>
        /// <param name="condition">A condition to throw an exception.</param>
        /// <param name="args">Arguments for a constructor of the exception.</param>
        public static void If(bool condition, params object[] args)
        {
            TException exception;

            try
            {
                exception = (TException)Activator.CreateInstance(typeof(TException), args);
            }
            catch
            {
                throw new TException();
            }

            if (condition)
            {
                throw exception;
            }
        }

        /// <summary>
        /// Throws the <typeparamref name="TException"/> if <paramref name="parameter"/> is <see langword="null"/>.
        /// </summary>
        /// <param name="parameter">A parameter to check.</param>
        /// <param name="args">Arguments for a constructor of the exception.</param>
        public static void IfNull(object parameter, params object[] args)
        {
            If(parameter == null, args);
        }

        /// <summary>
        /// Throws the <typeparamref name="TException"/>
        /// if <paramref name="parameter"/> is <see langword="null"/> or empty.
        /// </summary>
        /// <param name="parameter">A parameter to check.</param>
        /// <param name="args">Arguments for a constructor of the exception.</param>
        public static void IfNullOrEmpty(string parameter, params object[] args)
        {
            If(string.IsNullOrEmpty(parameter), args);
        }
    }
}