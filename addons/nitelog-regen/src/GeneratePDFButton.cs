using Godot;
using Godot.Collections;

public partial class GeneratePDFButton : Button
{
  private Dictionary<string, string> data;

  // Called when the node enters the scene tree for the first time.
  public override void _Ready()
  {
    this.Pressed += Export;

    data = new Dictionary<string, string>
    {
      { "abuble", "blebuba" },
      { "abuble2", "blebuba" },
      { "abuble3", "blebuba" },
      { "abuble4", "blebuba" },
      { "abuble5", "blebuba" },
      { "abuble6", "blebuba" },
    };
  }

  private void Export()
  {
    Exporter.ExportPDF("xablau", data);
  }
}
