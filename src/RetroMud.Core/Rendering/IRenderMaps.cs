﻿using System.Collections.Generic;
using RetroMud.Core.Maps;

namespace RetroMud.Core.Rendering
{
    public interface IRenderMaps
    {
        void RenderMap(IMap map, IEnumerable<string> statusMessages);
    }
}
