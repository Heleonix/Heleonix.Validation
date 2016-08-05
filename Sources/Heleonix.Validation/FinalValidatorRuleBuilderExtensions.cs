/*
The MIT License (MIT)

Copyright (c) 2015 Heleonix.Validation - Hennadii Lutsyshyn (Heleonix)

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
using System.Linq;
using Heleonix.Validation.Builders;
using Heleonix.Validation.Internal;
using Heleonix.Validation.Rules;

namespace Heleonix.Validation
{
    /// <summary>
    /// Provides extensions for the <see cref="IFinalValidatorRuleBuilder{TObject,TTarget}"/>.
    /// </summary>
    public static class FinalValidatorRuleBuilderExtensions
    {
        #region Methods

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
                where t is GroupRule && StringComparer.Ordinal.Compare(((GroupRule) t).Name, name) == 0
                select t as GroupRule).FirstOrDefault();

            Throw<ArgumentNullException>.IfNull(group, nameof(group));

            var rule = builder.Rule;

            group.Rules.Add(rule);

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
        /// <returns>The <see cref="IFinalValidatorRuleBuilder{TObject,TTarget}"/>.</returns>
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
        /// <returns>The <see cref="IFinalValidatorRuleBuilder{TObject,TTarget}"/>.</returns>
        public static IFinalValidatorRuleBuilder<TObject, TTarget> IfNot<TObject, TTarget>(
            this IFinalValidatorRuleBuilder<TObject, TTarget> builder, Predicate<RuleContext> condition)
        {
            Throw<ArgumentNullException>.IfNull(builder, nameof(builder));
            Throw<ArgumentNullException>.IfNull(condition, nameof(condition));

            builder.Rule = new IfNotRule(builder.Rule, condition);

            return new FinalValidatorRuleBuilder<TObject, TTarget>(builder.Validator, builder.Target, builder.Rule);
        }

        #endregion
    }
}