﻿// <copyright file="InitialRuleBuilder.cs" company="Heleonix - Hennadii Lutsyshyn">
// Copyright (c) Heleonix - Hennadii Lutsyshyn. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the repository root for full license information.
// </copyright>

namespace Heleonix.Validation.Builders
{
    /// <summary>
    /// Implements the <see cref="IInitialRuleBuilder{TObject,TTarget}"/>.
    /// </summary>
    /// <typeparam name="TObject">A type of an object to validate.</typeparam>
    /// <typeparam name="TTarget">A type of a target.</typeparam>
    public class InitialRuleBuilder<TObject, TTarget> : TargetBuilder<TObject, TTarget>,
        IInitialRuleBuilder<TObject, TTarget>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InitialRuleBuilder{TObject, TTarget}"/> class.
        /// </summary>
        /// <param name="validator">A validator.</param>
        /// <param name="target">A target.</param>
        /// <exception cref="ArgumentNullException">
        /// The <paramref name="validator"/> is <see langword="null"/>.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// The <paramref name="target"/> is <see langword="null"/>.
        /// </exception>
        public InitialRuleBuilder(Validator<TObject> validator, Target target)
            : base(validator, target)
        {
        }
    }
}