using System;
using Godot;

public partial class SaveDialog : FileDialog
{
  [Export]
  private Exporter exporter;

  public override void _Ready()
  {
    this.FileSelected += onSave;
  }

  private void onSave(string path)
  {
    exporter.ExportPDF(path);
  }
}
