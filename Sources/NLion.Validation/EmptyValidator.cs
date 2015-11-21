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
    /// Represents the empty validator for building other validators.
    /// </summary>
    /// <typeparam name="TObject">A type of an object to validate.</typeparam>
    public class EmptyValidator<TObject> : Validator<TObject>
    {
        #region Validator<TObject> Members

        /// <summary>
        /// Does nothing.
        /// </summary>
        /// <param name="context">A context to validate an object.</param>
        /// <exception cref="NotImplementedException">The method should not be called.</exception>
        /// <returns></returns>
        public override ValidatorResult Validate(ValidationContext context)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Does nothing.
        /// </summary>
        /// <param name="context">A validation context.</param>
        /// <exception cref="NotImplementedException">The method should not be called.</exception>
        /// <returns></returns>
        public override ValidatorResult CreateResult(ValidationContext context)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Does nothing.
        /// </summary>
        /// <param name="builder">
        /// An instance of the <see cref="IInitialTargetBuilder{TObject}"/> to set up a validator.
        /// </param>
        /// <exception cref="NotImplementedException">The method should not be called.</exception>
        protected override void Setup(IInitialTargetBuilder<TObject> builder)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}