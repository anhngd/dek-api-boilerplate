using System.ComponentModel.DataAnnotations;

namespace Dek.Api.Entities;

public abstract class BaseEntity
{
    [Key]
    public Guid Id { get; set; }
    public DateTimeOffset Created { get; set; }
    public DateTimeOffset Updated { get; set; }
}