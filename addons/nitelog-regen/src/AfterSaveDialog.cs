using System.Diagnostics;
using System.IO;
using Godot;

public partial class AfterSaveDialog : ConfirmationDialog
{
  [Export]
  private Exporter exporter;

  private string filePath;

  public override void _Ready()
  {
    this.Confirmed += onConfirmed;

    exporter.Saved += (path) =>
    {
      this.Visible = true;
      filePath = path;
      this.Title = $"Abrir {Path.GetFileName(path)}?";
    };
  }

  private void onConfirmed()
  {
    Process.Start(
      new ProcessStartInfo() { FileName = filePath, UseShellExecute = true }
    );
  }
}
