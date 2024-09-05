namespace CrmWindowsService.ApiRequests.Models
{
    public class GeneralResponse<T>
    {
        public string IndicadorExito { get; set; }
        public string DescripcionError { get; set; }
        public T Detail { get; set; }
    }
}

