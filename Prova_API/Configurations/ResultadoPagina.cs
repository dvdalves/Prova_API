namespace Prova_API.Configurations
{
    public class ResultadoPagina<T>
    {
        public int TotalItems { get; set; }
        public IEnumerable<T> Items { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
        public int TotalPages => (int)Math.Ceiling((double)TotalItems / PageSize);
    }
}
