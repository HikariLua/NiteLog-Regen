using Godot;

public partial class GeneratePDFButton : Button
{
  [Export]
  private FileDialog saveDialog;

  public override void _Ready()
  {
    this.Pressed += Export;
  }

  private void Export()
  {
    saveDialog.Visible = true;
  }
}
