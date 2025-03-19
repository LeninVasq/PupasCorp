using System;
using System.Collections.Generic;

namespace PupasCorp.Models;

public partial class Menu
{
    public int IdMenu { get; set; }

    public string? Nombre { get; set; }

    public string? Foto { get; set; }

    public string? Descripcion { get; set; }

    public bool Estado { get; set; }

    public virtual ICollection<MenuIngrediente> MenuIngredientes { get; set; } = new List<MenuIngrediente>();

    public virtual ICollection<PlatilloMenu> PlatilloMenus { get; set; } = new List<PlatilloMenu>();
}
