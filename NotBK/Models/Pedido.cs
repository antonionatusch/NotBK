using System;
using System.Collections.Generic;

namespace NotBK.Models;

public partial class Pedido
{
    public string CodPedido { get; set; } = null!;

    public string CodCliente { get; set; } = null!;

    public DateOnly Fecha { get; set; }

    public virtual Cliente CodClienteNavigation { get; set; } = null!;

    public virtual ICollection<DetallePedido> DetallePedidos { get; set; } = new List<DetallePedido>();
}
