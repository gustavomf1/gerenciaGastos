using Dominio.Dtos;
using FluentValidation;

namespace gerenciaGastos.Validation
{
    public class TransacaoValidation : AbstractValidator<TransacaoCreateDto>
    {
        public TransacaoValidation()
        {
            RuleFor(t => t.Descricao)
                .NotEmpty().WithMessage("Descrição não pode ser vazia")
                .MaximumLength(200).WithMessage("Descrição pode ter no máximo 200 caracteres");

            RuleFor(t => t.Valor)
                .GreaterThan(0).WithMessage("O valor da transação deve ser maior que zero");

            RuleFor(t => t.Data)
                .LessThanOrEqualTo(DateTime.Now).WithMessage("A data da transação não pode ser futura");

            RuleFor(t => t.Tipo)
                .IsInEnum().WithMessage("Tipo de transação inválido");

            RuleFor(t => t.CategoriaId)
                .GreaterThan(0).WithMessage("Categoria deve ser informada");

            RuleFor(t => t.UsuarioId)
                .GreaterThan(0).WithMessage("Usuário deve ser informado");

        }
    }
}
