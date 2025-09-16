using Dominio.Dtos;
using FluentValidation;

namespace gerenciaGastos.Validation
{
    public class OrcamentoValidation : AbstractValidator<OrcamentoDto>
    {
        public OrcamentoValidation() {
            RuleFor(v => v.ValorLimite).NotEmpty().WithMessage("Valor limite não pode ser nulo.")
                .GreaterThanOrEqualTo(10).WithMessage("O valor limite deve ser no mínimo 10.");

        }
    }
}
