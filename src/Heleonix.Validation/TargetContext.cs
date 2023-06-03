// <copyright file="TargetContext.cs" company="Heleonix - Hennadii Lutsyshyn">
// Copyright (c) Heleonix - Hennadii Lutsyshyn. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the repository root for full license information.
// </copyright>

namespace Heleonix.Validation
{
    using Heleonix.Validation.Internal;

    /// <summary>
    /// Represents the context of targets.
    /// </summary>
    public class TargetContext
    {
        /// <summary>
        /// Gets or sets a target.
        /// </summary>
        private Target target;

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

            this.target = target;
            this.ValidatorContext = validatorContext;
        }

        /// <summary>
        /// Gets or sets a target.
        /// </summary>
        /// <exception cref="ArgumentNullException">The <see langword="value"/> is <see langword="null"/>.</exception>
        public virtual Target Target
        {
            get
            {
                return this.target;
            }

            set
            {
                Throw<ArgumentNullException>.IfNull(value, nameof(value));
                this.target = value;
            }
        }

        /// <summary>
        /// Gets a context of a validator.
        /// </summary>
        public virtual ValidatorContext ValidatorContext { get; }
    }
}