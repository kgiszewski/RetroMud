using System;
using RetroMud.Core.Config;

namespace RetroMud.Core.Context
{
    public class GameContext : IGameContext
    {
        private IGameContextConfiguration _configuration;

        public IGameContextConfiguration Configuration
        {
            get { return _configuration ?? (_configuration = new GameContextConfiguration()); }

            set { _configuration = value; }
        }

        //this will hold the internal instance of our singleton
        private static volatile GameContext _instance;

        //this is used purely for thread safety
        private static readonly object _padLock = new Object();

        //note that the constructor is private and only the class itself can create a new instance
        private GameContext()
        {
        }

        //this is where the instance will live, notice the return value is the singleton class itself
        public static GameContext Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_padLock)
                    {
                        if (_instance == null)
                        {
                            _instance = new GameContext();
                        }
                    }
                }

                return _instance;
            }
        }
    }
}
