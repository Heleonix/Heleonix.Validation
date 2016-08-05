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
using System.Text.RegularExpressions;

namespace Heleonix.Validation.Rules
{
    /// <summary>
    /// Represents the regular expression rule result.
    /// </summary>
    [Serializable]
    public class RegexRuleResult : RuleResult
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="RegexRuleResult"/> class.
        /// </summary>
        /// <param name="name">A name of a rule.</param>
        /// <param name="value">A value of a rule.</param>
        /// <param name="regex">A regular expression to test match.</param>
        /// <param name="regexOptions">Regular expression options.</param>
        public RegexRuleResult(string name, object value, string regex, RegexOptions regexOptions) : base(name, value)
        {
            Regex = regex;
            RegexOptions = regexOptions;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets a regular expression to test match.
        /// </summary>
        public string Regex { get; set; }

        /// <summary>
        /// Gets or sets regular expression options.
        /// </summary>
        public RegexOptions RegexOptions { get; set; }

        #endregion
    }
}