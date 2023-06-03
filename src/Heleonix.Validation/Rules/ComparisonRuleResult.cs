// <copyright file="ComparisonRuleResult.cs" company="Heleonix - Hennadii Lutsyshyn">
// Copyright (c) Heleonix - Hennadii Lutsyshyn. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the repository root for full license information.
// </copyright>

namespace Heleonix.Validation.Rules
{
    /// <summary>
    /// Represents the comparison rules result.
    /// </summary>
    [Serializable]
    public class ComparisonRuleResult : RuleResult
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ComparisonRuleResult"/> class.
        /// </summary>
        /// <param name="name">A name of a rule.</param>
        /// <param name="value">A value of a rule.</param>
        /// <param name="otherValue">Other value.</param>
        public ComparisonRuleResult(string name, object value, object otherValue)
            : base(name, value)
        {
            this.OtherValue = otherValue;
        }

        /// <summary>
        /// Gets or sets other value.
        /// </summary>
        public object OtherValue { get; set; }
    }
}