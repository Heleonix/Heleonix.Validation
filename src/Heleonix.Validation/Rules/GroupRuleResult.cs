// <copyright file="GroupRuleResult.cs" company="Heleonix - Hennadii Lutsyshyn">
// Copyright (c) Heleonix - Hennadii Lutsyshyn. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the repository root for full license information.
// </copyright>

namespace Heleonix.Validation.Rules
{
    /// <summary>
    /// Represents the group rule result.
    /// </summary>
    public class GroupRuleResult : RuleResult
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GroupRuleResult"/> class.
        /// </summary>
        /// <param name="name">A name of a rule.</param>
        public GroupRuleResult(string name)
            : base(name, null)
        {
            this.Name = name;
        }

        /// <summary>
        /// Gets rule results.
        /// </summary>
        public ICollection<RuleResult> RuleResults { get; } = new List<RuleResult>();

        /// <summary>
        /// Indicates whether the result is empty.
        /// </summary>
        /// <returns><see langword="true"/> if the result is empty, otherwise <see langword="false"/>.</returns>
        public override bool IsEmpty() => this.RuleResults.Count == 0;
    }
}