using FluentValidation.Results;
using RH.Core.Data;
using System.Threading.Tasks;

namespace RH.Core.Messages
{
    public abstract class CommandHandler
    {
        protected ValidationResult ValidationResult;

        protected CommandHandler()
        {
            ValidationResult = new ValidationResult();
        }

        protected void AdicionarErro(string mensagem)
        {
            ValidationResult.Errors.Add(new ValidationFailure(string.Empty, mensagem));
        }

        protected async Task<ValidationResult> PersistirDados(IUnitOfWork uow)
        {
            if (!await uow.Commit()) AdicionarErro("Houve um erro ao persistir os dados");

            return ValidationResult;
        }
    }
}
