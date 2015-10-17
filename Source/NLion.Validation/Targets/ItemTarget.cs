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
using System.Collections.Generic;
using System.Diagnostics.Contracts;

namespace NLion.Validation.Targets
{
    /// <summary>
    /// Represents a target for an enumerable item.
    /// </summary>
    /// <typeparam name="TObject">A type of an object to create a target for.</typeparam>
    /// <typeparam name="TEnumerable">A type of an enumerable to target on.</typeparam>
    /// <typeparam name="TItem">A type of an item to target on.</typeparam>
    public class ItemTarget<TObject, TEnumerable, TItem> : MemberTarget<TObject, TEnumerable>
        where TEnumerable : IEnumerable<TItem>
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ItemTarget{TObject,TEnumerable,TItem}"/> class.
        /// </summary>
        /// <param name="name">A name of a target.</param>
        /// <param name="enumerableMember">A delegate of an enumerable member to validate item from.</param>
        /// <param name="itemsSelector">An expression to select a items.</param>
        /// <exception cref="ArgumentNullException">
        /// A <paramref name="enumerableMember"/> is <see langword="null"/>.
        /// </exception>
        public ItemTarget(string name, Func<TObject, TEnumerable> enumerableMember,
            Func<TEnumerable, IEnumerable<TItem>> itemsSelector) : base(name, enumerableMember)
        {
            ItemsSelector = itemsSelector ?? (items => items);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets an item selector.
        /// </summary>
        protected Func<TEnumerable, IEnumerable<TItem>> ItemsSelector { get; }

        #endregion

        #region Target Members

        /// <summary>
        /// Validates a target.
        /// </summary>
        /// <param name="context">A validation context.</param>
        /// <exception cref="ArgumentNullException">
        /// The <paramref name="context"/> is <see langword="null"/>.
        /// </exception>
        /// <exception cref="RuleValidationException">Validation was failed in one or more rules.</exception>
        /// <returns>A target result.</returns>
        public override TargetResult Validate(ValidationContext context)
        {
            Contract.Requires<ArgumentNullException>(context != null);

            var result = CreateResult(context) as ItemTargetResult;

            if (result == null)
            {
                return null;
            }

            var items = ItemsSelector((TEnumerable) GetValue(context));

            if (items == null)
            {
                return null;
            }

            foreach (var item in items)
            {
                var itemTarget = new MemberTarget<TObject, TItem>(Name, obj => item);

                var itemTargetResult = itemTarget.CreateResult(context);

                if (itemTargetResult == null)
                {
                    continue;
                }

                foreach (var container in RuleContainers)
                {
                    try
                    {
                        var ruleResult = container?.Rule?.Validate(new RuleValidationContext(context, itemTarget));

                        if (ruleResult != null && (!context.IgnoreEmptyResults
                                                   || ruleResult.ValueResults.Count > 0))
                        {
                            itemTargetResult.RuleResults.Add(ruleResult);
                        }
                    }
                    catch (RuleValidationException) when (context.ContinueOnFailedValidation)
                    {
                    }
                }

                result.ItemTargetResults.Add(itemTargetResult);
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
            Contract.Requires<ArgumentNullException>(context != null);

            return new ItemTargetResult(Name, GetValue(context));
        }

        #endregion
    }
}