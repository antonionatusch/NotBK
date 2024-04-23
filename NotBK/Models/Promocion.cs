using System;
using System.Collections.Generic;

namespace NotBK.Models;

public partial class Promocion
{
    public string CodPromo { get; set; } = null!;

    public string Nombre { get; set; } = null!;

    public decimal? Descuento { get; set; }

    public virtual ICollection<PromoItem> PromoItems { get; set; } = new List<PromoItem>();
}
