// See https://aka.ms/new-console-template for more information
using System.Text.Json;

//
class Program
{
    const string URL = "https://gist.githubusercontent.com/christianpanton/10d65ccef9f29de3acd49d97ed423736/raw/b09563bc0c4b318132c7a738e679d4f984ef0048/kings";

    static async Task Main(string[] args)
    {
        Program program = new Program();

        List<Person>? persons = await program.GetDataAsync();
        Console.WriteLine("Number of monarchs :" + persons?.Count());

        (Person? person, int oldest) personOldest = await program.GetOldPersonAsync(persons);
        Console.WriteLine($"Oldest name: {personOldest.person?.nm} + years {personOldest.oldest}");

        var oldestByHouse = await program.GetOldPersonByHouseAsync(persons);
        foreach (var person in oldestByHouse)
            Console.WriteLine($"Oldest by house {person.Key.hse} name: {person.Key.nm} + years {person.Value}");

        var mostCommon = await program.GetMostCommonFirstNameAsync(persons);
        Console.WriteLine("The most common first name is:" + mostCommon);
    }

    #region Methods

    /// <summary>
    /// Gets data of monarchs ref the api https://gist.githubusercontent.com/christianpanton/10d65ccef9f29de3acd49d97ed423736/raw/b09563bc0c4b318132c7a738e679d4f984ef0048/kings.
    /// </summary>
    /// <returns>
    /// The <see cref="Task"/> returning a <see cref="IEnumerable{Person}"/> or returning null if the Api doesnt work.
    /// </returns>
    private async Task<List<Person>?> GetDataAsync()
    {
        List<Person>? persons = new List<Person>();
        try
        {
            using (HttpClient client = new HttpClient())
            {
                var response = await client.GetStringAsync(URL);
                persons = JsonSerializer.Deserialize<List<Person>>(response);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error in GetDataAsync method : " + ex.Message);
        }
        return persons;
    }

    /// <summary>
    /// Gets the oldest monarchs in the list <see cref="IEnumerable{Person}"/>.
    /// </summary>
    /// <param name="persons">
    /// The target list of persons.
    /// </param>
    /// <returns>
    /// The <see cref="Task"/> returning a <see cref="(Person,int)"/> or returning null if entity is not found.
    /// </returns>
    private async Task<(Person person, int oldest)> GetOldPersonAsync(List<Person>? persons)
    {
        int oldest = 0;
        Person? personOldest = new Person();
        try
        {
            foreach (Person person in persons)
            {
                var yearRange = person.yrs.Split("-");
                if (yearRange != null)
                {
                    int.TryParse(yearRange[0], out int year1);
                    if (yearRange.Length > 1)
                    {
                        int.TryParse(yearRange[1], out int year2);

                        if (year2 - year1 > oldest)
                        {
                            personOldest = person;
                            oldest = year2 - year1;
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error in method GetOldPersonAsync :" + ex.Message);
        }
        return (personOldest, oldest);
    }

    /// <summary>
    /// Gets the most commond monarchs first name.
    /// </summary>
    /// <param name="persons">
    /// The target list of persons.
    /// </param>
    /// <returns>
    /// The <see cref="Task"/> returning a <see cref="IDictionary{String, int}"/> with the house name and the  or returning null if entity is not found.
    /// </returns>
    private async Task<Dictionary<Person, int>> GetOldPersonByHouseAsync(List<Person>? persons)
    {
        Dictionary<Person, int> oldestByHouse = new Dictionary<Person, int>();
        try
        {
            var hse = persons?.GroupBy(p => p.hse);
            if (hse == null)
                return oldestByHouse;

            foreach (var house in hse)
            {
                var hou = house.Key;
                var te = house.ToList();
                var result = await GetOldPersonAsync(house.ToList());
                oldestByHouse.Add(result.person, result.oldest);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error in method GetOldPersonByHouseAsync : " + ex.Message);
        }
        return oldestByHouse;
    }

    /// <summary>
    /// Gets the most commond monarchs first name.
    /// </summary>
    /// <param name="persons">
    /// The target list of persons.
    /// </param>
    /// <returns>
    /// The <see cref="Task"/> returning a <see cref="string"/> or returning null if entity is not found.
    /// </returns>
    private async Task<string?> GetMostCommonFirstNameAsync(List<Person>? persons)
    {
        var result = string.Empty;
        if (persons == null)
            return result;
        try
        {
            var resultList = persons.GroupBy(p => p.nm.Split(" ").FirstOrDefault()).OrderByDescending(a => a.ToList().Count()).FirstOrDefault();
            result = resultList?.Key;
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error in MostCommonFirstName :" + ex.Message);
        }
        return result;
    }

    #endregion

    #region DTOs

    /// <summary>
    /// The person entity represents a single instance of a application monarchs.
    /// </summary>
    public record Person
    {
        /// <summary>
        /// Gets or sets the person's id.
        /// </summary>
        public int id { get; init; } = default!;
        /// <summary>
        /// Gets or sets the person's name.
        /// </summary>
        public string nm { get; init; } = default!;
        /// <summary>
        /// Gets or sets the person's city.
        /// </summary>
        public string cty { get; init; } = default!;
        /// <summary>
        /// Gets or sets the person's house.
        /// </summary>
        public string hse { get; init; } = default!;
        /// <summary>
        /// Gets or sets the person's years.
        /// </summary>
        public string yrs { get; init; } = default!;
    }

    #endregion

    //TODO: unit test


}

