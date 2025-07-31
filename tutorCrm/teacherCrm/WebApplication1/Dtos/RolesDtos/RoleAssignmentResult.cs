namespace WebApplication1.Dtos.RolesDtos;

public record RoleAssignmentResult(
    int TotalRolesRequested,
    int SuccessfullyAssigned,
    string Message = "")
{
    public bool AllAssigned => TotalRolesRequested == SuccessfullyAssigned;
}