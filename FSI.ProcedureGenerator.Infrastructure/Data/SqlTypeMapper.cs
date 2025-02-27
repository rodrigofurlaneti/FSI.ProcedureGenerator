namespace FSI.ProcedureGenerator.Infrastructure.Data
{
    public static class SqlTypeMapper
    {
        public static string GetSqlType(Type type)
        {
            if (type == typeof(int)) return "INT";
            if (type == typeof(long)) return "BIGTINT";
            if (type == typeof(string)) return "VARCHAR(255)";
            if (type == typeof(DateTime)) return "DATETIME";
            if (type == typeof(bool)) return "BIT";
            if (type == typeof(decimal)) return "DECIMAL(18,2)";
            return "VARCHAR(255)";
        }
    }
}
