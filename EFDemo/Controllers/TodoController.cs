using EFDemo.Models;
using System.Linq;
using System.Web.Http;

namespace EFDemo.Controllers
{
    public class TodoController : ApiController
    {
        private readonly DemoDbContext _ctx;

        public TodoController(DemoDbContext dbContext)
        {
            _ctx = dbContext;
        }

        public TodoEntity Find(int id)
        {
            return _ctx.DbEntities.FirstOrDefault(dbEntity => dbEntity.Id == id);
        }

        public void Add(TodoEntity entity)
        {
            _ctx.DbEntities.Add(entity);
            _ctx.SaveChanges();
        }

        public void Delete(int id)
        {
            var entityToDelete = _ctx.DbEntities.Find(id);
            _ctx.DbEntities.Remove(entityToDelete);
            _ctx.SaveChanges();
        }
    }
}
