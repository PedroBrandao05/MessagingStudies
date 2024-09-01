namespace Primitives;

public abstract class RequestPayload<TPayload>
{
  public TPayload Payload { get; }

  public RequestPayload(TPayload payload)
  {
    Payload = payload;
    
    Validate();
  }

  public abstract void Validate();
}

public abstract class RequestPayload<TPayload, TEntityId>
{
  public TPayload Payload { get; }
  
  public TEntityId EntityId { get; }
  
  public RequestPayload(TPayload payload, TEntityId entityId)
  {
    Payload = payload;
    EntityId = entityId;
    
    Validate();
  }

  public abstract void Validate();
}