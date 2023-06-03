// <copyright file="RuleContext.cs" company="Heleonix - Hennadii Lutsyshyn">
// Copyright (c) Heleonix - Hennadii Lutsyshyn. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the repository root for full license information.
// </copyright>

namespace Heleonix.Validation
{
    using Heleonix.Validation.Internal;

    /// <summary>
    /// Represents the context of rules.
    /// </summary>
    public class RuleContext
    {
        /// <summary>
        /// Gets or sets a rule.
        /// </summary>
        private Rule rule;

        /// <summary>
        /// Initializes a new instance of the <see cref="RuleContext"/> class.
        /// </summary>
        /// <param name="rule">A rule.</param>
        /// <param name="targetContext">A context of a target.</param>
        /// <exception cref="ArgumentNullException">
        /// The <paramref name="targetContext"/> is <see langword="null"/>.
        /// </exception>
        public RuleContext(Rule rule, TargetContext targetContext)
        {
            Throw<ArgumentNullException>.IfNull(targetContext, nameof(targetContext));

            this.rule = rule;
            this.TargetContext = targetContext;
        }

        /// <summary>
        /// Gets or sets a rule.
        /// </summary>
        /// <exception cref="ArgumentNullException">The <see langword="value"/> is <see langword="null"/>.</exception>
        public virtual Rule Rule
        {
            get
            {
                return this.rule;
            }

            set
            {
                Throw<ArgumentNullException>.IfNull(value, nameof(value));
                this.rule = value;
            }
        }

        /// <summary>
        /// Gets a context of a target.
        /// </summary>
        public virtual TargetContext TargetContext { get; }
    }
}