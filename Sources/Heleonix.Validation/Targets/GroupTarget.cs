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

namespace Heleonix.Validation.Targets
{
    /// <summary>
    /// Represents the group target.
    /// </summary>
    public class GroupTarget : Target
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="GroupTarget"/> class.
        /// </summary>
        /// <param name="name">A name of a group.</param>
        public GroupTarget(string name) : base(name)
        {
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets targets.
        /// </summary>
        public virtual ObservableCollection<Target> Targets { get; } = new ObservableCollection<Target>();

        #endregion

        #region Target Members

        /// <summary>
        /// Validates a target.
        /// </summary>
        /// <param name="context">A context of a target.</param>
        /// <exception cref="ArgumentNullException">
        /// The <paramref name="context"/> is <see langword="null"/>.
        /// </exception>
        /// <returns>A target result.</returns>
        public override TargetResult Validate(TargetContext context)
        {
            Throw<ArgumentNullException>.IfNull(context, nameof(context));

            context.Target = this;

            var result = CreateResult(context) as GroupTargetResult;

            if (result == null)
            {
                return null;
            }

            foreach (var target in Targets)
            {
                if (!context.ValidatorContext.ContinueValidation)
                {
                    return result;
                }

                var targetResult = target?.Validate(new TargetContext(null, context.ValidatorContext));

                if (targetResult == null)
                {
                    continue;
                }

                if (!context.ValidatorContext.IgnoreEmptyResults || !targetResult.IsEmpty())
                {
                    result.TargetResults.Add(targetResult);
                }
            }

            return result;
        }

        /// <summary>
        /// Gets a value to validate.
        /// </summary>
        /// <param name="context">A context of a target.</param>
        /// <exception cref="ArgumentNullException">
        /// The <paramref name="context"/> is <see langword="null"/>.
        /// </exception>
        /// <returns>A value to validate.</returns>
        public override object GetValue(TargetContext context)
        {
            Throw<ArgumentNullException>.IfNull(context, nameof(context));

            return Targets;
        }

        /// <summary>
        /// Creates a group target result.
        /// </summary>
        /// <param name="context">A context of a target.</param>
        /// <exception cref="ArgumentNullException">
        /// The <paramref name="context"/> is <see langword="null"/>.
        /// </exception>
        /// <returns>A group target result.</returns>
        protected override TargetResult CreateResult(TargetContext context)
        {
            Throw<ArgumentNullException>.IfNull(context, nameof(context));

            return new GroupTargetResult(Name);
        }

        #endregion
    }
}