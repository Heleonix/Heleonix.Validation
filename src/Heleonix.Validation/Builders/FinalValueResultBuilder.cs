// <copyright file="FinalValueResultBuilder.cs" company="Heleonix - Hennadii Lutsyshyn">
// Copyright (c) Heleonix - Hennadii Lutsyshyn. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the repository root for full license information.
// </copyright>

namespace Heleonix.Validation.Builders
{
    /// <summary>
    /// Implements the <see cref="IFinalValueResultBuilder{TObject,TTarget,TValue}"/>.
    /// </summary>
    /// <typeparam name="TObject">A type of an object to validate.</typeparam>
    /// <typeparam name="TTarget">A type of a target.</typeparam>
    /// <typeparam name="TValue">A type of a value returned by a rule.</typeparam>
    public class FinalValueResultBuilder<TObject, TTarget, TValue> : ValueResultBuilder<TObject, TTarget, TValue>,
        IFinalValueResultBuilder<TObject, TTarget, TValue>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FinalValueResultBuilder{TObject, TTarget, TValue}"/> class.
        /// </summary>
        /// <param name="validator">A validator.</param>
        /// <param name="target">A target.</param>
        /// <param name="rule">A rule.</param>
        /// <param name="valueResult">A value result.</param>
        /// <exception cref="ArgumentNullException">
        /// The <paramref name="validator"/> is <see langword="null"/>.
        /// </exception>
        /// <exception cref="ArgumentNullException">The <paramref name="target"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentNullException">The <paramref name="rule"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentNullException">
        /// The <paramref name="valueResult"/> is <see langword="null"/>.
        /// </exception>
        public FinalValueResultBuilder(
            Validator<TObject> validator,
            Target target,
            Rule rule,
            ValueResult valueResult)
            : base(validator, target, rule, valueResult)
        {
        }
    }
}