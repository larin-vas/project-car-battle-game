namespace Code.Map
{
    public interface IMapGenerator
    {
        public float GenerationProgress { get; }

        public void Generate();
    }
}
