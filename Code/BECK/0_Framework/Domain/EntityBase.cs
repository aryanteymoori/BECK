namespace _0_Framework.Domain
{
    public class EntityBase
    {
        public long Id { get; set; }
        public DateTime CreateionDate { get; set; }

        public EntityBase()
        {
            CreateionDate = DateTime.Now;
        }
    }
}
