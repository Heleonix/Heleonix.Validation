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
using System.Linq.Expressions;
using System.Text.RegularExpressions;
using NLion.Validation.Builders;
using NLion.Validation.Rules;

namespace NLion.Validation
{
    /// <summary>
    /// Provides extensions for the <see cref="IInitialRuleBuilder{TObject,TTarget}"/>.
    /// </summary>
    public static class InitialRuleBuilderExtensions
    {
        #region Methods

        /// <summary>
        /// Adds the custom rule to a target.
        /// </summary>
        /// <typeparam name="TObject">A type of an object.</typeparam>
        /// <typeparam name="TTarget">A type of a target.</typeparam>
        /// <typeparam name="TValue">A type of a value returned by a rule.</typeparam>
        /// <param name="builder">The <see cref="IInitialRuleBuilder{TObject,TTarget}"/>.</param>
        /// <param name="rule">A rule to add.</param>
        /// <exception cref="ArgumentNullException">
        /// The <paramref name="builder"/> is <see langword="null"/>.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// The <paramref name="rule"/> is <see langword="null"/>.
        /// </exception>
        /// <returns>The <see cref="IFinalRuleBuilder{TObject,TTarget,TValue}"/>.</returns>
        public static IFinalRuleBuilder<TObject, TTarget, TValue> HasRule<TObject, TTarget, TValue>(
            this IInitialRuleBuilder<TObject, TTarget> builder, Rule rule)
        {
            Throw.ArgumentNullException(builder == null, nameof(builder));
            Throw.ArgumentNullException(rule == null, nameof(rule));

            var container = new RuleContainer(rule);

            builder.TargetContainer.Target?.RuleContainers.Add(container);

            return new FinalRuleBuilder<TObject, TTarget, TValue>(
                builder.Validator, builder.TargetContainer, container);
        }

        /// <summary>
        /// Creates the <see cref="CustomRule"/> rule.
        /// </summary>
        /// <typeparam name="TObject">A type of an object.</typeparam>
        /// <typeparam name="TTarget">A type of a target.</typeparam>
        /// <typeparam name="TValue">A type of a value returned by a rule.</typeparam>
        /// <param name="builder">The <see cref="IInitialRuleBuilder{TObject,TTarget}"/>.</param>
        /// <param name="ruleValidator">A delegate to create the <see cref="CustomRule"/>.</param>
        /// <exception cref="ArgumentNullException">
        /// The <paramref name="builder"/> is <see langword="null"/>.
        /// </exception>
        /// <returns>The <see cref="IFinalRuleBuilder{TObject,TTarget,TValue}"/>.</returns>
        public static IFinalRuleBuilder<TObject, TTarget, TValue> HasCustomRule<TObject, TTarget, TValue>(
            this IInitialRuleBuilder<TObject, TTarget> builder, Func<RuleValidationContext, RuleResult> ruleValidator)
            => builder.HasRule<TObject, TTarget, TValue>(new CustomRule(ruleValidator));

        /// <summary>
        /// Creates the <see cref="RequiredRule"/> rule.
        /// </summary>
        /// <typeparam name="TObject">A type of an object.</typeparam>
        /// <typeparam name="TTarget">A type of a target.</typeparam>
        /// <param name="builder">The <see cref="IInitialRuleBuilder{TObject,TTarget}"/>.</param>
        /// <param name="continueValidationWhenFalse">
        /// Determines whether to continue validation when rule value is <see langword="false"/>.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// The <paramref name="builder"/> is <see langword="null"/>.
        /// </exception>
        /// <returns>The <see cref="IFinalRuleBuilder{TObject,TTarget,TValue}"/>.</returns>
        public static IFinalRuleBuilder<TObject, TTarget, bool> IsRequired<TObject, TTarget>(
            this IInitialRuleBuilder<TObject, TTarget> builder, bool continueValidationWhenFalse = false)
            where TTarget : class
            => builder.HasRule<TObject, TTarget, bool>(new RequiredRule(continueValidationWhenFalse));

        /// <summary>
        /// Creates the <see cref="LengthRule"/> rule.
        /// </summary>
        /// <typeparam name="TObject">A type of an object.</typeparam>
        /// <typeparam name="TTarget">A type of a target.</typeparam>
        /// <param name="builder">The <see cref="IInitialRuleBuilder{TObject,TTarget}"/>.</param>
        /// <param name="min">Minimum allowed length.</param>
        /// /// <param name="max">Maximum allowed length.</param>
        /// <param name="continueValidationWhenFalse">
        /// Determines whether to continue validation when rule value is <see langword="false"/>.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// The <paramref name="builder"/> is <see langword="null"/>.
        /// </exception>
        /// <returns>The <see cref="IFinalRuleBuilder{TObject,TTarget,TValue}"/>.</returns>
        public static IFinalRuleBuilder<TObject, TTarget, bool> HasLength<TObject, TTarget>(
            this IInitialRuleBuilder<TObject, TTarget> builder, int? min, int? max,
            bool continueValidationWhenFalse = false)
            => builder.HasRule<TObject, TTarget, bool>(new LengthRule(continueValidationWhenFalse, min, max));

        /// <summary>
        /// Creates the <see cref="RangeRule"/> rule.
        /// </summary>
        /// <typeparam name="TObject">A type of an object.</typeparam>
        /// <typeparam name="TTarget">A type of a target.</typeparam>
        /// <param name="builder">The <see cref="IInitialRuleBuilder{TObject,TTarget}"/>.</param>
        /// <param name="min">Minimum allowed value.</param>
        /// /// <param name="max">Maximum allowed value.</param>
        /// <param name="continueValidationWhenFalse">
        /// Determines whether to continue validation when rule value is <see langword="false"/>.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// The <paramref name="builder"/> is <see langword="null"/>.
        /// </exception>
        /// <returns>The <see cref="IFinalRuleBuilder{TObject,TTarget,TValue}"/>.</returns>
        public static IFinalRuleBuilder<TObject, TTarget, bool> HasRange<TObject, TTarget>(
            this IInitialRuleBuilder<TObject, TTarget> builder, TTarget min, TTarget max,
            bool continueValidationWhenFalse = false)
            where TTarget : IComparable
            => builder.HasRule<TObject, TTarget, bool>(new RangeRule(continueValidationWhenFalse, min, max));

        /// <summary>
        /// Creates the <see cref="RegexRule"/> rule.
        /// </summary>
        /// <typeparam name="TObject">A type of an object.</typeparam>
        /// <typeparam name="TTarget">A type of a target.</typeparam>
        /// <param name="builder">The <see cref="IInitialRuleBuilder{TObject,TTarget}"/>.</param>
        /// <param name="regex">A regular expression to test match.</param>
        /// <param name="regexOptions">Regular expression options.</param>
        /// <param name="continueValidationWhenFalse">
        /// Determines whether to continue validation when rule value is <see langword="false"/>.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// The <paramref name="builder"/> is <see langword="null"/>.
        /// </exception>
        /// <returns>The <see cref="IFinalRuleBuilder{TObject,TTarget,TValue}"/>.</returns>
        public static IFinalRuleBuilder<TObject, TTarget, bool> MatchesRegex<TObject, TTarget>(
            this IInitialRuleBuilder<TObject, TTarget> builder, string regex, RegexOptions regexOptions,
            bool continueValidationWhenFalse = false)
            => builder.HasRule<TObject, TTarget, bool>(new RegexRule(continueValidationWhenFalse, regex, regexOptions));

        /// <summary>
        /// Creates the <see cref="DigitsRule"/> rule.
        /// </summary>
        /// <typeparam name="TObject">A type of an object.</typeparam>
        /// <typeparam name="TTarget">A type of a target.</typeparam>
        /// <param name="builder">The <see cref="IInitialRuleBuilder{TObject,TTarget}"/>.</param>
        /// <param name="continueValidationWhenFalse">
        /// Determines whether to continue validation when rule value is <see langword="false"/>.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// The <paramref name="builder"/> is <see langword="null"/>.
        /// </exception>
        /// <returns>The <see cref="IFinalRuleBuilder{TObject,TTarget,TValue}"/>.</returns>
        public static IFinalRuleBuilder<TObject, TTarget, bool> IsDigits<TObject, TTarget>(
            this IInitialRuleBuilder<TObject, TTarget> builder, bool continueValidationWhenFalse = false)
            => builder.HasRule<TObject, TTarget, bool>(new DigitsRule(continueValidationWhenFalse));

        /// <summary>
        /// Creates the <see cref="UriRule"/> rule.
        /// </summary>
        /// <typeparam name="TObject">A type of an object.</typeparam>
        /// <typeparam name="TTarget">A type of a target.</typeparam>
        /// <param name="builder">The <see cref="IInitialRuleBuilder{TObject,TTarget}"/>.</param>
        /// <param name="kind">A uri kind.</param>
        /// <param name="schemes">Acceptable uri schemes.</param>
        /// <param name="continueValidationWhenFalse">
        /// Determines whether to continue validation when rule value is <see langword="false"/>.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// The <paramref name="builder"/> is <see langword="null"/>.
        /// </exception>
        /// <returns>The <see cref="IFinalRuleBuilder{TObject,TTarget,TValue}"/>.</returns>
        public static IFinalRuleBuilder<TObject, TTarget, bool> IsUri<TObject, TTarget>(
            this IInitialRuleBuilder<TObject, TTarget> builder, UriKind kind, string[] schemes,
            bool continueValidationWhenFalse = false)
            => builder.HasRule<TObject, TTarget, bool>(new UriRule(continueValidationWhenFalse, kind, schemes));

        /// <summary>
        /// Creates the <see cref="EmailRule"/> rule.
        /// </summary>
        /// <typeparam name="TObject">A type of an object.</typeparam>
        /// <typeparam name="TTarget">A type of a target.</typeparam>
        /// <param name="builder">The <see cref="IInitialRuleBuilder{TObject,TTarget}"/>.</param>
        /// <param name="continueValidationWhenFalse">
        /// Determines whether to continue validation when rule value is <see langword="false"/>.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// The <paramref name="builder"/> is <see langword="null"/>.
        /// </exception>
        /// <returns>The <see cref="IFinalRuleBuilder{TObject,TTarget,TValue}"/>.</returns>
        public static IFinalRuleBuilder<TObject, TTarget, bool> IsEmail<TObject, TTarget>(
            this IInitialRuleBuilder<TObject, TTarget> builder, bool continueValidationWhenFalse = false)
            => builder.HasRule<TObject, TTarget, bool>(new EmailRule(continueValidationWhenFalse));

        /// <summary>
        /// Creates the <see cref="CreditCardRule"/> rule.
        /// </summary>
        /// <typeparam name="TObject">A type of an object.</typeparam>
        /// <typeparam name="TTarget">A type of a target.</typeparam>
        /// <param name="builder">The <see cref="IInitialRuleBuilder{TObject,TTarget}"/>.</param>
        /// <param name="continueValidationWhenFalse">
        /// Determines whether to continue validation when rule value is <see langword="false"/>.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// The <paramref name="builder"/> is <see langword="null"/>.
        /// </exception>
        /// <returns>The <see cref="IFinalRuleBuilder{TObject,TTarget,TValue}"/>.</returns>
        public static IFinalRuleBuilder<TObject, TTarget, bool> IsCreditCard<TObject, TTarget>(
            this IInitialRuleBuilder<TObject, TTarget> builder, bool continueValidationWhenFalse = false)
            => builder.HasRule<TObject, TTarget, bool>(new CreditCardRule(continueValidationWhenFalse));

        /// <summary>
        /// Creates the <see cref="EqualRule"/> rule.
        /// </summary>
        /// <typeparam name="TObject">A type of an object.</typeparam>
        /// <typeparam name="TTarget">A type of a target.</typeparam>
        /// <param name="builder">The <see cref="IInitialRuleBuilder{TObject,TTarget}"/>.</param>
        /// <param name="otherValueProvider">Other value provider.</param>
        /// <param name="continueValidationWhenFalse">
        /// Determines whether to continue validation when rule value is <see langword="false"/>.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// The <paramref name="builder"/> is <see langword="null"/>.
        /// </exception>
        /// <returns>The <see cref="IFinalRuleBuilder{TObject,TTarget,TValue}"/>.</returns>
        public static IFinalRuleBuilder<TObject, TTarget, bool> IsEqualTo<TObject, TTarget>(
            this IInitialRuleBuilder<TObject, TTarget> builder, Func<RuleValidationContext, object> otherValueProvider,
            bool continueValidationWhenFalse = false)
            => builder.HasRule<TObject, TTarget, bool>(new EqualRule(continueValidationWhenFalse, otherValueProvider));

        /// <summary>
        /// Creates the <see cref="EqualRule"/> rule.
        /// </summary>
        /// <typeparam name="TObject">A type of an object.</typeparam>
        /// <typeparam name="TTarget">A type of a target.</typeparam>
        /// <param name="builder">The <see cref="IInitialRuleBuilder{TObject,TTarget}"/>.</param>
        /// <param name="otherValue">Other value.</param>
        /// <param name="continueValidationWhenFalse">
        /// Determines whether to continue validation when rule value is <see langword="false"/>.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// The <paramref name="builder"/> is <see langword="null"/>.
        /// </exception>
        /// <returns>The <see cref="IFinalRuleBuilder{TObject,TTarget,TValue}"/>.</returns>
        public static IFinalRuleBuilder<TObject, TTarget, bool> IsEqualTo<TObject, TTarget>(
            this IInitialRuleBuilder<TObject, TTarget> builder, object otherValue,
            bool continueValidationWhenFalse = false)
            => builder.HasRule<TObject, TTarget, bool>(new EqualRule(continueValidationWhenFalse, otherValue));

        /// <summary>
        /// Creates the <see cref="NotEqualRule"/> rule.
        /// </summary>
        /// <typeparam name="TObject">A type of an object.</typeparam>
        /// <typeparam name="TTarget">A type of a target.</typeparam>
        /// <param name="builder">The <see cref="IInitialRuleBuilder{TObject,TTarget}"/>.</param>
        /// <param name="otherValueProvider">Other value provider.</param>
        /// <param name="continueValidationWhenFalse">
        /// Determines whether to continue validation when rule value is <see langword="false"/>.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// The <paramref name="builder"/> is <see langword="null"/>.
        /// </exception>
        /// <returns>The <see cref="IFinalRuleBuilder{TObject,TTarget,TValue}"/>.</returns>
        public static IFinalRuleBuilder<TObject, TTarget, bool> IsNotEqualTo<TObject, TTarget>(
            this IInitialRuleBuilder<TObject, TTarget> builder, Func<RuleValidationContext, object> otherValueProvider,
            bool continueValidationWhenFalse = false)
            => builder.HasRule<TObject, TTarget, bool>(
                new NotEqualRule(continueValidationWhenFalse, otherValueProvider));

        /// <summary>
        /// Creates the <see cref="NotEqualRule"/> rule.
        /// </summary>
        /// <typeparam name="TObject">A type of an object.</typeparam>
        /// <typeparam name="TTarget">A type of a target.</typeparam>
        /// <param name="builder">The <see cref="IInitialRuleBuilder{TObject,TTarget}"/>.</param>
        /// <param name="otherValue">Other value.</param>
        /// <param name="continueValidationWhenFalse">
        /// Determines whether to continue validation when rule value is <see langword="false"/>.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// The <paramref name="builder"/> is <see langword="null"/>.
        /// </exception>
        /// <returns>The <see cref="IFinalRuleBuilder{TObject,TTarget,TValue}"/>.</returns>
        public static IFinalRuleBuilder<TObject, TTarget, bool> IsNotEqualTo<TObject, TTarget>(
            this IInitialRuleBuilder<TObject, TTarget> builder, object otherValue,
            bool continueValidationWhenFalse = false)
            => builder.HasRule<TObject, TTarget, bool>(new NotEqualRule(continueValidationWhenFalse, otherValue));

        /// <summary>
        /// Creates the <see cref="LessThanRule"/> rule.
        /// </summary>
        /// <typeparam name="TObject">A type of an object.</typeparam>
        /// <typeparam name="TTarget">A type of a target.</typeparam>
        /// <param name="builder">The <see cref="IInitialRuleBuilder{TObject,TTarget}"/>.</param>
        /// <param name="otherValueProvider">Other value provider.</param>
        /// <param name="continueValidationWhenFalse">
        /// Determines whether to continue validation when rule value is <see langword="false"/>.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// The <paramref name="builder"/> is <see langword="null"/>.
        /// </exception>
        /// <returns>The <see cref="IFinalRuleBuilder{TObject,TTarget,TValue}"/>.</returns>
        public static IFinalRuleBuilder<TObject, TTarget, bool> IsLessThan<TObject, TTarget>(
            this IInitialRuleBuilder<TObject, TTarget> builder, Func<RuleValidationContext, object> otherValueProvider,
            bool continueValidationWhenFalse = false)
            => builder.HasRule<TObject, TTarget, bool>(
                new LessThanRule(continueValidationWhenFalse, otherValueProvider));

        /// <summary>
        /// Creates the <see cref="LessThanRule"/> rule.
        /// </summary>
        /// <typeparam name="TObject">A type of an object.</typeparam>
        /// <typeparam name="TTarget">A type of a target.</typeparam>
        /// <param name="builder">The <see cref="IInitialRuleBuilder{TObject,TTarget}"/>.</param>
        /// <param name="otherValue">Other value.</param>
        /// <param name="continueValidationWhenFalse">
        /// Determines whether to continue validation when rule value is <see langword="false"/>.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// The <paramref name="builder"/> is <see langword="null"/>.
        /// </exception>
        /// <returns>The <see cref="IFinalRuleBuilder{TObject,TTarget,TValue}"/>.</returns>
        public static IFinalRuleBuilder<TObject, TTarget, bool> IsLessThan<TObject, TTarget>(
            this IInitialRuleBuilder<TObject, TTarget> builder, object otherValue,
            bool continueValidationWhenFalse = false)
            => builder.HasRule<TObject, TTarget, bool>(new LessThanRule(continueValidationWhenFalse, otherValue));

        /// <summary>
        /// Creates the <see cref="LessThanOrEqualRule"/> rule.
        /// </summary>
        /// <typeparam name="TObject">A type of an object.</typeparam>
        /// <typeparam name="TTarget">A type of a target.</typeparam>
        /// <param name="builder">The <see cref="IInitialRuleBuilder{TObject,TTarget}"/>.</param>
        /// <param name="otherValueProvider">Other value provider.</param>
        /// <param name="continueValidationWhenFalse">
        /// Determines whether to continue validation when rule value is <see langword="false"/>.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// The <paramref name="builder"/> is <see langword="null"/>.
        /// </exception>
        /// <returns>The <see cref="IFinalRuleBuilder{TObject,TTarget,TValue}"/>.</returns>
        public static IFinalRuleBuilder<TObject, TTarget, bool> IsLessThanOrEqualTo<TObject, TTarget>(
            this IInitialRuleBuilder<TObject, TTarget> builder, Func<RuleValidationContext, object> otherValueProvider,
            bool continueValidationWhenFalse = false)
            => builder.HasRule<TObject, TTarget, bool>(
                new LessThanOrEqualRule(continueValidationWhenFalse, otherValueProvider));

        /// <summary>
        /// Creates the <see cref="LessThanOrEqualRule"/> rule.
        /// </summary>
        /// <typeparam name="TObject">A type of an object.</typeparam>
        /// <typeparam name="TTarget">A type of a target.</typeparam>
        /// <param name="builder">The <see cref="IInitialRuleBuilder{TObject,TTarget}"/>.</param>
        /// <param name="otherValue">Other value.</param>
        /// <param name="continueValidationWhenFalse">
        /// Determines whether to continue validation when rule value is <see langword="false"/>.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// The <paramref name="builder"/> is <see langword="null"/>.
        /// </exception>
        /// <returns>The <see cref="IFinalRuleBuilder{TObject,TTarget,TValue}"/>.</returns>
        public static IFinalRuleBuilder<TObject, TTarget, bool> IsLessThanOrEqualTo<TObject, TTarget>(
            this IInitialRuleBuilder<TObject, TTarget> builder, object otherValue,
            bool continueValidationWhenFalse = false)
            => builder.HasRule<TObject, TTarget, bool>(
                new LessThanOrEqualRule(continueValidationWhenFalse, otherValue));

        /// <summary>
        /// Creates the <see cref="GreaterThanRule"/> rule.
        /// </summary>
        /// <typeparam name="TObject">A type of an object.</typeparam>
        /// <typeparam name="TTarget">A type of a target.</typeparam>
        /// <param name="builder">The <see cref="IInitialRuleBuilder{TObject,TTarget}"/>.</param>
        /// <param name="otherValueProvider">Other value provider.</param>
        /// <param name="continueValidationWhenFalse">
        /// Determines whether to continue validation when rule value is <see langword="false"/>.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// The <paramref name="builder"/> is <see langword="null"/>.
        /// </exception>
        /// <returns>The <see cref="IFinalRuleBuilder{TObject,TTarget,TValue}"/>.</returns>
        public static IFinalRuleBuilder<TObject, TTarget, bool> IsGreaterThan<TObject, TTarget>(
            this IInitialRuleBuilder<TObject, TTarget> builder, Func<RuleValidationContext, object> otherValueProvider,
            bool continueValidationWhenFalse = false)
            => builder.HasRule<TObject, TTarget, bool>(
                new GreaterThanRule(continueValidationWhenFalse, otherValueProvider));

        /// <summary>
        /// Creates the <see cref="GreaterThanRule"/> rule.
        /// </summary>
        /// <typeparam name="TObject">A type of an object.</typeparam>
        /// <typeparam name="TTarget">A type of a target.</typeparam>
        /// <param name="builder">The <see cref="IInitialRuleBuilder{TObject,TTarget}"/>.</param>
        /// <param name="otherValue">Other value.</param>
        /// <param name="continueValidationWhenFalse">
        /// Determines whether to continue validation when rule value is <see langword="false"/>.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// The <paramref name="builder"/> is <see langword="null"/>.
        /// </exception>
        /// <returns>The <see cref="IFinalRuleBuilder{TObject,TTarget,TValue}"/>.</returns>
        public static IFinalRuleBuilder<TObject, TTarget, bool> IsGreaterThan<TObject, TTarget>(
            this IInitialRuleBuilder<TObject, TTarget> builder, object otherValue,
            bool continueValidationWhenFalse = false)
            => builder.HasRule<TObject, TTarget, bool>(new GreaterThanRule(continueValidationWhenFalse, otherValue));

        /// <summary>
        /// Creates the <see cref="GreaterThanOrEqualRule"/> rule.
        /// </summary>
        /// <typeparam name="TObject">A type of an object.</typeparam>
        /// <typeparam name="TTarget">A type of a target.</typeparam>
        /// <param name="builder">The <see cref="IInitialRuleBuilder{TObject,TTarget}"/>.</param>
        /// <param name="otherValueProvider">Other value provider.</param>
        /// <param name="continueValidationWhenFalse">
        /// Determines whether to continue validation when rule value is <see langword="false"/>.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// The <paramref name="builder"/> is <see langword="null"/>.
        /// </exception>
        /// <returns>The <see cref="IFinalRuleBuilder{TObject,TTarget,TValue}"/>.</returns>
        public static IFinalRuleBuilder<TObject, TTarget, bool> IsGreaterThanOrEqualTo<TObject, TTarget>(
            this IInitialRuleBuilder<TObject, TTarget> builder, Func<RuleValidationContext, object> otherValueProvider,
            bool continueValidationWhenFalse = false)
            => builder.HasRule<TObject, TTarget, bool>(
                new GreaterThanOrEqualRule(continueValidationWhenFalse, otherValueProvider));

        /// <summary>
        /// Creates the <see cref="GreaterThanOrEqualRule"/> rule.
        /// </summary>
        /// <typeparam name="TObject">A type of an object.</typeparam>
        /// <typeparam name="TTarget">A type of a target.</typeparam>
        /// <param name="builder">The <see cref="IInitialRuleBuilder{TObject,TTarget}"/>.</param>
        /// <param name="otherValue">Other value.</param>
        /// <param name="continueValidationWhenFalse">
        /// Determines whether to continue validation when rule value is <see langword="false"/>.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// The <paramref name="builder"/> is <see langword="null"/>.
        /// </exception>
        /// <returns>The <see cref="IFinalRuleBuilder{TObject,TTarget,TValue}"/>.</returns>
        public static IFinalRuleBuilder<TObject, TTarget, bool> IsGreaterThanOrEqualTo<TObject, TTarget>(
            this IInitialRuleBuilder<TObject, TTarget> builder, object otherValue,
            bool continueValidationWhenFalse = false)
            => builder.HasRule<TObject, TTarget, bool>(
                new GreaterThanOrEqualRule(continueValidationWhenFalse, otherValue));

        /// <summary>
        /// Creates the <see cref="EqualToTargetRule"/> rule.
        /// </summary>
        /// <typeparam name="TObject">A type of an object.</typeparam>
        /// <typeparam name="TTarget">A type of a target.</typeparam>
        /// <param name="builder">The <see cref="IInitialRuleBuilder{TObject,TTarget}"/>.</param>
        /// /// <param name="otherTargetExpression">An expression of other target to compare with.</param>
        /// <param name="continueValidationWhenFalse">
        /// Determines whether to continue validation when rule value is <see langword="false"/>.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// The <paramref name="builder"/> is <see langword="null"/>.
        /// </exception>
        /// <returns>The <see cref="IFinalRuleBuilder{TObject,TTarget,TValue}"/>.</returns>
        public static IFinalRuleBuilder<TObject, TTarget, bool> IsEqualToTarget<TObject, TTarget>(
            this IInitialRuleBuilder<TObject, TTarget> builder, Expression<Func<IInitialTargetBuilder<TObject>,
                IFinalTargetBuilder<TObject, object>>> otherTargetExpression,
            bool continueValidationWhenFalse = false)
            => builder.HasTargetComparisonRule(otherTargetExpression, (b, target)
                => new EqualToTargetRule(b, target), continueValidationWhenFalse);

        /// <summary>
        /// Creates the <see cref="NotEqualToTargetRule"/> rule.
        /// </summary>
        /// <typeparam name="TObject">A type of an object.</typeparam>
        /// <typeparam name="TTarget">A type of a target.</typeparam>
        /// <param name="builder">The <see cref="IInitialRuleBuilder{TObject,TTarget}"/>.</param>
        /// /// <param name="otherTargetExpression">An expression of other target to operate with.</param>
        /// <param name="continueValidationWhenFalse">
        /// Determines whether to continue validation when rule value is <see langword="false"/>.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// The <paramref name="builder"/> is <see langword="null"/>.
        /// </exception>
        /// <returns>The <see cref="IFinalRuleBuilder{TObject,TTarget,TValue}"/>.</returns>
        public static IFinalRuleBuilder<TObject, TTarget, bool> IsNotEqualToTarget<TObject, TTarget>(
            this IInitialRuleBuilder<TObject, TTarget> builder, Expression<Func<IInitialTargetBuilder<TObject>,
                IFinalTargetBuilder<TObject, object>>> otherTargetExpression,
            bool continueValidationWhenFalse = false)
            => builder.HasTargetComparisonRule(otherTargetExpression, (b, target)
                => new NotEqualToTargetRule(b, target), continueValidationWhenFalse);

        /// <summary>
        /// Creates the <see cref="LessThanTargetRule"/> rule.
        /// </summary>
        /// <typeparam name="TObject">A type of an object.</typeparam>
        /// <typeparam name="TTarget">A type of a target.</typeparam>
        /// <param name="builder">The <see cref="IInitialRuleBuilder{TObject,TTarget}"/>.</param>
        /// /// <param name="otherTargetExpression">An expression of other target to operate with.</param>
        /// <param name="continueValidationWhenFalse">
        /// Determines whether to continue validation when rule value is <see langword="false"/>.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// The <paramref name="builder"/> is <see langword="null"/>.
        /// </exception>
        /// <returns>The <see cref="IFinalRuleBuilder{TObject,TTarget,TValue}"/>.</returns>
        public static IFinalRuleBuilder<TObject, TTarget, bool> IsLessThanTarget<TObject, TTarget>(
            this IInitialRuleBuilder<TObject, TTarget> builder, Expression<Func<IInitialTargetBuilder<TObject>,
                IFinalTargetBuilder<TObject, object>>> otherTargetExpression,
            bool continueValidationWhenFalse = false)
            => builder.HasTargetComparisonRule(otherTargetExpression, (b, target)
                => new LessThanTargetRule(b, target), continueValidationWhenFalse);

        /// <summary>
        /// Creates the <see cref="LessThanOrEqualToTargetRule"/> rule.
        /// </summary>
        /// <typeparam name="TObject">A type of an object.</typeparam>
        /// <typeparam name="TTarget">A type of a target.</typeparam>
        /// <param name="builder">The <see cref="IInitialRuleBuilder{TObject,TTarget}"/>.</param>
        /// /// <param name="otherTargetExpression">An expression of other target to operate with.</param>
        /// <param name="continueValidationWhenFalse">
        /// Determines whether to continue validation when rule value is <see langword="false"/>.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// The <paramref name="builder"/> is <see langword="null"/>.
        /// </exception>
        /// <returns>The <see cref="IFinalRuleBuilder{TObject,TTarget,TValue}"/>.</returns>
        public static IFinalRuleBuilder<TObject, TTarget, bool> IsLessThanOrEqualToTarget<TObject, TTarget>(
            this IInitialRuleBuilder<TObject, TTarget> builder, Expression<Func<IInitialTargetBuilder<TObject>,
                IFinalTargetBuilder<TObject, object>>> otherTargetExpression,
            bool continueValidationWhenFalse = false)
            => builder.HasTargetComparisonRule(otherTargetExpression, (b, target)
                => new LessThanOrEqualToTargetRule(b, target), continueValidationWhenFalse);

        /// <summary>
        /// Creates the <see cref="GreaterThanTargetRule"/> rule.
        /// </summary>
        /// <typeparam name="TObject">A type of an object.</typeparam>
        /// <typeparam name="TTarget">A type of a target.</typeparam>
        /// <param name="builder">The <see cref="IInitialRuleBuilder{TObject,TTarget}"/>.</param>
        /// /// <param name="otherTargetExpression">An expression of other target to operate with.</param>
        /// <param name="continueValidationWhenFalse">
        /// Determines whether to continue validation when rule value is <see langword="false"/>.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// The <paramref name="builder"/> is <see langword="null"/>.
        /// </exception>
        /// <returns>The <see cref="IFinalRuleBuilder{TObject,TTarget,TValue}"/>.</returns>
        public static IFinalRuleBuilder<TObject, TTarget, bool> IsGreaterThanTarget<TObject, TTarget>(
            this IInitialRuleBuilder<TObject, TTarget> builder, Expression<Func<IInitialTargetBuilder<TObject>,
                IFinalTargetBuilder<TObject, object>>> otherTargetExpression,
            bool continueValidationWhenFalse = false)
            => builder.HasTargetComparisonRule(otherTargetExpression, (b, target)
                => new GreaterThanTargetRule(b, target), continueValidationWhenFalse);

        /// <summary>
        /// Creates the <see cref="GreaterThanOrEqualToTargetRule"/> rule.
        /// </summary>
        /// <typeparam name="TObject">A type of an object.</typeparam>
        /// <typeparam name="TTarget">A type of a target.</typeparam>
        /// <param name="builder">The <see cref="IInitialRuleBuilder{TObject,TTarget}"/>.</param>
        /// <param name="otherTargetExpression">An expression of other target to compare with.</param>
        /// <param name="continueValidationWhenFalse">
        /// Determines whether to continue validation when rule value is <see langword="false"/>.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// The <paramref name="builder"/> is <see langword="null"/>.
        /// </exception>
        /// <returns>The <see cref="IFinalRuleBuilder{TObject,TTarget,TValue}"/>.</returns>
        public static IFinalRuleBuilder<TObject, TTarget, bool> IsGreaterThanOrEqualToTarget<TObject, TTarget>(
            this IInitialRuleBuilder<TObject, TTarget> builder, Expression<Func<IInitialTargetBuilder<TObject>,
                IFinalTargetBuilder<TObject, object>>> otherTargetExpression,
            bool continueValidationWhenFalse = false)
            => builder.HasTargetComparisonRule(otherTargetExpression, (b, target)
                => new GreaterThanOrEqualToTargetRule(b, target), continueValidationWhenFalse);

        /// <summary>
        /// Adds the <see cref="TargetComparisonRule"/> rule.
        /// </summary>
        /// <typeparam name="TObject">A type of an object.</typeparam>
        /// <typeparam name="TTarget">A type of a target.</typeparam>
        /// <param name="builder">The <see cref="IInitialRuleBuilder{TObject,TTarget}"/>.</param>
        /// <param name="otherTargetExpression">An expression of other target to compare with.</param>
        /// <param name="ruleFactory">A factory to create a rule to add.</param>
        /// <param name="continueValidationWhenFalse">
        /// Determines whether to continue validation when rule value is <see langword="false"/>.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// The <paramref name="builder"/> is <see langword="null"/>.
        /// </exception>
        /// <returns>The <see cref="IFinalRuleBuilder{TObject,TTarget,TValue}"/>.</returns>
        private static IFinalRuleBuilder<TObject, TTarget, bool> HasTargetComparisonRule<TObject, TTarget>(
            this IInitialRuleBuilder<TObject, TTarget> builder, Expression<Func<IInitialTargetBuilder<TObject>,
                IFinalTargetBuilder<TObject, object>>> otherTargetExpression,
            Func<bool, Target, TargetComparisonRule> ruleFactory, bool continueValidationWhenFalse = false)
        {
            var otherTarget = otherTargetExpression?.Compile()(new InitialTargetBuilder<TObject>(
                new EmptyValidator<TObject>())).TargetContainer.Target;

            return builder.HasRule<TObject, TTarget, bool>(ruleFactory(continueValidationWhenFalse, otherTarget));
        }

        #endregion
    }
}