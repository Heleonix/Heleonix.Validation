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

namespace NLion.Validation
{
    /// <summary>
    /// Represents the context of rule validation.
    /// </summary>
    public class RuleValidationContext
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="RuleValidationContext"/> class.
        /// </summary>
        /// <param name="validationContext">A validation context.</param>
        /// <param name="target">A target to validate.</param>
        /// <exception cref="ArgumentNullException">
        /// The <paramref name="validationContext"/> is <see langword="null"/>.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// The <paramref name="target"/> is <see langword="null"/>.
        /// </exception>
        public RuleValidationContext(ValidationContext validationContext, Target target)
        {
            Throw.ArgumentNullException(validationContext == null, nameof(validationContext));
            Throw.ArgumentNullException(target == null, nameof(target));

            ValidationContext = validationContext;
            Target = target;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets a validation context.
        /// </summary>
        public ValidationContext ValidationContext { get; }

        /// <summary>
        /// Gets a target to validate.
        /// </summary>
        public Target Target { get; }

        #endregion
    }
}