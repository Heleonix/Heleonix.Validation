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
using System.Linq.Expressions;
using System.Text.RegularExpressions;
using Heleonix.Validation.Builders;
using Heleonix.Validation.Internal;
using Heleonix.Validation.Rules;

namespace Heleonix.Validation
{
    /// <summary>
    /// Provides extensions for the <see cref="IInitialRuleBuilder{TObject,TTarget}"/>.
    /// </summary>
    public static class InitialRuleBuilderExtensions
    {
        #region Methods

        /// <summary>
        /// Creates the <see cref="ValidatorRule"/>.
        /// </summary>
        /// <typeparam name="TObject">A type of an object to validate.</typeparam>
        /// <typeparam name="TTarget">A type of a target.</typeparam>
        /// <param name="builder">The <see cref="IInitialRuleBuilder{TObject,TTarget}"/>.</param>
        /// <exception cref="ArgumentNullException">
        /// The <paramref name="builder"/> is <see langword="null"/>.
        /// </exception>
        /// <returns>The <see cref="IFinalValidatorRuleBuilder{TObject,TTarget}"/>.</returns>
        public static IFinalValidatorRuleBuilder<TObject, TTarget> HasValidator<TObject, TTarget>(
            this IInitialRuleBuilder<TObject, TTarget> builder)
        {
            Throw<ArgumentNullException>.IfNull(builder, nameof(builder));

            var rule = new ValidatorRule();

            builder.Target.Rules.Add(rule);

            return new FinalValidatorRuleBuilder<TObject, TTarget>(builder.Validator, builder.Target, rule);
        }

        /// <summary>
        /// Creates the <see cref="GroupRule"/>.
        /// </summary>
        /// <typeparam name="TObject">A type of an object to validate.</typeparam>
        /// <typeparam name="TTarget">A type of a target.</typeparam>
        /// <param name="builder">The <see cref="IInitialRuleBuilder{TObject,TTarget}"/>.</param>
        /// <param name="name">A name of a group.</param>
        /// <exception cref="ArgumentNullException">
        /// The <paramref name="builder"/> is <see langword="null"/>.
        /// </exception>
        /// <returns>The <see cref="IFinalGroupRuleBuilder{TObject,TTarget}"/>.</returns>
        public static IFinalGroupRuleBuilder<TObject, TTarget> Group<TObject, TTarget>(
            this IInitialRuleBuilder<TObject, TTarget> builder, string name)
        {
            Throw<ArgumentNullException>.IfNull(builder, nameof(builder));

            var rule = new GroupRule(name);

            builder.Target.Rules.Add(rule);

            return new FinalGroupRuleBuilder<TObject, TTarget>(builder.Validator, builder.Target, rule);
        }

        /// <summary>
        /// Adds a custom rule.
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
            Throw<ArgumentNullException>.IfNull(builder, nameof(builder));
            Throw<ArgumentNullException>.IfNull(rule, nameof(rule));

            builder.Target.Rules.Add(rule);

            return new FinalRuleBuilder<TObject, TTarget, TValue>(builder.Validator, builder.Target, rule);
        }

        /// <summary>
        /// Creates the <see cref="CustomRule"/>.
        /// </summary>
        /// <typeparam name="TObject">A type of an object.</typeparam>
        /// <typeparam name="TTarget">A type of a target.</typeparam>
        /// <typeparam name="TValue">A type of a value returned by a rule.</typeparam>
        /// <param name="builder">The <see cref="IInitialRuleBuilder{TObject,TTarget}"/>.</param>
        /// <param name="ruleValidator">A delegate to create the <see cref="CustomRule"/>.</param>
        /// <exception cref="ArgumentNullException">
        /// The <paramref name="builder"/> is <see langword="null"/>.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// The <paramref name="ruleValidator"/> is <see langword="null"/>.
        /// </exception>
        /// <returns>The <see cref="IFinalRuleBuilder{TObject,TTarget,TValue}"/>.</returns>
        public static IFinalRuleBuilder<TObject, TTarget, TValue> HasCustomRule<TObject, TTarget, TValue>(
            this IInitialRuleBuilder<TObject, TTarget> builder, Func<RuleContext, RuleResult> ruleValidator)
            => builder.HasRule<TObject, TTarget, TValue>(new CustomRule(ruleValidator));

        /// <summary>
        /// Creates the <see cref="RequiredRule"/>.
        /// </summary>
        /// <typeparam name="TObject">A type of an object.</typeparam>
        /// <typeparam name="TTarget">A type of a target.</typeparam>
        /// <param name="builder">The <see cref="IInitialRuleBuilder{TObject,TTarget}"/>.</param>
        /// <param name="continueValidationWhenFalse">
        /// Determines whether to continue validation when a value of a rule is <see langword="false"/>.
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
        /// Creates the <see cref="LengthRule"/>.
        /// </summary>
        /// <typeparam name="TObject">A type of an object.</typeparam>
        /// <typeparam name="TTarget">A type of a target.</typeparam>
        /// <param name="builder">The <see cref="IInitialRuleBuilder{TObject,TTarget}"/>.</param>
        /// <param name="min">Minimum allowed length.</param>
        /// /// <param name="max">Maximum allowed length.</param>
        /// <param name="continueValidationWhenFalse">
        /// Determines whether to continue validation when a value of a rule is <see langword="false"/>.
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
        /// Creates the <see cref="RangeRule"/>.
        /// </summary>
        /// <typeparam name="TObject">A type of an object.</typeparam>
        /// <typeparam name="TTarget">A type of a target.</typeparam>
        /// <param name="builder">The <see cref="IInitialRuleBuilder{TObject,TTarget}"/>.</param>
        /// <param name="min">Minimum allowed value.</param>
        /// /// <param name="max">Maximum allowed value.</param>
        /// <param name="continueValidationWhenFalse">
        /// Determines whether to continue validation when a value of a rule is <see langword="false"/>.
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
        /// Creates the <see cref="RegexRule"/>.
        /// </summary>
        /// <typeparam name="TObject">A type of an object.</typeparam>
        /// <typeparam name="TTarget">A type of a target.</typeparam>
        /// <param name="builder">The <see cref="IInitialRuleBuilder{TObject,TTarget}"/>.</param>
        /// <param name="regex">A regular expression to test match.</param>
        /// <param name="regexOptions">Regular expression options.</param>
        /// <param name="continueValidationWhenFalse">
        /// Determines whether to continue validation when a value of a rule is <see langword="false"/>.
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
        /// Creates the <see cref="DigitsRule"/>.
        /// </summary>
        /// <typeparam name="TObject">A type of an object.</typeparam>
        /// <typeparam name="TTarget">A type of a target.</typeparam>
        /// <param name="builder">The <see cref="IInitialRuleBuilder{TObject,TTarget}"/>.</param>
        /// <param name="continueValidationWhenFalse">
        /// Determines whether to continue validation when a value of a rule is <see langword="false"/>.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// The <paramref name="builder"/> is <see langword="null"/>.
        /// </exception>
        /// <returns>The <see cref="IFinalRuleBuilder{TObject,TTarget,TValue}"/>.</returns>
        public static IFinalRuleBuilder<TObject, TTarget, bool> IsDigits<TObject, TTarget>(
            this IInitialRuleBuilder<TObject, TTarget> builder, bool continueValidationWhenFalse = false)
            => builder.HasRule<TObject, TTarget, bool>(new DigitsRule(continueValidationWhenFalse));

        /// <summary>
        /// Creates the <see cref="UriRule"/>.
        /// </summary>
        /// <typeparam name="TObject">A type of an object.</typeparam>
        /// <typeparam name="TTarget">A type of a target.</typeparam>
        /// <param name="builder">The <see cref="IInitialRuleBuilder{TObject,TTarget}"/>.</param>
        /// <param name="kind">A uri kind.</param>
        /// <param name="schemes">Acceptable uri schemes.</param>
        /// <param name="continueValidationWhenFalse">
        /// Determines whether to continue validation when a value of a rule is <see langword="false"/>.
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
        /// Creates the <see cref="EmailRule"/>.
        /// </summary>
        /// <typeparam name="TObject">A type of an object.</typeparam>
        /// <typeparam name="TTarget">A type of a target.</typeparam>
        /// <param name="builder">The <see cref="IInitialRuleBuilder{TObject,TTarget}"/>.</param>
        /// <param name="continueValidationWhenFalse">
        /// Determines whether to continue validation when a value of a rule is <see langword="false"/>.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// The <paramref name="builder"/> is <see langword="null"/>.
        /// </exception>
        /// <returns>The <see cref="IFinalRuleBuilder{TObject,TTarget,TValue}"/>.</returns>
        public static IFinalRuleBuilder<TObject, TTarget, bool> IsEmail<TObject, TTarget>(
            this IInitialRuleBuilder<TObject, TTarget> builder, bool continueValidationWhenFalse = false)
            => builder.HasRule<TObject, TTarget, bool>(new EmailRule(continueValidationWhenFalse));

        /// <summary>
        /// Creates the <see cref="CreditCardRule"/>.
        /// </summary>
        /// <typeparam name="TObject">A type of an object.</typeparam>
        /// <typeparam name="TTarget">A type of a target.</typeparam>
        /// <param name="builder">The <see cref="IInitialRuleBuilder{TObject,TTarget}"/>.</param>
        /// <param name="continueValidationWhenFalse">
        /// Determines whether to continue validation when a value of a rule is <see langword="false"/>.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// The <paramref name="builder"/> is <see langword="null"/>.
        /// </exception>
        /// <returns>The <see cref="IFinalRuleBuilder{TObject,TTarget,TValue}"/>.</returns>
        public static IFinalRuleBuilder<TObject, TTarget, bool> IsCreditCard<TObject, TTarget>(
            this IInitialRuleBuilder<TObject, TTarget> builder, bool continueValidationWhenFalse = false)
            => builder.HasRule<TObject, TTarget, bool>(new CreditCardRule(continueValidationWhenFalse));

        /// <summary>
        /// Creates the <see cref="Comparison.Equal"/> rule.
        /// </summary>
        /// <typeparam name="TObject">A type of an object.</typeparam>
        /// <typeparam name="TTarget">A type of a target.</typeparam>
        /// <param name="builder">The <see cref="IInitialRuleBuilder{TObject,TTarget}"/>.</param>
        /// <param name="otherValueProvider">Other value provider.</param>
        /// <param name="continueValidationWhenFalse">
        /// Determines whether to continue validation when a value of a rule is <see langword="false"/>.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// The <paramref name="builder"/> is <see langword="null"/>.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// The <paramref name="otherValueProvider"/> is <see langword="null"/>.
        /// </exception>
        /// <returns>The <see cref="IFinalRuleBuilder{TObject,TTarget,TValue}"/>.</returns>
        public static IFinalRuleBuilder<TObject, TTarget, bool> IsEqualTo<TObject, TTarget>(
            this IInitialRuleBuilder<TObject, TTarget> builder, Func<RuleContext, object> otherValueProvider,
            bool continueValidationWhenFalse = false)
            => builder.HasRule<TObject, TTarget, bool>(new ComparisonRule(
                continueValidationWhenFalse, otherValueProvider, Comparison.Equal));

        /// <summary>
        /// Creates the <see cref="Comparison.Equal"/> rule.
        /// </summary>
        /// <typeparam name="TObject">A type of an object.</typeparam>
        /// <typeparam name="TTarget">A type of a target.</typeparam>
        /// <param name="builder">The <see cref="IInitialRuleBuilder{TObject,TTarget}"/>.</param>
        /// <param name="otherValue">Other value.</param>
        /// <param name="continueValidationWhenFalse">
        /// Determines whether to continue validation when a value of a rule is <see langword="false"/>.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// The <paramref name="builder"/> is <see langword="null"/>.
        /// </exception>
        /// <returns>The <see cref="IFinalRuleBuilder{TObject,TTarget,TValue}"/>.</returns>
        public static IFinalRuleBuilder<TObject, TTarget, bool> IsEqualTo<TObject, TTarget>(
            this IInitialRuleBuilder<TObject, TTarget> builder, object otherValue,
            bool continueValidationWhenFalse = false)
            => builder.HasRule<TObject, TTarget, bool>(new ComparisonRule(
                continueValidationWhenFalse, otherValue, Comparison.Equal));

        /// <summary>
        /// Creates the <see cref="Comparison.NotEqual"/> rule.
        /// </summary>
        /// <typeparam name="TObject">A type of an object.</typeparam>
        /// <typeparam name="TTarget">A type of a target.</typeparam>
        /// <param name="builder">The <see cref="IInitialRuleBuilder{TObject,TTarget}"/>.</param>
        /// <param name="otherValueProvider">Other value provider.</param>
        /// <param name="continueValidationWhenFalse">
        /// Determines whether to continue validation when a value of a rule is <see langword="false"/>.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// The <paramref name="builder"/> is <see langword="null"/>.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// The <paramref name="otherValueProvider"/> is <see langword="null"/>.
        /// </exception>
        /// <returns>The <see cref="IFinalRuleBuilder{TObject,TTarget,TValue}"/>.</returns>
        public static IFinalRuleBuilder<TObject, TTarget, bool> IsNotEqualTo<TObject, TTarget>(
            this IInitialRuleBuilder<TObject, TTarget> builder, Func<RuleContext, object> otherValueProvider,
            bool continueValidationWhenFalse = false)
            => builder.HasRule<TObject, TTarget, bool>(
                new ComparisonRule(continueValidationWhenFalse, otherValueProvider, Comparison.NotEqual));

        /// <summary>
        /// Creates the <see cref="Comparison.NotEqual"/> rule.
        /// </summary>
        /// <typeparam name="TObject">A type of an object.</typeparam>
        /// <typeparam name="TTarget">A type of a target.</typeparam>
        /// <param name="builder">The <see cref="IInitialRuleBuilder{TObject,TTarget}"/>.</param>
        /// <param name="otherValue">Other value.</param>
        /// <param name="continueValidationWhenFalse">
        /// Determines whether to continue validation when a value of a rule is <see langword="false"/>.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// The <paramref name="builder"/> is <see langword="null"/>.
        /// </exception>
        /// <returns>The <see cref="IFinalRuleBuilder{TObject,TTarget,TValue}"/>.</returns>
        public static IFinalRuleBuilder<TObject, TTarget, bool> IsNotEqualTo<TObject, TTarget>(
            this IInitialRuleBuilder<TObject, TTarget> builder, object otherValue,
            bool continueValidationWhenFalse = false)
            => builder.HasRule<TObject, TTarget, bool>(new ComparisonRule(
                continueValidationWhenFalse, otherValue, Comparison.NotEqual));

        /// <summary>
        /// Creates the <see cref="Comparison.LessThan"/> rule.
        /// </summary>
        /// <typeparam name="TObject">A type of an object.</typeparam>
        /// <typeparam name="TTarget">A type of a target.</typeparam>
        /// <param name="builder">The <see cref="IInitialRuleBuilder{TObject,TTarget}"/>.</param>
        /// <param name="otherValueProvider">Other value provider.</param>
        /// <param name="continueValidationWhenFalse">
        /// Determines whether to continue validation when a value of a rule is <see langword="false"/>.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// The <paramref name="builder"/> is <see langword="null"/>.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// The <paramref name="otherValueProvider"/> is <see langword="null"/>.
        /// </exception>
        /// <returns>The <see cref="IFinalRuleBuilder{TObject,TTarget,TValue}"/>.</returns>
        public static IFinalRuleBuilder<TObject, TTarget, bool> IsLessThan<TObject, TTarget>(
            this IInitialRuleBuilder<TObject, TTarget> builder, Func<RuleContext, object> otherValueProvider,
            bool continueValidationWhenFalse = false)
            => builder.HasRule<TObject, TTarget, bool>(new ComparisonRule(
                continueValidationWhenFalse, otherValueProvider, Comparison.LessThan));

        /// <summary>
        /// Creates the <see cref="Comparison.LessThan"/> rule.
        /// </summary>
        /// <typeparam name="TObject">A type of an object.</typeparam>
        /// <typeparam name="TTarget">A type of a target.</typeparam>
        /// <param name="builder">The <see cref="IInitialRuleBuilder{TObject,TTarget}"/>.</param>
        /// <param name="otherValue">Other value.</param>
        /// <param name="continueValidationWhenFalse">
        /// Determines whether to continue validation when a value of a rule is <see langword="false"/>.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// The <paramref name="builder"/> is <see langword="null"/>.
        /// </exception>
        /// <returns>The <see cref="IFinalRuleBuilder{TObject,TTarget,TValue}"/>.</returns>
        public static IFinalRuleBuilder<TObject, TTarget, bool> IsLessThan<TObject, TTarget>(
            this IInitialRuleBuilder<TObject, TTarget> builder, object otherValue,
            bool continueValidationWhenFalse = false)
            => builder.HasRule<TObject, TTarget, bool>(new ComparisonRule(
                continueValidationWhenFalse, otherValue, Comparison.LessThan));

        /// <summary>
        /// Creates the <see cref="Comparison.LessThanOrEqual"/> rule.
        /// </summary>
        /// <typeparam name="TObject">A type of an object.</typeparam>
        /// <typeparam name="TTarget">A type of a target.</typeparam>
        /// <param name="builder">The <see cref="IInitialRuleBuilder{TObject,TTarget}"/>.</param>
        /// <param name="otherValueProvider">Other value provider.</param>
        /// <param name="continueValidationWhenFalse">
        /// Determines whether to continue validation when a value of a rule is <see langword="false"/>.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// The <paramref name="builder"/> is <see langword="null"/>.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// The <paramref name="otherValueProvider"/> is <see langword="null"/>.
        /// </exception>
        /// <returns>The <see cref="IFinalRuleBuilder{TObject,TTarget,TValue}"/>.</returns>
        public static IFinalRuleBuilder<TObject, TTarget, bool> IsLessThanOrEqualTo<TObject, TTarget>(
            this IInitialRuleBuilder<TObject, TTarget> builder, Func<RuleContext, object> otherValueProvider,
            bool continueValidationWhenFalse = false)
            => builder.HasRule<TObject, TTarget, bool>(new ComparisonRule(
                continueValidationWhenFalse, otherValueProvider, Comparison.LessThanOrEqual));

        /// <summary>
        /// Creates the <see cref="Comparison.LessThanOrEqual"/> rule.
        /// </summary>
        /// <typeparam name="TObject">A type of an object.</typeparam>
        /// <typeparam name="TTarget">A type of a target.</typeparam>
        /// <param name="builder">The <see cref="IInitialRuleBuilder{TObject,TTarget}"/>.</param>
        /// <param name="otherValue">Other value.</param>
        /// <param name="continueValidationWhenFalse">
        /// Determines whether to continue validation when a value of a rule is <see langword="false"/>.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// The <paramref name="builder"/> is <see langword="null"/>.
        /// </exception>
        /// <returns>The <see cref="IFinalRuleBuilder{TObject,TTarget,TValue}"/>.</returns>
        public static IFinalRuleBuilder<TObject, TTarget, bool> IsLessThanOrEqualTo<TObject, TTarget>(
            this IInitialRuleBuilder<TObject, TTarget> builder, object otherValue,
            bool continueValidationWhenFalse = false)
            => builder.HasRule<TObject, TTarget, bool>(new ComparisonRule(
                continueValidationWhenFalse, otherValue, Comparison.LessThanOrEqual));

        /// <summary>
        /// Creates the <see cref="Comparison.GreaterThan"/> rule.
        /// </summary>
        /// <typeparam name="TObject">A type of an object.</typeparam>
        /// <typeparam name="TTarget">A type of a target.</typeparam>
        /// <param name="builder">The <see cref="IInitialRuleBuilder{TObject,TTarget}"/>.</param>
        /// <param name="otherValueProvider">Other value provider.</param>
        /// <param name="continueValidationWhenFalse">
        /// Determines whether to continue validation when a value of a rule is <see langword="false"/>.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// The <paramref name="builder"/> is <see langword="null"/>.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// The <paramref name="otherValueProvider"/> is <see langword="null"/>.
        /// </exception>
        /// <returns>The <see cref="IFinalRuleBuilder{TObject,TTarget,TValue}"/>.</returns>
        public static IFinalRuleBuilder<TObject, TTarget, bool> IsGreaterThan<TObject, TTarget>(
            this IInitialRuleBuilder<TObject, TTarget> builder, Func<RuleContext, object> otherValueProvider,
            bool continueValidationWhenFalse = false)
            => builder.HasRule<TObject, TTarget, bool>(new ComparisonRule(
                continueValidationWhenFalse, otherValueProvider, Comparison.GreaterThan));

        /// <summary>
        /// Creates the <see cref="Comparison.GreaterThan"/> rule.
        /// </summary>
        /// <typeparam name="TObject">A type of an object.</typeparam>
        /// <typeparam name="TTarget">A type of a target.</typeparam>
        /// <param name="builder">The <see cref="IInitialRuleBuilder{TObject,TTarget}"/>.</param>
        /// <param name="otherValue">Other value.</param>
        /// <param name="continueValidationWhenFalse">
        /// Determines whether to continue validation when a value of a rule is <see langword="false"/>.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// The <paramref name="builder"/> is <see langword="null"/>.
        /// </exception>
        /// <returns>The <see cref="IFinalRuleBuilder{TObject,TTarget,TValue}"/>.</returns>
        public static IFinalRuleBuilder<TObject, TTarget, bool> IsGreaterThan<TObject, TTarget>(
            this IInitialRuleBuilder<TObject, TTarget> builder, object otherValue,
            bool continueValidationWhenFalse = false)
            => builder.HasRule<TObject, TTarget, bool>(new ComparisonRule(
                continueValidationWhenFalse, otherValue, Comparison.GreaterThan));

        /// <summary>
        /// Creates the <see cref="Comparison.GreaterThanOrEqual"/> rule.
        /// </summary>
        /// <typeparam name="TObject">A type of an object.</typeparam>
        /// <typeparam name="TTarget">A type of a target.</typeparam>
        /// <param name="builder">The <see cref="IInitialRuleBuilder{TObject,TTarget}"/>.</param>
        /// <param name="otherValueProvider">Other value provider.</param>
        /// <param name="continueValidationWhenFalse">
        /// Determines whether to continue validation when a value of a rule is <see langword="false"/>.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// The <paramref name="builder"/> is <see langword="null"/>.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// The <paramref name="otherValueProvider"/> is <see langword="null"/>.
        /// </exception>
        /// <returns>The <see cref="IFinalRuleBuilder{TObject,TTarget,TValue}"/>.</returns>
        public static IFinalRuleBuilder<TObject, TTarget, bool> IsGreaterThanOrEqualTo<TObject, TTarget>(
            this IInitialRuleBuilder<TObject, TTarget> builder, Func<RuleContext, object> otherValueProvider,
            bool continueValidationWhenFalse = false)
            => builder.HasRule<TObject, TTarget, bool>(new ComparisonRule(
                continueValidationWhenFalse, otherValueProvider, Comparison.GreaterThanOrEqual));

        /// <summary>
        /// Creates the <see cref="Comparison.GreaterThanOrEqual"/> rule.
        /// </summary>
        /// <typeparam name="TObject">A type of an object.</typeparam>
        /// <typeparam name="TTarget">A type of a target.</typeparam>
        /// <param name="builder">The <see cref="IInitialRuleBuilder{TObject,TTarget}"/>.</param>
        /// <param name="otherValue">Other value.</param>
        /// <param name="continueValidationWhenFalse">
        /// Determines whether to continue validation when a value of a rule is <see langword="false"/>.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// The <paramref name="builder"/> is <see langword="null"/>.
        /// </exception>
        /// <returns>The <see cref="IFinalRuleBuilder{TObject,TTarget,TValue}"/>.</returns>
        public static IFinalRuleBuilder<TObject, TTarget, bool> IsGreaterThanOrEqualTo<TObject, TTarget>(
            this IInitialRuleBuilder<TObject, TTarget> builder, object otherValue,
            bool continueValidationWhenFalse = false)
            => builder.HasRule<TObject, TTarget, bool>(new ComparisonRule(
                continueValidationWhenFalse, otherValue, Comparison.GreaterThanOrEqual));

        /// <summary>
        /// Creates the <see cref="Comparison.Equal"/> rule.
        /// </summary>
        /// <typeparam name="TObject">A type of an object.</typeparam>
        /// <typeparam name="TTarget">A type of a target.</typeparam>
        /// <param name="builder">The <see cref="IInitialRuleBuilder{TObject,TTarget}"/>.</param>
        /// <param name="otherTargetExpression">An expression of other target to compare with.</param>
        /// <param name="continueValidationWhenFalse">
        /// Determines whether to continue validation when a value of a rule is <see langword="false"/>.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// The <paramref name="builder"/> is <see langword="null"/>.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// The <paramref name="otherTargetExpression"/> is <see langword="null"/>.
        /// </exception>
        /// <returns>The <see cref="IFinalRuleBuilder{TObject,TTarget,TValue}"/>.</returns>
        public static IFinalRuleBuilder<TObject, TTarget, bool> IsEqualToTarget<TObject, TTarget>(
            this IInitialRuleBuilder<TObject, TTarget> builder,
            Expression<Func<IInitialTargetBuilder<TObject>, IFinalTargetBuilder<TObject, object>>> otherTargetExpression,
            bool continueValidationWhenFalse = false)
            => builder.HasTargetComparisonRule(otherTargetExpression, (b, target)
                => new TargetComparisonRule(b, target, Comparison.Equal), continueValidationWhenFalse);

        /// <summary>
        /// Creates the <see cref="Comparison.NotEqual"/> rule.
        /// </summary>
        /// <typeparam name="TObject">A type of an object.</typeparam>
        /// <typeparam name="TTarget">A type of a target.</typeparam>
        /// <param name="builder">The <see cref="IInitialRuleBuilder{TObject,TTarget}"/>.</param>
        /// <param name="otherTargetExpression">An expression of other target to operate with.</param>
        /// <param name="continueValidationWhenFalse">
        /// Determines whether to continue validation when a value of a rule is <see langword="false"/>.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// The <paramref name="builder"/> is <see langword="null"/>.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// The <paramref name="otherTargetExpression"/> is <see langword="null"/>.
        /// </exception>
        /// <returns>The <see cref="IFinalRuleBuilder{TObject,TTarget,TValue}"/>.</returns>
        public static IFinalRuleBuilder<TObject, TTarget, bool> IsNotEqualToTarget<TObject, TTarget>(
            this IInitialRuleBuilder<TObject, TTarget> builder,
            Expression<Func<IInitialTargetBuilder<TObject>, IFinalTargetBuilder<TObject, object>>> otherTargetExpression,
            bool continueValidationWhenFalse = false)
            => builder.HasTargetComparisonRule(otherTargetExpression, (b, target)
                => new TargetComparisonRule(b, target, Comparison.NotEqual), continueValidationWhenFalse);

        /// <summary>
        /// Creates the <see cref="Comparison.LessThan"/> rule.
        /// </summary>
        /// <typeparam name="TObject">A type of an object.</typeparam>
        /// <typeparam name="TTarget">A type of a target.</typeparam>
        /// <param name="builder">The <see cref="IInitialRuleBuilder{TObject,TTarget}"/>.</param>
        /// <param name="otherTargetExpression">An expression of other target to operate with.</param>
        /// <param name="continueValidationWhenFalse">
        /// Determines whether to continue validation when a value of a rule is <see langword="false"/>.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// The <paramref name="builder"/> is <see langword="null"/>.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// The <paramref name="otherTargetExpression"/> is <see langword="null"/>.
        /// </exception>
        /// <returns>The <see cref="IFinalRuleBuilder{TObject,TTarget,TValue}"/>.</returns>
        public static IFinalRuleBuilder<TObject, TTarget, bool> IsLessThanTarget<TObject, TTarget>(
            this IInitialRuleBuilder<TObject, TTarget> builder,
            Expression<Func<IInitialTargetBuilder<TObject>, IFinalTargetBuilder<TObject, object>>> otherTargetExpression,
            bool continueValidationWhenFalse = false)
            => builder.HasTargetComparisonRule(otherTargetExpression, (b, target)
                => new TargetComparisonRule(b, target, Comparison.LessThan), continueValidationWhenFalse);

        /// <summary>
        /// Creates the <see cref="Comparison.LessThanOrEqual"/> rule.
        /// </summary>
        /// <typeparam name="TObject">A type of an object.</typeparam>
        /// <typeparam name="TTarget">A type of a target.</typeparam>
        /// <param name="builder">The <see cref="IInitialRuleBuilder{TObject,TTarget}"/>.</param>
        /// <param name="otherTargetExpression">An expression of other target to operate with.</param>
        /// <param name="continueValidationWhenFalse">
        /// Determines whether to continue validation when a value of a rule is <see langword="false"/>.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// The <paramref name="builder"/> is <see langword="null"/>.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// The <paramref name="otherTargetExpression"/> is <see langword="null"/>.
        /// </exception>
        /// <returns>The <see cref="IFinalRuleBuilder{TObject,TTarget,TValue}"/>.</returns>
        public static IFinalRuleBuilder<TObject, TTarget, bool> IsLessThanOrEqualToTarget<TObject, TTarget>(
            this IInitialRuleBuilder<TObject, TTarget> builder,
            Expression<Func<IInitialTargetBuilder<TObject>, IFinalTargetBuilder<TObject, object>>> otherTargetExpression,
            bool continueValidationWhenFalse = false)
            => builder.HasTargetComparisonRule(otherTargetExpression, (b, target)
                => new TargetComparisonRule(b, target, Comparison.LessThanOrEqual), continueValidationWhenFalse);

        /// <summary>
        /// Creates the <see cref="Comparison.GreaterThan"/> rule.
        /// </summary>
        /// <typeparam name="TObject">A type of an object.</typeparam>
        /// <typeparam name="TTarget">A type of a target.</typeparam>
        /// <param name="builder">The <see cref="IInitialRuleBuilder{TObject,TTarget}"/>.</param>
        /// /// <param name="otherTargetExpression">An expression of other target to operate with.</param>
        /// <param name="continueValidationWhenFalse">
        /// Determines whether to continue validation when a value of a rule is <see langword="false"/>.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// The <paramref name="builder"/> is <see langword="null"/>.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// The <paramref name="otherTargetExpression"/> is <see langword="null"/>.
        /// </exception>
        /// <returns>The <see cref="IFinalRuleBuilder{TObject,TTarget,TValue}"/>.</returns>
        public static IFinalRuleBuilder<TObject, TTarget, bool> IsGreaterThanTarget<TObject, TTarget>(
            this IInitialRuleBuilder<TObject, TTarget> builder,
            Expression<Func<IInitialTargetBuilder<TObject>, IFinalTargetBuilder<TObject, object>>> otherTargetExpression,
            bool continueValidationWhenFalse = false)
            => builder.HasTargetComparisonRule(otherTargetExpression, (b, target)
                => new TargetComparisonRule(b, target, Comparison.GreaterThan), continueValidationWhenFalse);

        /// <summary>
        /// Creates the <see cref="Comparison.GreaterThanOrEqual"/> rule.
        /// </summary>
        /// <typeparam name="TObject">A type of an object.</typeparam>
        /// <typeparam name="TTarget">A type of a target.</typeparam>
        /// <param name="builder">The <see cref="IInitialRuleBuilder{TObject,TTarget}"/>.</param>
        /// <param name="otherTargetExpression">An expression of other target to compare with.</param>
        /// <param name="continueValidationWhenFalse">
        /// Determines whether to continue validation when a value of a rule is <see langword="false"/>.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// The <paramref name="builder"/> is <see langword="null"/>.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// The <paramref name="otherTargetExpression"/> is <see langword="null"/>.
        /// </exception>
        /// <returns>The <see cref="IFinalRuleBuilder{TObject,TTarget,TValue}"/>.</returns>
        public static IFinalRuleBuilder<TObject, TTarget, bool> IsGreaterThanOrEqualToTarget<TObject, TTarget>(
            this IInitialRuleBuilder<TObject, TTarget> builder,
            Expression<Func<IInitialTargetBuilder<TObject>, IFinalTargetBuilder<TObject, object>>> otherTargetExpression,
            bool continueValidationWhenFalse = false)
            => builder.HasTargetComparisonRule(otherTargetExpression, (b, target)
                => new TargetComparisonRule(b, target, Comparison.GreaterThanOrEqual), continueValidationWhenFalse);

        /// <summary>
        /// Adds the <see cref="TargetComparisonRule"/>.
        /// </summary>
        /// <typeparam name="TObject">A type of an object.</typeparam>
        /// <typeparam name="TTarget">A type of a target.</typeparam>
        /// <param name="builder">The <see cref="IInitialRuleBuilder{TObject,TTarget}"/>.</param>
        /// <param name="otherTargetExpression">An expression of other target to compare with.</param>
        /// <param name="ruleFactory">A factory to create a rule to add.</param>
        /// <param name="continueValidationWhenFalse">
        /// Determines whether to continue validation when a value of a rule is <see langword="false"/>.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// The <paramref name="builder"/> is <see langword="null"/>.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// The <paramref name="otherTargetExpression"/> is <see langword="null"/>.
        /// </exception>
        /// <returns>The <see cref="IFinalRuleBuilder{TObject,TTarget,TValue}"/>.</returns>
        private static IFinalRuleBuilder<TObject, TTarget, bool> HasTargetComparisonRule<TObject, TTarget>(
            this IInitialRuleBuilder<TObject, TTarget> builder,
            Expression<Func<IInitialTargetBuilder<TObject>, IFinalTargetBuilder<TObject, object>>> otherTargetExpression,
            Func<bool, Target, TargetComparisonRule> ruleFactory, bool continueValidationWhenFalse = false)
        {
            Throw<ArgumentNullException>.IfNull(otherTargetExpression, nameof(otherTargetExpression));

            var otherTarget = otherTargetExpression.Compile()(
                new InitialTargetBuilder<TObject>(new BuildingValidator<TObject>())).Target;

            return builder.HasRule<TObject, TTarget, bool>(ruleFactory(continueValidationWhenFalse, otherTarget));
        }

        #endregion
    }
}