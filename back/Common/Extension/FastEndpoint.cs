namespace FastEndpoints;

public abstract class FastEndpoint<TRequest, TResponse> : Endpoint<TRequest, TResponse>
    where TRequest : notnull
{
    protected void PermissionsByEndpointName()
        => Permissions(GetType().Name);
}

public abstract class FastEndpoint<TRequest>: Endpoint<TRequest> where TRequest : notnull
{
    protected void PermissionsByEndpointName()
        => Permissions(GetType().Name);
}

public abstract class FastEndpointWithoutRequest : EndpointWithoutRequest
{
    protected void PermissionsByEndpointName()
        => Permissions(GetType().Name);
}

public abstract class FastEndpointWithoutRequest<TResponse> : EndpointWithoutRequest<TResponse>
{
    protected void PermissionsByEndpointName()
        => Permissions(GetType().Name);
}


