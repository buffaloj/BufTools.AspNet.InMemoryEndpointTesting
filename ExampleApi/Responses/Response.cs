namespace ExampleApi.Responses
{
    /// <summary>
    /// A response returned by some endpoints
    /// </summary>
    public class Response
    {
        /// <summary>
        /// A string being returned by an endpoint
        /// </summary>
        public string? ReturnString { get; set; }

        /// <summary>
        /// If a string param was passed in to an endpoint, this var echos it back to the caller
        /// </summary>
        public string? StringParam { get; set; }

        /// <summary>
        /// If a int param was passed in to an endpoint, this var echos it back to the caller
        /// </summary>
        public int? IntParam { get; set; }

        /// <summary>
        /// If a float param was passed in to an endpoint, this var echos it back to the caller
        /// </summary>
        public float? FloatParam { get; set; }
    }
}
