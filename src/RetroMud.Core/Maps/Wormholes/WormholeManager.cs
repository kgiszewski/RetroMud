using System;
using System.Collections.Generic;
using System.Linq;

namespace RetroMud.Core.Maps.Wormholes
{
    public class WormholeManager : IWormholeManager
    {
        private readonly IEnumerable<IWormholePortalMap> _allPortalMaps;

        public WormholeManager()
        {
            if (_allPortalMaps == null)
            {
                _allPortalMaps = Context.ClientContext.Instance.MapManager.GetAllMaps().SelectMany(x => x.WormholePortalMaps);
            }
        }

        public IWormholePortal RouteFrom(IWormholePortal portal)
        {
            var mapping = _allPortalMaps.FirstOrDefault(x => 
                x.From.MapId == portal.MapId 
                && x.From.Position.Row == portal.Position.Row
                && x.From.Position.Column == portal.Position.Column
            );

            if (mapping == null)
            {
                throw new Exception($"Could not find a wormhold mapping for portal from mapId: {portal.MapId}, row: {portal.Position.Row}, column: {portal.Position.Column}");
            }

            return mapping?.To;
        }
    }
}
