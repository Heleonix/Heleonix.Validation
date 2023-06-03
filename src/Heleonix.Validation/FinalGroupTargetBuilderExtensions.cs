// <copyright file="FinalGroupTargetBuilderExtensions.cs" company="Heleonix - Hennadii Lutsyshyn">
// Copyright (c) Heleonix - Hennadii Lutsyshyn. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the repository root for full license information.
// </copyright>

namespace Heleonix.Validation
{
    using Heleonix.Validation.Builders;
    using Heleonix.Validation.Internal;
    using Heleonix.Validation.Targets;

    /// <summary>
    /// Provides extensions for the <see cref="IFinalGroupTargetBuilder{TObject}"/>.
    /// </summary>
    public static class FinalGroupTargetBuilderExtensions
    {
        /// <summary>
        /// Creates the <see cref="IfTarget"/>.
        /// </summary>
        /// <typeparam name="TObject">A type of an object to validate.</typeparam>
        /// <param name="builder">The <see cref="IFinalGroupTargetBuilder{TObject}"/>.</param>
        /// <param name="condition">A condition to perform validation.</param>
        /// <exception cref="ArgumentNullException">
        /// The <paramref name="builder"/> is <see langword="null"/>.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// The <paramref name="condition"/> is <see langword="null"/>.
        /// </exception>
        /// <returns>The <see cref="IFinalGroupTargetBuilder{TObject}"/> to build true conditions.</returns>
        public static IFinalGroupTargetBuilder<TObject> If<TObject>(
            this IFinalGroupTargetBuilder<TObject> builder, Predicate<TargetContext> condition)
        {
            Throw<ArgumentNullException>.IfNull(builder, nameof(builder));
            Throw<ArgumentNullException>.IfNull(condition, nameof(condition));

            builder.Target = new IfTarget(builder.Target, condition);

            return new FinalGroupTargetBuilder<TObject>(builder.Validator, builder.Target);
        }

        /// <summary>
        /// Creates the <see cref="IfNotTarget"/>.
        /// </summary>
        /// <typeparam name="TObject">A type of an object to validate.</typeparam>
        /// <param name="builder">The <see cref="IFinalGroupTargetBuilder{TObject}"/>.</param>
        /// <param name="condition">A condition to perform validation.</param>
        /// <exception cref="ArgumentNullException">
        /// The <paramref name="builder"/> is <see langword="null"/>.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// The <paramref name="condition"/> is <see langword="null"/>.
        /// </exception>
        /// <returns>The <see cref="IFinalGroupTargetBuilder{TObject}"/> to build false conditions.</returns>
        public static IFinalGroupTargetBuilder<TObject> IfNot<TObject>(
            this IFinalGroupTargetBuilder<TObject> builder, Predicate<TargetContext> condition)
        {
            Throw<ArgumentNullException>.IfNull(builder, nameof(builder));
            Throw<ArgumentNullException>.IfNull(condition, nameof(condition));

            builder.Target = new IfNotTarget(builder.Target, condition);

            return new FinalGroupTargetBuilder<TObject>(builder.Validator, builder.Target);
        }
    }
}