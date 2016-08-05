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

namespace Heleonix.Validation
{
    /// <summary>
    /// Represents the <see langword="interface"/> to finalize building value results.
    /// </summary>
    /// <typeparam name="TObject">A type of an object to validate.</typeparam>
    /// <typeparam name="TTarget">A type of a target.</typeparam>
    /// <typeparam name="TValue">A type of a value returned by a rule.</typeparam>
    public interface IFinalValueResultBuilder<TObject, TTarget, TValue> : IInitialRuleBuilder<TObject, TTarget>,
        IInitialValueResultBuilder<TObject, TTarget, TValue>
    {
        #region Properties

        /// <summary>
        /// Gets a target.
        /// </summary>
        new Target Target { get; }

        /// <summary>
        /// Gets a rule.
        /// </summary>
        new Rule Rule { get; }

        /// <summary>
        /// Gets or sets a value result.
        /// </summary>
        ValueResult ValueResult { get; set; }

        #endregion
    }
}