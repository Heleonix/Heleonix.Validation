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
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;

namespace NLion.Validation
{
    /// <summary>
    /// Represents a default validator provider.
    /// </summary>
    public class ValidatorProvider : IValidatorProvider
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ValidatorProvider"/> class.
        /// </summary>
        /// <param name="isCached">
        /// <see langword="true"/> if validators should be cached, otherwise <see langword="false"/>.
        /// </param>
        public ValidatorProvider(bool isCached)
        {
            IsCached = isCached;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Finds implementations of validators for a given object.
        /// </summary>
        /// <param name="objectType">A type of an object to find validators for.</param>
        /// <returns>Implementations of validators for a given object.</returns>
        protected virtual Type[] FindImplementations(Type objectType)
        {
            return (from assembly in AppDomain.CurrentDomain.GetAssemblies()
                from type in assembly.GetTypes()
                where type.IsPublic && !type.IsAbstract && !type.IsInterface
                      && typeof (Validator<>).IsAssignableFrom(type)
                      && type.GetGenericArguments().Contains(objectType)
                select type).ToArray();
        }

        /// <summary>
        /// Creates a validator.
        /// </summary>
        /// <param name="type">A type of a validator to create.</param>
        /// <returns>A validator.</returns>
        protected virtual IValidator CreateValidator(Type type) => Activator.CreateInstance(type) as IValidator;

        #endregion

        #region Properties

        /// <summary>
        /// Gets cached validators.
        /// </summary>
        protected IDictionary<Type, IValidator> Cache { get; } = new Dictionary<Type, IValidator>();

        #endregion

        #region IValidatorProvider Members

        /// <summary>
        /// Gets a validator.
        /// </summary>
        /// <param name="objectType">A type of an object to return a validator for.</param>
        /// <exception cref="ArgumentNullException">
        /// The <paramref name="objectType" /> is <see langword="null"/>.
        /// </exception>
        /// <exception cref="ArgumentException">Multiple validators were found.</exception>
        /// <exception cref="InvalidOperationException">Could not create a validator.</exception>
        /// <returns>A validator or <see langword="null"/> if a validator was not found.</returns>
        public virtual IValidator GetValidator(Type objectType)
        {
            Contract.Requires<ArgumentNullException>(objectType != null);

            if (IsCached && Cache.ContainsKey(objectType))
            {
                return Cache[objectType];
            }

            var implementations = FindImplementations(objectType);

            if (implementations == null || implementations.Length == 0)
            {
                return null;
            }

            Contract.Requires<ArgumentException>(implementations.Length == 1);

            try
            {
                var validator = CreateValidator(implementations[0]);

                if (IsCached && validator != null)
                {
                    validator.Build();

                    Cache.Add(objectType, validator);
                }

                return validator;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(ex.Message, ex);
            }
        }

        /// <summary>
        /// Gets <see langword="true"/> if validators are cached or <see langword="false"/> if are created every time.
        /// </summary>
        public bool IsCached { get; }

        #endregion
    }
}