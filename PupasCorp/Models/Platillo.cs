using System;
using System.Collections.Generic;

namespace PupasCorp.Models;

public partial class Platillo
{
    public int IdPlatillo { get; set; }

    public string? Nombre { get; set; }

    public string? Foto { get; set; }

    public int IdUsuario { get; set; }

    public bool Mostrar { get; set; }

    public virtual Usuario IdUsuarioNavigation { get; set; } = null!;

    public virtual ICollection<PedidosItem> PedidosItems { get; set; } = new List<PedidosItem>();

    public virtual ICollection<PlatilloMenu> PlatilloMenus { get; set; } = new List<PlatilloMenu>();
}
