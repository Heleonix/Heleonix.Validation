// <copyright file="RangeRuleResult.cs" company="Heleonix - Hennadii Lutsyshyn">
// Copyright (c) Heleonix - Hennadii Lutsyshyn. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the repository root for full license information.
// </copyright>

namespace Heleonix.Validation.Rules
{
    /// <summary>
    /// Represents the range rule result.
    /// </summary>
    [Serializable]
    public class RangeRuleResult : RuleResult
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RangeRuleResult"/> class.
        /// </summary>
        /// <param name="name">A name of a rule.</param>
        /// <param name="value">A value of a rule.</param>
        /// <param name="min">Minimum allowed value.</param>
        /// <param name="max">Maximum allowed value.</param>
        public RangeRuleResult(string name, object value, object min, object max)
            : base(name, value)
        {
            this.Min = min;
            this.Max = max;
        }

        /// <summary>
        /// Gets or sets minimum allowed value.
        /// </summary>
        public object Min { get; set; }

        /// <summary>
        /// Gets or sets maximum allowed value.
        /// </summary>
        public object Max { get; set; }
    }
}