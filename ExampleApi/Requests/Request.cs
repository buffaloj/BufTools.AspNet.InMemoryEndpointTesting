namespace ExampleApi.Requests
{
    public class Request
    {
        public string StringToReturn {  get; set; }

        public static Request Example()
        {
            return new Request
            {
                StringToReturn = "An example request string"
            };
        }
    }
}
