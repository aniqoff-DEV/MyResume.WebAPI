namespace MyResume.Infrasctructure
{
    public struct NpgsqlConfig
    {
        public static string CONNECTION_STRING = "Host=localhost:5432;" +
          "Username=postgres;" +
          "Password=@niqoff2612;" +
          "Database=myresumedb";
    }
}
