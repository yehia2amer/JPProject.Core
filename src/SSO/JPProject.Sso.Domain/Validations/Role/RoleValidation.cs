﻿using FluentValidation;
using JPProject.Sso.Domain.Commands.Role;

namespace JPProject.Sso.Domain.Validations.Role
{
    public abstract class RoleValidation<T> : AbstractValidator<T> where T : RoleCommand
    {
        protected void ValidateName()
        {
            RuleFor(c => c.Name).NotEmpty().WithMessage("Name must be set"); ;
        }

        protected void ValidateNewName()
        {
            RuleFor(c => c.OldName).NotEmpty().NotEqual(c => c.Name).WithMessage("New name must be different from old one"); ;
        }

        protected void ValidateUsername()
        {
            RuleFor(c => c.Username).NotEmpty().WithMessage("Uername must be set"); ;
        }
    }
}
