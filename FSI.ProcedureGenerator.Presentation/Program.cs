using FSI.ProcedureGenerator.Domain.Entity;
using FSI.ProcedureGenerator.Application.Services;
using System.Reflection;

class Program
{
    static void Main()
    {
        var procedureGenerator = new ProcedureGeneratorService();

        var domainAssembly = Assembly.Load("FSI.ProcedureGenerator.Domain");
        var entityNamespace = "FSI.ProcedureGenerator.Domain.Entity";
        var entityTypes = domainAssembly.GetTypes()
                                        .Where(t => t.IsClass && t.Namespace == entityNamespace)
                                        .ToList();

        if (!entityTypes.Any())
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("❌ Nenhuma entidade encontrada na camada Domain.Entity.");
            Console.ResetColor();
            return;
        }

        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("📌 Gerando procedures e scripts de criação de tabelas para todas as entidades...\n");
        Console.ResetColor();

        foreach (var entityType in entityTypes)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"🔹 Gerando scripts para: {entityType.Name}");
            Console.ResetColor();

            // Gera e salva os scripts no arquivo SQL
            procedureGenerator.GenerateProceduresAndSaveToFile(entityType);
        }

        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("\n✅ Todos os arquivos SQL foram gerados na pasta 'GeneratedSQL'.");
        Console.ResetColor();
        Console.ReadLine(); // Mantém o console aberto para visualizar os logs
    }
}