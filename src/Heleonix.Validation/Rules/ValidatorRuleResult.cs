// <copyright file="ValidatorRuleResult.cs" company="Heleonix - Hennadii Lutsyshyn">
// Copyright (c) Heleonix - Hennadii Lutsyshyn. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the repository root for full license information.
// </copyright>

namespace Heleonix.Validation.Rules
{
    /// <summary>
    /// Represents the validator rule result.
    /// </summary>
    [Serializable]
    public class ValidatorRuleResult : RuleResult
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ValidatorRuleResult"/> class.
        /// </summary>
        /// <param name="name">A name of a rule.</param>
        /// <param name="validatorResult">A validator result.</param>
        public ValidatorRuleResult(string name, ValidatorResult validatorResult)
            : base(name, null)
        {
            this.ValidatorResult = validatorResult;
        }

        /// <summary>
        /// Gets or sets a validator rule result.
        /// </summary>
        public ValidatorResult ValidatorResult { get; set; }

        /// <summary>
        /// Indicates whether the result is empty.
        /// </summary>
        /// <returns><see langword="true"/> if the result is empty, otherwise <see langword="false"/>.</returns>
        public override bool IsEmpty() => this.ValidatorResult == null;
    }
}