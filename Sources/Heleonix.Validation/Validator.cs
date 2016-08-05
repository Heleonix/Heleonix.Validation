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
using System.Collections.ObjectModel;
using Heleonix.Validation.Builders;
using Heleonix.Validation.Internal;

namespace Heleonix.Validation
{
    /// <summary>
    /// Represents the base class for all validators.
    /// </summary>
    /// <typeparam name="TObject">A type of an object to validate.</typeparam>
    public abstract class Validator<TObject> : IValidator
    {
        #region Methods

        /// <summary>
        /// When overridden in a derived class, sets up the validator.
        /// </summary>
        /// <param name="builder">A <see cref="IInitialTargetBuilder{TObject}"/> to set up the validator.</param>
        protected abstract void Setup(IInitialTargetBuilder<TObject> builder);

        /// <summary>
        /// Creates a <see cref="ValidatorResult"/>.
        /// </summary>
        /// <param name="context">A <see cref="ValidatorContext"/>.</param>
        /// <exception cref="ArgumentNullException">
        /// The <paramref name="context"/> is <see langword="null"/>.
        /// </exception>
        /// <returns>A <see cref="ValidatorResult"/>.</returns>
        protected virtual ValidatorResult CreateResult(ValidatorContext context)
        {
            Throw<ArgumentNullException>.IfNull(context, nameof(context));

            return new ValidatorResult();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets a list of <see cref="Target"/>.
        /// </summary>
        public virtual ObservableCollection<Target> Targets { get; } = new ObservableCollection<Target>();

        #endregion

        #region IValidator Members

        /// <summary>
        /// Executes validation.
        /// </summary>
        /// <param name="context">A <see cref="ValidatorContext"/>.</param>
        /// <exception cref="ArgumentNullException">
        /// The <paramref name="context"/> is <see langword="null"/>.
        /// </exception>
        /// <returns>A <see cref="ValidatorResult"/>.</returns>
        public virtual ValidatorResult Validate(ValidatorContext context)
        {
            Throw<ArgumentNullException>.IfNull(context, nameof(context));

            context.Validator = this;

            var result = CreateResult(context);

            if (result == null)
            {
                return null;
            }

            foreach (var target in Targets)
            {
                if (!context.ContinueValidation)
                {
                    return result;
                }

                var targetResult = target?.Validate(new TargetContext(null, context));

                if (targetResult == null)
                {
                    continue;
                }

                if (!context.IgnoreEmptyResults || !targetResult.IsEmpty())
                {
                    result.TargetResults.Add(targetResult);
                }
            }

            return result;
        }

        /// <summary>
        /// Sets up the <see cref="Validator{TObject}"/>.
        /// </summary>
        public virtual void Setup() => Setup(new InitialTargetBuilder<TObject>(this));

        #endregion
    }
}