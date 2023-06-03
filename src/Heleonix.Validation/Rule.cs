// <copyright file="Rule.cs" company="Heleonix - Hennadii Lutsyshyn">
// Copyright (c) Heleonix - Hennadii Lutsyshyn. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the repository root for full license information.
// </copyright>

namespace Heleonix.Validation
{
    using System.Collections.ObjectModel;
    using Heleonix.Validation.Internal;

    /// <summary>
    /// Represents the base class for all rules.
    /// </summary>
    public abstract class Rule
    {
        /// <summary>
        /// Gets a name of a rule.
        /// </summary>
        public virtual string Name
        {
            get
            {
                var name = this.GetType().Name;

                if (name.EndsWith(nameof(Rule), StringComparison.OrdinalIgnoreCase))
                {
                    name = name.Remove(name.Length - 4, 4);
                }

                return name;
            }
        }

        /// <summary>
        /// Gets value results.
        /// </summary>
        public virtual ObservableCollection<ValueResult> ValueResults { get; }
            = new ObservableCollection<ValueResult>();

        /// <summary>
        /// Performs validation.
        /// </summary>
        /// <param name="context">A context of a rule.</param>
        /// <exception cref="ArgumentNullException">
        /// The <paramref name="context"/> is <see langword="null"/>.
        /// </exception>
        /// <returns>A rule result.</returns>
        public virtual RuleResult Validate(RuleContext context)
        {
            Throw<ArgumentNullException>.IfNull(context, nameof(context));

            context.Rule = this;

            var value = this.Execute(context);

            var result = this.CreateResult(context, value);

            if (result == null)
            {
                return null;
            }

            var valueResults = this.SelectValueResults(context, value);

            if (valueResults == null)
            {
                return result;
            }

            foreach (var valueResult in valueResults.Where(valueResult => valueResult != null))
            {
                result.ValueResults.Add(valueResult);
            }

            return result;
        }

        /// <summary>
        /// When overridden in a derived class, executes validation.
        /// </summary>
        /// <param name="context">A context of a rule.</param>
        /// <returns>A value of a rule.</returns>
        protected abstract object Execute(RuleContext context);

        /// <summary>
        /// Creates a rule result.
        /// </summary>
        /// <param name="context">A context of a rule.</param>
        /// <param name="value">A value of a rule.</param>
        /// <exception cref="ArgumentNullException">
        /// The <paramref name="context"/> is <see langword="null"/>.
        /// </exception>
        /// <returns>A rule result.</returns>
        protected virtual RuleResult CreateResult(RuleContext context, object value)
        {
            Throw<ArgumentNullException>.IfNull(context, nameof(context));

            return new RuleResult(this.Name, value);
        }

        /// <summary>
        /// Selects value results.
        /// </summary>
        /// <param name="context">A context of a rule.</param>
        /// <param name="value">A value of a rule.</param>
        /// <exception cref="ArgumentNullException">
        /// The <paramref name="context"/> is <see langword="null"/>.
        /// </exception>
        /// <returns>Selected value results.</returns>
        protected virtual IEnumerable<ValueResult> SelectValueResults(RuleContext context, object value)
        {
            Throw<ArgumentNullException>.IfNull(context, nameof(context));

            return from r in this.ValueResults
                   where r != null && ((r.MatchValue == null && value == null)
                                       || (r.MatchValue != null && r.MatchValue.Equals(value)))
                   select r;
        }
    }
}