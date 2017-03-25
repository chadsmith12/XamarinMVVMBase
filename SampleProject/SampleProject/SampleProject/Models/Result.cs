namespace SampleProject.Models
{
    /// <summary>
    /// Generic Response model.
    /// This model is used to store a response from http request. 
    /// Gives us information on the message and if the request was successful and the data the request returns.
    /// </summary>
    /// <typeparam name="T">The type you exepct the result to be from the request.</typeparam>
    public class Response<T>
    {
        /// <summary>
        /// Gets or sets the error code of the request if there was an error.
        /// This will be null if the request was succesful.
        /// </summary>
        /// <value>
        /// The error code.
        /// </value>
        public string ErrorCode { get; set; }
        /// <summary>
        /// Gets or sets the message from the response.
        /// This will be null if the request was successful.
        /// </summary>
        /// <value>
        /// The message from the response.
        /// </value>
        public string Message { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether the request was successful.
        /// </summary>
        /// <value>
        ///   <c>true</c> if successful; otherwise, <c>false</c>.
        /// </value>
        public bool Success { get; set; }
        /// <summary>
        /// Gets or sets the data returned from the request.
        /// </summary>
        /// <value>
        /// The data.
        /// </value>
        public T Data { get; set; }
    }
}
