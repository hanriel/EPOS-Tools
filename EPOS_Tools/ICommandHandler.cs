//основной поток приложения
internal interface ICommandHandler
{
    void CommandWorker (string[] args);
}