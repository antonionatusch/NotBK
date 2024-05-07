using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NotBK.Models;

public partial class Promocion
{
    public string CodPromo { get; set; } = null!;

    public string Nombre { get; set; } = null!;

    [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:P0}")]
    public decimal? Descuento { get; set; }

    public virtual ICollection<PromoItem> PromoItems { get; set; } = new List<PromoItem>();
}
