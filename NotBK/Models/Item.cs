﻿using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace NotBK.Models;

public partial class Item
{
    public string CodItem { get; set; } = null!;

    public string CodCategoria { get; set; } = null!;

    public string Nombre { get; set; } = null!;

    public string Descripcion { get; set; } = null!;

    public decimal Precio { get; set; }

    public string Size { get; set; } = null!;

    [ValidateNever]

    public virtual Categorium CodCategoriaNavigation { get; set; } = null!;

    public virtual ICollection<DetallePedido> DetallePedidos { get; set; } = new List<DetallePedido>();

    public virtual ICollection<PromoItem> PromoItems { get; set; } = new List<PromoItem>();
}
