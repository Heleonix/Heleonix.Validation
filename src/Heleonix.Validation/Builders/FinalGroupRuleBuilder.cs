// <copyright file="FinalGroupRuleBuilder.cs" company="Heleonix - Hennadii Lutsyshyn">
// Copyright (c) Heleonix - Hennadii Lutsyshyn. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the repository root for full license information.
// </copyright>

namespace Heleonix.Validation.Builders
{
    /// <summary>
    /// Implements the <see cref="IFinalGroupRuleBuilder{TObject,TTarget}"/>.
    /// </summary>
    /// <typeparam name="TObject">A type of an object to validate.</typeparam>
    /// <typeparam name="TTarget">A type of a target.</typeparam>
    public class FinalGroupRuleBuilder<TObject, TTarget> : FinalRuleBuilder<TObject, TTarget, object>,
        IFinalGroupRuleBuilder<TObject, TTarget>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FinalGroupRuleBuilder{TObject, TTarget}"/> class.
        /// </summary>
        /// <param name="validator">A validator.</param>
        /// <param name="target">A target.</param>
        /// <param name="rule">A rule.</param>
        /// <exception cref="ArgumentNullException">
        /// The <paramref name="validator"/> is <see langword="null"/>.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// The <paramref name="target"/> is <see langword="null"/>.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// The <paramref name="rule"/> is <see langword="null"/>.
        /// </exception>
        public FinalGroupRuleBuilder(Validator<TObject> validator, Target target, Rule rule)
            : base(validator, target, rule)
        {
        }
    }
}