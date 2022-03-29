using FluentValidation;

namespace Application.AdapterInbound.Rest.Validators
{
    public class TodoRequestValidator : AbstractValidator<TodoRequest>
    {
        public TodoRequestValidator()
        {
            RuleFor(todoRequest => todoRequest.Name)
                .NotEmpty()
                .WithMessage(todoRequest => $"Given {todoRequest.Name} is Invalid!")
                .Matches(@"^[a-zA-Z\s]+$")
                .WithMessage("'{PropertyName}' nao pode conter numeros ou caracteres especiais")
                .MaximumLength(30)
                .WithMessage("Tamanho maximo eh 30");

            RuleFor(todoRequest => todoRequest.Email)
                .EmailAddress()
                .WithMessage("Given '{PropertyName}' is Invalid");

            RuleFor(todoRequest => todoRequest.Deadline)
                .Must(x => IsDeadlineInTheFuture(x))
                .WithMessage("Deadline must be in the future");
        }

        private bool IsDeadlineInTheFuture(DateTime dateTime)
        {
            return dateTime > DateTime.Now;
        }
    }
}
