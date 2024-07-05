namespace LensUp.Common.Types.Events;

public abstract class EventMessage<T> : IEventMessage where T : class
{
    public Guid Id { get; private set; }

    public T Payload { get; private set; }

    public abstract string EventName { get; protected set; }

    protected EventMessage(T payload)
    {
        this.Id = Guid.NewGuid();
        this.Payload = payload;
    }
}
