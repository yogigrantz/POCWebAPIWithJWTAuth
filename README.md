# POCWebAPIWithJWTAuth
POC WebAPI with JWT Authorization and Auth Dependency Injection implementation

This solution demonstrates the initial implementation of .net 6 WebAPI that accepts a login username and password and authenticate against a static dictionary, then issue a JWT whenever the authentication is successful.

It then demonstrates how the JWT token is used in the header to pass authorization on restricted areas.

It also demonstrates the Dependency Injection implementation to inject the Authorization class into the IoC Container and shows how the controller is able to use the Action filter to authorized restricted access.
