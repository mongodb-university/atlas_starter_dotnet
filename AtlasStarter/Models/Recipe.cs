using System.Collections.Generic;
using MongoDB.Bson.Serialization.Attributes;

namespace AtlasStarter.Models;

/// <summary>
/// This Recipe class provides formal C# code structure to the data
/// that is stored in MongoDB. Using strongly-typed classes makes
/// serialization & deserializaton of your data much easier. 
/// </summary>

[BsonIgnoreExtraElements]
public class Recipe
{
    public string Name { get; set; }
    public List<string> Ingredients { get; set; }
    public int PrepTimeInMinutes { get; set; }

    public Recipe(string name, List<string> ingredients, int prepTime)
    {
        Name = name;
        Ingredients = ingredients;
        PrepTimeInMinutes = prepTime;
    }

    /// <summary>
    /// This static method is just here so we have a convenient way
    /// to generate sample recipe data.
    /// </summary>
    /// <returns>A list of Recipes</returns>       
    public static List<Recipe> GetRecipes()
    {
        return new List<Recipe>
        {
            new("elotes", new List<string>(){"corn", "mayonnaise", "cotija cheese", "sour cream", "lime" }, 35),
            new("loco moco", new List<string>(){"ground beef", "butter", "onion", "egg", "bread bun", "mushrooms" }, 54),
            new("patatas bravas", new List<string>(){"potato", "tomato", "olive oil", "onion", "garlic", "paprika" }, 80),
            new("fried rice", new List<string>(){"rice", "soy sauce", "egg", "onion", "pea", "carrot", "sesame oil" }, 40),
        };
    }
}