using System;
using System.Collections.Generic;

namespace NotBK.Models;

public partial class DetallePedido
{
    public string CodItem { get; set; } = null!;

    public string CodPedido { get; set; } = null!;

    public int Cantidad { get; set; }

    public string Direccion { get; set; } = null!;

    public virtual Item CodItemNavigation { get; set; } = null!;

    public virtual Pedido CodPedidoNavigation { get; set; } = null!;
}
