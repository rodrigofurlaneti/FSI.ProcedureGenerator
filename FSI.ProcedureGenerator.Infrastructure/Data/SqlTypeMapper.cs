using System;

namespace FSI.ProcedureGenerator.Infrastructure.Data
{
    public static class SqlTypeMapper
    {
        public static string GetSqlType(Type type)
        {
            // Verifica se é um tipo anulável (Nullable<T>)
            Type underlyingType = Nullable.GetUnderlyingType(type) ?? type;

            if (underlyingType == typeof(int)) return "INT";
            if (underlyingType == typeof(long)) return "BIGINT";
            if (underlyingType == typeof(short)) return "SMALLINT";
            if (underlyingType == typeof(byte)) return "TINYINT";

            if (underlyingType == typeof(float)) return "REAL";
            if (underlyingType == typeof(double)) return "FLOAT";
            if (underlyingType == typeof(decimal)) return "DECIMAL(18,2)";

            if (underlyingType == typeof(bool)) return "BIT";

            if (underlyingType == typeof(char)) return "CHAR(1)";
            if (underlyingType == typeof(string)) return "VARCHAR(255)"; // Configurável

            if (underlyingType == typeof(DateTime)) return "DATETIME";
            if (underlyingType == typeof(DateOnly)) return "DATE";
            if (underlyingType == typeof(TimeOnly)) return "TIME";
            if (underlyingType == typeof(TimeSpan)) return "TIME(7)";

            if (underlyingType == typeof(Guid)) return "UNIQUEIDENTIFIER";

            // Para Enums, usamos INT por padrão
            if (underlyingType.IsEnum) return "INT";

            // Arrays e tipos complexos
            if (underlyingType == typeof(byte[])) return "VARBINARY(MAX)"; // Para arquivos binários

            // Se não for mapeado, retorna VARCHAR(255) por segurança
            return "VARCHAR(255)";
        }

        public static string GetSqlNullability(Type type)
        {
            // Se for anulável, retorna "NULL", caso contrário, "NOT NULL"
            return Nullable.GetUnderlyingType(type) != null ? "NULL" : "NOT NULL";
        }
    }
}
