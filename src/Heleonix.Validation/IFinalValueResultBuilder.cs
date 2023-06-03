// <copyright file="IFinalValueResultBuilder.cs" company="Heleonix - Hennadii Lutsyshyn">
// Copyright (c) Heleonix - Hennadii Lutsyshyn. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the repository root for full license information.
// </copyright>

namespace Heleonix.Validation
{
    /// <summary>
    /// Represents the <see langword="interface"/> to finalize building value results.
    /// </summary>
    /// <typeparam name="TObject">A type of an object to validate.</typeparam>
    /// <typeparam name="TTarget">A type of a target.</typeparam>
    /// <typeparam name="TValue">A type of a value returned by a rule.</typeparam>
    public interface IFinalValueResultBuilder<TObject, TTarget, TValue> : IInitialRuleBuilder<TObject, TTarget>,
        IInitialValueResultBuilder<TObject, TTarget, TValue>
    {
        /// <summary>
        /// Gets a target.
        /// </summary>
        new Target Target { get; }

        /// <summary>
        /// Gets a rule.
        /// </summary>
        new Rule Rule { get; }

        /// <summary>
        /// Gets or sets a value result.
        /// </summary>
        ValueResult ValueResult { get; set; }
    }
}