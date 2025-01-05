namespace MT.Innovation.Shared.Infrastructure;

public class ApplicationInfo
{
    public string CurrentUserName { get; set; } = null!;
    public int CurrentUserId { get; set; }
    public string? CorrelationId { get; set; }
    public DateTime CurrentDateTime { get; set; }
    public string? CurrentActionPath { get; set; }
    public string? ApplicationRootPath { get; set; }
    public string? CurrentLanguage { get; set; }
}

