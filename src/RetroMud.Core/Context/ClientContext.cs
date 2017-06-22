using System.Collections.Generic;
using RetroMud.Core.Config;
using RetroMud.Core.Maps.Managers;
using RetroMud.Core.Players;
using RetroMud.Core.Scenes;
using RetroMud.Core.Status;

namespace RetroMud.Core.Context
{
    public class ClientContext : IClientContext
    {
        private IGameContextConfiguration _configuration;

        public IGameContextConfiguration Configuration
        {
            get { return _configuration ?? (_configuration = new GameContextConfiguration()); }

            set { _configuration = value; }
        }

        //this will hold the internal instance of our singleton
        private static volatile ClientContext _instance;

        //this is used purely for thread safety
        private static readonly object _padLock = new object();

        public List<IStatusMessage> StatusMessages { get; set; } = new List<IStatusMessage>();

        //note that the constructor is private and only the class itself can create a new instance
        private ClientContext()
        {
        }

        //this is where the instance will live, notice the return value is the singleton class itself
        public static ClientContext Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_padLock)
                    {
                        if (_instance == null)
                        {
                            _instance = new ClientContext();
                        }
                    }
                }

                return _instance;
            }
        }

        public IGameSceneManager GameSceneManager { get; set; }
        public IMapManager MapManager { get; set; }
        public IPlayer Player { get; set; }
        public IStatusMessageManager StatusMessageManager { get; set; }
    }
}
