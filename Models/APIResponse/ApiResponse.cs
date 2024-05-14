namespace StudentsManagerMW.Models.APIResponse
{
    public class ApiResponse<T> where T : class
    {
        public bool IsSuccess { get; set; }
        public T Result { get; set; }
        public ApiError Error { get; set; }

        public ApiResponse()
        {

        }
        public ApiResponse(T res)
        {
            this.Result = res;
        }
    }

    public class ApiError
    {
        public int Code { get; set; }
        public string Description { get; set; }
    }
}
