namespace ExcellenceQuest.API.Helper
{
    using System.Collections.Generic;

    public enum ApiStatusCode
    {
        Success=200,
        Bad_Request=400,
        Internal_Server_Error
    }
    public class ConstMsg
    {
      
        public static string NoData = "No Records Found";
        public static string Saved = "Data Saved Successfully";
        public static string Deleted = "Data Deleted Successfully";
        public static string InvalidData = "Data is not valid";
        public static string SomethingWrong = "Something went wrong.try after sometime";
    }
    public class ApiResponse<T>
    {
        public T ResponseData { get; set; }
        public ApiStatusCode StatusCode { get; set; }
        public string Message { get; set; } = string.Empty;
        public string ErrorMessage { get; set; } = string.Empty;

        public static ApiResponse<T> BadRequest(string errorMessage)
        {
            return new ApiResponse<T> { StatusCode = ApiStatusCode.Bad_Request, ErrorMessage = errorMessage };
        }
        public static ApiResponse<T> Exception(string errorMessage)
        {
            return new ApiResponse<T> { StatusCode = ApiStatusCode.Internal_Server_Error, ErrorMessage = errorMessage };
        }

        public static ApiResponse<T> Success(string message)
        {
            return new ApiResponse<T> { StatusCode = ApiStatusCode.Success, Message = message };
        }

        public static ApiResponse<T> Success(T data, string message)
        {
            return new ApiResponse<T> { StatusCode = ApiStatusCode.Success, ResponseData = data, Message = message };
        }

        public static ApiResponse<T> Success(T data)
        {
            return new ApiResponse<T> { StatusCode = ApiStatusCode.Success, ResponseData = data };
        }
    }
}