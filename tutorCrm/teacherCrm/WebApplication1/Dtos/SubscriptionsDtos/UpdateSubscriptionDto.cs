using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Dtos.SubscriptionsDtos;

public class UpdateSubscriptionDto
{
    [Range(1, 100)]
    public int? LessonsTotal { get; set; }

    [Range(0, 100)]
    public int? LessonsLeft { get; set; }

    [Range(0.01, 1000000)]
    public decimal? Price { get; set; }

    public DateTime? EndDate { get; set; }
}
