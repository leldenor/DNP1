using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using FamilyAPI.Models;

namespace FamilyAPI.Data {
public class FileContext :IFamilyService {
    public IList<Family> Families { get; private set; }
    public IList<Adult> Adults { get; private set; }
    

    private readonly string familiesFile = "families.json";
    private readonly string adultsFile = "adults.json";

    public FileContext() {
        Families = File.Exists(familiesFile) ? ReadData<Family>(familiesFile) : new List<Family>();
        Adults = File.Exists(adultsFile) ? ReadData<Adult>(adultsFile) : new List<Adult>();
    }

    private IList<T> ReadData<T>(string s) {
        using (var jsonReader = File.OpenText(s)) {
            return JsonSerializer.Deserialize<List<T>>(jsonReader.ReadToEnd());
        }
    }

    public void SaveChanges() {
        // storing families
        string jsonFamilies = JsonSerializer.Serialize(Families, new JsonSerializerOptions {
            WriteIndented = true
        });

        using (StreamWriter outputFile = new StreamWriter(familiesFile, false)) {
            outputFile.Write(jsonFamilies);
        }

        // storing persons
        string jsonAdults = JsonSerializer.Serialize(Adults, new JsonSerializerOptions {
            WriteIndented = true
        });

        using (StreamWriter outputFile = new StreamWriter(adultsFile, false)) {
            outputFile.Write(jsonAdults);
        }
    }
    
    public async Task<IList<Family>> GetFamilies()
    {
        List<Family> tmp=new List<Family>(Families);
        return tmp;
    }
    
    public async Task RemoveFamily(int HouseNumber, string StreetName)
    {
        Family toRemove = Families.First(a => a.HouseNumber ==HouseNumber&&a.StreetName.Equals(StreetName));
        Families.Remove(toRemove);
        SaveChanges();
    }

    public async Task<Family> AddFamily(Family family)
    {
        Families.Add(family);
        SaveChanges();
        return family;
    }

    public async Task UpdateAsync(Family family)
    {
        for (int i = 0; i < Families.Count; i++)
        {
            if (Families[i].StreetName.Equals(family.StreetName)&Families[i].HouseNumber==family.HouseNumber)
            {
                Families[i] = family;
            }
        }
        SaveChanges();
        
    }
}
}