namespace GraphQLSample.Core.Application.Services.GraphTypes
{
    public class GraphQLPayload<T> 
    {
        public T Payload { get; }

        public GraphQLPayload(T payload)
        {
            Payload = payload;
        }
    }
}
