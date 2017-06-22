using System.Collections.Generic;

namespace RetroMud.Core.Maps.Managers
{
    public interface IMapManager
    {
        void SaveAsAltered(IMap map);
        IEnumerable<IMap> GetAllMaps();
        IMap GetById(int mapId);
    }
}