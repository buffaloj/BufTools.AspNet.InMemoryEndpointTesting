# AspNet In-Memory Endpoint Testing

This lets you easily startup your ASP WebApi in-memory from a unit test, call endpoints, and get a response back to Assert on. 

It allows injecting mocks, is ultra fast, and all with two lines of code!

example:
```cs
var browser = new Browser<Program>(c =>
            {
                c.UseDependency(_userServiceMock.Object);
                c.UseDependency(_validationMock.Object);
            });

var result = await browser.CreateRequest("/api/v1/example")
                          .WithQueryParam("string_param", myString)
                          .WithQueryParam("int_param", myInt)
                          .GetAsync();
```

# Getting Started

The general approach is to:
1. Create an instance of a Browser class with any mocks to inject.
2. Use the browser instance to build HTTP requests using fluent syntax and send them

## Creating a browser

1. When new'ing up a browser, you need to choose the program type that starts the application. 

Options are:
- Program : choose this if not using a Startup.cs file
- Startup : chose this if using a Startup.cs file

2. Inject dependencies

Use the configurator to inject classes in place of those thave have already been registered to act as replacements.

## Example
Example usage with Progam as the type and two mocks injected
```cs
var browser = new Browser<Program>(c =>
            {
                c.UseDependency(_userServiceMock.Object);
                c.UseDependency(_validationMock.Object);
            });
```

# Call an Endpoint

1. To call an endpoint, start with CreateRequest and provide the route to the endpoint with no base url
2. Use WithQueryParam to add any params that get appended to the route with a ?
3. Call GetAsync (or Put, Post, Delete) to send the request and get a response back

```cs
var result = await browser.CreateRequest("/api/v1/example")
                          .WithQueryParam("string_param", myString)
                          .WithQueryParam("int_param", myInt)
                          .GetAsync();
```						