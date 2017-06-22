using System.Collections.Generic;
using System.Text;
using RetroMud.Core.Maps.CharacterColors;
using RetroMud.Core.Maps.Coordinates;

namespace RetroMud.Core.Maps
{
    public class Map : IMap
    {
        public int Id { get; set; }
        public int Width => Buffer[0].Length;
        public int Height => Buffer.Length;
        public string[] Buffer { get; set; }
        public List<ICharacterColor> CharacterColors { get; set; }
        public void UpdateBufferAtPosition(IMapCoordinate position, char character)
        {
            var sb = new StringBuilder();

            sb = new StringBuilder(Buffer[position.Row]) { [position.Column] = character };

            Buffer[position.Row] = sb.ToString();
        }
    }
}
