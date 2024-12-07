namespace Talabat.APIS.Errors
{
    public class ApiValidtionErorrResponse:ApiResponse
    {
        public IEnumerable<string> Errors { get; set; }
        public ApiValidtionErorrResponse():base(400)
        {
            Errors = new List<string>();
        }
    }
}
