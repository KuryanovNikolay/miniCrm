using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Dtos.RolesDtos;

public class CreateRoleDto
{
    [Required]
    [StringLength(50)]
    public string Name { get; set; }

    [StringLength(255)]
    public string Description { get; set; }
}
