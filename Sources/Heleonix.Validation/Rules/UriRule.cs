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
using Heleonix.Validation.Internal;

namespace Heleonix.Validation.Rules
{
    /// <summary>
    /// Represents the uri rule.
    /// </summary>
    public class UriRule : BooleanRule
    {
        #region Fields

        /// <summary>
        /// Gets uri schemes.
        /// </summary>
        private readonly ICollection<string> _schemes = new List<string>();

        /// <summary>
        /// Gets or sets a uri kind.
        /// </summary>
        private UriKind _kind;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="UriRule"/> class.
        /// </summary>
        /// <param name="continueValidationWhenFalse">
        /// Determines whether to continue validation when a value of a rule is <see langword="false" />.
        /// </param>
        /// <param name="kind">A uri kind.</param>
        /// <param name="schemes">Acceptable uri schemes.</param>
        public UriRule(bool continueValidationWhenFalse, UriKind kind, IEnumerable<string> schemes)
            : base(continueValidationWhenFalse)
        {
            _kind = kind;

            if (schemes == null)
            {
                _schemes.Add(Uri.UriSchemeHttp);
                _schemes.Add(Uri.UriSchemeHttps);

                return;
            }

            foreach (var scheme in schemes.Where(scheme => scheme != null))
            {
                _schemes.Add(scheme);
            }
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets a uri kind.
        /// </summary>
        public virtual UriKind Kind
        {
            get { return _kind; }
            set { _kind = value; }
        }

        /// <summary>
        /// Gets uri schemes.
        /// </summary>
        public virtual ICollection<string> Schemes => _schemes;

        #endregion

        #region Rule Members

        /// <summary>
        /// Creates a uri rule result.
        /// </summary>
        /// <param name="context">A context of a rule.</param>
        /// <param name="value">A value of a rule.</param>
        /// <exception cref="ArgumentNullException">
        /// The <paramref name="context"/> is <see langword="null"/>.
        /// </exception>
        /// <returns>A rule result.</returns>
        protected override RuleResult CreateResult(RuleContext context, object value)
        {
            Throw<ArgumentNullException>.IfNull(context, nameof(context));

            return new UriRuleResult(Name, value, Kind, Schemes);
        }

        /// <summary>
        /// Executes validation.
        /// </summary>
        /// <param name="context">A context of a rule.</param>
        /// <exception cref="ArgumentNullException">
        /// The <paramref name="context"/> is <see langword="null"/>.
        /// </exception>
        /// <returns>A value of a rule.</returns>
        protected override object Execute(RuleContext context)
        {
            Throw<ArgumentNullException>.IfNull(context, nameof(context));

            Uri uri;

            var value = context.TargetContext.Target.GetValue(context.TargetContext)?.ToString();

            return value == null || Uri.TryCreate(value, Kind, out uri)
                   && Schemes.Any(s => uri.Scheme.Equals(s, StringComparison.OrdinalIgnoreCase));
        }

        #endregion
    }
}