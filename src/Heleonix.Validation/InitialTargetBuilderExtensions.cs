// <copyright file="InitialTargetBuilderExtensions.cs" company="Heleonix - Hennadii Lutsyshyn">
// Copyright (c) Heleonix - Hennadii Lutsyshyn. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the repository root for full license information.
// </copyright>

namespace Heleonix.Validation
{
    using System.Collections;
    using System.Linq.Expressions;
    using Heleonix.Validation.Builders;
    using Heleonix.Validation.Internal;
    using Heleonix.Validation.Targets;

    /// <summary>
    /// Provides extensions for the <see cref="IInitialTargetBuilder{TObject}"/>.
    /// </summary>
    public static class InitialTargetBuilderExtensions
    {
        /// <summary>
        /// Creates the <see cref="GroupTarget"/>.
        /// </summary>
        /// <typeparam name="TObject">A type of an object to validate.</typeparam>
        /// <param name="builder">The <see cref="IInitialTargetBuilder{TObject}"/>.</param>
        /// <param name="name">A name of a group.</param>
        /// <exception cref="ArgumentNullException">
        /// The <paramref name="builder"/> is <see langword="null"/>.
        /// </exception>
        /// <returns>The <see cref="IFinalGroupTargetBuilder{TObject}"/>.</returns>
        public static IFinalGroupTargetBuilder<TObject> Group<TObject>(
            this IInitialTargetBuilder<TObject> builder, string name)
        {
            Throw<ArgumentNullException>.IfNull(builder, nameof(builder));

            var target = new GroupTarget(name);

            builder.Validator.Targets.Add(target);

            return new FinalGroupTargetBuilder<TObject>(builder.Validator, target);
        }

        /// <summary>
        /// Creates the <see cref="MemberTarget"/>.
        /// </summary>
        /// <typeparam name="TObject">A type of an object to validate.</typeparam>
        /// <typeparam name="TMember">A type of a member.</typeparam>
        /// <param name="builder">The <see cref="IInitialTargetBuilder{TObject}"/>.</param>
        /// <param name="memberExpression">An expression of a member to validate.</param>
        /// <param name="name">A name of a member.</param>
        /// <exception cref="ArgumentNullException">
        /// The <paramref name="builder"/> is <see langword="null"/>.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// The <paramref name="memberExpression"/> is <see langword="null"/>.
        /// </exception>
        /// <returns>The <see cref="IFinalTargetBuilder{TObject,TTarget}"/>.</returns>
        public static IFinalTargetBuilder<TObject, TMember> Member<TObject, TMember>(
            this IInitialTargetBuilder<TObject> builder,
            Expression<Func<TObject, TMember>> memberExpression,
            string name = null)
        {
            Throw<ArgumentNullException>.IfNull(builder, nameof(builder));
            Throw<ArgumentNullException>.IfNull(memberExpression, nameof(memberExpression));

            var member = memberExpression.Compile();

            name = name ?? ReflectionHelper.GetMemberName(memberExpression);

            return builder.Target<TObject, TMember>(new MemberTarget(name, obj => member((TObject)obj)));
        }

        /// <summary>
        /// Creates the <see cref="AnyOfTarget"/>.
        /// </summary>
        /// <typeparam name="TObject">A type of an object to validate.</typeparam>
        /// <typeparam name="TEnumerable">A type of an enumerable target.</typeparam>
        /// <typeparam name="TItem">A type of items.</typeparam>
        /// <param name="builder">The <see cref="IInitialTargetBuilder{TObject}"/>.</param>
        /// <param name="memberExpression">An expression of an enumerable member to validate items from.</param>
        /// <param name="itemsSelector">A delegate to select items.</param>
        /// <param name="name">A name of an enumerable target.</param>
        /// <exception cref="ArgumentNullException">
        /// The <paramref name="builder"/> is <see langword="null"/>.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// The <paramref name="memberExpression"/> is <see langword="null"/>.
        /// </exception>
        /// <returns>The <see cref="IFinalTargetBuilder{TObject,TTarget}"/>.</returns>
        public static IFinalTargetBuilder<TObject, TItem> AnyOf<TObject, TEnumerable, TItem>(
            this IInitialTargetBuilder<TObject> builder,
            Expression<Func<TObject, TEnumerable>> memberExpression,
            Func<TEnumerable, TargetContext, IEnumerable<TItem>> itemsSelector = null,
            string name = null)
            where TEnumerable : IEnumerable<TItem>
        {
            Throw<ArgumentNullException>.IfNull(builder, nameof(builder));
            Throw<ArgumentNullException>.IfNull(memberExpression, nameof(memberExpression));

            var member = memberExpression.Compile();
            var selector = itemsSelector != null
                ? (items, context) => itemsSelector((TEnumerable)items, context)
                : (Func<IEnumerable, TargetContext, IEnumerable>)null;

            name = name ?? ReflectionHelper.GetMemberName(memberExpression);

            return builder.Target<TObject, TItem>(new AnyOfTarget(name, obj => member((TObject)obj), selector));
        }

        /// <summary>
        /// Creates the <see cref="EachOfTarget"/>.
        /// </summary>
        /// <typeparam name="TObject">A type of an object to validate.</typeparam>
        /// <typeparam name="TEnumerable">A type of an enumerable target.</typeparam>
        /// <typeparam name="TItem">A type of items.</typeparam>
        /// <param name="builder">The <see cref="IInitialTargetBuilder{TObject}"/>.</param>
        /// <param name="memberExpression">An expression of an enumerable member to validate items from.</param>
        /// <param name="itemsSelector">A delegate to select items.</param>
        /// <param name="name">A name of an enumerable target.</param>
        /// <exception cref="ArgumentNullException">
        /// The <paramref name="builder"/> is <see langword="null"/>.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// The <paramref name="memberExpression"/> is <see langword="null"/>.
        /// </exception>
        /// <returns>The <see cref="IFinalTargetBuilder{TObject,TTarget}"/>.</returns>
        public static IFinalTargetBuilder<TObject, TItem> EachOf<TObject, TEnumerable, TItem>(
            this IInitialTargetBuilder<TObject> builder,
            Expression<Func<TObject, TEnumerable>> memberExpression,
            Func<TEnumerable, TargetContext, IEnumerable<TItem>> itemsSelector = null,
            string name = null)
            where TEnumerable : IEnumerable<TItem>
        {
            Throw<ArgumentNullException>.IfNull(builder, nameof(builder));
            Throw<ArgumentNullException>.IfNull(memberExpression, nameof(memberExpression));

            var member = memberExpression.Compile();
            var selector = itemsSelector != null
                ? (items, context) => itemsSelector((TEnumerable)items, context)
                : (Func<IEnumerable, TargetContext, IEnumerable>)null;

            name = name ?? ReflectionHelper.GetMemberName(memberExpression);

            return builder.Target<TObject, TItem>(new EachOfTarget(
                name,
                member != null ? obj => member((TObject)obj) : (Func<object, IEnumerable>)null,
                selector));
        }

        /// <summary>
        /// Creates the <see cref="ObjectTarget"/>.
        /// </summary>
        /// <typeparam name="TObject">A type of an object to validate.</typeparam>
        /// <param name="builder">The <see cref="IInitialTargetBuilder{TObject}"/>.</param>
        /// <exception cref="ArgumentNullException">
        /// The <paramref name="builder"/> is <see langword="null"/>.
        /// </exception>
        /// <returns>The <see cref="IFinalTargetBuilder{TObject,TTarget}"/>.</returns>
        public static IFinalTargetBuilder<TObject, TObject> Object<TObject>(this IInitialTargetBuilder<TObject> builder)
        {
            Throw<ArgumentNullException>.IfNull(builder, nameof(builder));

            return builder.Target<TObject, TObject>(new ObjectTarget(string.Empty));
        }

        /// <summary>
        /// Adds a target.
        /// </summary>
        /// <typeparam name="TObject">A type of an object to validate.</typeparam>
        /// <typeparam name="TTarget">A type of a target.</typeparam>
        /// <param name="builder">The <see cref="IInitialTargetBuilder{TObject}"/>.</param>
        /// <param name="target">A target to validate.</param>
        /// <exception cref="ArgumentNullException">
        /// The <paramref name="builder"/> is <see langword="null"/>.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// The <paramref name="target"/> is <see langword="null"/>.
        /// </exception>
        /// <returns>The <see cref="IFinalTargetBuilder{TObject,TTarget}"/>.</returns>
        public static IFinalTargetBuilder<TObject, TTarget> Target<TObject, TTarget>(
            this IInitialTargetBuilder<TObject> builder, Target target)
        {
            Throw<ArgumentNullException>.IfNull(builder, nameof(builder));
            Throw<ArgumentNullException>.IfNull(target, nameof(target));

            builder.Validator.Targets.Add(target);

            return new FinalTargetBuilder<TObject, TTarget>(builder.Validator, target);
        }
    }
}