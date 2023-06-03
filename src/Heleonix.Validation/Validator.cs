// <copyright file="Validator.cs" company="Heleonix - Hennadii Lutsyshyn">
// Copyright (c) Heleonix - Hennadii Lutsyshyn. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the repository root for full license information.
// </copyright>

namespace Heleonix.Validation
{
    using System.Collections.ObjectModel;
    using Heleonix.Validation.Builders;
    using Heleonix.Validation.Internal;

    /// <summary>
    /// Represents the base class for all validators.
    /// </summary>
    /// <typeparam name="TObject">A type of an object to validate.</typeparam>
    public abstract class Validator<TObject> : IValidator
    {
        /// <summary>
        /// Gets a list of <see cref="Target"/>.
        /// </summary>
        public virtual ObservableCollection<Target> Targets { get; } = new ObservableCollection<Target>();

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

            var result = this.CreateResult(context);

            if (result == null)
            {
                return null;
            }

            foreach (var target in this.Targets)
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
        public virtual void Setup() => this.Setup(new InitialTargetBuilder<TObject>(this));

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
    }
}