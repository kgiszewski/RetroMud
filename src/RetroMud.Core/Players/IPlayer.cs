namespace RetroMud.Core.Players
{
    public interface IPlayer
    {
        int CurrentRow { get; set; }
        int CurrentColumn { get; set; }
    }
}
