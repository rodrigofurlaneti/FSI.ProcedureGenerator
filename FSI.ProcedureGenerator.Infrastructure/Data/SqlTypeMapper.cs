namespace FSI.ProcedureGenerator.Infrastructure.Data
{
    public static class SqlTypeMapper
    {
        public static string GetSqlType(Type type)
        {
            if (type == typeof(int)) return "INT";
            if (type == typeof(long)) return "BIGINT"; // Corrigido erro de "BIGTINT"
            if (type == typeof(short)) return "SMALLINT";
            if (type == typeof(byte)) return "TINYINT";

            if (type == typeof(float)) return "REAL";
            if (type == typeof(double)) return "FLOAT";
            if (type == typeof(decimal)) return "DECIMAL(18,2)";

            if (type == typeof(bool)) return "BIT";

            if (type == typeof(char)) return "CHAR(1)";
            if (type == typeof(string)) return "VARCHAR(255)"; // Pode ser configurável

            if (type == typeof(DateTime)) return "DATETIME";
            if (type == typeof(DateOnly)) return "DATE";
            if (type == typeof(TimeOnly)) return "TIME";
            if (type == typeof(TimeSpan)) return "TIME(7)";

            if (type == typeof(Guid)) return "UNIQUEIDENTIFIER";

            // Para Enums, usamos INT por padrão
            if (type.IsEnum) return "INT";

            // Arrays e tipos complexos
            if (type == typeof(byte[])) return "VARBINARY(MAX)"; // Para arquivos binários

            // Se não for mapeado, retorna VARCHAR(255) por segurança
            return "VARCHAR(255)";
        }
    }
}