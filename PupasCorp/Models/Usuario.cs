using System;
using System.Collections.Generic;

namespace PupasCorp.Models;

public partial class Usuario
{
    public int IdUsuario { get; set; }

    public string? Nombre { get; set; }

    public string? Apellido { get; set; }

    public string? Correo { get; set; }

    public string? Contrasenia { get; set; }

    public string? Token { get; set; }

    public bool Estado { get; set; }

    public string? Foto { get; set; }

    public string? Telefono { get; set; }

    public int IdTipoUsuario { get; set; }

    public virtual ICollection<Direccione> Direcciones { get; set; } = new List<Direccione>();

    public virtual ICollection<Entrega> Entregas { get; set; } = new List<Entrega>();

    public virtual TipoUsuario IdTipoUsuarioNavigation { get; set; } = null!;

    public virtual ICollection<Movimiento> Movimientos { get; set; } = new List<Movimiento>();

    public virtual ICollection<Pedido> Pedidos { get; set; } = new List<Pedido>();

    public virtual ICollection<Platillo> Platillos { get; set; } = new List<Platillo>();
}
