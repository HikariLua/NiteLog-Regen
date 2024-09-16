using System;
using Godot;
using Godot.Collections;

public partial class ReportScreen : Control
{
  public Godot.Collections.Array<Dictionary<string, string>> Data =
    GenerateRandomData();

  private static Godot.Collections.Array<
    Dictionary<string, string>
  > GenerateRandomData()
  {
    var random = new Random();
    var dataArray = new Godot.Collections.Array<Dictionary<string, string>>();

    // Create a list of random usernames
    string[] usernames =
    {
      "Alice",
      "Bob",
      "Charlie",
      "Diana",
      "Eve",
      "Frank",
      "Grace",
      "Hank",
      "Ivy",
      "Jack",
      "Kara",
      "Liam Bob Mcfallen the Fifith, the dispicable king of all mighty stipidity",
      "Mia",
      "Nina",
      "Oscar",
      "Paul",
      "Quinn",
      "Ray",
      "Sara",
      "Tom",
      "Uma",
      "Vince",
      "Wendy",
      "Xander",
      "Yara",
    };

    for (int i = 0; i < 250; i++)
    {
      var dictionary = new Dictionary<string, string>
      {
        { "userid", random.Next(1000, 9999).ToString() },
        { "username", usernames[i % usernames.Length] },
        { "clockin", $"{random.Next(6, 10):D2}:{random.Next(0, 60):D2}" },
        { "clockout", $"{random.Next(16, 20):D2}:{random.Next(0, 60):D2}" },
        {
          "date",
          new DateTime(2024, random.Next(1, 13), random.Next(1, 29)).ToString(
            "yyyy/MM/dd"
          )
        },
      };

      dataArray.Add(dictionary);
    }

    return dataArray;
  }
}
