using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace FamilyInfo.Models {
public class Child : Person {
    [JsonPropertyName("ChildInterests")]
    public List<ChildInterest> ChildInterests { get; set; }
    [JsonPropertyName("Pets")]
    public List<Pet> Pets { get; set; }

    public Child()
    {
        ChildInterests=new List<ChildInterest>();
        Pets=new List<Pet>();
    }
    public void Update(Child toUpdate) {
        base.Update(toUpdate);
        ChildInterests = toUpdate.ChildInterests;
        Pets = toUpdate.Pets;
    }

}
}