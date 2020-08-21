namespace QPH_MAIN.Core.Entities
{
    public partial class UserView : BaseEntity
    {
        public int parent { get; set; }
        public int children { get; set; }
        public int userId { get; set; }
        public virtual Views views { get; set; }
        public virtual User user { get; set; }
    }
}