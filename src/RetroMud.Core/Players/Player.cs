namespace RetroMud.Core.Players
{
    public class Player : IPlayer
    {
        public int Id { get; set; }
        public int CurrentRow { get; set; }
        public int CurrentColumn { get; set; }
    }
}
