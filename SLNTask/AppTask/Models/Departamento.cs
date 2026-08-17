using System;
using System.Collections.Generic;

namespace AppTask.Models;

public partial class Departamento
{
    public int Codigo { get; set; }

    public string Descricao { get; set; } = null!;

    public bool? Ativo { get; set; }
}
