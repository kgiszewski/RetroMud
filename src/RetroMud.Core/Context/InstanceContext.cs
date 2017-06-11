﻿using System;
using RetroMud.Core.Config;
using RetroMud.Core.Events.Helpers;
using RetroMud.Core.Events.Instance;

namespace RetroMud.Core.Context
{
    public class InstanceContext : IInstanceContext, IInstanceContextEvents
    {
        public event InstanceStartHandler OnInstanceStart;

        public void Start()
        {
            EventHelper.RegisterAllEventHandlers();

            OnInstanceStart?.Invoke(this, new EventArgs());
        }

        private IInstanceConfiguration _configuration;

        public IInstanceConfiguration Configuration
        {
            get { return _configuration ?? (_configuration = new InstanceConfiguration()); }

            set { _configuration = value; }
        }

        //this will hold the internal instance of our singleton
        private static volatile InstanceContext _instance;

        //this is used purely for thread safety
        private static readonly object _padLock = new Object();

        //you can create as many properties as you'd like
        public string Name => _configuration.Name;

        //note that the constructor is private and only the class itself can create a new instance
        private InstanceContext()
        {
        }

        //this is where the instance will live, notice the return value is the singleton class itself
        public static InstanceContext Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_padLock)
                    {
                        if (_instance == null)
                        {
                            _instance = new InstanceContext();
                        }
                    }
                }

                return _instance;
            }
        }
    }
}
