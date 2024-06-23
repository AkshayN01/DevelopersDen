namespace DevelopersDen.Contracts.DBModels
{
    public class Stakeholder
    {
        public Int32 Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public Int32 IsActive { get; set; } = 0;
    }
}
