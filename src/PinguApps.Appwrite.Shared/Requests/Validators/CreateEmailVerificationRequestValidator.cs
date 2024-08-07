﻿using System;
using FluentValidation;

namespace PinguApps.Appwrite.Shared.Requests.Validators;
public class CreateEmailVerificationRequestValidator : AbstractValidator<CreateEmailVerificationRequest>
{
    public CreateEmailVerificationRequestValidator()
    {
        RuleFor(x => x.Url).NotEmpty().Must(uri => Uri.TryCreate(uri, UriKind.Absolute, out _));
    }
}
