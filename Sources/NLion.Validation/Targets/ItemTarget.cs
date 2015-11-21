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
using System.Collections;

namespace NLion.Validation.Targets
{
    /// <summary>
    /// Represents the target for enumerable items.
    /// </summary>
    public abstract class ItemTarget : MemberTarget
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ItemTarget"/> class.
        /// </summary>
        /// <param name="name">A name of a target.</param>
        /// <param name="member">A delegate of an enumerable member to validate items from.</param>
        /// <param name="itemsSelector">A delegate to select items.</param>
        protected ItemTarget(string name, Func<object, IEnumerable> member,
            Func<IEnumerable, ValidationContext, IEnumerable> itemsSelector)
            : base(name, member)
        {
            ItemsSelector = itemsSelector ?? ((items, context) => items);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets an items selector.
        /// </summary>
        protected Func<IEnumerable, ValidationContext, IEnumerable> ItemsSelector { get; set; }

        #endregion

        #region Target Members

        /// <summary>
        /// Validates a target.
        /// </summary>
        /// <param name="context">A validation context.</param>
        /// <exception cref="ArgumentNullException">
        /// The <paramref name="context"/> is <see langword="null"/>.
        /// </exception>
        /// <returns>A target result.</returns>
        public override TargetResult Validate(ValidationContext context)
        {
            Throw.ArgumentNullException(context == null, nameof(context));

            var result = CreateResult(context) as ItemTargetResult;

            if (result == null)
            {
                return null;
            }

            var items = GetValue(context) as IEnumerable;

            if (items == null)
            {
                return null;
            }

            foreach (var item in items)
            {
                var itemTarget = new MemberTarget(Name, ctxt => item);

                var itemTargetResult = itemTarget.CreateResult(context);

                if (itemTargetResult == null)
                {
                    continue;
                }

                foreach (var container in RuleContainers)
                {
                    var ruleResult = container?.Rule?.Validate(new RuleValidationContext(context, itemTarget));

                    if (ruleResult != null && (!context.IgnoreEmptyResults || !ruleResult.IsEmpty()))
                    {
                        itemTargetResult.RuleResults.Add(ruleResult);
                    }
                }

                if (!context.IgnoreEmptyResults || !itemTargetResult.IsEmpty())
                {
                    result.ItemTargetResults.Add(itemTargetResult);
                }
            }

            return result;
        }

        /// <summary>
        /// Creates an item target result.
        /// </summary>
        /// <param name="context">A validation context.</param>
        /// <exception cref="ArgumentNullException">
        /// The <paramref name="context"/> is <see langword="null"/>.
        /// </exception>
        /// <returns>An item target result.</returns>
        public override TargetResult CreateResult(ValidationContext context)
        {
            Throw.ArgumentNullException(context == null, nameof(context));

            return new ItemTargetResult(Name, GetValue(context));
        }

        #endregion

        #region MemberTarget Members

        /// <summary>
        /// Gets selected target items.
        /// </summary>
        /// <param name="context">A validation context.</param>
        /// <exception cref="ArgumentNullException">
        /// The <paramref name="context"/> is <see langword="null"/>.
        /// </exception>
        /// <returns>Selected target items.</returns>
        public override object GetValue(ValidationContext context)
        {
            Throw.ArgumentNullException(context == null, nameof(context));

            return ItemsSelector?.Invoke((IEnumerable) base.GetValue(context), context);
        }

        #endregion
    }
}