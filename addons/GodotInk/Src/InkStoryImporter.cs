#if TOOLS

#nullable enable

using Godot;
using Ink;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Text.RegularExpressions;

using DependenciesCache = System.Collections.Generic.Dictionary<string, string[]>;
#if USE_NEW_GODOT_BINDINGS
using Godot.Collections;
using GArrayGictionary = Godot.Collections.GodotArray<Godot.Collections.GodotDictionary>;
using GArrayString = Godot.Collections.GodotArray<string>;
using Gictionary = Godot.Collections.GodotDictionary;
#else
using GArrayGictionary = Godot.Collections.Array<Godot.Collections.Dictionary>;
using GArrayString = Godot.Collections.Array<string>;
using Gictionary = Godot.Collections.Dictionary;
#endif

namespace GodotInk;

#if USE_NEW_GODOT_BINDINGS
[GodotClass(Tool = true)]
#else
[Tool]
#endif
public partial class InkStoryImporter : EditorImportPlugin
{
    private const string OPT_MAIN_FILE = "is_main_file";
    private const string OPT_COMPRESS = "compress";

    private static readonly Regex includeRegex = new(@"^\s*INCLUDE\s*(?<Path>.*)\s*$", RegexOptions.Multiline | RegexOptions.Compiled);

#pragma warning disable IDE0022
#if USE_NEW_GODOT_BINDINGS
    protected
#else
    public
#endif
    override string _GetImporterName() => "ink";

#if USE_NEW_GODOT_BINDINGS
    protected
#else
    public
#endif
    override string _GetVisibleName() => "Ink story";

#if USE_NEW_GODOT_BINDINGS
    protected override PackedStringArray _GetRecognizedExtensions() => ["ink"];
#else
    public override string[] _GetRecognizedExtensions() => new string[] { "ink" };
#endif

#if USE_NEW_GODOT_BINDINGS
    protected
#else
    public
#endif
    override string _GetResourceType() => nameof(Resource);

#if USE_NEW_GODOT_BINDINGS
    protected
#else
    public
#endif
    override string _GetSaveExtension() => "res";

#if USE_NEW_GODOT_BINDINGS
    protected
#else
    public
#endif
    override float _GetPriority() => 1.0f;

#if USE_NEW_GODOT_BINDINGS
    protected
#else
    public
#endif
    override int _GetPresetCount() => 0;

#if USE_NEW_GODOT_BINDINGS
    protected
#else
    public
#endif
    override int _GetImportOrder() => 0;

#if USE_NEW_GODOT_BINDINGS
    protected
#else
    public
#endif
    override GArrayGictionary _GetImportOptions(string path, int presetIndex) => new()
    {
        new() { { "name", OPT_MAIN_FILE }, { "default_value", false } },
        new() { { "name", OPT_COMPRESS }, { "default_value", true } }
    };

#if USE_NEW_GODOT_BINDINGS
    protected
#else
    public
#endif
    override bool _GetOptionVisibility(string path, StringName optionName, Gictionary options) => true;
#pragma warning restore IDE0022

#if USE_NEW_GODOT_BINDINGS
    protected
#else
    public
#endif
    override Error _Import(string sourceFile, string savePath, Gictionary options, GArrayString _, GArrayString __)
    {
        UpdateCache(sourceFile, ExtractIncludes(sourceFile));

        string destFile = $"{savePath}.{_GetSaveExtension()}";
        Error returnValue = options[OPT_MAIN_FILE].AsBool()
            ? ImportFromInk(sourceFile, destFile, options[OPT_COMPRESS].AsBool())
            : ResourceSaver.Singleton.Save(new StubInkStory(), destFile);

        string[] additionalFiles = GetCache().Where(kvp => kvp.Value.Contains(sourceFile)).Select(kvp => kvp.Key).ToArray();
        foreach (var additionalFile in additionalFiles)
            AppendImportExternalResource(additionalFile);

        return returnValue;
    }

    private static Error ImportFromInk(string sourceFile, string destFile, bool shouldCompress)
    {
        using Godot.FileAccess file = Godot.FileAccess.Open(sourceFile, Godot.FileAccess.ModeFlags.Read);

        if (file == null)
            return Godot.FileAccess.GetOpenError();

        Compiler compiler = new(file.GetAsText(), new Compiler.Options
        {
            countAllVisits = true,
            sourceFilename = sourceFile,
            errorHandler = InkCompilerErrorHandler,
            fileHandler = new FileHandler(
                Path.GetDirectoryName(file.GetPathAbsolute()) ?? ProjectSettings.Singleton.GlobalizePath("res://")
            ),
        });

        try
        {
            string storyContent = compiler.Compile().ToJson();
            InkStory resource = InkStory.Create(storyContent);
            ResourceSaver.SaverFlags flags = shouldCompress ? ResourceSaver.SaverFlags.Compress
                                                            : ResourceSaver.SaverFlags.None;
            return ResourceSaver.Singleton.Save(resource, destFile, flags);
        }
        catch (InvalidInkException)
        {
            return Error.CompilationFailed;
        }
    }

    private static List<string> ExtractIncludes(string sourceFile)
    {
        using Godot.FileAccess file = Godot.FileAccess.Open(sourceFile, Godot.FileAccess.ModeFlags.Read);
        return includeRegex.Matches(file.GetAsText())
                           .OfType<Match>()
                           .Select(match => sourceFile.GetBaseDir().PathJoin(match.Groups["Path"].Value.TrimEnd('\r')))
                           .ToList();
    }

    private static void InkCompilerErrorHandler(string message, ErrorType errorType)
    {
        switch (errorType)
        {
            case ErrorType.Warning:
                GD.PushWarning(message);
                break;
            case ErrorType.Error:
                GD.PushError(message);
                throw new InvalidInkException();
            case ErrorType.Author:
            default:
                break;
        }
    }

    private const string CACHE_FILE = "user://ink_cache.json";

    public static void UpdateCache(string sourceFile, List<string> dependencies)
    {
        DependenciesCache cache = GetCache();
        cache[sourceFile] = dependencies.ToArray();
        cache = cache.Where(kvp => kvp.Value.Length > 0).ToDictionary(kvp => kvp.Key, kvp => kvp.Value);

        using Godot.FileAccess file = Godot.FileAccess.Open(CACHE_FILE, Godot.FileAccess.ModeFlags.Write);
        file?.StoreString(JsonSerializer.Serialize(cache, DependenciesCacheJsonContext.DefaultTypeInfo));
    }

    public static DependenciesCache GetCache()
    {
        using Godot.FileAccess file = Godot.FileAccess.Open(CACHE_FILE, Godot.FileAccess.ModeFlags.Read);
        try
        {
            return JsonSerializer.Deserialize(file?.GetAsText(true) ?? "{}", DependenciesCacheJsonContext.DefaultTypeInfo)
                ?? new DependenciesCache();
        }
        catch (JsonException)
        {
            return new DependenciesCache();
        }
    }

    private class FileHandler : IFileHandler
    {
        private readonly string rootDir;

        public FileHandler(string rootDir)
        {
            this.rootDir = rootDir;
        }

        public string ResolveInkFilename(string includeName)
        {
            return Path.Combine(rootDir, includeName);
        }

        public string LoadInkFileContents(string fullFilename)
        {
            return File.ReadAllText(fullFilename);
        }
    }
}

#endif
