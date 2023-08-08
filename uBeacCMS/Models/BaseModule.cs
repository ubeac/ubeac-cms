namespace uBeacCMS.Models
{
    public class BaseModule: AuditEntity
    {
        public Guid ModuleId { get; set; }
        public Dictionary<string, string> Setting { get; set; }
    }
}
