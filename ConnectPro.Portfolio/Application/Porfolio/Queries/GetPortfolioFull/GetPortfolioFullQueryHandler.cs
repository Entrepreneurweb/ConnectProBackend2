using ConnectPro.SharedKernel.Exceptions;
using MediatR;
using Portfolio.Application.DTOs;
using Portfolio.Domain.Aggregates.Portfolio.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio.Application.Porfolio.Queries.GetPortfolioFull
{
    public class GetPortfolioFullQueryHandler : IRequestHandler<GetPortfolioFullQuery, PortfolioFullDto>
    {
        private readonly IPortfolioRepository _portfolioRepository;

        public GetPortfolioFullQueryHandler(IPortfolioRepository portfolioRepository)
        {
            _portfolioRepository = portfolioRepository;
        }

        public async Task<PortfolioFullDto> Handle(GetPortfolioFullQuery query, CancellationToken ct)
        {
            var portfolio = await _portfolioRepository.GetByIdWithFullDetailsAsync(query.PortfolioId, ct);
            if (portfolio is null)
                throw new DomainException(" portfolio not found");

            return new PortfolioFullDto(
                Id: portfolio.Id,
                OwnerId: portfolio.GetOwnerId,
                Type: portfolio.GetPortfolioType.ToString(),
                Status: portfolio.Status.ToString(),
                ActiveServicesCount: portfolio.ActiveServicesCount,
                GeneralInfo: portfolio.GeneralInfo is null ? null : new GeneralInfoDto(
                    portfolio.GeneralInfo.FirstName,
                    portfolio.GeneralInfo.LastName,
                    portfolio.GeneralInfo.Bio,
                    portfolio.GeneralInfo.AvatarUrl?.Value
                ),
                ContactInfo: portfolio.ContactInfo is null ? null : new ContactInfoDto(
                    portfolio.ContactInfo.Email.Value,
                    portfolio.ContactInfo.Phone?.Value,
                    portfolio.ContactInfo.Website?.Value
                ),
                LocationInfo: portfolio.LocationInfo is null ? null : new LocationInfoDto(
                    portfolio.LocationInfo.Country,
                    portfolio.LocationInfo.City,
                    portfolio.LocationInfo.Timezone
                ),
                ProfessionalInfo: portfolio.ProfessionalInfo is null ? null : new ProfessionalInfoDto(
                    portfolio.ProfessionalInfo.Headline,
                    portfolio.ProfessionalInfo.Skills
                        .Select(s => new SkillDto(s.Name, s.Level))
                        .ToList()
                ),
                Experiences: portfolio.Experiences
                    .Select(e => new ExperienceDto(
                        e.Id,
                        e.Company,
                        e.Role,
                        e.Description,
                        e.Period.Start,
                        e.Period.End
                    ))
                    .ToList(),
                Certifications: portfolio.Certifications
                    .Select(c => new CertificationDto(
                        c.Id,
                        c.Name,
                        c.IssuingOrganization,
                        c.IssueDate,
                        c.ExpiryDate,
                        c.CredentialUrl?.Value
                    ))
                    .ToList()
            );
        }
    }
}
