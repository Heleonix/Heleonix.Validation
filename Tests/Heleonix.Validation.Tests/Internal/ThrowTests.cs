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
using Heleonix.Validation.Internal;
using NUnit.Framework;

namespace Heleonix.Validation.Tests.Internal
{
    /// <summary>
    /// The the <see cref="Throw{TException}"/>.
    /// </summary>
    public class ThrowTests
    {
        #region Tests

        /// <summary>
        /// Tests the <see cref="Throw{TException}.If"/>.
        /// </summary>
        /// <param name="condition">A condition to throw an exception.</param>
        /// <param name="arg">An argument to pass to a constructor of an exception.</param>
        [Test, Combinatorial]
        public void If([Values(true, false)] bool condition, [Values(12345, "message")] object arg)
        {
            if (arg is int)
            {
                Assert.That(Assert.Catch<Exception>(() => Throw<Exception>.If(condition, arg)).Message,
                    Is.EqualTo("Exception of type 'System.Exception' was thrown."));
            }
            else if (condition)
            {
                Assert.That(Assert.Catch<Exception>(() => Throw<Exception>.If(true, arg)).Message, Is.EqualTo(arg));
            }
            else
            {
                Assert.That(() => Throw<Exception>.If(false, arg), Throws.Nothing);
            }
        }

        /// <summary>
        /// Tests the <see cref="Throw{TException}.IfNullOrEmpty"/>.
        /// </summary>
        /// <param name="parameter">A parameter to check.</param>
        /// <param name="arg">An argument to pass to a constructor of an exception.</param>
        [Test, Combinatorial]
        public void IfNullOrEmpty([Values(null, "", "parameter")] string parameter,
            [Values(12345, "message")] object arg)
        {
            if (arg is int)
            {
                Assert.That(Assert.Catch<Exception>(() => Throw<Exception>.IfNull(parameter, arg)).Message,
                    Is.EqualTo("Exception of type 'System.Exception' was thrown."));
            }
            else if (string.IsNullOrEmpty(parameter))
            {
                Assert.That(Assert.Catch(() => Throw<Exception>.IfNullOrEmpty(parameter, arg)).Message, Is.EqualTo(arg));
            }
            else
            {
                Assert.That(() => Throw<Exception>.IfNullOrEmpty(parameter, arg), Throws.Nothing);
            }
        }

        /// <summary>
        /// Tests the <see cref="Throw{TException}.IfNull"/>.
        /// </summary>
        /// <param name="parameter">A parameter to check.</param>
        /// <param name="arg">An argument to pass to a constructor of an exception.</param>
        [Test, Combinatorial]
        public void IfNull([Values(null, "parameter")] object parameter, [Values(12345, "message")] object arg)
        {
            if (arg is int)
            {
                Assert.That(Assert.Catch<Exception>(() => Throw<Exception>.IfNull(parameter, arg)).Message,
                    Is.EqualTo("Exception of type 'System.Exception' was thrown."));
            }
            else if (parameter == null)
            {
                Assert.That(Assert.Catch(() => Throw<Exception>.IfNull(null, arg)).Message, Is.EqualTo(arg));
            }
            else
            {
                Assert.That(() => Throw<Exception>.IfNull(parameter, arg), Throws.Nothing);
            }
        }

        #endregion
    }
}