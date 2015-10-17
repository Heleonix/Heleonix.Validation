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
using System.Collections.ObjectModel;
using System.Diagnostics.Contracts;
using NLion.Validation.Builders;

namespace NLion.Validation
{
    /// <summary>
    /// Represents a base class for all validators.
    /// </summary>
    /// <typeparam name="TObject">A type of an object to validate.</typeparam>
    public abstract class Validator<TObject> : IValidator
    {
        #region Methods

        /// <summary>
        /// Creates a validator result.
        /// </summary>
        /// <param name="context">A validation context.</param>
        /// <returns>A validator result.</returns>
        public virtual ValidatorResult CreateResult(ValidationContext context) => new ValidatorResult();

        /// <summary>
        /// When overridden in a derived class, builds a validator.
        /// </summary>
        /// <param name="builder">
        /// An instance of the <see cref="IInitialTargetBuilder{TObject}"/> to build a validator.
        /// </param>
        protected abstract void Build(IInitialTargetBuilder<TObject> builder);

        #endregion

        #region Properties

        /// <summary>
        /// Gets targets containers.
        /// </summary>
        public ObservableCollection<TargetContainer> TargetContainers { get; } =
            new ObservableCollection<TargetContainer>();

        #endregion

        #region IValidator Members

        /// <summary>
        /// Validates an object within the specified <paramref name="context"/>.
        /// </summary>
        /// <param name="context">A context to validate an object within.</param>
        /// <exception cref="ArgumentNullException">
        /// The <paramref name="context"/> is <see langword="null"/>.
        /// </exception>
        /// <exception cref="RuleValidationException">Validation was failed in one or more rules.</exception>
        public virtual ValidatorResult Validate(ValidationContext context)
        {
            Contract.Requires<ArgumentNullException>(context != null);

            var result = CreateResult(context);

            if (result == null)
            {
                return null;
            }

            foreach (var container in TargetContainers)
            {
                var targetResult = container?.Target?.Validate(context);

                if (targetResult != null && (!context.IgnoreEmptyResults || targetResult.RuleResults.Count > 0))
                {
                    result.TargetResults.Add(targetResult);
                }
            }

            return result;
        }

        /// <summary>
        /// Builds a validator.
        /// </summary>
        public virtual void Build() => Build(new InitialTargetBuilder<TObject>(this));

        #endregion
    }
}