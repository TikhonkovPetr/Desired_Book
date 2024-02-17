using model;

namespace Viwe
{
    public class ViweConverter
    {
        static public ViweDesiredBook ModelToViwe(DesiredBook? book)
        {
            return new ViweDesiredBook
            {
                Id = book.Id,
                Name = book.Name
            };
        }
        static public DesiredBook ViweToModel(ViweDesiredBook? book) 
        {
            return new DesiredBook
            {
                Id = book.Id,
                Name = book.Name
            };
            }
    }
}
