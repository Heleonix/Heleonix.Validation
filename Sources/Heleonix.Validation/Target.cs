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
using System.Collections.ObjectModel;
using Heleonix.Validation.Internal;

namespace Heleonix.Validation
{
    /// <summary>
    /// Represents the base class for all targets.
    /// </summary>
    public abstract class Target
    {
        #region Fields

        /// <summary>
        /// Gets or sets a name of a target.
        /// </summary>
        private string _name;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Target"/> class.
        /// </summary>
        /// <param name="name">A name of a target.</param>
        protected Target(string name)
        {
            _name = name;
        }

        #endregion

        #region Methods

        /// <summary>
        /// When overridden in a derived class, gets a value to validate.
        /// </summary>
        /// <param name="context">A context of a target.</param>
        /// <returns>A value to validate.</returns>
        public abstract object GetValue(TargetContext context);

        /// <summary>
        /// Validates a target.
        /// </summary>
        /// <param name="context">A context of a target.</param>
        /// <exception cref="ArgumentNullException">
        /// The <paramref name="context"/> is <see langword="null"/>.
        /// </exception>
        /// <returns>A target result.</returns>
        public virtual TargetResult Validate(TargetContext context)
        {
            Throw<ArgumentNullException>.IfNull(context, nameof(context));

            context.Target = this;

            var result = CreateResult(context);

            if (result == null)
            {
                return null;
            }

            foreach (var rule in Rules)
            {
                if (!context.ValidatorContext.ContinueValidation)
                {
                    return result;
                }

                var ruleResult = rule?.Validate(new RuleContext(null, context));

                if (ruleResult == null)
                {
                    continue;
                }

                if (!context.ValidatorContext.IgnoreEmptyResults || !ruleResult.IsEmpty())
                {
                    result.RuleResults.Add(ruleResult);
                }
            }

            return result;
        }

        /// <summary>
        /// Creates a target result.
        /// </summary>
        /// <param name="context">A context of a target.</param>
        /// <exception cref="ArgumentNullException">
        /// The <paramref name="context"/> is <see langword="null"/>.
        /// </exception>
        /// <returns>A target result.</returns>
        protected virtual TargetResult CreateResult(TargetContext context)
        {
            Throw<ArgumentNullException>.IfNull(context, nameof(context));

            return new TargetResult(Name, GetValue(context));
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets a name of a target.
        /// </summary>
        public virtual string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        /// <summary>
        /// Gets rules.
        /// </summary>
        public virtual ObservableCollection<Rule> Rules { get; } = new ObservableCollection<Rule>();

        #endregion
    }
}