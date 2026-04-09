using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio.Application.DTOs
{
    public record ServiceFullDto(
    Guid Id,
    Guid PortfolioId,
    string Title,
    string Description,
    string Status,
    string? CoverImageUrl,
    PricingDto? Pricing,
    IReadOnlyList<TagDto> Tags,
    IReadOnlyList<ImageUrlDto> Images,
    IReadOnlyList<FaqDto> Faqs,
    IReadOnlyList<AwardDto> Awards
);

}
