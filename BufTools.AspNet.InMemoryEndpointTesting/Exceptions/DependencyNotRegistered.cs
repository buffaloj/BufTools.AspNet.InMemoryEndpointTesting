using System;

namespace BufTools.AspNet.TestFramework
{
    /// <summary>
    /// This exception is thrown when a dependency that is passed in to a <see cref="Browser{TProgram}"/> is not found in the application DI container
    /// </summary>
    public  class DependencyNotRegistered : Exception
    {
        /// <summary>
        /// Constructs an instance of an object
        /// </summary>
        /// <param name="message">A message to include with the exception</param>
        public DependencyNotRegistered(string message) : base(message)
        {
        }
    }
}
