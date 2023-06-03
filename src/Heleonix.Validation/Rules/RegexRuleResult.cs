// <copyright file="RegexRuleResult.cs" company="Heleonix - Hennadii Lutsyshyn">
// Copyright (c) Heleonix - Hennadii Lutsyshyn. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the repository root for full license information.
// </copyright>

namespace Heleonix.Validation.Rules
{
    using System.Text.RegularExpressions;

    /// <summary>
    /// Represents the regular expression rule result.
    /// </summary>
    [Serializable]
    public class RegexRuleResult : RuleResult
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RegexRuleResult"/> class.
        /// </summary>
        /// <param name="name">A name of a rule.</param>
        /// <param name="value">A value of a rule.</param>
        /// <param name="regex">A regular expression to test match.</param>
        /// <param name="regexOptions">Regular expression options.</param>
        public RegexRuleResult(string name, object value, string regex, RegexOptions regexOptions)
            : base(name, value)
        {
            this.Regex = regex;
            this.RegexOptions = regexOptions;
        }

        /// <summary>
        /// Gets or sets a regular expression to test match.
        /// </summary>
        public string Regex { get; set; }

        /// <summary>
        /// Gets or sets regular expression options.
        /// </summary>
        public RegexOptions RegexOptions { get; set; }
    }
}