// <copyright file="ValueResult.cs" company="Heleonix - Hennadii Lutsyshyn">
// Copyright (c) Heleonix - Hennadii Lutsyshyn. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the repository root for full license information.
// </copyright>

namespace Heleonix.Validation
{
    /// <summary>
    /// Represents a result for a specific rule validation value.
    /// </summary>
    [Serializable]
    public class ValueResult : Result
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ValueResult"/> class.
        /// </summary>
        /// <param name="matchValue">A value to select the result when it equals to a rule validation result.</param>
        /// <param name="resourceName">A resource name.</param>
        /// <param name="resourceKey">A resource key.</param>
        public ValueResult(object matchValue, string resourceName, string resourceKey)
        {
            this.MatchValue = matchValue;
            this.ResourceName = resourceName;
            this.ResourceKey = resourceKey;
        }

        /// <summary>
        /// Gets or sets a value to select the result when it equals to a rule validation result.
        /// </summary>
        public object MatchValue { get; set; }

        /// <summary>
        /// Gets or sets a resource name.
        /// </summary>
        public string ResourceName { get; set; }

        /// <summary>
        /// Gets or sets a resource key.
        /// </summary>
        public string ResourceKey { get; set; }

        /// <summary>
        /// Indicates whether the result is empty.
        /// </summary>
        /// <returns><see langword="true"/> if the result is empty, otherwise <see langword="false"/>.</returns>
        public override bool IsEmpty() => string.IsNullOrEmpty(this.ResourceName) && string.IsNullOrEmpty(this.ResourceKey);
    }
}