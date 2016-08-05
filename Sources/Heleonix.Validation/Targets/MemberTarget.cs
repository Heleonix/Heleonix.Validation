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
using Heleonix.Validation.Internal;

namespace Heleonix.Validation.Targets
{
    /// <summary>
    /// Represents the target for a member.
    /// </summary>
    public class MemberTarget : Target
    {
        #region Fields

        /// <summary>
        /// Gets or sets a member.
        /// </summary>
        private Func<object, object> _member;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="MemberTarget"/> class.
        /// </summary>
        /// <param name="name">A name of a target.</param>
        /// <param name="member">A delegate to get a member value.</param>
        /// <exception cref="ArgumentNullException">
        /// The <paramref name="member"/> is <see langword="null"/>.
        /// </exception>
        public MemberTarget(string name, Func<object, object> member) : base(name)
        {
            Throw<ArgumentNullException>.IfNull(member, nameof(member));

            _member = member;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets a delegate to get a member value.
        /// </summary>
        /// <exception cref="ArgumentNullException">The <see langword="value"/> is <see langword="null"/>.</exception>
        protected virtual Func<object, object> Member
        {
            get { return _member; }
            set
            {
                Throw<ArgumentNullException>.IfNull(value, nameof(value));

                _member = value;
            }
        }

        #endregion

        #region Target Members

        /// <summary>
        /// Gets a member value.
        /// </summary>
        /// <param name="context">A context of a target.</param>
        /// <exception cref="ArgumentNullException">
        /// The <paramref name="context"/> is <see langword="null"/>.
        /// </exception>
        /// <returns>A member value.</returns>
        public override object GetValue(TargetContext context)
        {
            Throw<ArgumentNullException>.IfNull(context, nameof(context));

            return Member.Invoke(context.ValidatorContext.Object);
        }

        #endregion
    }
}