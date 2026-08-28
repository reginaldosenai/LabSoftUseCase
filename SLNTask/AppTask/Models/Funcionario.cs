using System;
using System.Collections.Generic;

namespace AppTask.Models;

public partial class Funcionario
{
    public int Codigo { get; set; }

    public string Nome { get; set; } = null!;

    public string Cargo { get; set; } = null!;

    public int? CodigoGerente { get; set; }


    public virtual Funcionario? Gerente { get; set; }

    public virtual ICollection<Tarefa> Tarefas { get; set; } = new List<Tarefa>();
}
