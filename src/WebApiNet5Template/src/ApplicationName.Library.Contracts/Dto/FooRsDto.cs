using Newtonsoft.Json;

namespace ApplicationName.Library.Contracts.Dto
{
    /// <summary>
    /// My foo response dto
    /// </summary>
    public class FooRsDto
    {
        /// <summary>
        /// Data
        /// </summary>
        public string Data { get; set; }

        /// <summary>
        /// Colors
        /// </summary>
        public Color Colors { get; set; }

        /// <summary>
        /// Ignore property
        /// </summary>
        [JsonIgnore]
        public string IgnoreDate { get; set; }
    }
}