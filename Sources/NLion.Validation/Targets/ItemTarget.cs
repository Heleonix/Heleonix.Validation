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
using System.Linq;

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
        /// <exception cref="ArgumentNullException">
        /// The <paramref name="member"/> is <see langword="null"/>.
        /// </exception>
        protected ItemTarget(string name, Func<object, IEnumerable> member,
            Func<IEnumerable, TargetContext, IEnumerable> itemsSelector)
            : base(name, member)
        {
            ItemsSelector = itemsSelector ?? ((items, context) => items);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets an items selector.
        /// </summary>
        protected virtual Func<IEnumerable, TargetContext, IEnumerable> ItemsSelector { get; }

        #endregion

        #region Target Members

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

            foreach (var itemTarget in from object item in items select new MemberTarget(Name, ctxt => item))
            {
                foreach (var rule in Rules)
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
        /// Creates an item target result.
        /// </summary>
        /// <param name="context">A context of a target.</param>
        /// <exception cref="ArgumentNullException">
        /// The <paramref name="context"/> is <see langword="null"/>.
        /// </exception>
        /// <returns>An item target result.</returns>
        protected override TargetResult CreateResult(TargetContext context)
        {
            Throw.ArgumentNullException(context == null, nameof(context));

            return new ItemTargetResult(Name);
        }

        #endregion

        #region MemberTarget Members

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
            Throw.ArgumentNullException(context == null, nameof(context));

            return ItemsSelector.Invoke((IEnumerable) base.GetValue(context), context);
        }

        #endregion
    }
}