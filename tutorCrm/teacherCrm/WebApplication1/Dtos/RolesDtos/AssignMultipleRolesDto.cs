using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Dtos.RolesDtos;

public class AssignMultipleRolesDto
{
    [Required]
    public Guid UserId { get; set; }

    [Required]
    [MinLength(1)]
    public IEnumerable<Guid> RoleIds { get; set; }
}