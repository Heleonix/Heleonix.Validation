// <copyright file="UriRule.cs" company="Heleonix - Hennadii Lutsyshyn">
// Copyright (c) Heleonix - Hennadii Lutsyshyn. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the repository root for full license information.
// </copyright>

namespace Heleonix.Validation.Rules
{
    using Heleonix.Validation.Internal;

    /// <summary>
    /// Represents the uri rule.
    /// </summary>
    public class UriRule : BooleanRule
    {
        /// <summary>
        /// Gets uri schemes.
        /// </summary>
        private readonly ICollection<string> schemes = new List<string>();

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
            this.Kind = kind;

            if (schemes == null)
            {
                this.schemes.Add(Uri.UriSchemeHttp);
                this.schemes.Add(Uri.UriSchemeHttps);

                return;
            }

            foreach (var scheme in schemes.Where(scheme => scheme != null))
            {
                this.schemes.Add(scheme);
            }
        }

        /// <summary>
        /// Gets or sets a uri kind.
        /// </summary>
        public virtual UriKind Kind { get; set; }

        /// <summary>
        /// Gets uri schemes.
        /// </summary>
        public virtual ICollection<string> Schemes => this.schemes;

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

            return new UriRuleResult(this.Name, value, this.Kind, this.Schemes);
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

            return value == null || (Uri.TryCreate(value, this.Kind, out uri)
                   && this.Schemes.Any(s => uri.Scheme.Equals(s, StringComparison.OrdinalIgnoreCase)));
        }
    }
}