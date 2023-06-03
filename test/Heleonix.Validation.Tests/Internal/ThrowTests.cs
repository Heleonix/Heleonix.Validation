// <copyright file="ThrowTests.cs" company="Heleonix - Hennadii Lutsyshyn">
// Copyright (c) Heleonix - Hennadii Lutsyshyn. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the repository root for full license information.
// </copyright>

namespace Heleonix.Validation.Tests.Internal
{
    using Heleonix.Validation.Internal;
    using NUnit.Framework;

    /// <summary>
    /// The the <see cref="Throw{TException}"/>.
    /// </summary>
    public class ThrowTests
    {
        /// <summary>
        /// Tests the <see cref="Throw{TException}.If"/>.
        /// </summary>
        /// <param name="condition">A condition to throw an exception.</param>
        /// <param name="arg">An argument to pass to a constructor of an exception.</param>
        [Test]
        [Combinatorial]
        public void If([Values(true, false)] bool condition, [Values(12345, "message")] object arg)
        {
            if (arg is int)
            {
                Assert.That(
                    Assert.Catch<Exception>(() => Throw<Exception>.If(condition, arg)).Message,
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
        [Test]
        [Combinatorial]
        public void IfNullOrEmpty(
            [Values(null, "", "parameter")] string parameter,
            [Values(12345, "message")] object arg)
        {
            if (arg is int)
            {
                Assert.That(
                    Assert.Catch<Exception>(() => Throw<Exception>.IfNull(parameter, arg)).Message,
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
        [Test]
        [Combinatorial]
        public void IfNull([Values(null, "parameter")] object parameter, [Values(12345, "message")] object arg)
        {
            if (arg is int)
            {
                Assert.That(
                    Assert.Catch<Exception>(() => Throw<Exception>.IfNull(parameter, arg)).Message,
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
    }
}