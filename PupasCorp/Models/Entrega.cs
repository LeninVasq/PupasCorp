using System;
using System.Collections.Generic;

namespace PupasCorp.Models;

public partial class Entrega
{
    public int IdEntrega { get; set; }

    public int IdPedido { get; set; }

    public int IdDireccion { get; set; }

    public decimal? CostoEnvio { get; set; }

    public DateTime? FechaEnvio { get; set; }

    public DateTime? FechaEntrega { get; set; }

    public int IdEstadoPedido { get; set; }

    public int IdUsuarioDelivery { get; set; }

    public int IdMetodoEntrega { get; set; }

    public virtual ICollection<Factura> Facturas { get; set; } = new List<Factura>();

    public virtual Direccione IdDireccionNavigation { get; set; } = null!;

    public virtual EstadoPedido IdEstadoPedidoNavigation { get; set; } = null!;

    public virtual MetodoEntrega IdMetodoEntregaNavigation { get; set; } = null!;

    public virtual Pedido IdPedidoNavigation { get; set; } = null!;

    public virtual Usuario IdUsuarioDeliveryNavigation { get; set; } = null!;
}
