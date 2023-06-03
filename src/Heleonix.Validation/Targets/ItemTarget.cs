// <copyright file="ItemTarget.cs" company="Heleonix - Hennadii Lutsyshyn">
// Copyright (c) Heleonix - Hennadii Lutsyshyn. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the repository root for full license information.
// </copyright>

namespace Heleonix.Validation.Targets
{
    using System.Collections;
    using Heleonix.Validation.Internal;

    /// <summary>
    /// Represents the target for enumerable items.
    /// </summary>
    public abstract class ItemTarget : MemberTarget
    {
        /// <summary>
        /// Gets or sets an items selector.
        /// </summary>
        private Func<IEnumerable, TargetContext, IEnumerable> itemsSelector;

        /// <summary>
        /// Initializes a new instance of the <see cref="ItemTarget"/> class.
        /// </summary>
        /// <param name="name">A name of a target.</param>
        /// <param name="member">A delegate of an enumerable member to validate items from.</param>
        /// <param name="itemsSelector">A delegate to select items.</param>
        /// <exception cref="ArgumentNullException">
        /// The <paramref name="member"/> is <see langword="null"/>.
        /// </exception>
        protected ItemTarget(
            string name,
            Func<object, IEnumerable> member,
            Func<IEnumerable, TargetContext, IEnumerable> itemsSelector)
            : base(name, member)
        {
            this.itemsSelector = itemsSelector ?? ((items, context) => items);
        }

        /// <summary>
        /// Gets or sets an items selector.
        /// </summary>
        /// <exception cref="ArgumentNullException">The <see langword="value"/> is <see langword="null"/>.</exception>
        protected virtual Func<IEnumerable, TargetContext, IEnumerable> ItemsSelector
        {
            get
            {
                return this.itemsSelector;
            }

            set
            {
                Throw<ArgumentNullException>.IfNull(value, nameof(value));
                this.itemsSelector = value;
            }
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

            var result = this.CreateResult(context) as ItemTargetResult;

            if (result == null)
            {
                return null;
            }

            var items = this.GetValue(context) as IEnumerable;

            if (items == null)
            {
                return null;
            }

            foreach (var itemTarget in from object item in items select new MemberTarget(this.Name, ctxt => item))
            {
                foreach (var rule in this.Rules)
                {
                    itemTarget.Rules.Add(rule);
                }

                var itemTargetResult = itemTarget.Validate(new TargetContext(null, context.ValidatorContext));

                if (itemTargetResult == null)
                {
                    continue;
                }

                if (!context.ValidatorContext.IgnoreEmptyResults || !itemTargetResult.IsEmpty())
                {
                    result.ItemTargetResults.Add(itemTargetResult);
                }
            }

            return result;
        }

        /// <summary>
        /// Gets selected target items.
        /// </summary>
        /// <param name="context">A context of a target.</param>
        /// <exception cref="ArgumentNullException">
        /// The <paramref name="context"/> is <see langword="null"/>.
        /// </exception>
        /// <returns>Selected target items.</returns>
        public override object GetValue(TargetContext context)
        {
            Throw<ArgumentNullException>.IfNull(context, nameof(context));

            return this.ItemsSelector.Invoke((IEnumerable)base.GetValue(context), context);
        }

        /// <summary>
        /// Creates an item target result.
        /// </summary>
        /// <param name="context">A context of a target.</param>
        /// <exception cref="ArgumentNullException">
        /// The <paramref name="context"/> is <see langword="null"/>.
        /// </exception>
        /// <returns>An item target result.</returns>
        protected override TargetResult CreateResult(TargetContext context)
        {
            Throw<ArgumentNullException>.IfNull(context, nameof(context));

            return new ItemTargetResult(this.Name);
        }
    }
}