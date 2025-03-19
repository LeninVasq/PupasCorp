using System;
using System.Collections.Generic;

namespace PupasCorp.Models;

public partial class PedidosItem
{
    public int IdPedidosItem { get; set; }

    public int IdPedido { get; set; }

    public int IdPlatillo { get; set; }

    public int Cantidad { get; set; }

    public decimal Precio { get; set; }

    public bool Estado { get; set; }

    public virtual Pedido IdPedidoNavigation { get; set; } = null!;

    public virtual Platillo IdPlatilloNavigation { get; set; } = null!;
}
