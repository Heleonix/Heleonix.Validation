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

namespace NLion.Validation
{
    /// <summary>
    /// Represents an <see langword="interface"/> to finalize building value results.
    /// </summary>
    /// <typeparam name="TObject">A type of an object to validate.</typeparam>
    /// <typeparam name="TTarget">A type of a target to continue build items for.</typeparam>
    /// <typeparam name="TValue">A type of a value to continue build value results for.</typeparam>
    public interface IFinalValueResultBuilder<TObject, TTarget, TValue> :
        IInitialValueResultBuilder<TObject, TTarget, TValue>, IInitialRuleBuilder<TObject, TTarget>
    {
        #region Properties

        /// <summary>
        /// Gets a target container.
        /// </summary>
        new TargetContainer TargetContainer { get; }

        /// <summary>
        /// Gets a value result container.
        /// </summary>
        ValueResultContainer ValueResultContainer { get; }

        #endregion
    }
}