// <copyright file="LoginFormValidator.cs" company="Heleonix - Hennadii Lutsyshyn">
// Copyright (c) Heleonix - Hennadii Lutsyshyn. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the repository root for full license information.
// </copyright>

namespace Heleonix.Validation.Tests.Examples
{
    using Heleonix.Validation;

    /// <summary>
    /// Validates the <see cref="LoginForm"/>.
    /// </summary>
    public class LoginFormValidator : Validator<LoginForm>
    {
        /// <inheritdoc/>
        protected override void Setup(IInitialTargetBuilder<LoginForm> validate)
        {
            validate.Member(form => form.Password)
                .IsRequired().WithError("Errors", "Password.Required")
                .HasLength(12, null).WithError("Errors", "Password.MinLength");

            validate.Member(form => form.Username)
                .IsRequired().WithError("Errors", "Username.Required")
                .HasLength(8, 80).WithError("Errors", "Username.Length")
                .IsSafeText().WithError("Errors", "Username.Unsafe");
        }
    }
}
