using Microsoft.AspNetCore.Mvc;

using IService;
using model;
using Viwe;

namespace WebApplication2.BookAPI
{
    [ApiController]
    [Route("[controller]")]
    public class ControllerBook :ControllerBase
    {
        IServiceModel<DesiredBook> _desiredBook;
        public ControllerBook(IServiceModel<DesiredBook> desiredBook)
        {
            _desiredBook = desiredBook;
        }
        [HttpGet]
        public async Task<IEnumerable<ViweDesiredBook>> Get()
        {
            var t = await Task.Run(delegate ()
            {
                return _desiredBook.get().Select(u => ViweConverter.ModelToViwe(u));
            });
            return t;
        }
        [HttpGet("{Id}")]
        public async Task<ViweDesiredBook> Get([FromQuery] Guid Id)
        {
            var t = await Task.Run(delegate ()
            {
                var book = _desiredBook.get(Id);
                return ViweConverter.ModelToViwe(book);
            });
            return t;
        }
        [HttpPost]
        public async Task<ActionResult<ViweDesiredBook>> Post([FromBody] ViweDesiredBook viweDesiredBook)
        {
            if(viweDesiredBook.Id == Guid.Empty)
            {
                var Book = _desiredBook.Add(
                    ViweConverter.ViweToModel(viweDesiredBook)
                    );
                return ViweConverter.ModelToViwe(Book);
            }
            var book = _desiredBook.Upd(viweDesiredBook.Id,
                ViweConverter.ViweToModel(viweDesiredBook));
            return ViweConverter.ModelToViwe(book);
        }
        [HttpPost("{Id}")]
        public async Task<ActionResult<ViweDesiredBook>> PostId([FromQuery] Guid Id, [FromBody] ViweDesiredBook viweDesiredBook)
        {
            if (Id != Guid.Empty)
            {
                viweDesiredBook.Id = Id;
                var book = _desiredBook.Upd(viweDesiredBook.Id,
                    ViweConverter.ViweToModel(viweDesiredBook));
                return ViweConverter.ModelToViwe(book);
            }
            return BadRequest();
        }

    }
}
