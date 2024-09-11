using Godot;
using Godot.Collections;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

public partial class Exporter : Node
{
  private static Dictionary<string, string> Data;

  public static void ExportPDF(string header, Dictionary<string, string> data)
  {
    QuestPDF.Settings.License = LicenseType.Community;

    Data = data;

    Document
      .Create(container =>
      {
        container.Page(page =>
        {
          page.Size(PageSizes.A4);
          page.Margin(2, Unit.Centimetre);
          page.PageColor(QuestPDF.Helpers.Colors.White);
          page.DefaultTextStyle(x => x.FontSize(12));

          page.Header()
            .Text(header)
            .SemiBold()
            .FontSize(36)
            .FontColor(QuestPDF.Helpers.Colors.Blue.Medium);

          page.Content().Element(ComposeTable);

          page.Footer()
            .AlignCenter()
            .Text(x =>
            {
              x.Span("Page ");
              x.CurrentPageNumber();
            });
        });
      })
      .GeneratePdf("hello.pdf");
  }

  static void ComposeTable(IContainer container)
  {
    container.Table(table =>
    {
      // step 1
      table.ColumnsDefinition(columns =>
      {
        columns.ConstantColumn(25);
        columns.RelativeColumn(3);
        columns.RelativeColumn();
        columns.RelativeColumn();
        columns.RelativeColumn();
      });

      // step 2
      table.Header(header =>
      {
        header.Cell().Element(CellStyle).Text("#");
        header.Cell().Element(CellStyle).Text("Product");
        header.Cell().Element(CellStyle).AlignRight().Text("Unit price");
        header.Cell().Element(CellStyle).AlignRight().Text("Quantity");
        header.Cell().Element(CellStyle).AlignRight().Text("Total");

        static IContainer CellStyle(IContainer container)
        {
          return container
            .DefaultTextStyle(x => x.SemiBold())
            .PaddingVertical(5)
            .BorderBottom(1)
            .BorderColor(QuestPDF.Helpers.Colors.Black);
        }
      });

      // step 3
      foreach (var item in Data)
      {
        table.Cell().Element(CellStyle).Text(item.Key);
        table.Cell().Element(CellStyle).Text(item);
        table.Cell().Element(CellStyle).AlignRight().Text(item);
        table.Cell().Element(CellStyle).AlignRight().Text(item);
        table.Cell().Element(CellStyle).AlignRight().Text($"haha");

        static IContainer CellStyle(IContainer container)
        {
          return container
            .BorderBottom(1)
            .BorderColor(QuestPDF.Helpers.Colors.Grey.Lighten2)
            .PaddingVertical(5);
        }
      }
    });
  }
}
