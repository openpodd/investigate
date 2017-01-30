using Newtonsoft.Json;

namespace Investigate
{
    public class AuthorityResult
    {
        public bool Success;
        public string ErrorMessage;

        public AuthorityItem[] Items;
    }

    [JsonObject(MemberSerialization.OptIn)]
    public class AuthorityItem
    {
        [JsonProperty("id")]
        public long Id { set; get; }

        [JsonProperty("code")]
        public string Code { set; get; }

        [JsonProperty("name")]
        public string Name { set; get; }

        [JsonProperty("description")]
        public string Description { set; get; }

        [JsonProperty("parentName")]
        public string ParentName { set; get; }
    }
}