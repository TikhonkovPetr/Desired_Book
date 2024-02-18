using Microsoft.AspNetCore.Mvc;

using IService;
using model;
using Viwe;
using AbstractDBModel;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore;

namespace WebApplication2.BookAPI
{
    [ApiController]
    [Route("[controller]")]
    public class ControllerBook :ControllerBase
    {
        //IServiceModel<DesiredBook> _desiredBook;
        AppDBModel db_model;
        public ControllerBook(AppDBModel context)
        {
            //_desiredBook = desiredBook;
            db_model = context;
        }
        [HttpGet]
        public async Task<IEnumerable<ViweDesiredBook>> Get()
        {
            //var t = Task.Run( delegate ()
            //{
            //    return db_model.desiredBooks.Select( u=> ViweConverter.ModelToViwe(u));
            //return _desiredBook.get().Select(u => ViweConverter.ModelToViwe(u));
            //});
            //var t = db_model.desiredBooks.Select(u => ViweConverter.ModelToViwe(u));
            var t = db_model.desiredBooks.ToList();
            var o = new List<ViweDesiredBook>();
            foreach (DesiredBook d in t)
            {
                o.Add(ViweConverter.ModelToViwe(d));
            }
            return o;
        }
        [HttpGet("{Id}")]
        public async Task<ViweDesiredBook> Get([FromQuery] Guid Id)
        {
            //var t = await Task.Run(delegate ()
            //{
            //    //var book = _desiredBook.get(Id);
            //    var book = db_model.desiredBooks.FirstOrDefault(u => u.Id == Id);
            //    return ViweConverter.ModelToViwe(book);
            //});
            //var t = db_model.desiredBooks.FirstOrDefault(u => u.Id == Id);
            //return t;
            if (Id == Guid.Empty || db_model.desiredBooks == null)
            {
                return null;
            }

            var desirBook = await db_model.desiredBooks
                .FirstAsync(m => m.Id == Id);
            if (desirBook == null)
            {
                return null;
            }

            return ViweConverter.ModelToViwe(desirBook);
        }
        [HttpPost]
        public async Task<ActionResult<ViweDesiredBook>> Post([FromBody] ViweDesiredBook viweDesiredBook)
        {
            viweDesiredBook.Id = Guid.Empty;
            if (viweDesiredBook.Id == Guid.Empty)
            {
                //var Book = _desiredBook.Add(
                //    ViweConverter.ViweToModel(viweDesiredBook)
                //    );
                var Book = db_model.desiredBooks.Add(ViweConverter.ViweToModel(viweDesiredBook));
                //db_model.desiredBooks.Add(ViweConverter.ViweToModel(viweDesiredBook));
                db_model.SaveChanges();
                return ViweConverter.ModelToViwe(Book.Entity);
            }
            //var book = _desiredBook.Upd(viweDesiredBook.Id,
            //    ViweConverter.ViweToModel(viweDesiredBook));
            var book = db_model.desiredBooks.Update(ViweConverter.ViweToModel(viweDesiredBook));
            //db_model.desiredBooks.Add(ViweConverter.ViweToModel(viweDesiredBook));
            //db_model.desiredBooks.Update(ViweConverter.ViweToModel(viweDesiredBook));
            db_model.SaveChanges();
            return ViweConverter.ModelToViwe(book.Entity);
        }
        [HttpPost("{Id}")]
        public async Task<ActionResult<ViweDesiredBook>> PostId([FromQuery] Guid Id, [FromBody] ViweDesiredBook viweDesiredBook)
        {
            if (Id != Guid.Empty)
            {
                viweDesiredBook.Id = Id;
                //var book = _desiredBook.Upd(viweDesiredBook.Id,
                //    ViweConverter.ViweToModel(viweDesiredBook));
                var book = db_model.Update(ViweConverter.ViweToModel(viweDesiredBook));
                db_model.Update(ViweConverter.ViweToModel(viweDesiredBook));
                db_model.SaveChanges();
                return ViweConverter.ModelToViwe(book.Entity);
            }
            return BadRequest();
        }

    }
}
