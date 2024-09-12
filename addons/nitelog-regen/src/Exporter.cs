using System;
using Godot;
using Godot.Collections;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

public partial class Exporter : Node
{
  [Signal]
  public delegate void SavedEventHandler(string path);

  private static Godot.Collections.Array<Dictionary> Data;

  public void ExportPDF(string path)
  {
    QuestPDF.Settings.License = LicenseType.Community;

    Data = GenerateRandomData();

    Document
      .Create(container =>
      {
        container.Page(page =>
        {
          page.Size(PageSizes.A4);
          page.Margin(2, Unit.Centimetre);
          page.DefaultTextStyle(x => x.FontSize(12));
          page.Background().Image("addons/nitelog-regen/assets/bgalt.png");

          page.Header()
            .Column(column =>
            {
              column
                .Item()
                .ShowOnce()
                .Text("Relatório NiteLog")
                .SemiBold()
                .FontSize(36)
                .FontColor("#000000");

              var date1 = Data[0]["date"];
              var date2 = Data[Data.Count - 1]["date"];
              column
                .Item()
                .ShowOnce()
                .Text($"{date1} - {date2}")
                .FontSize(20)
                .FontColor(QuestPDF.Helpers.Colors.Grey.Darken2);
            });

          page.Content().Element(ComposeTable);

          page.Footer()
            .AlignCenter()
            .Text(x =>
            {
              x.CurrentPageNumber();
            });
        });
      })
      .GeneratePdf(path);

    EmitSignal(SignalName.Saved, path);
  }

  static void ComposeTable(IContainer container)
  {
    container.Table(table =>
    {
      // step 1
      table.ColumnsDefinition(columns =>
      {
        columns.ConstantColumn(40);
        columns.RelativeColumn();
        columns.RelativeColumn();
        columns.RelativeColumn();
        columns.RelativeColumn();
        columns.RelativeColumn();
      });

      // step 2
      table.Header(header =>
      {
        header.Cell().Element(CellStyle).Text("#");
        header.Cell().Element(CellStyle).Text("Nome");
        header.Cell().Element(CellStyle).AlignRight().Text("Userid");
        header.Cell().Element(CellStyle).AlignRight().Text("Clockin");
        header.Cell().Element(CellStyle).AlignRight().Text("Clockout");
        header.Cell().Element(CellStyle).AlignRight().Text("Data");

        static IContainer CellStyle(IContainer container)
        {
          return container
            .DefaultTextStyle(x => x.SemiBold())
            .PaddingVertical(5)
            .BorderBottom(1)
            .BorderColor(QuestPDF.Helpers.Colors.Black);
        }
      });

      foreach (Dictionary item in Data)
      {
        table.Cell().Element(CellStyle).Text(Data.IndexOf(item) + 1);
        table.Cell().Element(CellStyle).Text(item["username"]);
        table.Cell().Element(CellStyle).AlignRight().Text(item["userid"]);
        table.Cell().Element(CellStyle).AlignRight().Text(item["clockin"]);
        table.Cell().Element(CellStyle).AlignRight().Text(item["clockout"]);
        table.Cell().Element(CellStyle).AlignRight().Text(item["date"]);

        IContainer CellStyle(IContainer container)
        {
          return container
            .BorderBottom(1)
            .BorderColor(
              (Data.IndexOf(item) % 2 == 0)
                ? QuestPDF.Helpers.Colors.Grey.Lighten1
                : QuestPDF.Helpers.Colors.Grey.Darken1
            )
            .PaddingVertical(5);
        }
      }
    });
  }

  private static Godot.Collections.Array<Dictionary> GenerateRandomData()
  {
    var random = new Random();
    var dataArray = new Godot.Collections.Array<Dictionary>();

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
      "Liam",
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
      var dictionary = new Godot.Collections.Dictionary
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
