using FluentValidation;
using TrueStory.Models;

namespace TrueStory.Validators;

public class ItemValidator : AbstractValidator<ItemModel>
{
    public ItemValidator()
    {
        RuleFor(x => x.Name).NotNull();
        RuleFor(x => x.Name).NotEmpty();
        // to be continued...
    }
}