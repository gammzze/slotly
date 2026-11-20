using FluentValidation;
using Slotly.Application.Services.Dtos;

namespace Slotly.Application.Services.Validators;

public class CreateServiceRequestValidator : AbstractValidator<CreateServiceRequest>
{
    public CreateServiceRequestValidator()
    {
        RuleFor(x => x.TenantId)
            .NotEmpty().WithMessage("İşletme kimliği zorunlu.");

        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Hizmet adı boş olamaz.")
            .MaximumLength(150).WithMessage("Hizmet adı en fazla 150 karakter olabilir.");

        RuleFor(x => x.Description)
            .MaximumLength(1000).WithMessage("Açıklama en fazla 1000 karakter olabilir.");

        RuleFor(x => x.DurationMinutes)
            .GreaterThan(0).WithMessage("Süre sıfırdan büyük olmalı.")
            .LessThanOrEqualTo(480).WithMessage("Süre en fazla 480 dakika (8 saat) olabilir.");

        RuleFor(x => x.BufferMinutes)
            .InclusiveBetween(0, 120).WithMessage("Ara süre 0 ile 120 dakika arasında olmalı.");

        RuleFor(x => x.Price)
            .GreaterThanOrEqualTo(0).WithMessage("Fiyat negatif olamaz.")
            .PrecisionScale(10, 2, false).WithMessage("Fiyat en fazla 2 ondalık basamak içerebilir.");
    }
}
