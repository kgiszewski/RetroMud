using System;
using System.Linq;

namespace RetroMud.Core.Maps.Wormholes
{
    public class WormholeManager : IWormholeManager
    {
        public IWormholePortal RouteFrom(IWormholePortal portal)
        {
            var allPortalMaps = Context.ClientContext.Instance.MapManager.GetAllMaps().SelectMany(x => x.WormholePortalMaps);

            var mapping = allPortalMaps.FirstOrDefault(x => 
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
