// <copyright file="DefaultValidatorProvider.cs" company="Heleonix - Hennadii Lutsyshyn">
// Copyright (c) Heleonix - Hennadii Lutsyshyn. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the repository root for full license information.
// </copyright>

namespace Heleonix.Validation
{
    using Heleonix.Validation.Internal;

    /// <summary>
    /// Implements the <see cref="IValidatorProvider"/>.
    /// </summary>
    public class DefaultValidatorProvider : IValidatorProvider
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DefaultValidatorProvider"/> class.
        /// </summary>
        /// <param name="isCached">Determines whether validators are cached.</param>
        public DefaultValidatorProvider(bool isCached)
        {
            this.IsCached = isCached;
        }

        /// <summary>
        /// Gets a value indicating whether validators are cached.
        /// </summary>
        public virtual bool IsCached { get; }

        /// <summary>
        /// Gets cached validators.
        /// </summary>
        protected virtual IDictionary<Type, IValidator> Cache { get; } = new Dictionary<Type, IValidator>();

        /// <summary>
        /// Gets a validator.
        /// </summary>
        /// <param name="objectType">A type of an object to validate.</param>
        /// <exception cref="ArgumentNullException">
        /// The <paramref name="objectType" /> is <see langword="null"/>.
        /// </exception>
        /// <exception cref="ArgumentException">Multiple validators were found.</exception>
        /// <exception cref="InvalidOperationException">Could not create a validator.</exception>
        /// <returns>A validator or <see langword="null"/> if a validator was not found.</returns>
        public virtual IValidator GetValidator(Type objectType)
        {
            Throw<ArgumentNullException>.IfNull(objectType, nameof(objectType));

            if (this.IsCached && this.Cache.ContainsKey(objectType))
            {
                return this.Cache[objectType];
            }

            var implementations = this.FindImplementations(objectType);

            if (implementations == null || implementations.Length == 0)
            {
                return null;
            }

            Throw<ArgumentException>.If(implementations.Length > 1, string.Empty, nameof(objectType));

            try
            {
                var validator = this.CreateValidator(implementations[0]);

                if (validator == null || !this.IsCached)
                {
                    return validator;
                }

                validator.Setup();

                this.Cache.Add(objectType, validator);

                return validator;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(ex.Message, ex);
            }
        }

        /// <summary>
        /// Finds implementations of validators for a given object type.
        /// </summary>
        /// <param name="objectType">A type of an object to find validators for.</param>
        /// <exception cref="ArgumentNullException">
        /// The <paramref name="objectType" /> is <see langword="null"/>.
        /// </exception>
        /// <returns>Implementations of validators for a given object type.</returns>
        protected virtual Type[] FindImplementations(Type objectType)
        {
            Throw<ArgumentNullException>.IfNull(objectType, nameof(objectType));

            return (from assembly in AppDomain.CurrentDomain.GetAssemblies()
                    from type in assembly.GetTypes()
                    where type.IsPublic && !type.IsAbstract && !type.IsInterface
                          && typeof(IValidator).IsAssignableFrom(type)
                          && type.BaseType.GetGenericArguments().Contains(objectType)
                    select type).ToArray();
        }

        /// <summary>
        /// Creates a validator.
        /// </summary>
        /// <param name="type">A type of a validator to create.</param>
        /// <returns>A validator.</returns>
        protected virtual IValidator CreateValidator(Type type) => Activator.CreateInstance(type) as IValidator;
    }
}