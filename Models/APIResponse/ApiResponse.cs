namespace StudentsManagerMW.Models.APIResponse
{
    public class ApiResponse<T> where T : class
    {
        public bool IsSuccess { get; set; }
        public T Result { get; set; }
        public ApiError Error { get; set; }
    }

    public class ApiError
    {
        public int Code { get; set; }
        public string Description { get; set; }
    }
}
