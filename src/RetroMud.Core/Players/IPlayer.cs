namespace RetroMud.Core.Players
{
    public interface IPlayer
    {
        int Id { get; set; }
        int CurrentRow { get; set; }
        int CurrentColumn { get; set; }
        int Gold { get; set; }
    }
}
