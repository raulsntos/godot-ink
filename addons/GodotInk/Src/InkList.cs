#nullable enable

using Godot;

#if USE_NEW_GODOT_BINDINGS
using Godot.Collections;
#endif

namespace GodotInk;

#if USE_NEW_GODOT_BINDINGS
[GodotClass]
#elif GODOT4_1_OR_GREATER
[GlobalClass]
#endif
public partial class InkList : RefCounted
{
    private readonly Ink.Runtime.InkList inner;

#if USE_NEW_GODOT_BINDINGS
    // NOTE: We need to define a parameterless constructor because the current
    // source generators don't allow non-instantiable classes.
    private InkList()
    {
        throw new System.Diagnostics.UnreachableException();
    }
#endif

    public InkList(Ink.Runtime.InkList inner)
    {
        this.inner = inner;
    }

    public InkList(InkList otherList)
    : this(new Ink.Runtime.InkList(otherList.inner))
    {
    }

#pragma warning disable IDE0022
    public InkList Inverse => new(inner.inverse);
    public InkList All => new(inner.all);

    [BindMethod]
    public void AddItem(string itemName) => inner.AddItem(itemName);

    [BindMethod]
    public bool Contains(string listItemName) => inner.Contains(listItemName);
    public bool Contains(InkList otherList) => inner.Contains(otherList.inner);
    [BindMethod]
    public bool ContainsItemNamed(string itemName) => inner.ContainsItemNamed(itemName);

    public override bool Equals(object? other) => inner.Equals(other);
    public override int GetHashCode() => inner.GetHashCode();
    public override string ToString() => inner.ToString();

    [BindMethod]
    public bool GreaterThan(InkList otherList) => inner.GreaterThan(otherList.inner);
    [BindMethod]
    public bool GreaterThanOrEquals(InkList otherList) => inner.GreaterThanOrEquals(otherList.inner);
    [BindMethod]
    public bool LessThan(InkList otherList) => inner.LessThan(otherList.inner);
    [BindMethod]
    public bool LessThanOrEquals(InkList otherList) => inner.LessThanOrEquals(otherList.inner);

    [BindMethod]
    public bool HashIntersection(InkList otherList) => inner.HasIntersection(otherList.inner);
    [BindMethod]
    public InkList Intersect(InkList otherList) => new(inner.Intersect(otherList.inner));
    [BindMethod]
    public InkList Union(InkList otherList) => new(inner.Union(otherList.inner));
    [BindMethod]
    public InkList Without(InkList listToRemove) => new(inner.Without(listToRemove.inner));

    [BindMethod]
    public InkList MaxAsList() => new(inner.MaxAsList());
    [BindMethod]
    public InkList MinAsList() => new(inner.MinAsList());

    [BindMethod]
    public void SetInitialOriginName(string initialOriginName) => inner.SetInitialOriginName(initialOriginName);
    public void SetInitialOriginNames(string[] initialOriginNames) => inner.SetInitialOriginNames(new(initialOriginNames));
#if USE_NEW_GODOT_BINDINGS
    [BindMethod]
    private void SetInitialOriginNames(PackedStringArray initialOriginNames) => inner.SetInitialOriginNames(new(initialOriginNames));
#endif
#pragma warning restore IDE0022
}
