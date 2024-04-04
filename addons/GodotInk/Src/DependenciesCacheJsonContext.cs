using System.Text.Json.Serialization;
using System.Text.Json.Serialization.Metadata;
using DependenciesCache = System.Collections.Generic.Dictionary<string, string[]>;

namespace GodotInk;

[JsonSerializable(typeof(DependenciesCache))]
internal partial class DependenciesCacheJsonContext : JsonSerializerContext
{
    public static JsonTypeInfo<DependenciesCache> DefaultTypeInfo => Default.DictionaryStringStringArray;
}
