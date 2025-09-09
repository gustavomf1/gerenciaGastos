using Dominio.Dtos;
using FluentValidation;

namespace gerenciaGastos.Validation
{
    public class CategoriaValidation : AbstractValidator<CategoriaDto>
    {
        public CategoriaValidation()
        {
            RuleFor(p => p.Descricao).NotEmpty().WithMessage("Descrição não pode servazia");
            RuleFor(p => p.Descricao).MaximumLength(150).WithMessage("Descrição nomáximo 150");

        }
    }
}
