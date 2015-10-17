///*
//The MIT License (MIT)

//Copyright (c) 2015 NLion.Validation - Hennadii Lutsyshyn (NLion)

//Permission is hereby granted, free of charge, to any person obtaining a copy
//of this software and associated documentation files (the "Software"), to deal
//in the Software without restriction, including without limitation the rights
//to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
//copies of the Software, and to permit persons to whom the Software is
//furnished to do so, subject to the following conditions:

//The above copyright notice and this permission notice shall be included in all
//copies or substantial portions of the Software.

//THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
//IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
//FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
//AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
//LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
//OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
//SOFTWARE.
//*/

//using System;
//using System.Collections.Generic;
//using System.Globalization;
//using System.Linq.Expressions;
//using System.Text.RegularExpressions;
//using System.Threading;
//using NLion.Validation.Internal;

//namespace NLion.Validation
//{
//	/// <summary>
//	/// Provides extensions for the rule builder.
//	/// </summary>
//	public static class RuleBuilderExtensions
//	{
//		#region Fields

//		private const string EmailPattern = @"^((([a-z]|\d|[!#\$%&'\*\+\-\/=\?\^_`{\|}~]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+(\.([a-z]|\d|[!#\$%&'\*\+\-\/=\?\^_`{\|}~]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+)*)|((\x22)((((\x20|\x09)*(\x0d\x0a))?(\x20|\x09)+)?(([\x01-\x08\x0b\x0c\x0e-\x1f\x7f]|\x21|[\x23-\x5b]|[\x5d-\x7e]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(\\([\x01-\x09\x0b\x0c\x0d-\x7f]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]))))*(((\x20|\x09)*(\x0d\x0a))?(\x20|\x09)+)?(\x22)))@((([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))\.)+(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))$";

//		private const string UrlPattern = @"^(https?|ftp):\/\/(((([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(%[\da-f]{2})|[!\$&'\(\)\*\+,;=]|:)*@)?(((\d|[1-9]\d|1\d\d|2[0-4]\d|25[0-5])\.(\d|[1-9]\d|1\d\d|2[0-4]\d|25[0-5])\.(\d|[1-9]\d|1\d\d|2[0-4]\d|25[0-5])\.(\d|[1-9]\d|1\d\d|2[0-4]\d|25[0-5]))|((([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))\.)+(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))\.?)(:\d*)?)(\/((([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(%[\da-f]{2})|[!\$&'\(\)\*\+,;=]|:|@)+(\/(([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(%[\da-f]{2})|[!\$&'\(\)\*\+,;=]|:|@)*)*)?)?(\?((([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(%[\da-f]{2})|[!\$&'\(\)\*\+,;=]|:|@)|[\uE000-\uF8FF]|\/|\?)*)?(\#((([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(%[\da-f]{2})|[!\$&'\(\)\*\+,;=]|:|@)|\/|\?)*)?$";

//		private const string DigitsPattern = @"^\d+$";

//		/// <summary>
//		/// The required rule name.
//		/// </summary>
//		public const string RequiredRuleName = "Required";

//		/// <summary>
//		/// The length rule name.
//		/// </summary>
//		public const string LengthRuleName = "Length";

//		/// <summary>
//		/// The range rule name.
//		/// </summary>
//		public const string RangeRuleName = "Range";

//		/// <summary>
//		/// The regex rule name.
//		/// </summary>
//		public const string RegexRuleName = "Regex";

//		/// <summary>
//		/// The equal to rule name.
//		/// </summary>
//		public const string EqualToRuleName = "EqualTo";

//		/// <summary>
//		/// The email rule name.
//		/// </summary>
//		public const string EmailRuleName = "Email";

//		/// <summary>
//		/// The URL rule name.
//		/// </summary>
//		public const string UrlRuleName = "Url";

//		/// <summary>
//		/// The digits rule name.
//		/// </summary>
//		public const string DigitsRuleName = "Digits";

//		/// <summary>
//		/// The credit card rule name.
//		/// </summary>
//		public const string CreditCardRuleName = "CreditCard";

//		/// <summary>
//		/// The less than rule name.
//		/// </summary>
//		public const string LessThanRuleName = "LessThan";

//		/// <summary>
//		/// The less than or equal rule name.
//		/// </summary>
//		public const string LessThanOrEqualRuleName = "LessThanOrEqual";

//		/// <summary>
//		/// The short date rule name.
//		/// </summary>
//		public const string ShortDateRuleName = "ShortDate";

//		/// <summary>
//		/// The rule name parameter name.
//		/// </summary>
//		public const string RuleNameParamName = "ruleName";

//		/// <summary>
//		/// The target name parameter name
//		/// </summary>
//		public const string TargetNameParamName = "targetName";

//		/// <summary>
//		/// The minimum parameter name.
//		/// </summary>
//		public const string MinimumParamName = "minimum";

//		/// <summary>
//		/// The maximum parameter name.
//		/// </summary>
//		public const string MaximumParamName = "maximum";

//		/// <summary>
//		/// The pattern parameter name.
//		/// </summary>
//		public const string PatternParamName = "pattern";

//		/// <summary>
//		/// The member parameter name.
//		/// </summary>
//		public const string MemberParamName = "member";

//		#endregion

//		#region Methods

//		/// <summary>
//		/// Creates a custom rule.
//		/// </summary>
//		/// <typeparam name="TEntity">The type of the entity.</typeparam>
//		/// <typeparam name="TTarget">The type of the target.</typeparam>
//		/// <typeparam name="TOperatorResult">The type of the operator result.</typeparam>
//		/// <param name="builder">The builder.</param>
//		/// <param name="ruleOperator">The rule operator.</param>
//		/// <param name="ruleName">Name of the rule.</param>
//		/// <param name="parameters">The parameters.</param>
//		/// <returns>The rule result builder.</returns>
//		/// <exception cref="System.ArgumentNullException">
//		/// The builder is null
//		/// or
//		/// The <paramref name="ruleOperator"/> is null.
//		/// </exception>
//		/// <exception cref="System.ArgumentException">The <paramref name="ruleName"/> is null or empty.</exception>
//		/// <exception cref="System.InvalidOperationException">
//		/// The <see cref="IInitialRuleBuilder{TEntity,TTarget}.Target"/> is null
//		/// or
//		/// The <see cref="IBuilder.Validator"/> is null in the
//		/// <see cref="IInitialRuleBuilder{TEntity,TTarget}"/>
//		/// or
//		/// The component factory couldn't create a <see cref="IRule"/> instance
//		/// or
//		/// The <see cref="IRule.RuleParameters"/> in a rule is null
//		/// or
//		/// The component factory couldn't create a
//		/// <see cref="IRuleResultBuilder{TEntity,TTarget,TOperatorResult}"/> instance.
//		/// </exception>
//		public static IRuleResultBuilder<TEntity, TTarget, TOperatorResult>
//			HasCustomRule<TEntity, TTarget, TOperatorResult>(
//			this IInitialRuleBuilder<TEntity, TTarget> builder,
//			Func<RuleOperatorContext, object> ruleOperator,
//			string ruleName, IDictionary<string, object> parameters = null)
//		{
//			Throw.ArgumentNullException_IfNull(builder, "builder");
//			Throw.ArgumentException_IfNullOrEmpty(ruleName, "ruleName");
//			Throw.ArgumentNullException_IfNull(ruleOperator, "ruleOperator");
//			Throw.InvalidOperationException_IfMemberIsNull(builder, o => o.Target);
//			Throw.InvalidOperationException_IfMemberIsNull(builder, o => o.Validator);

//			var rule = NvalBuilder.Current.Factory.CreateRule();

//			Throw.InvalidOperationException_IfFactoryFailed(rule);

//			rule.Name = ruleName;
//			rule.Target = builder.Target;

//			rule.Operator = ruleOperator;

//			Throw.InvalidOperationException_IfMemberIsNull(rule, o => o.RuleParameters);

//			if (parameters != null && parameters.Count > 0)
//			{
//				foreach (var kvp in parameters)
//				{
//					rule.RuleParameters.Add(kvp.Key, kvp.Value);
//				}
//			}

//			builder.Validator.Rules.Add(rule);

//			var ruleResultBuilder = NvalBuilder.Current
//				.Factory.CreateRuleResultBuilder<TEntity, TTarget, TOperatorResult>();

//			Throw.InvalidOperationException_IfFactoryFailed(ruleResultBuilder);

//			ruleResultBuilder.Rule = rule;
//			ruleResultBuilder.Validator = builder.Validator;

//			return ruleResultBuilder;
//		}

//		/// <summary>
//		/// Creates the required rule.
//		/// </summary>
//		/// <typeparam name="TEntity">The type of the entity.</typeparam>
//		/// <typeparam name="TTarget">The type of the target.</typeparam>
//		/// <param name="builder">The builder.</param>
//		/// <param name="ruleName">Name of the rule.</param>
//		/// <param name="continueOnMismatch"><c>true</c> if continue on mismatch, otherwise <c>false</c>.</param>
//		/// <returns>The rule result builder.</returns>
//		/// <exception cref="System.ArgumentNullException">
//		/// The builder is null.</exception>
//		/// <exception cref="System.ArgumentException">The <paramref name="ruleName"/> is null or empty.</exception>
//		/// <exception cref="System.InvalidOperationException">
//		/// The <see cref="IInitialRuleBuilder{TEntity,TTarget}.Target"/> is null
//		/// or
//		/// The <see cref="IBuilder.Validator"/> is null in the
//		/// <see cref="IInitialRuleBuilder{TEntity,TTarget}"/>
//		/// or
//		/// The component factory couldn't create a <see cref="IRule"/> instance
//		/// or
//		/// The <see cref="IRule.RuleParameters"/> in a rule is null
//		/// or
//		/// The component factory couldn't create a
//		/// <see cref="IRuleResultBuilder{TEntity,TTarget,TOperatorResult}"/> instance.
//		/// </exception>
//		public static IRuleResultBuilder<TEntity, TTarget, bool>
//			IsRequired<TEntity, TTarget>(this IInitialRuleBuilder<TEntity, TTarget> builder,
//			string ruleName = RequiredRuleName, bool continueOnMismatch = false)
//		{
//			Func<RuleOperatorContext, object> ruleOperator = (context) =>
//			{
//				var value = context.Target.Value(context.Entity);
//				bool match = value != null;
//				context.ValidationContext.ContinueValidation = continueOnMismatch || match;
//				return match;
//			};

//			return builder.HasCustomRule<TEntity, TTarget, bool>(ruleOperator, ruleName);
//		}

//		/// <summary>
//		/// Creates the length rule.
//		/// </summary>
//		/// <typeparam name="TEntity">The type of the entity.</typeparam>
//		/// <typeparam name="TTarget">The type of the target.</typeparam>
//		/// <param name="builder">The builder.</param>
//		/// <param name="minimum">The minimum.</param>
//		/// <param name="maximum">The maximum.</param>
//		/// <param name="ruleName">Name of the rule.</param>
//		/// <param name="continueOnMismatch"><c>true</c> if continue on mismatch, otherwise <c>false</c>.</param>
//		/// <returns>The rule result builder.</returns>
//		/// <exception cref="System.ArgumentNullException">
//		/// The builder is null.</exception>
//		/// <exception cref="System.ArgumentException">The <paramref name="ruleName"/> is null or empty.</exception>
//		/// <exception cref="System.InvalidOperationException">
//		/// The <see cref="IInitialRuleBuilder{TEntity,TTarget}.Target"/> is null
//		/// or
//		/// The <see cref="IBuilder.Validator"/> is null in the
//		/// <see cref="IInitialRuleBuilder{TEntity,TTarget}"/>
//		/// or
//		/// The component factory couldn't create a <see cref="IRule"/> instance
//		/// or
//		/// The <see cref="IRule.RuleParameters"/> in a rule is null
//		/// or
//		/// The component factory couldn't create a
//		/// <see cref="IRuleResultBuilder{TEntity,TTarget,TOperatorResult}"/> instance.
//		/// </exception>
//		public static IRuleResultBuilder<TEntity, TTarget, bool> HasLength<TEntity, TTarget>(
//			this IInitialRuleBuilder<TEntity, TTarget> builder, int? minimum = null,
//			int? maximum = null, string ruleName = LengthRuleName, bool continueOnMismatch = false)
//		{
//			Func<RuleOperatorContext, object> ruleOperator = (context) =>
//			{
//				int length = context.Target.Value(context.Entity).ToString().Length;
//				int? min = context.RuleParameters.ContainsKey(MinimumParamName)
//					? (int?)context.RuleParameters[MinimumParamName]
//					: null;
//				int? max = context.RuleParameters.ContainsKey(MaximumParamName)
//					? (int?)context.RuleParameters[MaximumParamName]
//					: null;
//				bool match = (min != null && length >= min.Value || min == null)
//					&& (max != null && length <= max.Value || max == null);
//				context.ValidationContext.ContinueValidation = continueOnMismatch || match;
//				return match;
//			};

//			var ruleParameters = new Dictionary<string, object>();

//			if (minimum != null)
//			{
//				ruleParameters.Add(MinimumParamName, minimum);
//			}

//			if (maximum != null)
//			{
//				ruleParameters.Add(MaximumParamName, maximum);
//			}

//			return builder.HasCustomRule<TEntity, TTarget, bool>(
//				ruleOperator, ruleName, ruleParameters);
//		}

//		/// <summary>
//		/// Creates the range rule.
//		/// </summary>
//		/// <typeparam name="TEntity">The type of the entity.</typeparam>
//		/// <typeparam name="TTarget">The type of the target.</typeparam>
//		/// <param name="builder">The builder.</param>
//		/// <param name="minimum">The minimum.</param>
//		/// <param name="maximum">The maximum.</param>
//		/// <param name="ruleName">Name of the rule.</param>
//		/// <param name="continueOnMismatch"><c>true</c> if continue on mismatch, otherwise <c>false</c>.</param>
//		/// <returns>The rule result builder.</returns>
//		/// <exception cref="System.ArgumentNullException">
//		/// The builder is null.</exception>
//		/// <exception cref="System.ArgumentException">The <paramref name="ruleName"/> is null or empty.</exception>
//		/// <exception cref="System.InvalidOperationException">
//		/// The <see cref="IInitialRuleBuilder{TEntity,TTarget}.Target"/> TargetBase is null
//		/// or
//		/// The <see cref="IBuilder.Validator"/> is null in the
//		/// <see cref="IInitialRuleBuilder{TEntity,TTarget}"/>
//		/// or
//		/// The component factory couldn't create a <see cref="IRule"/> instance
//		/// or
//		/// The <see cref="IRule.RuleParameters"/> in a rule is null
//		/// or
//		/// The component factory couldn't create a
//		/// <see cref="IRuleResultBuilder{TEntity,TTarget,TOperatorResult}"/> instance.
//		/// </exception>
//		public static IRuleResultBuilder<TEntity, TTarget, bool> InRange<TEntity, TTarget>(
//			this IInitialRuleBuilder<TEntity, TTarget> builder, IComparable<TTarget> minimum = null,
//			IComparable<TTarget> maximum = null, string ruleName = RangeRuleName, bool continueOnMismatch = false)
//			where TTarget : IComparable<TTarget>
//		{
//			Func<RuleOperatorContext, object> ruleOperator = (context) =>
//			{
//				var value = (TTarget)context.Target.Value(context.Entity);
//				IComparable<TTarget> min = context.RuleParameters.ContainsKey(MinimumParamName)
//					? (IComparable<TTarget>)context.RuleParameters[MinimumParamName]
//					: null;
//				IComparable<TTarget> max = context.RuleParameters.ContainsKey(MaximumParamName)
//					? (IComparable<TTarget>)context.RuleParameters[MaximumParamName]
//					: null;
//				bool match = (min != null && min.CompareTo(value) <= 0 || min == null)
//					&& (max != null && max.CompareTo(value) >= 0 || max == null);
//				context.ValidationContext.ContinueValidation = continueOnMismatch || match;
//				return match;
//			};

//			var ruleParameters = new Dictionary<string, object>();

//			if (minimum != null)
//			{
//				ruleParameters.Add(MinimumParamName, minimum);
//			}

//			if (maximum != null)
//			{
//				ruleParameters.Add(MaximumParamName, maximum);
//			}

//			return builder.HasCustomRule<TEntity, TTarget, bool>(
//				ruleOperator, ruleName, ruleParameters);
//		}

//		/// <summary>
//		/// Creates the ECMAScript regex rule.
//		/// </summary>
//		/// <typeparam name="TEntity">The type of the entity.</typeparam>
//		/// <typeparam name="TTarget">The type of the target.</typeparam>
//		/// <param name="builder">The builder.</param>
//		/// <param name="pattern">The pattern.</param>
//		/// <param name="ruleName">Name of the rule.</param>
//		/// <param name="continueOnMismatch"><c>true</c> if continue on mismatch, otherwise <c>false</c>.</param>
//		/// <returns>The rule result builder.</returns>
//		/// <exception cref="System.ArgumentNullException">
//		/// The builder is null.</exception>
//		/// <exception cref="System.ArgumentException">The <paramref name="ruleName"/> is null or empty.</exception>
//		/// <exception cref="System.InvalidOperationException">
//		/// The <see cref="IInitialRuleBuilder{TEntity,TTarget}.Target"/> is null
//		/// or
//		/// The <see cref="IBuilder.Validator"/> is null in the
//		/// <see cref="IInitialRuleBuilder{TEntity,TTarget}"/>
//		/// or
//		/// The component factory couldn't create a <see cref="IRule"/> instance
//		/// or
//		/// The <see cref="IRule.RuleParameters"/> in a rule is null
//		/// or
//		/// The component factory couldn't create a
//		/// <see cref="IRuleResultBuilder{TEntity,TTarget,TOperatorResult}"/> instance.
//		/// </exception>
//		public static IRuleResultBuilder<TEntity, TTarget, bool> MatchesRegex<TEntity, TTarget>(
//			this IInitialRuleBuilder<TEntity, TTarget> builder, string pattern, string ruleName = RegexRuleName,
//			bool continueOnMismatch = false)
//		{
//			Throw.ArgumentException_IfNullOrEmpty(pattern, PatternParamName);

//			Func<RuleOperatorContext, object> ruleOperator = (context) =>
//			{
//				var value = context.Target.Value(context.Entity).ToString();
//				string expression = context.RuleParameters[PatternParamName] as string;
//				bool match = Regex.IsMatch(value, expression, RegexOptions.ECMAScript);
//				context.ValidationContext.ContinueValidation = continueOnMismatch || match;
//				return match;
//			};

//			var ruleParameters = new Dictionary<string, object>();

//			ruleParameters.Add(PatternParamName, pattern);

//			return builder.HasCustomRule<TEntity, TTarget, bool>(
//				ruleOperator, ruleName, ruleParameters);
//		}

//		/// <summary>
//		/// Creates the equal to rule.
//		/// </summary>
//		/// <typeparam name="TEntity">The type of the entity.</typeparam>
//		/// <typeparam name="TTarget">The type of the target.</typeparam>
//		/// <param name="builder">The builder.</param>
//		/// <param name="member">The member.</param>
//		/// <param name="ruleName">Name of the rule.</param>
//		/// <param name="continueOnMismatch"><c>true</c> if continue on mismatch, otherwise <c>false</c>.</param>
//		/// <returns>The rule result builder.</returns>
//		/// <exception cref="System.ArgumentNullException">
//		/// The builder is null.</exception>
//		/// <exception cref="System.ArgumentException">The <paramref name="ruleName"/> is null or empty.</exception>
//		/// <exception cref="System.InvalidOperationException">
//		/// The <see cref="IInitialRuleBuilder{TEntity,TTarget}.Target"/> is null
//		/// or
//		/// The <see cref="IBuilder.Validator"/> is null in the
//		/// <see cref="IInitialRuleBuilder{TEntity,TTarget}"/>
//		/// or
//		/// The component factory couldn't create a <see cref="IRule"/> instance
//		/// or
//		/// The <see cref="IRule.RuleParameters"/> in a rule is null
//		/// or
//		/// The component factory couldn't create a
//		/// <see cref="IRuleResultBuilder{TEntity,TTarget,TOperatorResult}"/> instance.
//		/// </exception>
//		public static IRuleResultBuilder<TEntity, TTarget, bool>
//			EqualsTo<TEntity, TTarget>(this IInitialRuleBuilder<TEntity, TTarget> builder,
//			Expression<Func<TEntity, TTarget>> member, string ruleName = EqualToRuleName,
//			bool continueOnMismatch = false) where TTarget : IEquatable<TTarget>
//		{
//			Throw.ArgumentNullException_IfNull(member, MemberParamName);

//			Func<RuleOperatorContext, object> ruleOperator = (context) =>
//			{
//				var value = context.Target.Value(context.Entity);
//				var memberValue = ReflectionHelper.GetMemberValue<TTarget>(context.Entity,
//					context.RuleParameters[MemberParamName] as string);
//				bool match;
//				if (value != null)
//				{
//					match = value.Equals(memberValue);
//				}
//				else if (memberValue != null)
//				{
//					match = false;
//				}
//				else
//				{
//					match = true;
//				}
//				context.ValidationContext.ContinueValidation = continueOnMismatch || match;
//				return match;
//			};

//			var ruleParameters = new Dictionary<string, object>();

//			ruleParameters.Add(MemberParamName, ReflectionHelper.GetMemberName(member));

//			return builder.HasCustomRule<TEntity, TTarget, bool>(
//				ruleOperator, ruleName, ruleParameters);
//		}

//		/// <summary>
//		/// Creates the less than rule.
//		/// </summary>
//		/// <typeparam name="TEntity">The type of the entity.</typeparam>
//		/// <typeparam name="TTarget">The type of the target.</typeparam>
//		/// <param name="builder">The builder.</param>
//		/// <param name="member">The member to compare.</param>
//		/// <param name="ruleName">Name of the rule.</param>
//		/// <param name="continueOnMismatch"><c>true</c> if continue on mismatch, otherwise <c>false</c>.</param>
//		/// <returns>The rule result builder.</returns>
//		/// <exception cref="System.ArgumentNullException">
//		/// The builder is null.</exception>
//		/// <exception cref="System.ArgumentException">The <paramref name="ruleName"/> is null or empty.</exception>
//		/// <exception cref="System.InvalidOperationException">
//		/// The <see cref="IInitialRuleBuilder{TEntity,TTarget}.Target"/> is null
//		/// or
//		/// The <see cref="IBuilder.Validator"/> is null in the
//		/// <see cref="IInitialRuleBuilder{TEntity,TTarget}"/>
//		/// or
//		/// The component factory couldn't create a <see cref="IRule"/> instance
//		/// or
//		/// The <see cref="IRule.RuleParameters"/> in a rule is null
//		/// or
//		/// The component factory couldn't create a
//		/// <see cref="IRuleResultBuilder{TEntity,TTarget,TOperatorResult}"/> instance.
//		/// </exception>
//		public static IRuleResultBuilder<TEntity, TTarget, bool>
//			IsLessThan<TEntity, TTarget>(this IInitialRuleBuilder<TEntity, TTarget> builder,
//			Expression<Func<TEntity, TTarget>> member, string ruleName = LessThanRuleName,
//			bool continueOnMismatch = false) where TTarget : IComparable<TTarget>
//		{
//			Throw.ArgumentNullException_IfNull(member, MemberParamName);

//			Func<RuleOperatorContext, object> ruleOperator = (context) =>
//			{
//				var value = context.Target.Value(context.Entity) as IComparable<TTarget>;

//				var memberValue = ReflectionHelper.GetMemberValue<TTarget>(context.Entity,
//					context.RuleParameters[MemberParamName] as string);

//				bool match;

//				if (value != null)
//				{
//					match = value.CompareTo(memberValue) < 0;
//				}
//				else if (memberValue != null)
//				{
//					match = false;
//				}
//				else
//				{
//					match = true;
//				}
//				context.ValidationContext.ContinueValidation = continueOnMismatch || match;
//				return match;
//			};

//			var ruleParameters = new Dictionary<string, object>();

//			ruleParameters.Add(MemberParamName, ReflectionHelper.GetMemberName(member));

//			return builder.HasCustomRule<TEntity, TTarget, bool>(
//				ruleOperator, ruleName, ruleParameters);
//		}

//		/// <summary>
//		/// Creates the less than or equal rule.
//		/// </summary>
//		/// <typeparam name="TEntity">The type of the entity.</typeparam>
//		/// <typeparam name="TTarget">The type of the target.</typeparam>
//		/// <param name="builder">The builder.</param>
//		/// <param name="member">The member to compare.</param>
//		/// <param name="ruleName">Name of the rule.</param>
//		/// <param name="continueOnMismatch"><c>true</c> if continue on mismatch, otherwise <c>false</c>.</param>
//		/// <returns>The rule result builder.</returns>
//		/// <exception cref="System.ArgumentNullException">
//		/// The builder is null.</exception>
//		/// <exception cref="System.ArgumentException">The <paramref name="ruleName"/> is null or empty.</exception>
//		/// <exception cref="System.InvalidOperationException">
//		/// The <see cref="IInitialRuleBuilder{TEntity,TTarget}.Target"/> is null
//		/// or
//		/// The <see cref="IBuilder.Validator"/> is null in the
//		/// <see cref="IInitialRuleBuilder{TEntity,TTarget}"/>
//		/// or
//		/// The component factory couldn't create a <see cref="IRule"/> instance
//		/// or
//		/// The <see cref="IRule.RuleParameters"/> in a rule is null
//		/// or
//		/// The component factory couldn't create a
//		/// <see cref="IRuleResultBuilder{TEntity,TTarget,TOperatorResult}"/> instance.
//		/// </exception>
//		public static IRuleResultBuilder<TEntity, TTarget, bool>
//			IsLessThanOrEqual<TEntity, TTarget>(this IInitialRuleBuilder<TEntity, TTarget> builder,
//			Expression<Func<TEntity, TTarget>> member, string ruleName = LessThanOrEqualRuleName,
//			bool continueOnMismatch = false) where TTarget : IComparable<TTarget>
//		{
//			Throw.ArgumentNullException_IfNull(member, MemberParamName);

//			Func<RuleOperatorContext, object> ruleOperator = (context) =>
//			{
//				var value = context.Target.Value(context.Entity) as IComparable<TTarget>;

//				var memberValue = ReflectionHelper.GetMemberValue<TTarget>(context.Entity,
//					context.RuleParameters[MemberParamName] as string);

//				bool match;

//				if (value != null)
//				{
//					match = value.CompareTo(memberValue) <= 0;
//				}
//				else if (memberValue != null)
//				{
//					match = false;
//				}
//				else
//				{
//					match = true;
//				}
//				context.ValidationContext.ContinueValidation = continueOnMismatch || match;
//				return match;
//			};

//			var ruleParameters = new Dictionary<string, object>();

//			ruleParameters.Add(MemberParamName, ReflectionHelper.GetMemberName(member));

//			return builder.HasCustomRule<TEntity, TTarget, bool>(
//				ruleOperator, ruleName, ruleParameters);
//		}

//		/// <summary>
//		/// Creates the email rule.
//		/// </summary>
//		/// <typeparam name="TEntity">The type of the entity.</typeparam>
//		/// <typeparam name="TTarget">The type of the target.</typeparam>
//		/// <param name="builder">The builder.</param>
//		/// <param name="ruleName">Name of the rule.</param>
//		/// <param name="continueOnMismatch"><c>true</c> if continue on mismatch, otherwise <c>false</c>.</param>
//		/// <returns>The rule result builder.</returns>
//		/// <exception cref="System.ArgumentNullException">
//		/// The builder is null.</exception>
//		/// <exception cref="System.ArgumentException">The <paramref name="ruleName"/> is null or empty.</exception>
//		/// <exception cref="System.InvalidOperationException">
//		/// The <see cref="IInitialRuleBuilder{TEntity,TTarget}.Target"/> is null
//		/// or
//		/// The <see cref="IBuilder.Validator"/> is null in the
//		/// <see cref="IInitialRuleBuilder{TEntity,TTarget}"/>
//		/// or
//		/// The component factory couldn't create a <see cref="IRule"/> instance
//		/// or
//		/// The <see cref="IRule.RuleParameters"/> in a rule is null
//		/// or
//		/// The component factory couldn't create a
//		/// <see cref="IRuleResultBuilder{TEntity,TTarget,TOperatorResult}"/> instance.
//		/// </exception>
//		public static IRuleResultBuilder<TEntity, TTarget, bool>
//			IsEmail<TEntity, TTarget>(this IInitialRuleBuilder<TEntity, TTarget> builder,
//			string ruleName = EmailRuleName, bool continueOnMismatch = false)
//		{
//			Func<RuleOperatorContext, object> ruleOperator = (context) =>
//			{
//				bool match = Regex.IsMatch(context.Target.Value(context.Entity).ToString(),
//					EmailPattern, RegexOptions.ECMAScript | RegexOptions.IgnoreCase);
//				context.ValidationContext.ContinueValidation = continueOnMismatch || match;
//				return match;
//			};

//			return builder.HasCustomRule<TEntity, TTarget, bool>(ruleOperator, ruleName);
//		}

//		/// <summary>
//		/// Creates the url rule.
//		/// </summary>
//		/// <typeparam name="TEntity">The type of the entity.</typeparam>
//		/// <typeparam name="TTarget">The type of the target.</typeparam>
//		/// <param name="builder">The builder.</param>
//		/// <param name="ruleName">Name of the rule.</param>
//		/// <param name="continueOnMismatch"><c>true</c> if continue on mismatch, otherwise <c>false</c>.</param>
//		/// <returns>The rule result builder.</returns>
//		/// <exception cref="System.ArgumentNullException">
//		/// The builder is null.</exception>
//		/// <exception cref="System.ArgumentException">The <paramref name="ruleName"/> is null or empty.</exception>
//		/// <exception cref="System.InvalidOperationException">
//		/// The <see cref="IInitialRuleBuilder{TEntity,TTarget}.Target"/> is null
//		/// or
//		/// The <see cref="IBuilder.Validator"/> is null in the
//		/// <see cref="IInitialRuleBuilder{TEntity,TTarget}"/>
//		/// or
//		/// The component factory couldn't create a <see cref="IRule"/> instance
//		/// or
//		/// The <see cref="IRule.RuleParameters"/> in a rule is null
//		/// or
//		/// The component factory couldn't create a
//		/// <see cref="IRuleResultBuilder{TEntity,TTarget,TOperatorResult}"/> instance.
//		/// </exception>
//		public static IRuleResultBuilder<TEntity, TTarget, bool>
//			IsUrl<TEntity, TTarget>(this IInitialRuleBuilder<TEntity, TTarget> builder,
//			string ruleName = UrlRuleName, bool continueOnMismatch = false)
//		{
//			Func<RuleOperatorContext, object> ruleOperator = (context) =>
//			{
//				bool match = Regex.IsMatch(context.Target.Value(context.Entity).ToString(),
//					UrlPattern, RegexOptions.ECMAScript | RegexOptions.IgnoreCase);
//				context.ValidationContext.ContinueValidation = continueOnMismatch || match;
//				return match;
//			};

//			return builder.HasCustomRule<TEntity, TTarget, bool>(ruleOperator, ruleName);
//		}

//		/// <summary>
//		/// Creates the digits rule.
//		/// </summary>
//		/// <typeparam name="TEntity">The type of the entity.</typeparam>
//		/// <typeparam name="TTarget">The type of the target.</typeparam>
//		/// <param name="builder">The builder.</param>
//		/// <param name="ruleName">Name of the rule.</param>
//		/// <param name="continueOnMismatch"><c>true</c> if continue on mismatch, otherwise <c>false</c>.</param>
//		/// <returns>The rule result builder.</returns>
//		/// <exception cref="System.ArgumentNullException">
//		/// The builder is null.</exception>
//		/// <exception cref="System.ArgumentException">The <paramref name="ruleName"/> is null or empty.</exception>
//		/// <exception cref="System.InvalidOperationException">
//		/// The <see cref="IInitialRuleBuilder{TEntity,TTarget}.Target"/> is null
//		/// or
//		/// The <see cref="IBuilder.Validator"/> is null in the
//		/// <see cref="IInitialRuleBuilder{TEntity,TTarget}"/>
//		/// or
//		/// The component factory couldn't create a <see cref="IRule"/> instance
//		/// or
//		/// The <see cref="IRule.RuleParameters"/> in a rule is null
//		/// or
//		/// The component factory couldn't create a
//		/// <see cref="IRuleResultBuilder{TEntity,TTarget,TOperatorResult}"/> instance.
//		/// </exception>
//		public static IRuleResultBuilder<TEntity, TTarget, bool>
//			HasOnlyDigits<TEntity, TTarget>(this IInitialRuleBuilder<TEntity, TTarget> builder,
//			string ruleName = DigitsRuleName, bool continueOnMismatch = false)
//		{
//			Func<RuleOperatorContext, object> ruleOperator = (context) =>
//			{
//				bool match = Regex.IsMatch(context.Target.Value(context.Entity).ToString(),
//					DigitsPattern, RegexOptions.ECMAScript | RegexOptions.IgnoreCase);
//				context.ValidationContext.ContinueValidation = continueOnMismatch || match;
//				return match;
//			};

//			return builder.HasCustomRule<TEntity, TTarget, bool>(ruleOperator, ruleName);
//		}

//		/// <summary>
//		/// Creates the credit card rule.
//		/// </summary>
//		/// <typeparam name="TEntity">The type of the entity.</typeparam>
//		/// <typeparam name="TTarget">The type of the target.</typeparam>
//		/// <param name="builder">The builder.</param>
//		/// <param name="ruleName">Name of the rule.</param>
//		/// <param name="continueOnMismatch"><c>true</c> if continue on mismatch, otherwise <c>false</c>.</param>
//		/// <returns>The rule result builder.</returns>
//		/// <exception cref="System.ArgumentNullException">
//		/// The builder is null.</exception>
//		/// <exception cref="System.ArgumentException">The <paramref name="ruleName"/> is null or empty.</exception>
//		/// <exception cref="System.InvalidOperationException">
//		/// The <see cref="IInitialRuleBuilder{TEntity,TTarget}.Target"/> is null
//		/// or
//		/// The <see cref="IBuilder.Validator"/> is null in the
//		/// <see cref="IInitialRuleBuilder{TEntity,TTarget}"/>
//		/// or
//		/// The component factory couldn't create a <see cref="IRule"/> instance
//		/// or
//		/// The <see cref="IRule.RuleParameters"/> in a rule is null
//		/// or
//		/// The component factory couldn't create a
//		/// <see cref="IRuleResultBuilder{TEntity,TTarget,TOperatorResult}"/> instance.
//		/// </exception>
//		public static IRuleResultBuilder<TEntity, TTarget, bool>
//			IsCreditCard<TEntity, TTarget>(this IInitialRuleBuilder<TEntity, TTarget> builder,
//			string ruleName = CreditCardRuleName, bool continueOnMismatch = false)
//		{
//			Func<RuleOperatorContext, object> ruleOperator = (context) =>
//			{
//				var value = context.Target.Value(context.Entity).ToString();

//				if (Regex.IsMatch(value, @"[^0-9 -]+"))
//				{
//					return false;
//				}

//				int nCheck = 0;
//				int nDigit = 0;
//				bool bEven = false;

//				value = Regex.Replace(value, @"\D", "", RegexOptions.ECMAScript);

//				for (int n = value.Length - 1; n >= 0; n--)
//				{
//					char cDigit = value[n];

//					nDigit = Convert.ToInt32(cDigit.ToString(), 10);

//					if (bEven)
//					{
//						if ((nDigit *= 2) > 9)
//						{
//							nDigit -= 9;
//						}
//					}

//					nCheck += nDigit;
//					bEven = !bEven;
//				}

//				bool match = (nCheck % 10) == 0;

//				context.ValidationContext.ContinueValidation = continueOnMismatch || match;

//				return match;
//			};

//			return builder.HasCustomRule<TEntity, TTarget, bool>(ruleOperator, ruleName);
//		}

//		/// <summary>
//		/// Creates the short date rule.
//		/// </summary>
//		/// <typeparam name="TEntity">The type of the entity.</typeparam>
//		/// <param name="builder">The builder.</param>
//		/// <param name="ruleName">Name of the rule.</param>
//		/// <param name="continueOnMismatch"><c>true</c> if continue on mismatch, otherwise <c>false</c>.</param>
//		/// <returns>The rule result builder.</returns>
//		/// <exception cref="System.ArgumentNullException">
//		/// The builder is null.</exception>
//		/// <exception cref="System.ArgumentException">The <paramref name="ruleName"/> is null or empty.</exception>
//		/// <exception cref="System.InvalidOperationException">
//		/// The <see cref="IInitialRuleBuilder{TEntity,TTarget}.Target"/> is null
//		/// or
//		/// The <see cref="IBuilder.Validator"/> is null in the
//		/// <see cref="IInitialRuleBuilder{TEntity,TTarget}"/>
//		/// or
//		/// The component factory couldn't create a <see cref="IRule"/> instance
//		/// or
//		/// The <see cref="IRule.RuleParameters"/> in a rule is null
//		/// or
//		/// The component factory couldn't create a
//		/// <see cref="IRuleResultBuilder{TEntity,TTarget,TOperatorResult}"/> instance.
//		/// </exception>
//		public static IRuleResultBuilder<TEntity, string, bool>
//			IsShortDate<TEntity>(this IInitialRuleBuilder<TEntity, string> builder,
//			string ruleName = ShortDateRuleName, bool continueOnMismatch = false)
//		{
//			Func<RuleOperatorContext, object> ruleOperator = (context) =>
//			{
//				var value = context.Target.Value(context.Entity);
//				DateTime dt;
//				bool match = value != null && DateTime.TryParseExact(value.ToString(),
//					Thread.CurrentThread.CurrentUICulture.DateTimeFormat.ShortDatePattern,
//					Thread.CurrentThread.CurrentUICulture, DateTimeStyles.None, out dt);
//				context.ValidationContext.ContinueValidation = continueOnMismatch || match;
//				return match;
//			};

//			return builder.HasCustomRule<TEntity, string, bool>(ruleOperator, ruleName);
//		}

//		#endregion
//	}
//}
