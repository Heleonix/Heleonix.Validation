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

using System.ComponentModel;

namespace NLion.Validation
{
    /// <summary>
    /// Represents the container for a rule.
    /// </summary>
    public class RuleContainer : INotifyPropertyChanged
    {
        #region Fields

        /// <summary>
        /// Gets or sets a rule.
        /// </summary>
        private Rule _rule;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="RuleContainer"/> class.
        /// </summary>
        /// <param name="rule">A rule to contain.</param>
        public RuleContainer(Rule rule)
        {
            Rule = rule;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Raises a <see cref="PropertyChanged"/>.
        /// </summary>
        /// <param name="propertyName">A name of a changed property.</param>
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets a rule.
        /// </summary>
        public Rule Rule
        {
            get { return _rule; }
            set
            {
                _rule = value;
                OnPropertyChanged(nameof(Rule));
            }
        }

        #endregion

        #region Events

        /// <summary>
        /// Occurs when a property changed.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        #endregion
    }
}