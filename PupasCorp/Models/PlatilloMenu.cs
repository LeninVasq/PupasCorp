using System;
using System.Collections.Generic;

namespace PupasCorp.Models;

public partial class PlatilloMenu
{
    public int IdPlatilloMenu { get; set; }

    public int IdPlatillo { get; set; }

    public int IdMenu { get; set; }

    public int Cantidad { get; set; }

    public virtual Menu IdMenuNavigation { get; set; } = null!;

    public virtual Platillo IdPlatilloNavigation { get; set; } = null!;
}
