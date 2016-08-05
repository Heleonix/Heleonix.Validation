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

using System;

namespace Heleonix.Validation.Internal
{
    /// <summary>
    /// Represents the helper to raise exceptions.
    /// </summary>
    /// <typeparam name="TException">A type of an exception to throw.</typeparam>
    internal static class Throw<TException>
        where TException : Exception, new()
    {
        #region Methods

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
                exception = (TException) Activator.CreateInstance(typeof (TException), args);
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

        #endregion
    }
}