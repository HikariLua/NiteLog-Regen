using System;
using Godot;
using Godot.Collections;

public partial class Table : ItemList
{
  [Export]
  private ReportScreen report;

  public override void _Ready()
  {
    FixedColumnWidth = (int)(GetParent<ScrollContainer>().Size.X / 6.5);
    GD.Print($"{FixedColumnWidth}, {FixedColumnWidth * 6}");

    // Add Header
    AddItem("#");
    AddItem("Nome");
    AddItem("UserID");
    AddItem("Clockin");
    AddItem("Clockout");
    AddItem("Data");

    foreach (Dictionary<string, string> item in report.Data)
    {
      // string cappedUsername =
      //   item["username"].Length > 25
      //     ? item["username"].Substring(0, 22).StripEdges() + "..."
      //     : item["username"];

      AddItem((report.Data.IndexOf(item) + 1).ToString());
      // AddItem(cappedUsername);
      AddItem(item["username"]);
      AddItem(item["userid"]);
      AddItem(item["clockin"]);
      AddItem(item["clockout"]);
      AddItem(item["date"]);
    }
  }
}
