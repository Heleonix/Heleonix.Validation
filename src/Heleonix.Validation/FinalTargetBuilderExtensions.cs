// <copyright file="FinalTargetBuilderExtensions.cs" company="Heleonix - Hennadii Lutsyshyn">
// Copyright (c) Heleonix - Hennadii Lutsyshyn. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the repository root for full license information.
// </copyright>

namespace Heleonix.Validation
{
    using Heleonix.Validation.Builders;
    using Heleonix.Validation.Internal;
    using Heleonix.Validation.Targets;

    /// <summary>
    /// Provides extensions for the <see cref="IFinalTargetBuilder{TObject,TTarget}"/>.
    /// </summary>
    public static class FinalTargetBuilderExtensions
    {
        /// <summary>
        /// Moves a target to a group with the specified <paramref name="name"/>.
        /// </summary>
        /// <typeparam name="TObject">A type of an object to validate.</typeparam>
        /// <typeparam name="TTarget">A type of a built target.</typeparam>
        /// <param name="builder">The <see cref="IFinalTargetBuilder{TObject,TTarget}"/>.</param>
        /// <param name="name">A name of a group.</param>
        /// <exception cref="ArgumentNullException">
        /// The <paramref name="builder"/> is <see langword="null"/>.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// A group with the specified <paramref name="name"/> was not found.
        /// </exception>
        /// <returns>The <see cref="IInitialRuleBuilder{TObject,TTarget}"/>.</returns>
        public static IInitialRuleBuilder<TObject, TTarget> InGroup<TObject, TTarget>(
            this IFinalTargetBuilder<TObject, TTarget> builder, string name)
        {
            Throw<ArgumentNullException>.IfNull(builder, nameof(builder));

            var group = (from t in builder.Validator.Targets
                         where t is GroupTarget tgt && StringComparer.Ordinal.Compare(tgt.Name, name) == 0
                         select t as GroupTarget).FirstOrDefault();

            Throw<ArgumentNullException>.IfNull(group, nameof(group));

            var target = builder.Target;

#pragma warning disable S2259 // Null pointers should not be dereferenced
            group.Targets.Add(target);
#pragma warning restore S2259 // Null pointers should not be dereferenced

            builder.Validator.Targets.Remove(target);

            return new InitialRuleBuilder<TObject, TTarget>(builder.Validator, target);
        }

        /// <summary>
        /// Creates the <see cref="IfTarget"/>.
        /// </summary>
        /// <typeparam name="TObject">A type of an object to validate.</typeparam>
        /// <typeparam name="TTarget">A type of a built target.</typeparam>
        /// <param name="builder">The <see cref="IFinalTargetBuilder{TObject,TTarget}"/>.</param>
        /// <param name="condition">A condition to perform validation.</param>
        /// <exception cref="ArgumentNullException">
        /// The <paramref name="builder"/> is <see langword="null"/>.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// The <paramref name="condition"/> is <see langword="null"/>.
        /// </exception>
        /// <returns>The <see cref="IFinalTargetBuilder{TObject,TTarget}"/> to build true conditions.</returns>
        public static IFinalTargetBuilder<TObject, TTarget> If<TObject, TTarget>(
            this IFinalTargetBuilder<TObject, TTarget> builder, Predicate<TargetContext> condition)
        {
            Throw<ArgumentNullException>.IfNull(builder, nameof(builder));
            Throw<ArgumentNullException>.IfNull(condition, nameof(condition));

            builder.Target = new IfTarget(builder.Target, condition);

            return new FinalTargetBuilder<TObject, TTarget>(builder.Validator, builder.Target);
        }

        /// <summary>
        /// Creates the <see cref="IfNotTarget"/>.
        /// </summary>
        /// <typeparam name="TObject">A type of an object to validate.</typeparam>
        /// <typeparam name="TTarget">A type of a built target.</typeparam>
        /// <param name="builder">The <see cref="IFinalTargetBuilder{TObject,TTarget}"/>.</param>
        /// <param name="condition">A condition to perform validation.</param>
        /// <exception cref="ArgumentNullException">
        /// The <paramref name="builder"/> is <see langword="null"/>.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// The <paramref name="condition"/> is <see langword="null"/>.
        /// </exception>
        /// <returns>The <see cref="IFinalTargetBuilder{TObject,TTarget}"/> to build false conditions.</returns>
        public static IFinalTargetBuilder<TObject, TTarget> IfNot<TObject, TTarget>(
            this IFinalTargetBuilder<TObject, TTarget> builder, Predicate<TargetContext> condition)
        {
            Throw<ArgumentNullException>.IfNull(builder, nameof(builder));
            Throw<ArgumentNullException>.IfNull(condition, nameof(condition));

            builder.Target = new IfNotTarget(builder.Target, condition);

            return new FinalTargetBuilder<TObject, TTarget>(builder.Validator, builder.Target);
        }
    }
}