namespace Plataforma.Admin.Models
{
    public class RetornoDataTablesModel<T>
    {
        public int Draw { get; set; }
        public int RecordsTotal { get; set; }
        public int RecordsFiltered { get; set; }
        public System.Collections.Generic.IEnumerable<T> Data { get; set; }
        public string Error { get; set; }
    }
}