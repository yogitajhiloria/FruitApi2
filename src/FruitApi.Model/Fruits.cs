using System.Text.Json.Serialization;

namespace FruitApi.Model;
public class Fruits
{
    public string Name { get; set; } 
    public string Family { get; set; } 
    public string Genus { get; set; } 
    public string Order { get; set; } 
    public int Id { get; set; } 
    public Nutrition Nutritions { get; set; }

}
