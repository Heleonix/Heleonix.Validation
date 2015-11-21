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
using NLion.Validation.Builders;

namespace NLion.Validation
{
    /// <summary>
    /// Provides extensions for the <see cref="IInitialValueResultBuilder{TObject,TTarget,TValue}"/>.
    /// </summary>
    public static class InitialValueResultBuilderExtensions
    {
        #region Methods

        /// <summary>
        /// Builds an error value result.
        /// </summary>
        /// <param name="builder">The <see cref="IInitialValueResultBuilder{TObject,TTarget,TValue}"/>.</param>
        /// <param name="resourceName">A resource name.</param>
        /// <param name="resourceKey">A resource key.</param>
        /// <typeparam name="TObject">A type of an object to validate.</typeparam>
        /// <typeparam name="TTarget">A type of a target to build value results for.</typeparam>
        /// <exception cref="ArgumentNullException">
        /// The <paramref name="builder"/> is <see langword="null"/>.
        /// </exception>
        /// <returns>The <see cref="IFinalValueResultBuilder{TObject,TTarget,TValue}"/>.</returns>
        public static IFinalValueResultBuilder<TObject, TTarget, bool> WithError<TObject, TTarget>(
            this IInitialValueResultBuilder<TObject, TTarget, bool> builder, string resourceName = null,
            string resourceKey = null) => WithResult(builder, false, resourceName, resourceKey);

        /// <summary>
        /// Builds a success value result.
        /// </summary>
        /// <param name="builder">The <see cref="IInitialValueResultBuilder{TObject,TTarget,TValue}"/>.</param>
        /// <param name="resourceName">A resource name.</param>
        /// <param name="resourceKey">A resource key.</param>
        /// <typeparam name="TObject">A type of an object to validate.</typeparam>
        /// <typeparam name="TTarget">A type of a target to build value results for.</typeparam>
        /// <exception cref="ArgumentNullException">
        /// The <paramref name="builder"/> is <see langword="null"/>.
        /// </exception>
        /// <returns>The <see cref="IFinalValueResultBuilder{TObject,TTarget,TValue}"/>.</returns>
        public static IFinalValueResultBuilder<TObject, TTarget, bool> WithSuccess<TObject, TTarget>(
            this IInitialValueResultBuilder<TObject, TTarget, bool> builder, string resourceName = null,
            string resourceKey = null) => WithResult(builder, true, resourceName, resourceKey);

        /// <summary>
        /// Builds a value result.
        /// </summary>
        /// <param name="builder">The <see cref="IInitialValueResultBuilder{TObject,TTarget,TValue}"/>.</param>
        /// <param name="matchValue">A value to select the result if it equals to a rule validation result.</param>
        /// <param name="resourceName">A resource name.</param>
        /// <param name="resourceKey">A resource key.</param>
        /// <typeparam name="TObject">A type of an object to validate.</typeparam>
        /// <typeparam name="TTarget">A type of a target to build value results for.</typeparam>
        /// <typeparam name="TValue">A type of a value to build value results for.</typeparam>
        /// <exception cref="ArgumentNullException">
        /// The <paramref name="builder"/> is <see langword="null"/>.
        /// </exception>
        /// <returns>The <see cref="IFinalValueResultBuilder{TObject,TTarget,TValue}"/>.</returns>
        public static IFinalValueResultBuilder<TObject, TTarget, TValue> WithResult<TObject, TTarget, TValue>(
            this IInitialValueResultBuilder<TObject, TTarget, TValue> builder, TValue matchValue,
            string resourceName = null, string resourceKey = null)
            => WithResult(builder, new ValueResult(matchValue, resourceName, resourceKey));

        /// <summary>
        /// Builds a value result.
        /// </summary>
        /// <param name="builder">The <see cref="IInitialValueResultBuilder{TObject,TTarget,TValue}"/>.</param>
        /// <param name="result">A value result.</param>
        /// <typeparam name="TObject">A type of an object to validate.</typeparam>
        /// <typeparam name="TTarget">A type of a target to build value results for.</typeparam>
        /// <typeparam name="TValue">A type of a value to build value results for.</typeparam>
        /// <exception cref="ArgumentNullException">
        /// The <paramref name="builder"/> is <see langword="null"/>.
        /// </exception>
        /// <returns>The <see cref="IFinalValueResultBuilder{TObject,TTarget,TValue}"/>.</returns>
        public static IFinalValueResultBuilder<TObject, TTarget, TValue> WithResult<TObject, TTarget, TValue>(
            this IInitialValueResultBuilder<TObject, TTarget, TValue> builder, ValueResult result)
        {
            Throw.ArgumentNullException(builder == null, nameof(builder));

            var container = new ValueResultContainer(result);

            builder.RuleContainer.Rule?.ValueResultContainers.Add(container);

            return new FinalValueResultBuilder<TObject, TTarget, TValue>(builder.Validator,
                builder.TargetContainer, builder.RuleContainer, container);
        }

        #endregion
    }
}