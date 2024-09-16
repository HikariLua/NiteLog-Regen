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

  [Export]
  public ReportScreen report;

  public void ExportPDF(string path)
  {
    QuestPDF.Settings.License = LicenseType.Community;

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

              var date1 = report.Data[0]["date"];
              var date2 = report.Data[report.Data.Count - 1]["date"];
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

  void ComposeTable(IContainer container)
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
        header.Cell().Element(CellStyle).AlignRight().Text("UserID");
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

      foreach (Dictionary<string, string> item in report.Data)
      {
        table.Cell().Element(CellStyle).Text(report.Data.IndexOf(item) + 1);
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
              (report.Data.IndexOf(item) % 2 == 0)
                ? QuestPDF.Helpers.Colors.Grey.Lighten1
                : QuestPDF.Helpers.Colors.Grey.Darken1
            )
            .PaddingVertical(5);
        }
      }
    });
  }
}
