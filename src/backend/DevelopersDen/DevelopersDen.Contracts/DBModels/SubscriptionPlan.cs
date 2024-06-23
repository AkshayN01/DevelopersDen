namespace DevelopersDen.Contracts.DBModels
{
    public  class SubscriptionPlan : AuditableEntity
    {
        public Int32 Id { get; set; }
        public String Name { get; set; }
        public Int32 AllowedSubAccounts { get; set; }
        public Double PricePerMonth { get; set; } 
        public ICollection<Subscription> Subscriptions { get; set; } = new List<Subscription>();
    }
}
