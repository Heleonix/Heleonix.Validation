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

namespace NLion.Validation.Builders
{
    /// <summary>
    /// Represents the implementation of a final value result builder.
    /// </summary>
    /// <typeparam name="TObject">A type of an object to validate.</typeparam>
    /// <typeparam name="TTarget">A type of a target to continue build items for.</typeparam>
    /// <typeparam name="TValue">A type of a value to continue build value results for.</typeparam>
    public class FinalValueResultBuilder<TObject, TTarget, TValue> :
        InitialValueResultBuilder<TObject, TTarget, TValue>,
        IFinalValueResultBuilder<TObject, TTarget, TValue>
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="FinalValueResultBuilder{TObject, TTarget, TValue}"/> class.
        /// </summary>
        /// <param name="validator">A validator.</param>
        /// <param name="targetContainer">A target container.</param>
        /// <param name="ruleContainer">A rule container.</param>
        /// <param name="valueResultContainer">A value result container.</param>
        /// <exception cref="ArgumentNullException">
        /// The <paramref name="validator"/> is <see langword="null"/>.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// The <paramref name="targetContainer"/> is <see langword="null"/>.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// The <paramref name="ruleContainer"/> is <see langword="null"/>.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// The <paramref name="valueResultContainer"/> is <see langword="null"/>.
        /// </exception>
        public FinalValueResultBuilder(Validator<TObject> validator, TargetContainer targetContainer,
            RuleContainer ruleContainer, ValueResultContainer valueResultContainer)
            : base(validator, targetContainer, ruleContainer)
        {
            Throw.ArgumentNullException(valueResultContainer == null, nameof(valueResultContainer));

            ValueResultContainer = valueResultContainer;
        }

        #endregion

        #region IFinalValueResultBuilder<TObject,TTarget,TValue> Members

        /// <summary>
        /// Gets a value result container.
        /// </summary>
        public ValueResultContainer ValueResultContainer { get; }

        #endregion
    }
}