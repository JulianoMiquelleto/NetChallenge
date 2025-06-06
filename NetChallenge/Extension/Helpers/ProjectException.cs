namespace NetChallenge.Extension.Helpers;

public class ProjectCreateException:Exception
{
    public ProjectCreateException(string message = "Erro ao criar projeto")
        : base(message)
    {
    }
}
public class ProjectNotFoundException : Exception
{
    public ProjectNotFoundException(string message = "Projeto não encontrado")
        : base(message)
    {
    }
}
public class ProjectTaskException : Exception
{
    public ProjectTaskException(string message = "Tasks pendentes para o projeto, favor finalizar ou excluir.")
        : base(message)
    {
    }
}