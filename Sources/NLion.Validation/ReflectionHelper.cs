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
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace NLion.Validation
{
    /// <summary>
    /// The helper for working with reflection.
    /// </summary>
    public static class ReflectionHelper
    {
        #region Methods

        /// <summary>
        /// Gets the name of the member.
        /// </summary>
        /// <typeparam name="TObject">The type of an object.</typeparam>
        /// <typeparam name="TMember">The type of the member.</typeparam>
        /// <param name="expression">The expression.</param>
        /// <returns>The name of the member.</returns>
        public static string GetMemberName<TObject, TMember>(
            Expression<Func<TObject, TMember>> expression)
        {
            return expression != null ? string.Join(".", expression.Body.ToString().Split('.').Skip(1)) : null;
        }

        /// <summary>
        /// Gets a <paramref name="member"/> value.
        /// </summary>
        /// <typeparam name="TMember">The type of a member.</typeparam>
        /// <param name="obj">An object.</param>
        /// <param name="member">The member.</param>
        /// <returns>The member value.</returns>
        /// <exception cref="ArgumentException">The <paramref name="member"/> does not belong to an object.</exception>
        public static TMember GetMemberValue<TMember>(object obj, string member)
        {
            if (string.IsNullOrEmpty(member))
            {
                return default(TMember);
            }

            var bindingFlags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance;

            foreach (var memberName in member.Split(new[] {'.'}, StringSplitOptions.RemoveEmptyEntries))
            {
                if (obj == null)
                {
                    return default(TMember);
                }

                var currentType = obj.GetType();

                var propertyInfo = currentType.GetProperty(memberName, bindingFlags);

                if (propertyInfo != null)
                {
                    obj = propertyInfo.GetValue(obj, null);
                }
                else
                {
                    var fieldInfo = currentType.GetField(memberName, bindingFlags);

                    if (fieldInfo != null)
                    {
                        obj = fieldInfo.GetValue(obj);
                    }
                    else
                    {
                        throw new ArgumentException(string.Empty, nameof(member));
                    }
                }
            }

            return obj is TMember ? (TMember) obj : default(TMember);
        }

        #endregion
    }
}