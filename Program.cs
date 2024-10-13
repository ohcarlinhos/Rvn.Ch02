using Raven.Client.Documents;
using Rvn.Ch02;

var store = new DocumentStore()
{
    Urls = ["http://localhost:8080"],
    Database = "Tasks"
};

store.Initialize();

using (var session = store.OpenSession())
{
    var task = new ToDoTask()
    {
        DueDate = DateTime.Now.AddDays(1),
        Task = "Nada de mais"
    };
    
    session.Store(task);
    session.SaveChanges();
}

using (var session = store.OpenSession())
{
    var task = session.Load<ToDoTask>("ToDoTasks/1-A");
    task.Completed = true;
    session.SaveChanges();
}

using (var session = store.OpenSession())
{
    for (var i = 0; i < 5; i++)
    {
        session.Store(new ToDoTask
        {
            DueDate = DateTime.Today.AddDays(i),
            Task = "Take the dog for a walk"
        });
    }

    session.SaveChanges();
}


using (var session = store.OpenSession())
{
    var taskToDo =
        from t in session.Query<ToDoTask>()
        where t.DueDate >= DateTime.Today &&
              t.DueDate <= DateTime.Today.AddDays(2) &&
              t.Completed == false
        orderby t.DueDate
        select t;

    Console.WriteLine(taskToDo.ToString());

    foreach (var task in taskToDo)
    {
        Console.WriteLine($"{task.Id} - {task.Task} - {task.DueDate}");
    }
}

using (var session = store.OpenSession())
{
    var tasksPerDay =
        from t in session.Query<ToDoTask>()
        group t by t.DueDate
        into g
        select new
        {
            DueDate = g.Key,
            TasksPerDate = g.Count()
        };

    Console.WriteLine(tasksPerDay.ToString());

    foreach (var tpd in tasksPerDay)
    {
        Console.WriteLine($"{tpd.DueDate} - {tpd.TasksPerDate}");
    }
}