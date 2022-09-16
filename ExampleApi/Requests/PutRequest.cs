namespace ExampleApi.Requests
{
    public class PutRequest
    {
        public string StringToReturn {  get; set; }

        public static PutRequest Example()
        {
            return new PutRequest
            {
                StringToReturn = "An example request string"
            };
        }
    }
}
