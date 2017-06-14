﻿using RetroMud.Core.Events.Helpers;
using RetroMud.Core.Logging;

namespace RetroMud.Core.Collision
{
    public class CollisionDetectionEventHandler : IRegisterEvents
    {
        public void Register()
        {
            CollisionDetector.Instance().OnCollision += CollisionDetectionEventHandler_OnCollision;
        }

        private void CollisionDetectionEventHandler_OnCollision(object sending, CollisionDetectedEventArgs e)
        {
            Logger.Debug<CollisionDetectedHandler>($"The player has encountered a: {e.Character} character at position: {e.Column}, {e.Row}");
        }
    }
}
