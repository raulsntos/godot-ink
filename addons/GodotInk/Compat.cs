#if !USE_NEW_GODOT_BINDINGS
using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Godot;

#nullable enable

[Conditional("DEBUG")]
[AttributeUsage(AttributeTargets.Method)]
internal class BindMethodAttribute : Attribute { }

internal enum VariantType : long
{
    Nil = Variant.Type.Nil,
    Bool = Variant.Type.Bool,
    Int = Variant.Type.Int,
    Float = Variant.Type.Float,
    String = Variant.Type.String,
    Vector2 = Variant.Type.Vector2,
    Vector2I = Variant.Type.Vector2I,
    Rect2 = Variant.Type.Rect2,
    Rect2I = Variant.Type.Rect2I,
    Vector3 = Variant.Type.Vector3,
    Vector3I = Variant.Type.Vector3I,
    Transform2D = Variant.Type.Transform2D,
    Vector4 = Variant.Type.Vector4,
    Vector4I = Variant.Type.Vector4I,
    Plane = Variant.Type.Plane,
    Quaternion = Variant.Type.Quaternion,
    Aabb = Variant.Type.Aabb,
    Basis = Variant.Type.Basis,
    Transform3D = Variant.Type.Transform3D,
    Projection = Variant.Type.Projection,
    Color = Variant.Type.Color,
    StringName = Variant.Type.StringName,
    NodePath = Variant.Type.NodePath,
    Rid = Variant.Type.Rid,
    Object = Variant.Type.Object,
    Callable = Variant.Type.Callable,
    Signal = Variant.Type.Signal,
    Dictionary = Variant.Type.Dictionary,
    Array = Variant.Type.Array,
    PackedByteArray = Variant.Type.PackedByteArray,
    PackedInt32Array = Variant.Type.PackedInt32Array,
    PackedInt64Array = Variant.Type.PackedInt64Array,
    PackedFloat32Array = Variant.Type.PackedFloat32Array,
    PackedFloat64Array = Variant.Type.PackedFloat64Array,
    PackedStringArray = Variant.Type.PackedStringArray,
    PackedVector2Array = Variant.Type.PackedVector2Array,
    PackedVector3Array = Variant.Type.PackedVector3Array,
    PackedColorArray = Variant.Type.PackedColorArray,
}

internal static class CompatExtensions
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static object? AsSystemObject(this Variant variant)
    {
        return variant.Obj;
    }
}
#endif
