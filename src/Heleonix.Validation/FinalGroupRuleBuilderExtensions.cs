// <copyright file="FinalGroupRuleBuilderExtensions.cs" company="Heleonix - Hennadii Lutsyshyn">
// Copyright (c) Heleonix - Hennadii Lutsyshyn. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the repository root for full license information.
// </copyright>

namespace Heleonix.Validation
{
    using Heleonix.Validation.Builders;
    using Heleonix.Validation.Internal;
    using Heleonix.Validation.Rules;

    /// <summary>
    /// Provides extensions for the <see cref="IFinalGroupRuleBuilder{TObject,TTarget}"/>.
    /// </summary>
    public static class FinalGroupRuleBuilderExtensions
    {
        /// <summary>
        /// Creates the <see cref="IfRule"/>.
        /// </summary>
        /// <typeparam name="TObject">A type of an object to validate.</typeparam>
        /// <typeparam name="TTarget">A type of a target.</typeparam>
        /// <param name="builder">The <see cref="IFinalGroupRuleBuilder{TObject,TTarget}"/>.</param>
        /// <param name="condition">A condition to perform validation.</param>
        /// <exception cref="ArgumentNullException">
        /// The <paramref name="builder"/> is <see langword="null"/>.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// The <paramref name="condition"/> is <see langword="null"/>.
        /// </exception>
        /// <returns>The <see cref="IFinalGroupRuleBuilder{TObject,TTarget}"/> to build true conditions.</returns>
        public static IFinalGroupRuleBuilder<TObject, TTarget> If<TObject, TTarget>(
            this IFinalGroupRuleBuilder<TObject, TTarget> builder, Predicate<RuleContext> condition)
        {
            Throw<ArgumentNullException>.IfNull(builder, nameof(builder));
            Throw<ArgumentNullException>.IfNull(condition, nameof(condition));

            builder.Rule = new IfRule(builder.Rule, condition);

            return new FinalGroupRuleBuilder<TObject, TTarget>(builder.Validator, builder.Target, builder.Rule);
        }

        /// <summary>
        /// Creates the <see cref="IfNotRule"/>.
        /// </summary>
        /// <typeparam name="TObject">A type of an object to validate.</typeparam>
        /// <typeparam name="TTarget">A type of a target.</typeparam>
        /// <param name="builder">The <see cref="IFinalGroupRuleBuilder{TObject,TTarget}"/>.</param>
        /// <param name="condition">A condition to perform validation.</param>
        /// <exception cref="ArgumentNullException">
        /// The <paramref name="builder"/> is <see langword="null"/>.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// The <paramref name="condition"/> is <see langword="null"/>.
        /// </exception>
        /// <returns>The <see cref="IFinalGroupRuleBuilder{TObject,TTarget}"/> to build false conditions.</returns>
        public static IFinalGroupRuleBuilder<TObject, TTarget> IfNot<TObject, TTarget>(
            this IFinalGroupRuleBuilder<TObject, TTarget> builder, Predicate<RuleContext> condition)
        {
            Throw<ArgumentNullException>.IfNull(builder, nameof(builder));
            Throw<ArgumentNullException>.IfNull(condition, nameof(condition));

            builder.Rule = new IfNotRule(builder.Rule, condition);

            return new FinalGroupRuleBuilder<TObject, TTarget>(builder.Validator, builder.Target, builder.Rule);
        }
    }
}