// <copyright file="FinalValidatorRuleBuilderExtensions.cs" company="Heleonix - Hennadii Lutsyshyn">
// Copyright (c) Heleonix - Hennadii Lutsyshyn. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the repository root for full license information.
// </copyright>

namespace Heleonix.Validation
{
    using Heleonix.Validation.Builders;
    using Heleonix.Validation.Internal;
    using Heleonix.Validation.Rules;

    /// <summary>
    /// Provides extensions for the <see cref="IFinalValidatorRuleBuilder{TObject,TTarget}"/>.
    /// </summary>
    public static class FinalValidatorRuleBuilderExtensions
    {
        /// <summary>
        /// Moves a rule to a group with the specified <paramref name="name"/>.
        /// </summary>
        /// <typeparam name="TObject">A type of an object to validate.</typeparam>
        /// <typeparam name="TTarget">A type of a target.</typeparam>
        /// <param name="builder">The <see cref="IFinalValidatorRuleBuilder{TObject,TTarget}"/>.</param>
        /// <param name="name">A name of a group.</param>
        /// <exception cref="ArgumentNullException">
        /// The <paramref name="builder"/> is <see langword="null"/>.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// A group with the specified <paramref name="name"/> was not found.
        /// </exception>
        /// <returns>The <see cref="IInitialRuleBuilder{TObject,TTarget}"/>.</returns>
        public static IInitialRuleBuilder<TObject, TTarget> InGroup<TObject, TTarget>(
            this IFinalValidatorRuleBuilder<TObject, TTarget> builder, string name)
        {
            Throw<ArgumentNullException>.IfNull(builder, nameof(builder));

            var group = (from t in builder.Target.Rules
                         where t is GroupRule && StringComparer.Ordinal.Compare(((GroupRule)t).Name, name) == 0
                         select t as GroupRule).FirstOrDefault();

            Throw<ArgumentNullException>.IfNull(group, nameof(group));

            var rule = builder.Rule;

#pragma warning disable S2259 // Null pointers should not be dereferenced
            group.Rules.Add(rule);
#pragma warning restore S2259 // Null pointers should not be dereferenced

            builder.Target.Rules.Remove(rule);

            return new InitialRuleBuilder<TObject, TTarget>(builder.Validator, builder.Target);
        }

        /// <summary>
        /// Creates the <see cref="IfRule"/>.
        /// </summary>
        /// <typeparam name="TObject">A type of an object to validate.</typeparam>
        /// <typeparam name="TTarget">A type of a target.</typeparam>
        /// <param name="builder">The <see cref="IFinalValidatorRuleBuilder{TObject,TTarget}"/>.</param>
        /// <param name="condition">A condition to perform validation.</param>
        /// <exception cref="ArgumentNullException">
        /// The <paramref name="builder"/> is <see langword="null"/>.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// The <paramref name="condition"/> is <see langword="null"/>.
        /// </exception>
        /// <returns>The <see cref="IFinalValidatorRuleBuilder{TObject,TTarget}"/> to build true conditions.</returns>
        public static IFinalValidatorRuleBuilder<TObject, TTarget> If<TObject, TTarget>(
            this IFinalValidatorRuleBuilder<TObject, TTarget> builder, Predicate<RuleContext> condition)
        {
            Throw<ArgumentNullException>.IfNull(builder, nameof(builder));
            Throw<ArgumentNullException>.IfNull(condition, nameof(condition));

            builder.Rule = new IfRule(builder.Rule, condition);

            return new FinalValidatorRuleBuilder<TObject, TTarget>(builder.Validator, builder.Target, builder.Rule);
        }

        /// <summary>
        /// Creates the <see cref="IfNotRule"/>.
        /// </summary>
        /// <typeparam name="TObject">A type of an object to validate.</typeparam>
        /// <typeparam name="TTarget">A type of a target.</typeparam>
        /// <param name="builder">The <see cref="IFinalValidatorRuleBuilder{TObject,TTarget}"/>.</param>
        /// <param name="condition">A condition to perform validation.</param>
        /// <exception cref="ArgumentNullException">
        /// The <paramref name="builder"/> is <see langword="null"/>.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// The <paramref name="condition"/> is <see langword="null"/>.
        /// </exception>
        /// <returns>The <see cref="IFinalValidatorRuleBuilder{TObject,TTarget}"/> to build false conditions.</returns>
        public static IFinalValidatorRuleBuilder<TObject, TTarget> IfNot<TObject, TTarget>(
            this IFinalValidatorRuleBuilder<TObject, TTarget> builder, Predicate<RuleContext> condition)
        {
            Throw<ArgumentNullException>.IfNull(builder, nameof(builder));
            Throw<ArgumentNullException>.IfNull(condition, nameof(condition));

            builder.Rule = new IfNotRule(builder.Rule, condition);

            return new FinalValidatorRuleBuilder<TObject, TTarget>(builder.Validator, builder.Target, builder.Rule);
        }
    }
}