// <copyright file="RuleResult.cs" company="Heleonix - Hennadii Lutsyshyn">
// Copyright (c) Heleonix - Hennadii Lutsyshyn. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the repository root for full license information.
// </copyright>

namespace Heleonix.Validation
{
    /// <summary>
    /// Represents the rule result.
    /// </summary>
    [Serializable]
    public class RuleResult : Result
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RuleResult"/> class.
        /// </summary>
        /// <param name="name">A name of a rule.</param>
        /// <param name="value">A value of a rule.</param>
        public RuleResult(string name, object value)
        {
            this.Name = name;
            this.Value = value;
        }

        /// <summary>
        /// Gets or sets a name of a rule.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets a value of a rule.
        /// </summary>
        public object Value { get; set; }

        /// <summary>
        /// Gets value results.
        /// </summary>
        public ICollection<ValueResult> ValueResults { get; } = new List<ValueResult>();

        /// <summary>
        /// Indicates whether the result is empty.
        /// </summary>
        /// <returns><see langword="true"/> if the result is empty, otherwise <see langword="false"/>.</returns>
        public override bool IsEmpty() => this.ValueResults.Count == 0;
    }
}