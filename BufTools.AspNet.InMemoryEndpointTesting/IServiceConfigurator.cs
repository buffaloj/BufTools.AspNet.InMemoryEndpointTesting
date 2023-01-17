namespace BufTools.AspNet.TestFramework
{
    public interface IServiceConfigurator
    {
        IServiceConfigurator UseDependency<T>(T instance) where T : class;
    }
}
