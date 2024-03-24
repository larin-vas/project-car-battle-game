public interface IMovableInput
{
    public float Movement { get; }
    public float Rotation { get; }
    public bool Handbrake { get; }
    public bool Brake { get; }
}