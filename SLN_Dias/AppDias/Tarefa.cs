namespace GestaoTarefas;

public class Tarefa
{
    public string Nome { get; set; } = string.Empty;
    public DateTime DataInicio { get; set; }
    public DateTime DataFim { get; set; }

    public Tarefa(string nome, DateTime dataInicio, DateTime dataFim)
    {
        Nome = nome;
        DataInicio = dataInicio;
        DataFim = dataFim;
    }

    public int ObterQuantidadeDias()
    {
        return (DataFim - DataInicio).Days;
    }
}