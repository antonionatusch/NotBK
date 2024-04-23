﻿using System;
using System.Collections.Generic;

namespace NotBK.Models;

public partial class PromoItem
{
    public string CodItem { get; set; } = null!;

    public string CodPromo { get; set; } = null!;

    public DateOnly FechaInicio { get; set; }

    public DateOnly FechaFin { get; set; }

    public virtual Item CodItemNavigation { get; set; } = null!;

    public virtual Promocion CodPromoNavigation { get; set; } = null!;
}
