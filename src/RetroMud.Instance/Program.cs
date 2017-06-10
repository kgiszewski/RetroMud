using Topshelf;

namespace RetroMud.Instance
{
    class Program
    {
        static void Main(string[] args)
        {
            HostFactory.Run(configurator =>
            {
                configurator.Service<ServiceHolder>(s =>
                {
                    s.ConstructUsing(name => new ServiceHolder());
                    s.WhenStarted(tc => tc.Start());
                    s.WhenStopped(tc => tc.Stop());
                });

                configurator.RunAsLocalSystem();

                configurator.SetDescription("RetroMud Instance");
                configurator.SetDisplayName("RetroMud Instance");
                configurator.SetServiceName("RetroMud Instance");
            });
        }
    }
}
