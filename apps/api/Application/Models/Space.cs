using System;
using System.Collections.Generic;

namespace Application.Models;

public class Space : BaseEntity
{
    public string Name { get; set; }
    public Workspace Workspace {get; set;}
    public virtual ICollection<Note> Notes { get; set; }
}
