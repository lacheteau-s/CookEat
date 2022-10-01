namespace CookEat.Data
{
    public interface IDatabaseManager
    {
        int ExpectedSchemaVersion { get; }
    }
}
