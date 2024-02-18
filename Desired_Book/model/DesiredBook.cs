using Infrastruct;

namespace model
{
    public class DesiredBook : IUpdate<DesiredBook>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid Id_User { get; set; }
        private void Update(DesiredBook book)
        {
            Id = book.Id;
            Name = book.Name;
            Id_User = book.Id_User;
        }
        DesiredBook IUpdate<DesiredBook>.Update(DesiredBook? entity)
        {
            if (entity is not null)
            {
                Update(entity);
            }
            return this;
        }
    }
}
