namespace ExampleApi.Requests
{
    public class Request
    {
        /// <summary>
        /// A string that was to be returned via an endpoint
        /// </summary>
        /// <example>Example String!</example>
        public string StringToReturn {  get; set; }

        /// <summary>
        /// Provides an example of a <see cref="Request"/> with properties with in with example values
        /// </summary>
        /// <returns>A instance of a <see cref="Request"/></returns>
        public static Request Example()
        {
            return new Request
            {
                StringToReturn = "An example request string"
            };
        }
    }
}
