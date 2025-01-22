// Här skapar vi en lista med användare som innehåller 1000 slumpmässiga 
// användare var. Vi kommer använda denna lista för att utföra LINQ-övningar på dem.
List<User> users1 = User.GetRandomListOfUsers(1000);

// FILTERING

// 1a. Använd Where() för att sortera ut alla användare i listan som kommer från exempelvis "Sweden".
// 1b. Skriv ut dem i konsolen med users1.Foreach(). Fortsätt skriva ut resultatet på liknande säät
// i kommande övningar också.
var swedishUsers = users1.Where(u => u.Country == "Sweden").ToList();

// 2. Använd Where() för att hitta alla användare vars efternamn börjar på "S".
var usersWithS = users1.Where(u => u.LastName.StartsWith("S")).ToList();

// 2b. Använd Where() för att hitta alla användare som loggat in den senaste veckan
var recentUsers = users1.Where(u => u.LastLogin > DateTime.Now - TimeSpan.FromDays(7)).ToList();

// SORTING

// 3. Använd OrderBy() för att sortera användarna i listan efter deras förnamn.
var orderedUsers = users1.OrderBy(u => u.FirstName).ToList();

// 4. Använd OrderByDescending() och FirstOrDefault() för att hitta den användare som har mest DataStored.
var topUser = users1.OrderByDescending(u => u.DataStored).FirstOrDefault();

// 5. Använd OrderBy() och ThenBy() för att sortera användare efter land och sedan efter efternamn.
var sortedUsers = users1.OrderBy(u => u.Country).ThenBy(u => u.LastName).ToList();

// PROJECTION:

// 6. Använd Select() för att skapa en ny lista innehållandes bara användarnas email.
var emailList = users1.Select(u => u.Email).ToList();

// 7. Använd Select() för att skapa en lista av anonyma objekt med FirstName och Email.
// LITE EXTRA KLURIG KANSKE! Hoppa över om du inte känner dig redo. (Vad är ens anonyma objekt liksom?)
var firstNameAndEmail = users1
    .Select(u => new { u.FirstName, u.Email })
    .ToList();

// 8. Använd Where() och Select() för att få en lista med e-postadresser från användare som har lagrat mer än 5000 dataenheter.
var highDataEmails = users1.Where(u => u.DataStored > 5000).Select(u => u.Email).ToList();

// QUANTIFIERS

// 9. Använd Any() för att kontrollera om det finns någon användare från "USA".
// bool hasUsersFromUSA = SKRIV NÅTT HÄR

// 10. Använd All() för att kontrollera om alla användare har en giltig e-postadress (innehåller '@').
bool allValidEmails = users1.All(u => u.Email.Contains("@"));

// 11. Använd Select() och Contains() för att kontrollera om det finns någon användare med förnamnet "Anna".
// 11b. Detta är inte jättebra rent minnes- och prestandamässigt. Varför inte tror du och vad skulle ett 
// bättre alternativ vara?
bool annaExists = users1.Select(u => u.FirstName).Contains("Anna");

// SET OPERATIONS (Här bara med Distinct(), inte Union, Intersect eller Except)

// 12. Använd Select() med Distinct() för att få en lista med unika länder som användarna kommer ifrån.
var uniqueCountries = users1.Select(u => u.Country).Distinct().ToList();

// ELEMENTS

// 13. Använd FirstOrDefault() för att hitta den första användaren med ett specifikt efternamn.
var userWithLastName = users1.FirstOrDefault(u => u.LastName == "Smith");

// CONVERSION:

// 14. Använd ToDictionary() för att skapa en dictionary där nyckeln är användarens e-post och värdet är deras fullständiga namn.
var userDictionary = users1.ToDictionary(u => u.Email, u => u.FullName);

// JOIN

// 15. Använd Join() för att kombinera användare från två listor baserat på e-post.
List<User> users2 = User.GetRandomListOfUsers(1000);

var joinedUsers = users1.Join(users2,
    u1 => u1.Email,
    u2 => u2.Email,
    (u1, u2) => new { u1.FullName, u2.Country }).ToList();

// AGGREGERING

// 16. Använd Max() för att hitta det högsta värdet av DataStored.
var maxDataStored = users1.Max(u => u.DataStored);

// 17. Använd Min() för att hitta den lägsta åldern bland användarna (Kanske lite klurig)
var youngestAge = users1.Min(u => DateTime.Now.Year - u.DateOfBirth.Year);

// 18. Använd Count() för att räkna hur många användare som är födda före år 2000.
var bornBefore2000Count = users1.Count(u => u.DateOfBirth.Year < 2000);

// 19. Använd Sum() för att beräkna den totala mängden lagrad data av alla användare.
var totalDataStored = users1.Sum(u => u.DataStored);

// 20. Använd Average() för att beräkna den genomsnittliga mängden lagrad data per användare.
var averageDataStored = users1.Average(u => u.DataStored);

// 21. Använd Aggregate() för att beräkna den totala mängden lagrad data av alla användare (int).
var totalDataStoredAgg = users1.Aggregate(0, (total, user) => total + user.DataStored);

// 22. Använd Select() och Aggregate() för att sammanfoga alla användares fullständiga namn till en enda sträng.
var allNames = users1.Select(u => u.FullName).Aggregate((current, next) => current + ", " + next);

// PARTITIONING

// 23. Använd Take() för att ta de första 10 användarna i listan.
var firstTenUsers = users1.Take(10).ToList();

// 24. Använd Skip() för att hoppa över de första 990 användarna i listan.
var usersAfterTen = users1.Skip(10).ToList();

// 25. Använd TakeWhile() för att ta alla användare tills en användare med mindre än 10000 DataStored dyker upp.
var usersUntil1000 = users1.TakeWhile(u => u.DataStored >= 10000).ToList();

// 26. Använd SkipWhile() för att hoppa över alla användare tills en användare med mindre än 10000 DataStored dyker upp.
var usersAfter1000 = users1.SkipWhile(u => u.DataStored <= 10000).ToList();

// GROUPING

// 27. Använd GroupBy() och Count() för att räkna antalet användare per land.
var usersPerCountry = users1.GroupBy(u => u.Country)
    .Select(group => new { Country = group.Key, Count = group.Count() })
    .ToList();

// GROUPING PROJECTION SORTING  

// 28. Hur kan du ta reda på vilket land som har användare med högst totala DataStored?
//Alltså, räkna ihop den totala DataStored per land och skriv ut det land som ligger högst.
//Använd dig av så mkt LINQ som möjligt, ex Sum() och Max().  (Hint: Select() behövs också...)
var countryWithMostData = users1
    .GroupBy(u => u.Country)
    .Select(group => new { Country = group.Key, TotalData = group.Sum(u => u.DataStored) })
    .OrderByDescending(g => g.TotalData)
    .FirstOrDefault()?.Country;

// STOR DATAKÄLLA!

// 29. Använda LINQ på ännu större datakällor
// Läs in textfilen pg46.txt
// Hur kan du använda LINQ för att analysera den? Exempelvis:
// - Hur många ord finns det i texten?
// - Hur många ord börjar på bokstaven "a"?
// - Vilket är det vanligaste ordet?
// - Hur många ord är längre än 10 tecken?
// - Hur många ord är unika? (Hint: Distinct() )
// - Hur många ord förekommer mer än 10 gånger? (Hint: GroupBy() )
// - Vilka ord förekommer endast en gång?