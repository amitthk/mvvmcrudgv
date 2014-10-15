using MvvmCrudGv.Service.Entity;
using MvvmCrudGv.Service.MvvmCrudGv.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace MvvmCrudGv.Service
{
    [ServiceContract]
    public interface ITodoService
    {
        [OperationContract]
        Guid Add(Todo todo);
        [OperationContract]
        void Delete(Guid id);
        [OperationContract]
        List<Todo> List();
        [OperationContract]
        bool Update(Todo todo);
        [OperationContract]
        Todo Get(Guid id);
    }

    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single, UseSynchronizationContext = false)]
    public class TodoService:ITodoService
    {
        //List<Todo> _lstDb = new List<Todo>();
        //Nasty code for simplicity. Use dependency injection and separate the projects in real world app.
        ITodoPersistence _todoRepository;

        public TodoService()
        {
            _todoRepository = new TodoPersistence();
        }

        public Guid Add(Todo todo)
        {
            _todoRepository.Add(todo);
            return (todo.Id);
        }

        public Todo Get(Guid id)
        {
            return (_todoRepository.Get(id));
        }

        public void Delete(Guid id)
        {
            _todoRepository.Delete(id);
        }

        public List<Todo> List()
        {
            return (_todoRepository.List());
        }

        public bool Update(Todo todo)
        {
            return (_todoRepository.Update(todo));
        }
    }
}
