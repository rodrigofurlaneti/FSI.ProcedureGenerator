using System;

namespace FSI.ProcedureGenerator.Domain.Interfaces
{
    public interface IProcedureGeneratorService
    {
        string GenerateCreateTableScript(Type entityType);
        string GenerateInsertProcedure(Type entityType);
        string GenerateUpdateProcedure(Type entityType);
        string GenerateSoftDeleteProcedure(Type entityType);
        string GenerateSelectProcedure(Type entityType);
        string GenerateGetByIdProcedure(Type entityType);
        string GenerateExistsByIdProcedure(Type entityType);
        List<string> GeneratePropertyBasedProcedures(Type entityType);
        void GenerateProceduresAndSaveToFile(Type entityType);
    }
}
