// <copyright file="Target.cs" company="Heleonix - Hennadii Lutsyshyn">
// Copyright (c) Heleonix - Hennadii Lutsyshyn. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the repository root for full license information.
// </copyright>

namespace Heleonix.Validation
{
    using System.Collections.ObjectModel;
    using Heleonix.Validation.Internal;

    /// <summary>
    /// Represents the base class for all targets.
    /// </summary>
    public abstract class Target
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Target"/> class.
        /// </summary>
        /// <param name="name">A name of a target.</param>
        protected Target(string name)
        {
            this.Name = name;
        }

        /// <summary>
        /// Gets or sets a name of a target.
        /// </summary>
        public virtual string Name { get; set; }

        /// <summary>
        /// Gets rules.
        /// </summary>
        public virtual ObservableCollection<Rule> Rules { get; } = new ObservableCollection<Rule>();

        /// <summary>
        /// When overridden in a derived class, gets a value to validate.
        /// </summary>
        /// <param name="context">A context of a target.</param>
        /// <returns>A value to validate.</returns>
        public abstract object GetValue(TargetContext context);

        /// <summary>
        /// Validates a target.
        /// </summary>
        /// <param name="context">A context of a target.</param>
        /// <exception cref="ArgumentNullException">
        /// The <paramref name="context"/> is <see langword="null"/>.
        /// </exception>
        /// <returns>A target result.</returns>
        public virtual TargetResult Validate(TargetContext context)
        {
            Throw<ArgumentNullException>.IfNull(context, nameof(context));

            context.Target = this;

            var result = this.CreateResult(context);

            if (result == null)
            {
                return null;
            }

            foreach (var rule in this.Rules)
            {
                if (!context.ValidatorContext.ContinueValidation)
                {
                    return result;
                }

                var ruleResult = rule?.Validate(new RuleContext(null, context));

                if (ruleResult == null)
                {
                    continue;
                }

                if (!context.ValidatorContext.IgnoreEmptyResults || !ruleResult.IsEmpty())
                {
                    result.RuleResults.Add(ruleResult);
                }
            }

            return result;
        }

        /// <summary>
        /// Creates a target result.
        /// </summary>
        /// <param name="context">A context of a target.</param>
        /// <exception cref="ArgumentNullException">
        /// The <paramref name="context"/> is <see langword="null"/>.
        /// </exception>
        /// <returns>A target result.</returns>
        protected virtual TargetResult CreateResult(TargetContext context)
        {
            Throw<ArgumentNullException>.IfNull(context, nameof(context));

            return new TargetResult(this.Name, this.GetValue(context));
        }
    }
}