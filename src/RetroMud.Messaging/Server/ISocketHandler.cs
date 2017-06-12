namespace RetroMud.Messaging.Server
{
    public interface ISocketHandler
    {
        void StartWorker();
        void Cleanup();
        bool Completed { get; set; }
    }
}
