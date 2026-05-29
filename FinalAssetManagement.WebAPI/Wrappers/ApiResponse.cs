namespace FinalAssetManagement.WebAPI.Wrappers
{
    public class ApiResponse<T> // T is Type of Response
    {
        public T? Data { get; set; }
        public bool Succeeded { get; set; }
        public string? Message { get; set; }
        public List<string>? Errors { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ApiResponse{T}"/> class.
        /// Required for serialization.
        /// </summary>
        public ApiResponse()
        {
        }


        public ApiResponse(T? data, string? message, bool succeeded)
        {
            Data = data;
            Message = message;
            Succeeded = succeeded;
        }
    }
}
