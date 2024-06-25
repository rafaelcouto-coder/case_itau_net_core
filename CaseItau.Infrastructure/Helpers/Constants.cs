namespace CaseItau.Infrastructure.Helpers;

public static class Constants
{
    public static readonly string ProjectPath =
        Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\..\CaseItau.Infrastructure\Data\Database"));

    public static readonly string DatabasePath = Path.Combine(ProjectPath, "dbCaseItau.s3db");

    public static readonly string DbConnectionString = $"Data Source={DatabasePath};";
}
