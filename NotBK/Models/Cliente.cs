using System;
using System.Collections.Generic;

namespace NotBK.Models;

public partial class Cliente
{
    public string CodCliente { get; set; } = null!;

    public string Nit { get; set; } = null!;

    public string Nombre { get; set; } = null!;

    public virtual ICollection<Pedido> Pedidos { get; set; } = new List<Pedido>();
}
