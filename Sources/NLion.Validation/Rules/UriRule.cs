/*
The MIT License (MIT)

Copyright (c) 2015 NLion.Validation - Hennadii Lutsyshyn (NLion)

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
*/

using System;
using System.Collections.Generic;
using System.Linq;

namespace NLion.Validation.Rules
{
    /// <summary>
    /// Represents the uri rule.
    /// </summary>
    public class UriRule : BooleanRule
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="UriRule"/> class.
        /// </summary>
        /// <param name="continueValidationWhenFalse">Determines whether to continue validation
        /// when rule value is <see langword="false" />.
        /// </param>
        /// <param name="kind">A uri kind.</param>
        /// <param name="schemes">Acceptable uri schemes.</param>
        public UriRule(bool continueValidationWhenFalse, UriKind kind, IEnumerable<string> schemes)
            : base(continueValidationWhenFalse)
        {
            Kind = kind;

            if (schemes == null)
            {
                Schemes.Add(Uri.UriSchemeHttp);
                Schemes.Add(Uri.UriSchemeHttps);

                return;
            }

            foreach (var scheme in schemes.Where(scheme => scheme != null))
            {
                Schemes.Add(scheme);
            }
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets a uri kind.
        /// </summary>
        public UriKind Kind { get; set; }

        /// <summary>
        /// Gets uri schemes.
        /// </summary>
        public ICollection<string> Schemes { get; } = new List<string>();

        #endregion

        #region Rule Members

        /// <summary>
        /// Creates a uri rule result.
        /// </summary>
        /// <param name="context">A rule validation context.</param>
        /// <param name="value">A rule value.</param>
        /// <returns>A rule result.</returns>
        public override RuleResult CreateResult(RuleValidationContext context, object value)
            => new UriRuleResult(Name, value, Kind, Schemes);

        /// <summary>
        /// Executes validation.
        /// </summary>
        /// <param name="context">A rule validation context.</param>
        /// <returns>A rule value.</returns>
        protected override object Execute(RuleValidationContext context)
        {
            Uri uri;

            var value = context.Target.GetValue(context.ValidationContext)?.ToString();

            return value == null || Uri.TryCreate(value, Kind, out uri)
                   && (Schemes?.Any(s => uri.Scheme.Equals(s, StringComparison.OrdinalIgnoreCase)) ?? false);
        }

        #endregion
    }
}