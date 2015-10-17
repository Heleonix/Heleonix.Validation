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
using System.Collections.ObjectModel;
using System.Diagnostics.Contracts;

namespace NLion.Validation
{
    /// <summary>
    /// Represents a base class for all targets.
    /// </summary>
    public abstract class Target
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Target"/> class.
        /// </summary>
        /// <param name="name">A name of a target.</param>
        protected Target(string name)
        {
            Name = name;
        }

        #endregion

        #region Methods

        /// <summary>
        /// When overridden in a derived class, gets a value to validate.
        /// </summary>
        /// <param name="context">A validation context.</param>
        /// <returns>A value to validate.</returns>
        public abstract object GetValue(ValidationContext context);

        /// <summary>
        /// Validates a target.
        /// </summary>
        /// <param name="context">A validation context.</param>
        /// <exception cref="ArgumentNullException">
        /// The <paramref name="context"/> is <see langword="null"/>.
        /// </exception>
        /// <exception cref="RuleValidationException">Validation was failed in one or more rules.</exception>
        /// <returns>A target result.</returns>
        public virtual TargetResult Validate(ValidationContext context)
        {
            Contract.Requires<ArgumentNullException>(context != null);

            var result = CreateResult(context);

            if (result == null)
            {
                return null;
            }

            foreach (var container in RuleContainers)
            {
                try
                {
                    var ruleResult = container?.Rule?.Validate(new RuleValidationContext(context, this));

                    if (ruleResult != null && (!context.IgnoreEmptyResults || ruleResult.ValueResults.Count > 0))
                    {
                        result.RuleResults.Add(ruleResult);
                    }
                }
                catch (RuleValidationException) when (context.ContinueOnFailedValidation)
                {
                }
            }

            return result;
        }

        /// <summary>
        /// Creates a target result.
        /// </summary>
        /// <param name="context">A validation context.</param>
        /// <exception cref="ArgumentNullException">
        /// The <paramref name="context"/> is <see langword="null"/>.
        /// </exception>
        /// <returns>A target result.</returns>
        public virtual TargetResult CreateResult(ValidationContext context)
        {
            Contract.Requires<ArgumentNullException>(context != null);

            return new TargetResult(Name, GetValue(context));
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets a target name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets rules containers.
        /// </summary>
        public ObservableCollection<RuleContainer> RuleContainers { get; } = new ObservableCollection<RuleContainer>();

        #endregion
    }
}