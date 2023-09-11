using _4Create.Application.Dtos.Companies.CreateCompany;
using _4Create.Contracts.Dtos.Companies.CreateCompany.Request;
using Riok.Mapperly.Abstractions;

namespace _4Create.WebApi.Mappers;

[Mapper(AllowNullPropertyAssignment = true)]
public static partial class CompaniesMapper
{
    public static partial CreateCompanyDto CreateCompanyRequestToDto(CreateCompanyRequest request);
}
