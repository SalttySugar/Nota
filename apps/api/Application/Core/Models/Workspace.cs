using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Application.Models;

public class Workspace : BaseEntity
{
    [Required] public string Name { get; set; } = null!;

    public virtual ICollection<Space> Spaces { get; set; } = new HashSet<Space>();
}