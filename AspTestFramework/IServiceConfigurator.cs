namespace AspTestFramework
{
    public interface IServiceConfigurator
    {
        IServiceConfigurator UseDependency<T>(T instance) where T : class;
    }
}
