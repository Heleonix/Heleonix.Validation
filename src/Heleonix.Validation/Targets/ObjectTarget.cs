// <copyright file="ObjectTarget.cs" company="Heleonix - Hennadii Lutsyshyn">
// Copyright (c) Heleonix - Hennadii Lutsyshyn. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the repository root for full license information.
// </copyright>

namespace Heleonix.Validation.Targets
{
    using Heleonix.Validation.Internal;

    /// <summary>
    /// Represents the target for an object.
    /// </summary>
    public class ObjectTarget : Target
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ObjectTarget"/> class.
        /// </summary>
        /// <param name="name">A name of a target.</param>
        public ObjectTarget(string name)
            : base(name)
        {
        }

        /// <summary>
        /// Returns an object to validate.
        /// </summary>
        /// <param name="context">A context of a target.</param>
        /// <exception cref="ArgumentNullException">
        /// The <paramref name="context"/> is <see langword="null"/>.
        /// </exception>
        /// <returns>An object to validate.</returns>
        public override object GetValue(TargetContext context)
        {
            Throw<ArgumentNullException>.IfNull(context, nameof(context));

            return context.ValidatorContext.Object;
        }

        /// <summary>
        /// Creates a target result with <see langword="null"/> value, because a whole object is the value.
        /// </summary>
        /// <param name="context">A context of a target.</param>
        /// <returns>A target result.</returns>
        protected override TargetResult CreateResult(TargetContext context) => new TargetResult(this.Name, null);
    }
}