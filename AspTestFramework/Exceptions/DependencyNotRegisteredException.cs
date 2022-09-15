using System;

namespace AspTestFramework.Exceptions
{
    public  class DependencyNotRegisteredException : Exception
    {
        public DependencyNotRegisteredException(string message) : base(message)
        {
        }
    }
}
