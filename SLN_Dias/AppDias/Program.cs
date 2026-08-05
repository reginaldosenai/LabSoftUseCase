using System;
using System.Globalization;

namespace GestaoTarefas;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("=====================================");
        Console.WriteLine(" SoftwareHouse - SISTEMA DE GESTÃO DE TAREFAS - Versão 1.0.0");
        Console.WriteLine("=====================================\n");

        Console.Write("Digite o nome da tarefa: ");
        string nome = Console.ReadLine() ?? "Sem nome";

        Console.Write("Digite o nome do funcionário responsável: ");
        string funcionario = Console.ReadLine() ?? "Não informado";

        // Leitura e Validação da Data de Início
        DateTime dataInicio;
        Console.Write("Digite a data de início (dd/mm/aaaa): ");
        while (!DateTime.TryParseExact(Console.ReadLine(), "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out dataInicio))
        {
            Console.Write("Data inválida! Digite novamente no formato dd/mm/aaaa: ");
        }

        // Leitura e Validação da Data de Término
        DateTime dataFim;
        Console.Write("Digite a data de término (dd/mm/aaaa): ");
        while (!DateTime.TryParseExact(Console.ReadLine(), "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out dataFim))
        {
            Console.Write("Data inválida! Digite novamente no formato dd/mm/aaaa: ");
        }

        // Instanciação e exibição do resumo
        Tarefa tarefa = new Tarefa(nome, funcionario, dataInicio, dataFim);

        Console.WriteLine("\n--- RESUMO DA TAREFA ---");
        Console.WriteLine($"Tarefa: {tarefa.Nome}");
        Console.WriteLine($"Responsável: {tarefa.NomeFuncionario}");
        Console.WriteLine($"Início: {tarefa.DataInicio:dd/MM/yyyy}");
        Console.WriteLine($"Término: {tarefa.DataFim:dd/MM/yyyy}");
        Console.WriteLine($"Duração: {tarefa.ObterQuantidadeDias()} dias");
    }
}