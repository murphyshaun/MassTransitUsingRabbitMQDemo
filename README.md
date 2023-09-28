https://www.c-sharpcorner.com/article/getting-started-with-masstransit-and-rabbitmq/

Consumer Code Sample
With the producer code in place, it is time to concentrate on the consumer application. 
The consumer application would read any messages from the designated queue and process them depending on the message types. 
Each message type that needs to be processed by the consumer application would require an implementation of `IConsumer<TMessageType>`. 
This would trigger the required actions that need to happen when a message of the particular type arrives.
```
public interface IConsumer < in TMessage > : IConsumer
where TMessage: class {
    Task Consume(ConsumeContext < TMessage > context);
}
public interface IConsumer {}
```