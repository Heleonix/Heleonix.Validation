// <copyright file="ReflectionHelper.cs" company="Heleonix - Hennadii Lutsyshyn">
// Copyright (c) Heleonix - Hennadii Lutsyshyn. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the repository root for full license information.
// </copyright>

namespace Heleonix.Validation
{
    using System.Linq.Expressions;

    /// <summary>
    /// The helper for working with reflection.
    /// </summary>
    public static class ReflectionHelper
    {
        /// <summary>
        /// Gets a name of a member.
        /// </summary>
        /// <param name="expression">An expression.</param>
        /// <returns>A name of a member.</returns>
        public static string GetMemberName(Expression expression)
        {
            return string.Join(
                ".",
                (expression as LambdaExpression)?.Body.ToString().Split('.').Skip(1) ?? Enumerable.Empty<string>());
        }
    }
}