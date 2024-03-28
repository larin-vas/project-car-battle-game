namespace Code.Common.Interfaces
{
    public interface IHealthProvider
    {
        public float GetCurrentHealth();
        public void RestoreHealth();
    }
}
