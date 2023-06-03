// <copyright file="InitialValueResultBuilderExtensions.cs" company="Heleonix - Hennadii Lutsyshyn">
// Copyright (c) Heleonix - Hennadii Lutsyshyn. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the repository root for full license information.
// </copyright>

namespace Heleonix.Validation
{
    using Heleonix.Validation.Builders;
    using Heleonix.Validation.Internal;

    /// <summary>
    /// Provides extensions for the <see cref="IInitialValueResultBuilder{TObject,TTarget,TValue}"/>.
    /// </summary>
    public static class InitialValueResultBuilderExtensions
    {
        /// <summary>
        /// Creates the  <see cref="ValueResult"/>.
        /// </summary>
        /// <param name="builder">The <see cref="IInitialValueResultBuilder{TObject,TTarget,TValue}"/>.</param>
        /// <param name="resourceName">A resource name.</param>
        /// <param name="resourceKey">A resource key.</param>
        /// <typeparam name="TObject">A type of an object to validate.</typeparam>
        /// <typeparam name="TTarget">A type of a target.</typeparam>
        /// <exception cref="ArgumentNullException">
        /// The <paramref name="builder"/> is <see langword="null"/>.
        /// </exception>
        /// <returns>The <see cref="IFinalValueResultBuilder{TObject,TTarget,TValue}"/>.</returns>
        public static IFinalValueResultBuilder<TObject, TTarget, bool> WithError<TObject, TTarget>(
            this IInitialValueResultBuilder<TObject, TTarget, bool> builder,
            string resourceName = null,
            string resourceKey = null) => WithResult(builder, false, resourceName, resourceKey);

        /// <summary>
        /// Creates the  <see cref="ValueResult"/>.
        /// </summary>
        /// <param name="builder">The <see cref="IInitialValueResultBuilder{TObject,TTarget,TValue}"/>.</param>
        /// <param name="resourceName">A resource name.</param>
        /// <param name="resourceKey">A resource key.</param>
        /// <typeparam name="TObject">A type of an object to validate.</typeparam>
        /// <typeparam name="TTarget">A type of a target.</typeparam>
        /// <exception cref="ArgumentNullException">
        /// The <paramref name="builder"/> is <see langword="null"/>.
        /// </exception>
        /// <returns>The <see cref="IFinalValueResultBuilder{TObject,TTarget,TValue}"/>.</returns>
        public static IFinalValueResultBuilder<TObject, TTarget, bool> WithSuccess<TObject, TTarget>(
            this IInitialValueResultBuilder<TObject, TTarget, bool> builder,
            string resourceName = null,
            string resourceKey = null) => WithResult(builder, true, resourceName, resourceKey);

        /// <summary>
        /// Creates the  <see cref="ValueResult"/>.
        /// </summary>
        /// <param name="builder">The <see cref="IInitialValueResultBuilder{TObject,TTarget,TValue}"/>.</param>
        /// <param name="matchValue">A value to select the result if it equals to a rule validation result.</param>
        /// <param name="resourceName">A resource name.</param>
        /// <param name="resourceKey">A resource key.</param>
        /// <typeparam name="TObject">A type of an object to validate.</typeparam>
        /// <typeparam name="TTarget">A type of a target.</typeparam>
        /// <typeparam name="TValue">A type of a value returned by a rule.</typeparam>
        /// <exception cref="ArgumentNullException">
        /// The <paramref name="builder"/> is <see langword="null"/>.
        /// </exception>
        /// <returns>The <see cref="IFinalValueResultBuilder{TObject,TTarget,TValue}"/>.</returns>
        public static IFinalValueResultBuilder<TObject, TTarget, TValue> WithResult<TObject, TTarget, TValue>(
            this IInitialValueResultBuilder<TObject, TTarget, TValue> builder,
            TValue matchValue,
            string resourceName = null,
            string resourceKey = null)
            => WithResult(builder, new ValueResult(matchValue, resourceName, resourceKey));

        /// <summary>
        /// Adds a value result.
        /// </summary>
        /// <param name="builder">The <see cref="IInitialValueResultBuilder{TObject,TTarget,TValue}"/>.</param>
        /// <param name="result">A value result.</param>
        /// <typeparam name="TObject">A type of an object to validate.</typeparam>
        /// <typeparam name="TTarget">A type of a target.</typeparam>
        /// <typeparam name="TValue">A type of a value returned by a rule.</typeparam>
        /// <exception cref="ArgumentNullException">
        /// The <paramref name="builder"/> is <see langword="null"/>.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// The <paramref name="result"/> is <see langword="null"/>.
        /// </exception>
        /// <returns>The <see cref="IFinalValueResultBuilder{TObject,TTarget,TValue}"/>.</returns>
        public static IFinalValueResultBuilder<TObject, TTarget, TValue> WithResult<TObject, TTarget, TValue>(
            this IInitialValueResultBuilder<TObject, TTarget, TValue> builder, ValueResult result)
        {
            Throw<ArgumentNullException>.IfNull(builder, nameof(builder));
            Throw<ArgumentNullException>.IfNull(result, nameof(result));

            builder.Rule.ValueResults.Add(result);

            return new FinalValueResultBuilder<TObject, TTarget, TValue>(
                builder.Validator, builder.Target, builder.Rule, result);
        }
    }
}