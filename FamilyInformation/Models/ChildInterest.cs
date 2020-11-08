using System.Text.Json.Serialization;

namespace FamilyInformation.Models {
public class ChildInterest {
    [JsonPropertyName("Id")]
    public int ChildId { get; set; }
    [JsonIgnore]
    public Child Child { get; set; }
    [JsonPropertyName("InterestId")]
    public string InterestId { get; set; }
    [JsonIgnore]
    public Interest Interest { get; set; }

    public override bool Equals(object? obj) {
        ChildInterest ci = ((ChildInterest) obj);
        if (ci.Child.Equals(Child) && ci.Interest.Equals(Interest)) return true;
        return base.Equals(obj);
    }
}
}