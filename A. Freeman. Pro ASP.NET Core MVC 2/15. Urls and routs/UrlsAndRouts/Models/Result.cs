namespace Web.Models
{
    using System.Collections.Generic;

    public class Result
    {
        public string Controller { get; set; }

        public string Action { get; set; }
        
        public IDictionary<string, object> Data = new Dictionary<string, object>();
    }
}