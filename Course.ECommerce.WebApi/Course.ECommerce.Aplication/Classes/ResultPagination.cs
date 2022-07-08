namespace Course.ECommerce.Aplication.Classes
{
    public class ResultPagination<T>
    {
        public int Total { get; set; }
        public ICollection<T> Items { get; set; } = new List<T>();
    }
}