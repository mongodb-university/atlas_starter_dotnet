using System;
using System.Collections.Generic;
using MongoDB.Driver;

namespace AtlasStarter
{
    class MainClassNoPrompt
    {
        public static void Main(string[] args)
        {
            // TODO:
            // Replace the placeholder connection string below with your
            // Altas cluster specifics. Be sure it includes
            // a valid username and password! Note that in a production environment,
            // you do not want to store your password in plain-text here.

            var mongoUri = "<Your Atlas Connection String>";

            // The IMongoClient is the object that defines the connection to our
            // datastore (Atlas, for example)
            IMongoClient client;

            // An IMongoCollection defines a connection to a specific MongoDB
            // collection. Your app may have one or many different IMongoCollection
            // objects.
            IMongoCollection<Recipe> collection;

            // Note that you must define the *type* of data stored in the
            // IMongoCollection. We have created a class called Recipe at
            // the bottom of this file that serves as a "mapping class" -- the 
            // driver maps the C# class to the BSON stored in MongoDB.

            // Using mapping classes is strongly advised, but if you
            // don't create them, you can always use the more generic BsonDocument
            // type.

            try
            {
                client = new MongoClient(mongoUri);
            }
            catch (Exception e)
            {
                Console.WriteLine("There was a problem connecting to your " +
                    "Atlas cluster. Check that the URI includes a valid " +
                    "username and password, and that your IP address is " +
                    $"in the Access List. Message: {e.Message}");
                Console.WriteLine(e);
                Console.WriteLine();
                return;
            }

            // Provide the name of the database and collection you want to use.
            // If they don't already exist, the driver and Atlas will create them
            // automatically when you first write data.
            var dbName = "myDatabase";
            var collectionName = "recipes";

            collection = client.GetDatabase(dbName)
               .GetCollection<Recipe>(collectionName);

            /*      *** INSERT DOCUMENTS ***
             * 
             * You can insert individual documents using collection.Insert(). 
             * In this example, we're going to create 4 documents and then 
             * insert them all in one call with InsertMany().
             */

            var docs = Recipe.GetRecipes();

            try
            {
                collection.InsertMany(docs);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Something went wrong trying to insert the new documents." +
                    $" Message: {e.Message}");
                Console.WriteLine(e);
                Console.WriteLine();
                return;
            }

            /*      *** FIND DOCUMENTS ***
             * 
             * Now that we have data in Atlas, we can read it. To retrieve all of
             * the data in a collection, we call Find() with an empty filter. 
             * The Builders class is very helpful when building complex 
             * filters, and is used here to show its most basic use.
             */

            var allDocs = collection.Find(Builders<Recipe>.Filter.Empty)
                .ToList();

            foreach (Recipe recipe in allDocs)
            {
                Console.WriteLine($"{recipe.Name} has {recipe.Ingredients.Count} ingredients " +
                    $"and takes {recipe.PrepTimeInMinutes} minutes to make");
                Console.WriteLine();
            }

            // We can also find a single document. Let's find the first document
            // that has the string "potato" in the Ingredients list. Again we
            // use the Builders class to create the filter, and a LINQ
            // statement to define the property and value we're after:

            var findFilter = Builders<Recipe>
                .Filter.AnyEq(t => t.Ingredients,
                "potato");

            var findResult = collection.Find(findFilter).FirstOrDefault();

            if (findResult == null)
            {
                Console.WriteLine(
                    "I didn't find any recipes that contain 'potato' as an ingredient.");
                Console.WriteLine();
                return;
            }
            Console.WriteLine("We've retrieved the document:");
            Console.WriteLine(findResult.ToString());
            Console.WriteLine();
            
            /*      *** UPDATE A DOCUMENT ***
             * 
             * You can update a single document or multiple documents in a single call.
             * 
             * Here we update the PrepTimeInMinutes value on the document we 
             * just found.
             */

            var updateFilter = Builders<Recipe>.Update.Set(t => t.PrepTimeInMinutes, 72);

            // The following FindOneAndUpdateOptions specify that we want the *updated* document
            // to be returned to us. By default, we get the document as it was *before*
            // the update.

            var options = new FindOneAndUpdateOptions<Recipe, Recipe>()
            {
                ReturnDocument = ReturnDocument.After
            };

            // The updatedDocument object is a Recipe object that reflects the
            // changes we just made.
            var updatedDocument = collection.FindOneAndUpdate(findFilter,
                updateFilter, options);

            Console.WriteLine("Here's the updated document:");
            Console.WriteLine(updatedDocument.ToString());
            Console.WriteLine();
            /*      *** DELETE DOCUMENTS ***
             *      
             *      As with other CRUD methods, you can delete a single document 
             *      or all documents that match a specified filter. To delete all 
             *      of the documents in a collection, pass an empty filter to 
             *      the DeleteMany() method. In this example, we'll delete 2 of 
             *      the recipes.
             */

            var deleteResult = collection
                .DeleteMany(Builders<Recipe>.Filter.In(r => r.Name, new string[] { "elotes", "fried rice" }));

            Console.WriteLine($"I deleted {deleteResult.DeletedCount} records.");

            Console.Read();
        }
    }

    /// <summary>
    /// This Recipe class provides formal C# code structure to the data
    /// that is stored in MongoDB. Using strongly-typed classes makes
    /// serialization & deserializaton of your data much easier. 
    /// </summary>
  
    public class Recipe
    {
        public string Name { get; set; }
        public List<string> Ingredients { get; set; }
        public int PrepTimeInMinutes { get; set; }

        public Recipe(string name, List<string> ingredients, int prepTime)
        {
            this.Name = name;
            this.Ingredients = ingredients;
            this.PrepTimeInMinutes = prepTime;
        }

        /// <summary>
        /// This static method is just here so we have a convenient way
        /// to generate sample recipe data.
        /// </summary>
        /// <returns>A list of Recipes</returns>       
        public static List<Recipe> GetRecipes()
        {
            return new List<Recipe>()
            {
                new Recipe("elotes", new List<string>(){"corn", "mayonnaise", "cotija cheese", "sour cream", "lime" }, 35),
                new Recipe("loco moco", new List<string>(){"ground beef", "butter", "onion", "egg", "bread bun", "mushrooms" }, 54),
                new Recipe("patatas bravas", new List<string>(){"potato", "tomato", "olive oil", "onion", "garlic", "paprika" }, 80),
                new Recipe("fried rice", new List<string>(){"rice", "soy sauce", "egg", "onion", "pea", "carrot", "sesame oil" }, 40),
            };
        }
    }
}


