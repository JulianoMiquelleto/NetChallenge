namespace NetChallenge.Extension.Helpers;

public class TasksException:Exception
{
    public TasksException(string message = "Erro ao criar task")
        : base(message)
    {
    }
}
public class TasksNotFoundException : Exception
{
    public TasksNotFoundException(string message = "Task não encontrada")
        : base(message)
    {
    }
}

public class TasksExceedNumberTasksException : Exception
{
    public TasksExceedNumberTasksException(string message = "Limite máximo de 20 tasks para o projeto")
        : base(message)
    {
    }
}