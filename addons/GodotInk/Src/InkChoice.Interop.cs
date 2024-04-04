#nullable enable

using System.ComponentModel;

#if USE_NEW_GODOT_BINDINGS
using Godot;
using Godot.Collections;
#endif

namespace GodotInk;

public partial class InkChoice
{
#pragma warning disable IDE0022
    /// <summary>
    /// This method is here for GDScript compatibility. Use <see cref="CanContinue" /> instead.
    /// </summary>
    [BindMethod]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public string GetText() => Text;

    /// <summary>
    /// This method is here for GDScript compatibility. Use <see cref="CanContinue" /> instead.
    /// </summary>
    [BindMethod]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public string GetPathStringOnChoice() => PathStringOnChoice;

    /// <summary>
    /// This method is here for GDScript compatibility. Use <see cref="CanContinue" /> instead.
    /// </summary>
    [BindMethod]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public string GetSourcePath() => SourcePath;

    /// <summary>
    /// This method is here for GDScript compatibility. Use <see cref="CanContinue" /> instead.
    /// </summary>
    [BindMethod]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public int GetIndex() => Index;

    /// <summary>
    /// This method is here for GDScript compatibility. Use <see cref="CanContinue" /> instead.
    /// </summary>
    [BindMethod]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public
#if USE_NEW_GODOT_BINDINGS
    GodotArray<string>
#else
    Godot.Collections.Array<string>
#endif
    GetTags() => new(Tags);
#pragma warning restore IDE0022
}
