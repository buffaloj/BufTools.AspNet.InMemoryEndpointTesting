namespace BufTools.AspNet.TestFramework
{
    /// <summary>
    /// Interface used to configure services
    /// </summary>
    public interface IServiceConfigurator
    {
        /// <summary>
        /// Replaces an existing dependency in favor of another
        /// </summary>
        /// <typeparam name="T">The type of dependency to replace</typeparam>
        /// <param name="instance">The instance of the type to replace with</param>
        /// <returns>A <see cref="IServiceConfigurator"/> instance for use in chaining</returns>
        IServiceConfigurator UseDependency<T>(T instance) where T : class;
    }
}
