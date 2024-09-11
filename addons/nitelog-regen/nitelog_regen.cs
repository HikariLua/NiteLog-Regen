#if TOOLS
using Godot;
using QuestPDF.Infrastructure;

[Tool]
public partial class nitelog_regen : EditorPlugin
{
  public override void _EnterTree() { }

  public override void _ExitTree()
  {
    // Clean-up of the plugin goes here.
  }
}
#endif
