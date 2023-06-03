// <copyright file="LengthRuleResult.cs" company="Heleonix - Hennadii Lutsyshyn">
// Copyright (c) Heleonix - Hennadii Lutsyshyn. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the repository root for full license information.
// </copyright>

namespace Heleonix.Validation.Rules
{
    /// <summary>
    /// Represents the length rule result.
    /// </summary>
    [Serializable]
    public class LengthRuleResult : RuleResult
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LengthRuleResult"/> class.
        /// </summary>
        /// <param name="name">A name of a rule.</param>
        /// <param name="value">A value of a rule.</param>
        /// <param name="min">Minimum allowed length.</param>
        /// <param name="max">Maximum allowed length.</param>
        public LengthRuleResult(string name, object value, int? min, int? max)
            : base(name, value)
        {
            this.Min = min;
            this.Max = max;
        }

        /// <summary>
        /// Gets or sets minimum allowed length.
        /// </summary>
        public int? Min { get; set; }

        /// <summary>
        /// Gets or sets maximum allowed length.
        /// </summary>
        public int? Max { get; set; }
    }
}