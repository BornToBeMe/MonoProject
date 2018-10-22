using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project.WebAPI.RestModels
{
    /// <summary>
    /// Gets or sets the calls for SelectAll.
    /// </summary>
    /// <value>Sort, Filter, Page, PageSize, Ascending.</value>
    public class CallDetails
    {
        [JsonProperty("sortBy")]
        public string Sort { get; set; }
        [JsonProperty("currentFilter")]
        public string Filter { get; set; }
        [JsonProperty("page")]
        public int Page { get; set; }
        [JsonProperty("pageSize")]
        public int PageSize { get; set; }
        [JsonProperty("ascending")]
        public bool Ascending { get; set; }
    }
}