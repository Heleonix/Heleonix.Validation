// <copyright file="FinalRuleBuilderExtensions.cs" company="Heleonix - Hennadii Lutsyshyn">
// Copyright (c) Heleonix - Hennadii Lutsyshyn. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the repository root for full license information.
// </copyright>

namespace Heleonix.Validation
{
    using Heleonix.Validation.Builders;
    using Heleonix.Validation.Internal;
    using Heleonix.Validation.Rules;

    /// <summary>
    /// Provides extensions for the <see cref="IFinalRuleBuilder{TObject,TTarget,TValue}"/>.
    /// </summary>
    public static class FinalRuleBuilderExtensions
    {
        /// <summary>
        /// Moves a rule to a group with the specified <paramref name="name"/>.
        /// </summary>
        /// <typeparam name="TObject">A type of an object to validate.</typeparam>
        /// <typeparam name="TTarget">A type of a target.</typeparam>
        /// <typeparam name="TValue">A type of a value returned by a rule.</typeparam>
        /// <param name="builder">The <see cref="IFinalRuleBuilder{TObject,TTarget,TValue}"/>.</param>
        /// <param name="name">A name of a group.</param>
        /// <exception cref="ArgumentNullException">
        /// The <paramref name="builder"/> is <see langword="null"/>.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// A group with the specified <paramref name="name"/> was not found.
        /// </exception>
        /// <returns>The <see cref="IInitialRuleBuilder{TObject,TTarget}"/>.</returns>
        public static IInitialValueResultBuilder<TObject, TTarget, TValue> InGroup<TObject, TTarget, TValue>(
            this IFinalRuleBuilder<TObject, TTarget, TValue> builder, string name)
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

            return new InitialValueResultBuilder<TObject, TTarget, TValue>(builder.Validator, builder.Target, rule);
        }

        /// <summary>
        /// Creates the <see cref="IfRule"/>.
        /// </summary>
        /// <typeparam name="TObject">A type of an object to validate.</typeparam>
        /// <typeparam name="TTarget">A type of a target.</typeparam>
        /// <typeparam name="TValue">A type of a value returned by a rule.</typeparam>
        /// <param name="builder">The <see cref="IFinalRuleBuilder{TObject,TTarget,TValue}"/>.</param>
        /// <param name="condition">A condition to perform validation.</param>
        /// <exception cref="ArgumentNullException">
        /// The <paramref name="builder"/> is <see langword="null"/>.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// The <paramref name="condition"/> is <see langword="null"/>.
        /// </exception>
        /// <returns>The <see cref="IFinalRuleBuilder{TObject,TTarget,TValue}"/> to build true conditions.</returns>
        public static IFinalRuleBuilder<TObject, TTarget, TValue> If<TObject, TTarget, TValue>(
            this IFinalRuleBuilder<TObject, TTarget, TValue> builder, Predicate<RuleContext> condition)
        {
            Throw<ArgumentNullException>.IfNull(builder, nameof(builder));
            Throw<ArgumentNullException>.IfNull(condition, nameof(condition));

            builder.Rule = new IfRule(builder.Rule, condition);

            return new FinalRuleBuilder<TObject, TTarget, TValue>(builder.Validator, builder.Target, builder.Rule);
        }

        /// <summary>
        /// Creates the <see cref="IfNotRule"/>.
        /// </summary>
        /// <typeparam name="TObject">A type of an object to validate.</typeparam>
        /// <typeparam name="TTarget">A type of a target.</typeparam>
        /// <typeparam name="TValue">A type of a value returned by a rule.</typeparam>
        /// <param name="builder">The <see cref="IFinalRuleBuilder{TObject,TTarget,TValue}"/>.</param>
        /// <param name="condition">A condition to perform validation.</param>
        /// <exception cref="ArgumentNullException">
        /// The <paramref name="builder"/> is <see langword="null"/>.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// The <paramref name="condition"/> is <see langword="null"/>.
        /// </exception>
        /// <returns>The <see cref="IFinalRuleBuilder{TObject,TTarget,TValue}"/> to build false conditions.</returns>
        public static IFinalRuleBuilder<TObject, TTarget, TValue> IfNot<TObject, TTarget, TValue>(
            this IFinalRuleBuilder<TObject, TTarget, TValue> builder, Predicate<RuleContext> condition)
        {
            Throw<ArgumentNullException>.IfNull(builder, nameof(builder));
            Throw<ArgumentNullException>.IfNull(condition, nameof(condition));

            builder.Rule = new IfNotRule(builder.Rule, condition);

            return new FinalRuleBuilder<TObject, TTarget, TValue>(builder.Validator, builder.Target, builder.Rule);
        }
    }
}