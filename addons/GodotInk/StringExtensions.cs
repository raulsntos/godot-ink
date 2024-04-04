#if USE_NEW_GODOT_BINDINGS
using System.IO;

namespace Godot;

public static class StringExtensions
{
    public static string GetBaseDir(this string value)
    {
        return Path.GetDirectoryName(value);
    }

    public static string PathJoin(this string value, string str)
    {
        return Path.Join(value, str);
    }

    public static string GetExtension(this string value)
    {
        string extension = Path.GetExtension(value);
        if (string.IsNullOrEmpty(extension))
        {
            return extension;
        }
        return extension[1..];
    }
}
#endif
