// <copyright file="GroupTarget.cs" company="Heleonix - Hennadii Lutsyshyn">
// Copyright (c) Heleonix - Hennadii Lutsyshyn. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the repository root for full license information.
// </copyright>

namespace Heleonix.Validation.Targets
{
    using System.Collections.ObjectModel;
    using Heleonix.Validation.Internal;

    /// <summary>
    /// Represents the group target.
    /// </summary>
    public class GroupTarget : Target
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GroupTarget"/> class.
        /// </summary>
        /// <param name="name">A name of a group.</param>
        public GroupTarget(string name)
            : base(name)
        {
        }

        /// <summary>
        /// Gets targets.
        /// </summary>
        public virtual ObservableCollection<Target> Targets { get; } = new ObservableCollection<Target>();

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

            context.Target = this;

            var result = this.CreateResult(context) as GroupTargetResult;

            if (result == null)
            {
                return null;
            }

            foreach (var target in this.Targets)
            {
                if (!context.ValidatorContext.ContinueValidation)
                {
                    return result;
                }

                var targetResult = target?.Validate(new TargetContext(null, context.ValidatorContext));

                if (targetResult == null)
                {
                    continue;
                }

                if (!context.ValidatorContext.IgnoreEmptyResults || !targetResult.IsEmpty())
                {
                    result.TargetResults.Add(targetResult);
                }
            }

            return result;
        }

        /// <summary>
        /// Gets a value to validate.
        /// </summary>
        /// <param name="context">A context of a target.</param>
        /// <exception cref="ArgumentNullException">
        /// The <paramref name="context"/> is <see langword="null"/>.
        /// </exception>
        /// <returns>A value to validate.</returns>
        public override object GetValue(TargetContext context)
        {
            Throw<ArgumentNullException>.IfNull(context, nameof(context));

            return this.Targets;
        }

        /// <summary>
        /// Creates a group target result.
        /// </summary>
        /// <param name="context">A context of a target.</param>
        /// <exception cref="ArgumentNullException">
        /// The <paramref name="context"/> is <see langword="null"/>.
        /// </exception>
        /// <returns>A group target result.</returns>
        protected override TargetResult CreateResult(TargetContext context)
        {
            Throw<ArgumentNullException>.IfNull(context, nameof(context));

            return new GroupTargetResult(this.Name);
        }
    }
}