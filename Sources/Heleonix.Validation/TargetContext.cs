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

namespace Heleonix.Validation
{
    /// <summary>
    /// Represents the context of targets.
    /// </summary>
    public class TargetContext
    {
        #region Fields

        /// <summary>
        /// Gets or sets a target.
        /// </summary>
        private Target _target;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="TargetContext"/> class.
        /// </summary>
        /// <param name="target">A target.</param>
        /// <param name="validatorContext">A context of a validator.</param>
        /// <exception cref="ArgumentNullException">
        /// The <paramref name="validatorContext"/> is <see langword="null"/>.
        /// </exception>
        public TargetContext(Target target, ValidatorContext validatorContext)
        {
            Throw<ArgumentNullException>.IfNull(validatorContext, nameof(validatorContext));

            _target = target;
            ValidatorContext = validatorContext;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets a target.
        /// </summary>
        /// <exception cref="ArgumentNullException">The <see langword="value"/> is <see langword="null"/>.</exception>
        public virtual Target Target
        {
            get { return _target; }
            set
            {
                Throw<ArgumentNullException>.IfNull(value, nameof(value));

                _target = value;
            }
        }

        /// <summary>
        /// Gets a context of a validator.
        /// </summary>
        public virtual ValidatorContext ValidatorContext { get; }

        #endregion
    }
}