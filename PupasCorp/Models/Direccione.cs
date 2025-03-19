using System;
using System.Collections.Generic;

namespace PupasCorp.Models;

public partial class Direccione
{
    public int IdDireccion { get; set; }

    public string Direccion { get; set; } = null!;

    public int IdUsuario { get; set; }

    public virtual ICollection<Entrega> Entregas { get; set; } = new List<Entrega>();

    public virtual Usuario IdUsuarioNavigation { get; set; } = null!;
}
