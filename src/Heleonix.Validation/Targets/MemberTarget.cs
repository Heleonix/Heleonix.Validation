// <copyright file="MemberTarget.cs" company="Heleonix - Hennadii Lutsyshyn">
// Copyright (c) Heleonix - Hennadii Lutsyshyn. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the repository root for full license information.
// </copyright>

namespace Heleonix.Validation.Targets
{
    using Heleonix.Validation.Internal;

    /// <summary>
    /// Represents the target for a member.
    /// </summary>
    public class MemberTarget : Target
    {
        /// <summary>
        /// Gets or sets a member.
        /// </summary>
        private Func<object, object> member;

        /// <summary>
        /// Initializes a new instance of the <see cref="MemberTarget"/> class.
        /// </summary>
        /// <param name="name">A name of a target.</param>
        /// <param name="member">A delegate to get a member value.</param>
        /// <exception cref="ArgumentNullException">
        /// The <paramref name="member"/> is <see langword="null"/>.
        /// </exception>
        public MemberTarget(string name, Func<object, object> member)
            : base(name)
        {
            Throw<ArgumentNullException>.IfNull(member, nameof(member));

            this.member = member;
        }

        /// <summary>
        /// Gets or sets a delegate to get a member value.
        /// </summary>
        /// <exception cref="ArgumentNullException">The <see langword="value"/> is <see langword="null"/>.</exception>
        protected virtual Func<object, object> Member
        {
            get
            {
                return this.member;
            }

            set
            {
                Throw<ArgumentNullException>.IfNull(value, nameof(value));
                this.member = value;
            }
        }

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

            return this.Member.Invoke(context.ValidatorContext.Object);
        }
    }
}