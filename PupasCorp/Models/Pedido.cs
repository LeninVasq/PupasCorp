using System;
using System.Collections.Generic;

namespace PupasCorp.Models;

public partial class Pedido
{
    public int IdPedido { get; set; }

    public int IdUsuario { get; set; }

    public DateTime? FechaEntrega { get; set; }

    public bool Estado { get; set; }

    public virtual ICollection<Entrega> Entregas { get; set; } = new List<Entrega>();

    public virtual Usuario IdUsuarioNavigation { get; set; } = null!;

    public virtual ICollection<PedidosItem> PedidosItems { get; set; } = new List<PedidosItem>();
}
