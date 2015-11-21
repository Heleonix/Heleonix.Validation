/*
The MIT License (MIT)

Copyright (c) 2015 NLion.Validation - Hennadii Lutsyshyn (NLion)

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

namespace NLion.Validation.Targets
{
    /// <summary>
    /// Represents the target for an object.
    /// </summary>
    public class ObjectTarget : Target
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ObjectTarget"/> class.
        /// </summary>
        /// <param name="name">A name of a target.</param>
        public ObjectTarget(string name) : base(name)
        {
        }

        #endregion

        #region Target Members

        /// <summary>
        /// Returns an object to validate.
        /// </summary>
        /// <param name="context">A validation context.</param>
        /// <exception cref="ArgumentNullException">
        /// The <paramref name="context"/> is <see langword="null"/>.
        /// </exception>
        /// <returns>An object to validate.</returns>
        public override object GetValue(ValidationContext context)
        {
            Throw.ArgumentNullException(context == null, nameof(context));

            return context.Object;
        }

        /// <summary>
        /// Creates a target result with <see langword="null"/> value, because a whole object is the value itself.
        /// </summary>
        /// <param name="context">A validation context.</param>
        /// <returns>A target result.</returns>
        public override TargetResult CreateResult(ValidationContext context) => new TargetResult(Name, null);

        #endregion
    }
}