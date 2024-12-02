I was trying to use DDD layers and DI.

I am using here default instance of rabbitMq on default ports. 

This is just a simple code, that wraps around Rabbit Mq. If we consider this as a normal project, then there is a lot missing. Like error handlings, settings, loggig, unit tests, integration tests, maybe a docker support for rabbit mq server side. 

If you want to run it:
- ensure you have rabbitMq installed, their documentation offers to use docker, but i had it installed locally, so did not try this way
- set multiple startup projects (or just build it and run executables)
- use host to send messages and client to receive them
