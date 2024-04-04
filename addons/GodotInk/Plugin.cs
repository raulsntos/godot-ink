#if USE_NEW_GODOT_BINDINGS
using Godot;
using Godot.Collections;

namespace GodotInk;

[GodotClass]
internal partial class Plugin : EditorPlugin
{
    private EditorImportPlugin _importer;
    private InkDock _dock;

    protected override void _EnterTree()
    {
        _importer = new InkStoryImporter();
        AddImportPlugin(_importer);

        // NOTE: This requires including the tscn file in the right directory in the user's project.
        PackedScene dockScene = (PackedScene)ResourceLoader.Singleton.Load("res://lib/InkDock.tscn");
        _dock = dockScene.Instantiate<InkDock>();
        AddControlToBottomPanel(_dock, "Ink preview");

        var fs = GetEditorInterface().GetResourceFilesystem();
        fs.ResourcesReimported -= WhenResourcesReimported;
        fs.ResourcesReimported += WhenResourcesReimported;
    }

    protected override void _ExitTree()
    {
        var fs = GetEditorInterface().GetResourceFilesystem();
        fs.ResourcesReimported -= WhenResourcesReimported;

        if (_dock != null)
        {
            RemoveControlFromBottomPanel(_dock);
            _dock.Dispose();
            _dock = null;
        }

        if (_importer != null)
        {
            RemoveImportPlugin(_importer);
            _importer = null;
        }
    }

    private void WhenResourcesReimported(PackedStringArray resources)
    {
        if (_dock == null) return;

        foreach (var resource in resources)
        {
            if (resource.GetExtension() == "ink")
            {
                _dock.WhenInkResourceReimported();
                return;
            }
        }
    }
}
#endif
