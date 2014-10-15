using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MvvmCrudGv.Service.Entity;

namespace MvvmCrudGv.Service.MvvmCrudGv.Persistence
{
    class TodoPersistence: ITodoPersistence
    {
        AbstractCrudDB _protobufDb;

        public TodoPersistence()
        {
            _protobufDb = new ProtobufDB(MvvmCrudGvConstants.DefaultDataPath,"bin");
        }

        public Guid Add(Entity.Todo todo)
        {
            var filename = _protobufDb.Write<Todo>(todo, todo.Id.ToString());
            return (todo.Id);
        }

        public void Delete(Guid id)
        {
            _protobufDb.Delete<Todo>(id.ToString());
        }

        public List<Entity.Todo> List()
        {
            return (_protobufDb.Read<Todo>().ToList());
        }

        public bool Update(Entity.Todo todo)
        {
            //Nasty isn't it
            Guid id = Add(todo);
            return (true);
        }

        public Entity.Todo Get(Guid id)
        {
           return _protobufDb.Read<Todo>(id.ToString());
        }
    }
}
