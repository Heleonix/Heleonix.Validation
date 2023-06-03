// <copyright file="CustomRuleResult.cs" company="Heleonix - Hennadii Lutsyshyn">
// Copyright (c) Heleonix - Hennadii Lutsyshyn. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the repository root for full license information.
// </copyright>

namespace Heleonix.Validation.Rules
{
    /// <summary>
    /// Represents the custom rule result.
    /// </summary>
    [Serializable]
    public class CustomRuleResult : RuleResult
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CustomRuleResult"/> class.
        /// </summary>
        /// <param name="name">A name of a rule.</param>
        /// <param name="value">A value of a rule.</param>
        public CustomRuleResult(string name, object value)
            : base(name, value)
        {
        }

        /// <summary>
        /// Gets custom data.
        /// </summary>
        public IDictionary<string, object> Data { get; } = new Dictionary<string, object>();
    }
}