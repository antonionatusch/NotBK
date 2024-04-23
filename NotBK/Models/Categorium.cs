using System;
using System.Collections.Generic;

namespace NotBK.Models;

public partial class Categorium
{
    public string CodCategoria { get; set; } = null!;

    public string Descripcion { get; set; } = null!;

    public virtual ICollection<Item> Items { get; set; } = new List<Item>();
}
