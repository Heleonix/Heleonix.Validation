// <copyright file="EachOfTarget.cs" company="Heleonix - Hennadii Lutsyshyn">
// Copyright (c) Heleonix - Hennadii Lutsyshyn. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the repository root for full license information.
// </copyright>

namespace Heleonix.Validation.Targets
{
    using System.Collections;

    /// <summary>
    /// Represents the target for each of enumerable items.
    /// </summary>
    public class EachOfTarget : ItemTarget
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EachOfTarget"/> class.
        /// </summary>
        /// <param name="name">A name of a target.</param>
        /// <param name="member">A delegate of an enumerable member to validate items from.</param>
        /// <param name="itemsSelector">A delegate to select items.</param>
        /// <exception cref="ArgumentNullException">
        /// The <paramref name="member"/> is <see langword="null"/>.
        /// </exception>
        public EachOfTarget(
            string name,
            Func<object, IEnumerable> member,
            Func<IEnumerable, TargetContext, IEnumerable> itemsSelector)
            : base(name, member, itemsSelector)
        {
        }
    }
}