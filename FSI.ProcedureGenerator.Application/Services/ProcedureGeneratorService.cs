using FSI.ProcedureGenerator.Domain.Interfaces;
using FSI.ProcedureGenerator.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace FSI.ProcedureGenerator.Application.Services
{
    public class ProcedureGeneratorService : IProcedureGeneratorService
    {
        private readonly string _outputPath = "GeneratedSQL"; // Pasta para salvar os arquivos SQL

        public ProcedureGeneratorService()
        {
            if (!Directory.Exists(_outputPath))
                Directory.CreateDirectory(_outputPath);
        }

        public string GenerateCreateTableScript(Type entityType)
        {
            string tableName = entityType.Name;

            StringBuilder sql = new StringBuilder();
            sql.AppendLine($"CREATE TABLE {tableName} (");

            var properties = entityType.GetProperties();
            var columns = properties.Select(p => $"    {p.Name} {SqlTypeMapper.GetSqlType(p.PropertyType)}");
            sql.AppendLine(string.Join(",\n", columns));

            sql.AppendLine("    PRIMARY KEY (Id)");
            sql.AppendLine(");");

            return sql.ToString();
        }

        public string GenerateInsertProcedure(Type entityType)
        {
            string tableName = entityType.Name;

            StringBuilder sql = new StringBuilder();
            sql.AppendLine($"CREATE PROCEDURE SP_{tableName}_Post");
            sql.AppendLine("(");
            sql.AppendLine(string.Join(",\n", entityType.GetProperties().Select(p => $"    @{p.Name} {SqlTypeMapper.GetSqlType(p.PropertyType)}")));
            sql.AppendLine(")");
            sql.AppendLine("AS");
            sql.AppendLine("BEGIN");
            sql.AppendLine($"    INSERT INTO {tableName} ({string.Join(", ", entityType.GetProperties().Select(p => p.Name))})");
            sql.AppendLine($"    VALUES ({string.Join(", ", entityType.GetProperties().Select(p => "@" + p.Name))});");
            sql.AppendLine("END");

            return sql.ToString();
        }

        public string GenerateUpdateProcedure(Type entityType)
        {
            string tableName = entityType.Name;
            PropertyInfo[] properties = entityType.GetProperties().Where(p => p.Name != "Id" && p.Name != "CreatedAt" && p.Name != "CreatedId").ToArray();
            string setClause = string.Join(", ", properties.Select(p => $"{p.Name} = @{p.Name}"));

            StringBuilder sql = new StringBuilder();
            sql.AppendLine($"CREATE PROCEDURE SP_{tableName}_Put");
            sql.AppendLine("(");
            sql.AppendLine(string.Join(",\n", properties.Select(p => $"    @{p.Name} {SqlTypeMapper.GetSqlType(p.PropertyType)}")));
            sql.AppendLine("    @Id BIGINT");
            sql.AppendLine(")");
            sql.AppendLine("AS");
            sql.AppendLine("BEGIN");
            sql.AppendLine($"    UPDATE {tableName} SET {setClause}, UpdateAt = GETDATE(), UpdateId = @UpdateId WHERE Id = @Id;");
            sql.AppendLine("END");

            return sql.ToString();
        }

        public string GenerateSoftDeleteProcedure(Type entityType)
        {
            string tableName = entityType.Name;

            StringBuilder sql = new StringBuilder();
            sql.AppendLine($"CREATE PROCEDURE SP_{tableName}_Delete");
            sql.AppendLine("(");
            sql.AppendLine("    @Id BIGINT,");
            sql.AppendLine("    @UpdateId BIGINT");
            sql.AppendLine(")");
            sql.AppendLine("AS");
            sql.AppendLine("BEGIN");
            sql.AppendLine($"    UPDATE {tableName} SET IsActive = 0, UpdateAt = GETDATE(), UpdateId = @UpdateId WHERE Id = @Id;");
            sql.AppendLine("END");

            return sql.ToString();
        }

        public string GenerateSelectProcedure(Type entityType)
        {
            string tableName = entityType.Name;

            StringBuilder sql = new StringBuilder();
            sql.AppendLine($"CREATE PROCEDURE SP_{tableName}_Get");
            sql.AppendLine("AS");
            sql.AppendLine("BEGIN");
            sql.AppendLine($"    SELECT * FROM {tableName} WHERE IsActive = 1;");
            sql.AppendLine("END");

            return sql.ToString();
        }

        public string GenerateGetByIdProcedure(Type entityType)
        {
            string tableName = entityType.Name;

            StringBuilder sql = new StringBuilder();
            sql.AppendLine($"CREATE PROCEDURE SP_{tableName}_GetById");
            sql.AppendLine("(");
            sql.AppendLine("    @Id BIGINT");
            sql.AppendLine(")");
            sql.AppendLine("AS");
            sql.AppendLine("BEGIN");
            sql.AppendLine($"    SELECT * FROM {tableName} WHERE Id = @Id;");
            sql.AppendLine("END");

            return sql.ToString();
        }

        public string GenerateExistsByIdProcedure(Type entityType)
        {
            string tableName = entityType.Name;

            StringBuilder sql = new StringBuilder();
            sql.AppendLine($"CREATE PROCEDURE SP_{tableName}_ExistsById");
            sql.AppendLine("(");
            sql.AppendLine("    @Id BIGINT,");
            sql.AppendLine("    @Exists BIT OUTPUT");
            sql.AppendLine(")");
            sql.AppendLine("AS");
            sql.AppendLine("BEGIN");
            sql.AppendLine($"    IF EXISTS (SELECT 1 FROM {tableName} WHERE Id = @Id)");
            sql.AppendLine("        SET @Exists = 1;");
            sql.AppendLine("    ELSE");
            sql.AppendLine("        SET @Exists = 0;");
            sql.AppendLine("END");

            return sql.ToString();
        }

        public List<string> GeneratePropertyBasedProcedures(Type entityType)
        {
            List<string> procedures = new List<string>();

            foreach (PropertyInfo property in entityType.GetProperties())
            {
                if (property.Name == "Id") continue;

                procedures.Add(GenerateExistsByPropertyProcedure(entityType, property));
                procedures.Add(GenerateGetByPropertyProcedure(entityType, property));
            }

            return procedures;
        }

        public string GenerateExistsByPropertyProcedure(Type entityType, PropertyInfo property)
        {
            string tableName = entityType.Name;
            string propertyName = property.Name;
            string sqlType = SqlTypeMapper.GetSqlType(property.PropertyType);

            StringBuilder sql = new StringBuilder();
            sql.AppendLine($"CREATE PROCEDURE SP_{tableName}_ExistsBy_{propertyName}");
            sql.AppendLine("(");
            sql.AppendLine($"    @{propertyName} {sqlType},");
            sql.AppendLine("    @Exists BIT OUTPUT");
            sql.AppendLine(")");
            sql.AppendLine("AS");
            sql.AppendLine("BEGIN");
            sql.AppendLine($"    IF EXISTS (SELECT 1 FROM {tableName} WHERE {propertyName} = @{propertyName})");
            sql.AppendLine("        SET @Exists = 1;");
            sql.AppendLine("    ELSE");
            sql.AppendLine("        SET @Exists = 0;");
            sql.AppendLine("END");

            return sql.ToString();
        }

        public string GenerateGetByPropertyProcedure(Type entityType, PropertyInfo property)
        {
            string tableName = entityType.Name;
            string propertyName = property.Name;
            string sqlType = SqlTypeMapper.GetSqlType(property.PropertyType);

            StringBuilder sql = new StringBuilder();
            sql.AppendLine($"CREATE PROCEDURE SP_{tableName}_GetBy_{propertyName}");
            sql.AppendLine("(");
            sql.AppendLine($"    @{propertyName} {sqlType}");
            sql.AppendLine(")");
            sql.AppendLine("AS");
            sql.AppendLine("BEGIN");
            sql.AppendLine($"    SELECT * FROM {tableName} WHERE {propertyName} = @{propertyName};");
            sql.AppendLine("END");

            return sql.ToString();
        }

        public void GenerateProceduresAndSaveToFile(Type entityType)
        {
            string tableName = entityType.Name;
            List<string> procedures = new List<string>
            {
                GenerateCreateTableScript(entityType),
                GenerateInsertProcedure(entityType),
                GenerateUpdateProcedure(entityType),
                GenerateSoftDeleteProcedure(entityType),
                GenerateSelectProcedure(entityType),
                GenerateGetByIdProcedure(entityType),
                GenerateExistsByIdProcedure(entityType)
            };

            procedures.AddRange(GeneratePropertyBasedProcedures(entityType));

            string filePath = Path.Combine(_outputPath, $"Procedure_{tableName}.sql");
            File.WriteAllText(filePath, string.Join("\n\n", procedures));

            Console.WriteLine($"✅ Arquivo gerado: {filePath}");
        }
    }
}
