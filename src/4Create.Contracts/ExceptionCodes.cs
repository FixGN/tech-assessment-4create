namespace _4Create.Contracts;

public enum ExceptionCodes
{
    Undefined = 0,
    
    // Default
    InternalServerError = 1001,
    
    // Domain
    CompanyDoesntExistError = 2001,
    CompanyNameExistsError = 2002,
    DuplicateEmployeeTitleError = 2003,
    DuplicateEmployeeTitleInCompaniesError = 2004,
    
    EmailAlreadyExistsError = 2101,
    
    // Application
    CompanyNotFoundError = 3001,
    EmployeesNotFoundError = 3002,
    UserNotFoundError = 3003,
    UserPasswordIsInvalidError = 3004
}
