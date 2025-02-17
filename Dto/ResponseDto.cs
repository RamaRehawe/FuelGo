namespace FuelGo.Dto
{
    public class ResponseDto<T>
    {
        public int StatusCode { get; set; }
        public T Data { get; set; }
    }
}
