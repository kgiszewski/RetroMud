using System.Collections.Generic;
using System.Linq;

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
                    Row =  17,
                    Column = 65
                },
                To = new WormholePortal
                {
                    MapId = 2,
                    Row = 27,
                    Column = 58
                }
            },
            new WormholePortalMap
            {
                To = new WormholePortal
                {
                    MapId = 1,
                    Row =  17,
                    Column = 64
                },
                From = new WormholePortal
                {
                    MapId = 2,
                    Row = 27,
                    Column = 57
                }
            }
        };

        public IWormholePortal RouteFrom(IWormholePortal portal)
        {
            var mapping = _portalMap.FirstOrDefault(x => 
                x.From.MapId == portal.MapId 
                && x.From.Column == portal.Column
                && x.From.Row == portal.Row
            );

            return mapping?.To;
        }
    }
}
