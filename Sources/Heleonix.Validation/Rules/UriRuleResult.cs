/*
The MIT License (MIT)

Copyright (c) 2015 Heleonix.Validation - Hennadii Lutsyshyn (Heleonix)

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

namespace Heleonix.Validation.Rules
{
    /// <summary>
    /// Represents the uri rule result.
    /// </summary>
    [Serializable]
    public class UriRuleResult : RuleResult
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="UriRuleResult"/> class.
        /// </summary>
        /// <param name="name">A name of a rule.</param>
        /// <param name="value">A value of a rule.</param>
        /// <param name="kind">A uri kind.</param>
        /// <param name="schemes">Uri schemes.</param>
        public UriRuleResult(string name, object value, UriKind kind, IEnumerable<string> schemes) : base(name, value)
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
    }
}