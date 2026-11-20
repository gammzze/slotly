using Slotly.Application.Services.Dtos;
using Slotly.Application.Services.Validators;

namespace Slotly.UnitTests.Application;

public class CreateServiceRequestValidatorTests
{
    private readonly CreateServiceRequestValidator _validator = new();

    private static CreateServiceRequest Valid() => new(
        TenantId: Guid.NewGuid(),
        Name: "Cilt Bakımı",
        Description: "Derin temizlik",
        DurationMinutes: 60,
        BufferMinutes: 10,
        Price: 900m);

    [Fact]
    public void Valid_request_passes()
    {
        var result = _validator.Validate(Valid());

        Assert.True(result.IsValid);
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    public void Empty_name_fails(string name)
    {
        var result = _validator.Validate(Valid() with { Name = name });

        Assert.Contains(result.Errors, e => e.PropertyName == "Name");
    }

    [Fact]
    public void Name_longer_than_150_characters_fails()
    {
        var result = _validator.Validate(Valid() with { Name = new string('a', 151) });

        Assert.Contains(result.Errors, e => e.PropertyName == "Name");
    }

    [Fact]
    public void Name_of_exactly_150_characters_passes()
    {
        var result = _validator.Validate(Valid() with { Name = new string('a', 150) });

        Assert.True(result.IsValid);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-5)]
    [InlineData(481)]
    public void Duration_out_of_range_fails(int minutes)
    {
        var result = _validator.Validate(Valid() with { DurationMinutes = minutes });

        Assert.Contains(result.Errors, e => e.PropertyName == "DurationMinutes");
    }

    [Theory]
    [InlineData(1)]
    [InlineData(480)]
    public void Duration_at_the_limits_passes(int minutes)
    {
        var result = _validator.Validate(Valid() with { DurationMinutes = minutes });

        Assert.True(result.IsValid);
    }

    [Fact]
    public void Negative_price_fails()
    {
        var result = _validator.Validate(Valid() with { Price = -0.01m });

        Assert.Contains(result.Errors, e => e.PropertyName == "Price");
    }

    [Fact]
    public void Price_with_three_decimals_fails()
    {
        var result = _validator.Validate(Valid() with { Price = 10.123m });

        Assert.Contains(result.Errors, e => e.PropertyName == "Price");
    }

    [Fact]
    public void Free_service_is_allowed()
    {
        var result = _validator.Validate(Valid() with { Price = 0m });

        Assert.True(result.IsValid);
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(121)]
    public void Buffer_out_of_range_fails(int minutes)
    {
        var result = _validator.Validate(Valid() with { BufferMinutes = minutes });

        Assert.Contains(result.Errors, e => e.PropertyName == "BufferMinutes");
    }

    [Fact]
    public void Empty_tenant_id_fails()
    {
        var result = _validator.Validate(Valid() with { TenantId = Guid.Empty });

        Assert.Contains(result.Errors, e => e.PropertyName == "TenantId");
    }
}
