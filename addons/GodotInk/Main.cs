#if USE_NEW_GODOT_BINDINGS
using System.Runtime.InteropServices;
using Godot.Bridge;

namespace GodotInk;

internal class Main
{
    public static void InitializeGodotInkTypes(InitializationLevel level)
    {
        if (level != InitializationLevel.Editor)
        {
            return;
        }

        ClassDB.RegisterClass<Plugin>(Plugin.BindMethods);
        EditorPlugins.AddByType<Plugin>();

        ClassDB.RegisterClass<InkStoryImporter>(InkStoryImporter.BindMethods);

        ClassDB.RegisterClass<InkStory>(InkStory.BindMethods);
        ClassDB.RegisterClass<StubInkStory>(StubInkStory.BindMethods);

        ClassDB.RegisterRuntimeClass<InkList>(InkList.BindMethods);

        ClassDB.RegisterClass<InkDock>(InkDock.BindMethods);

        ClassDB.RegisterRuntimeClass<InkChoice>(InkChoice.BindMethods);
    }

    public static void UninitializeGodotInkTypes(InitializationLevel level)
    {
        if (level != InitializationLevel.Editor)
        {
            return;
        }
    }

    // Initialization

    [UnmanagedCallersOnly(EntryPoint = "godotink_library_init")]
    public static bool GodotInkLibraryInit(nint getProcAddress, nint library, nint initialization)
    {
        GodotBridge.Initialize(getProcAddress, library, initialization, config =>
        {
            config.SetMinimumLibraryInitializationLevel(InitializationLevel.Scene);
            config.RegisterInitializer(InitializeGodotInkTypes);
            config.RegisterTerminator(UninitializeGodotInkTypes);
        });

        return true;
    }
}
#endif
