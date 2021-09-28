namespace GraphQLSample.Core.Application.Services.BaseViewModels
{
    public class ApiIdViewModel<TKey>
    {
        public ApiIdViewModel(TKey id)
        {
            Id = id;
        }

        public TKey Id { get; set; }
    }
}
