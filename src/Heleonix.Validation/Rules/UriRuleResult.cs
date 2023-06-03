// <copyright file="UriRuleResult.cs" company="Heleonix - Hennadii Lutsyshyn">
// Copyright (c) Heleonix - Hennadii Lutsyshyn. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the repository root for full license information.
// </copyright>

namespace Heleonix.Validation.Rules
{
    /// <summary>
    /// Represents the uri rule result.
    /// </summary>
    [Serializable]
    public class UriRuleResult : RuleResult
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UriRuleResult"/> class.
        /// </summary>
        /// <param name="name">A name of a rule.</param>
        /// <param name="value">A value of a rule.</param>
        /// <param name="kind">A uri kind.</param>
        /// <param name="schemes">Uri schemes.</param>
        public UriRuleResult(string name, object value, UriKind kind, IEnumerable<string> schemes)
            : base(name, value)
        {
            this.Kind = kind;

            if (schemes == null)
            {
                this.Schemes.Add(Uri.UriSchemeHttp);
                this.Schemes.Add(Uri.UriSchemeHttps);

                return;
            }

            foreach (var scheme in schemes.Where(scheme => scheme != null))
            {
                this.Schemes.Add(scheme);
            }
        }

        /// <summary>
        /// Gets or sets a uri kind.
        /// </summary>
        public UriKind Kind { get; set; }

        /// <summary>
        /// Gets uri schemes.
        /// </summary>
        public ICollection<string> Schemes { get; } = new List<string>();
    }
}