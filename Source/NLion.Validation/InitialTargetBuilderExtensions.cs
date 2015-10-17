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
using System.Linq.Expressions;
using NLion.Validation.Builders;
using NLion.Validation.Targets;

namespace NLion.Validation
{
    /// <summary>
    /// Represents extensions for a target builder.
    /// </summary>
    public static class InitialTargetBuilderExtensions
    {
        #region Methods

        /// <summary>
        /// Creates a member's target.
        /// </summary>
        /// <typeparam name="TObject">A type of an object to validate.</typeparam>
        /// <typeparam name="TMember">A type of a member to target on.</typeparam>
        /// <param name="builder">An initial target builder.</param>
        /// <param name="memberExpression">An expression of a member to validate.</param>
        /// <exception cref="ArgumentNullException">
        /// The <paramref name="builder"/> is <see langword="null"/>.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// The <paramref name="memberExpression" /> is <see langword="null" />.
        /// </exception>
        /// <returns>A final target builder.</returns>
        public static IFinalTargetBuilder<TObject, TMember> Member<TObject, TMember>(
            this IInitialTargetBuilder<TObject> builder, Expression<Func<TObject, TMember>> memberExpression)
        {
            Contract.Requires<ArgumentNullException>(builder != null);
            Contract.Requires<ArgumentNullException>(memberExpression != null);

            var container = new TargetContainer(new MemberTarget<TObject, TMember>(
                ReflectionHelper.GetMemberName(memberExpression), memberExpression.Compile()));

            builder.Validator.TargetContainers.Add(container);

            return new FinalTargetBuilder<TObject, TMember>(builder.Validator, container);
        }

        /// <summary>
        /// Creates an enumerable item target.
        /// </summary>
        /// <typeparam name="TObject">A type of an object to validate.</typeparam>
        /// <typeparam name="TEnumerable">A type of an enumerable to target on.</typeparam>
        /// <typeparam name="TItem">A type of an item to target on.</typeparam>
        /// <param name="builder">An initial target builder.</param>
        /// <param name="enumerableExpression">An expression of an enumerable to validate item from.</param>
        /// <param name="itemSelector">An expression to select an item.</param>
        /// <exception cref="ArgumentNullException">
        /// The <paramref name="builder"/> is <see langword="null"/>.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// The <paramref name="enumerableExpression" /> is <see langword="null" />.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// The <paramref name="itemSelector" /> is <see langword="null" />.
        /// </exception>
        /// <returns>A final target builder.</returns>
        public static IFinalTargetBuilder<TObject, TItem> OneOf<TObject, TEnumerable, TItem>(
            this IInitialTargetBuilder<TObject> builder,
            Expression<Func<TObject, TEnumerable>> enumerableExpression, Func<TEnumerable, TItem> itemSelector)
            where TEnumerable : IEnumerable<TItem>
        {
            Contract.Requires<ArgumentNullException>(builder != null);
            Contract.Requires<ArgumentNullException>(enumerableExpression != null);
            Contract.Requires<ArgumentNullException>(itemSelector != null);

            Func<TEnumerable, IEnumerable<TItem>> itemsSelector = items => new[] {itemSelector(items)};

            var container =
                new TargetContainer(new ItemTarget<TObject, TEnumerable, TItem>(
                    ReflectionHelper.GetMemberName(enumerableExpression),
                    enumerableExpression.Compile(), itemsSelector));

            builder.Validator.TargetContainers.Add(container);

            return new FinalTargetBuilder<TObject, TItem>(builder.Validator, container);
        }

        /// <summary>
        /// Creates enumerable items target.
        /// </summary>
        /// <typeparam name="TObject">A type of an object to validate.</typeparam>
        /// <typeparam name="TEnumerable">A type of an enumerable to target on.</typeparam>
        /// <typeparam name="TItem">A type of a items to target on.</typeparam>
        /// <param name="builder">An initial target builder.</param>
        /// <param name="enumerableExpression">An expression of an enumerable to validate item from.</param>
        /// <param name="itemsSelector">An expression to select items.</param>
        /// <exception cref="ArgumentNullException">
        /// The <paramref name="builder"/> is <see langword="null"/>.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// The <paramref name="enumerableExpression" /> is <see langword="null" />.
        /// </exception>
        /// <returns>A final target builder.</returns>
        public static IFinalTargetBuilder<TObject, TItem> EachOf<TObject, TEnumerable, TItem>(
            this IInitialTargetBuilder<TObject> builder,
            Expression<Func<TObject, TEnumerable>> enumerableExpression,
            Func<TEnumerable, IEnumerable<TItem>> itemsSelector = null)
            where TEnumerable : IEnumerable<TItem>
        {
            Contract.Requires<ArgumentNullException>(builder != null);
            Contract.Requires<ArgumentNullException>(enumerableExpression != null);

            var container =
                new TargetContainer(new ItemTarget<TObject, TEnumerable, TItem>(
                    ReflectionHelper.GetMemberName(enumerableExpression),
                    enumerableExpression.Compile(), itemsSelector));

            builder.Validator.TargetContainers.Add(container);

            return new FinalTargetBuilder<TObject, TItem>(builder.Validator, container);
        }

        /// <summary>
        /// Creates an object target.
        /// </summary>
        /// <typeparam name="TObject">A type of an object to validate.</typeparam>
        /// <param name="builder">An initial target builder.</param>
        /// <exception cref="ArgumentNullException">
        /// The <paramref name="builder"/> is <see langword="null"/>.
        /// </exception>
        /// <returns>A final target builder.</returns>
        public static IFinalTargetBuilder<TObject, TObject> Object<TObject>(this IInitialTargetBuilder<TObject> builder)
        {
            Contract.Requires<ArgumentNullException>(builder != null);

            var container = new TargetContainer(new ObjectTarget(string.Empty));

            builder.Validator.TargetContainers.Add(container);

            return new FinalTargetBuilder<TObject, TObject>(builder.Validator, container);
        }

        /// <summary>
        /// Creates a target.
        /// </summary>
        /// <typeparam name="TObject">A type of an object to validate.</typeparam>
        /// <typeparam name="TTarget">A type of a target.</typeparam>
        /// <param name="builder">An initial target builder.</param>
        /// <param name="target">A target to validate.</param>
        /// <exception cref="ArgumentNullException">
        /// The <paramref name="builder"/> is <see langword="null"/>.
        /// </exception>
        /// <returns>A final target builder.</returns>
        public static IFinalTargetBuilder<TObject, TTarget> Target<TObject, TTarget>(
            this IInitialTargetBuilder<TObject> builder, Target target)
        {
            Contract.Requires<ArgumentNullException>(builder != null);

            var container = new TargetContainer(target);

            builder.Validator.TargetContainers.Add(container);

            return new FinalTargetBuilder<TObject, TTarget>(builder.Validator, container);
        }

        #endregion
    }
}