// <copyright file="IfNotTarget.cs" company="Heleonix - Hennadii Lutsyshyn">
// Copyright (c) Heleonix - Hennadii Lutsyshyn. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the repository root for full license information.
// </copyright>

namespace Heleonix.Validation.Targets
{
    using Heleonix.Validation.Internal;

    /// <summary>
    /// Represents the opposite conditional target.
    /// </summary>
    public class IfNotTarget : ConditionalTarget
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="IfNotTarget"/> class.
        /// </summary>
        /// <param name="target">A target to wrap with the <paramref name="condition"/>.</param>
        /// <param name="condition">A condition to perform validation.</param>
        /// <exception cref="ArgumentNullException">
        /// The <paramref name="target"/> is <see langword="null"/>.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// The <paramref name="condition"/> is <see langword="null"/>.
        /// </exception>
        public IfNotTarget(Target target, Predicate<TargetContext> condition)
            : base(target, condition)
        {
        }

        /// <summary>
        /// Validates a target.
        /// </summary>
        /// <param name="context">A context of a target.</param>
        /// <exception cref="ArgumentNullException">
        /// The <paramref name="context"/> is <see langword="null"/>.
        /// </exception>
        /// <returns>A target result.</returns>
        public override TargetResult Validate(TargetContext context)
        {
            Throw<ArgumentNullException>.IfNull(context, nameof(context));

            return !this.Condition.Invoke(context) ? this.Target.Validate(context) : null;
        }
    }
}