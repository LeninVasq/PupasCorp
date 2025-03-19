using System;
using System.Collections.Generic;

namespace PupasCorp.Models;

public partial class MenuIngrediente
{
    public int IdMenuIngredi { get; set; }

    public int IdIngrediente { get; set; }

    public int IdMenu { get; set; }

    public int Cantidad { get; set; }

    public virtual Ingrediente IdIngredienteNavigation { get; set; } = null!;

    public virtual Menu IdMenuNavigation { get; set; } = null!;
}
