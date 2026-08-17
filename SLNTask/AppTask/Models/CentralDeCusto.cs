using System;
using System.Collections.Generic;

namespace AppTask.Models;

public partial class CentralDeCusto
{
    public int Codigo { get; set; }

    public string NomeCentral { get; set; } = null!;

    public decimal? ValorMetaAnual { get; set; }
}
