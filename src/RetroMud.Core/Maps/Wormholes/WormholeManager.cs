using System.Collections.Generic;
using System.Linq;
using RetroMud.Core.Maps.Coordinates;

namespace RetroMud.Core.Maps.Wormholes
{
    public class WormholeManager : IWormholeManager
    {
        private List<WormholePortalMap> _portalMap = new List<WormholePortalMap>
        {
            new WormholePortalMap
            {
                From = new WormholePortal
                {
                    MapId = 1,
                    Position = new MapCoordinate(17, 65)
                },
                To = new WormholePortal
                {
                    MapId = 2,
                    Position = new MapCoordinate(27, 58)
                }
            },
            new WormholePortalMap
            {
                To = new WormholePortal
                {
                    MapId = 1,
                    Position = new MapCoordinate(17, 64)
                },
                From = new WormholePortal
                {
                    MapId = 2,
                    Position = new MapCoordinate(27, 57)
                }
            }
        };

        public IWormholePortal RouteFrom(IWormholePortal portal)
        {
            var mapping = _portalMap.FirstOrDefault(x => 
                x.From.MapId == portal.MapId 
                && x.From.Position.Row == portal.Position.Row
                && x.From.Position.Column == portal.Position.Column
            );

            return mapping?.To;
        }
    }
}
