using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using API.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class SeedData
    {
        public static async Task SeedDataAsync(DataContext context)
        {
            // Check if there are any users in the database, if so, exit the method
            if (await context.Users.AnyAsync()) return;

            // Read the user seed data from the JSON file
            var userData = await File.ReadAllTextAsync("Data/UserSeedData.json");

            // Configure JSON serializer options to ignore case sensitivity for property names
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            // Deserialize the JSON data into a list of AppUser objects
            var users = JsonSerializer.Deserialize<List<AppUser>>(userData, options);

            // If no users were deserialized, exit the method
            if (users == null) return;

            // Iterate through each user in the list
            foreach (var user in users)
            {
                // Create a new HMACSHA512 instance for password hashing
                using var hmac = new HMACSHA512();

                // Compute the password hash using a predefined password
                user.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes("Pa$$w0rd"));

                // Store the HMAC key as the password salt
                user.PasswordSalt = hmac.Key;

                // Convert the username to lowercase for consistency
                user.UserName = user.UserName.ToLower();

                // Add the user to the database context
                context.Users.Add(user);
            }

            // Save all changes to the database
            await context.SaveChangesAsync();
        }
    }
}