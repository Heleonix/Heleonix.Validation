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

namespace Heleonix.Validation.Builders
{
    /// <summary>
    /// Implements the <see cref="IBuilder{TObject}"/>.
    /// </summary>
    /// <typeparam name="TObject">A type of an object to validate.</typeparam>
    public abstract class Builder<TObject> : IBuilder<TObject>
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Builder{TObject}"/> class.
        /// </summary>
        /// <param name="validator">A validator.</param>
        /// <exception cref="ArgumentNullException">
        /// The <paramref name="validator"/> is <see langword="null"/>.
        /// </exception>
        protected Builder(Validator<TObject> validator)
        {
            Throw<ArgumentNullException>.IfNull(validator, nameof(validator));

            Validator = validator;
        }

        #endregion

        #region IBuilder<TObject> Members

        /// <summary>
        /// Gets a validator.
        /// </summary>
        public Validator<TObject> Validator { get; }

        #endregion
    }
}