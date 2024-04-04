#if USE_NEW_GODOT_BINDINGS
using Godot;
using Godot.Bridge;
using Godot.Collections;
using System;
using System.Linq;

namespace GodotInk;

partial class InkStory
{
    public new class PropertyName : Resource.PropertyName
    {
        public static StringName RawStory { get; } = StringName.CreateStaticFromAscii("RawStory"u8);
        public static StringName CurrentText { get; } = StringName.CreateStaticFromAscii("CurrentText"u8);
    }

    public new class SignalName : Resource.SignalName
    {
        public static StringName Continued { get; } = StringName.CreateStaticFromAscii("Continued"u8);
        public static StringName MadeChoice { get; } = StringName.CreateStaticFromAscii("MadeChoice"u8);
    }

    public new class MethodName : Resource.MethodName
    {
        public static StringName Create { get; } = StringName.CreateStaticFromAscii("Create"u8);
        public static StringName InitializeRuntimeStory { get; } = StringName.CreateStaticFromAscii("InitializeRuntimeStory"u8);
        public static StringName GetCurrentText { get; } = StringName.CreateStaticFromAscii("GetCurrentText"u8);
        public static StringName GetCurrentChoices { get; } = StringName.CreateStaticFromAscii("GetCurrentChoices"u8);
        public static StringName GetCurrentTags { get; } = StringName.CreateStaticFromAscii("GetCurrentTags"u8);
        public static StringName GetCanContinue { get; } = StringName.CreateStaticFromAscii("GetCanContinue"u8);
        public static StringName Continue { get; } = StringName.CreateStaticFromAscii("Continue"u8);
        public static StringName ContinueMaximally { get; } = StringName.CreateStaticFromAscii("ContinueMaximally"u8);
        public static StringName ChooseChoiceIndex { get; } = StringName.CreateStaticFromAscii("ChooseChoiceIndex"u8);
        public static StringName ChoosePathString { get; } = StringName.CreateStaticFromAscii("ChoosePathString"u8);
        public static StringName ResetCallstack { get; } = StringName.CreateStaticFromAscii("ResetCallstack"u8);
        public static StringName ResetState { get; } = StringName.CreateStaticFromAscii("ResetState"u8);
        public static StringName RemoveFlow { get; } = StringName.CreateStaticFromAscii("RemoveFlow"u8);
        public static StringName SwitchFlow { get; } = StringName.CreateStaticFromAscii("SwitchFlow"u8);
        public static StringName SwitchToDefaultFlow { get; } = StringName.CreateStaticFromAscii("SwitchToDefaultFlow"u8);
        public static StringName VisitCountAtPathString { get; } = StringName.CreateStaticFromAscii("VisitCountAtPathString"u8);
        public static StringName FetchVariable { get; } = StringName.CreateStaticFromAscii("FetchVariable"u8);
        public static StringName StoreVariable { get; } = StringName.CreateStaticFromAscii("StoreVariable"u8);
        public static StringName ObserveVariable { get; } = StringName.CreateStaticFromAscii("ObserveVariable"u8);
        public static StringName RemoveVariableObserver { get; } = StringName.CreateStaticFromAscii("RemoveVariableObserver"u8);
        public static StringName HasFunction { get; } = StringName.CreateStaticFromAscii("HasFunction"u8);
        public static StringName EvaluateFunction { get; } = StringName.CreateStaticFromAscii("EvaluateFunction"u8);
        public static StringName BindExternalFunction { get; } = StringName.CreateStaticFromAscii("BindExternalFunction"u8);
        public static StringName UnbindExternalFunction { get; } = StringName.CreateStaticFromAscii("UnbindExternalFunction"u8);
        public static StringName Error { get; } = StringName.CreateStaticFromAscii("Error"u8);
        public static StringName Warning { get; } = StringName.CreateStaticFromAscii("Warning"u8);
        public static StringName SaveState { get; } = StringName.CreateStaticFromAscii("SaveState"u8);
        public static StringName SaveStateFile { get; } = StringName.CreateStaticFromAscii("SaveStateFile"u8);
        public static StringName LoadState { get; } = StringName.CreateStaticFromAscii("LoadState"u8);
        public static StringName LoadStateFile { get; } = StringName.CreateStaticFromAscii("LoadStateFile"u8);
        public static StringName OnContinued { get; } = StringName.CreateStaticFromAscii("OnContinued"u8);
    }

    public event ContinuedEventHandler Continued
    {
        add => Connect(SignalName.Continued, Callable.From(new System.Action(value)));
        remove => Disconnect(SignalName.Continued, Callable.From(new System.Action(value)));
    }

    public event MadeChoiceEventHandler MadeChoice
    {
        add => Connect(SignalName.MadeChoice, Callable.From(new System.Action<InkChoice>(value)));
        remove => Disconnect(SignalName.MadeChoice, Callable.From(new System.Action<InkChoice>(value)));
    }

    public static void BindMethods(ClassDBRegistrationContext context)
    {
        context.BindConstructor(() => new InkStory());

        context.BindSignal(new SignalInfo(SignalName.Continued));

        context.BindSignal(new SignalInfo(SignalName.MadeChoice)
        {
            Parameters =
            {
                new ParameterInfo(StringName.CreateStaticFromAscii("choice"u8), VariantType.Object)
                {
                    ClassName = StringName.CreateStaticFromAscii("InkChoice"u8),
                },
            },
        });

        context.BindStaticMethod(MethodName.Create,
            new ParameterInfo(StringName.CreateStaticFromAscii("rawStory"u8), VariantType.String),
            new ReturnInfo(VariantType.Object)
            {
                Hint = PropertyHint.ResourceType,
                HintString = "InkStory",
                ClassName = StringName.CreateStaticFromAscii("InkStory"u8),
            },
            static (string rawStory) =>
            {
                return Create(rawStory);
            });

        context.BindMethod(MethodName.InitializeRuntimeStory,
            static (InkStory instance) =>
            {
                instance.InitializeRuntimeStory();
            });

        context.BindMethod(MethodName.GetCurrentText,
            new ReturnInfo(VariantType.String),
            static (InkStory instance) =>
            {
                return instance.CurrentText;
            });

        context.BindMethod(MethodName.GetCurrentChoices,
            new ReturnInfo(VariantType.Array)
            {
                Hint = PropertyHint.TypeString,
                HintString = "24/0:",
            },
            static (InkStory instance) =>
            {
                return new GodotArray<InkChoice>(instance.CurrentChoices);
            });

        context.BindMethod(MethodName.GetCurrentTags,
            new ReturnInfo(VariantType.String),
            static (InkStory instance) =>
            {
                return new GodotArray<string>(instance.CurrentTags);
            });

        context.BindMethod(MethodName.GetCanContinue,
            new ReturnInfo(VariantType.Bool),
            static (InkStory instance) =>
            {
                return instance.CanContinue;
            });

        context.BindMethod(MethodName.Continue,
            new ReturnInfo(VariantType.String),
            static (InkStory instance) =>
            {
                return instance.Continue();
            });

        context.BindMethod(MethodName.ContinueMaximally,
            new ReturnInfo(VariantType.String),
            static (InkStory instance) =>
            {
                return instance.ContinueMaximally();
            });

        context.BindMethod(MethodName.ChooseChoiceIndex,
            new ParameterInfo(StringName.CreateStaticFromAscii("choiceIdx"u8), VariantType.Int),
            static (InkStory instance, int choiceIdx) =>
            {
                instance.ChooseChoiceIndex(choiceIdx);
            });

        // NOTE: This method has overloads, it breaks source compat for GDScript in the following cases:
        //         - ChoosePathStrings(string, VariantGArray)
        context.BindMethod(MethodName.ChoosePathString,
            new ParameterInfo(StringName.CreateStaticFromAscii("path"u8), VariantType.String),
            new ParameterInfo(StringName.CreateStaticFromAscii("resetCallstack"u8), VariantType.Bool, VariantTypeMetadata.None, true),
            new ParameterInfo(StringName.CreateStaticFromAscii("arguments"u8), VariantType.Array, VariantTypeMetadata.None, default)
            {
                Hint = PropertyHint.TypeString,
                HintString = "0/0:",
            },
            static (InkStory instance, string path, bool resetCallstack, GodotArray<Variant> arguments) =>
            {
                instance.ChoosePathString(path, resetCallstack, arguments?.ToArray() ?? Array.Empty<Variant>());
            });

        context.BindMethod(MethodName.ResetCallstack,
            static (InkStory instance) =>
            {
                instance.ResetCallstack();
            });

        context.BindMethod(MethodName.ResetState,
            static (InkStory instance) =>
            {
                instance.ResetState();
            });

        context.BindMethod(MethodName.RemoveFlow,
            new ParameterInfo(StringName.CreateStaticFromAscii("flowName"u8), VariantType.String),
            static (InkStory instance, string flowName) =>
            {
                instance.RemoveFlow(flowName);
            });

        context.BindMethod(MethodName.SwitchFlow,
            new ParameterInfo(StringName.CreateStaticFromAscii("flowName"u8), VariantType.String),
            static (InkStory instance, string flowName) =>
            {
                instance.SwitchFlow(flowName);
            });

        context.BindMethod(MethodName.SwitchToDefaultFlow,
            static (InkStory instance) =>
            {
                instance.SwitchToDefaultFlow();
            });

        context.BindMethod(MethodName.VisitCountAtPathString,
            new ParameterInfo(StringName.CreateStaticFromAscii("pathString"u8), VariantType.String),
            new ReturnInfo(VariantType.Int),
            static (InkStory instance, string pathString) =>
            {
                return instance.VisitCountAtPathString(pathString);
            });

        context.BindMethod(MethodName.FetchVariable,
            new ParameterInfo(StringName.CreateStaticFromAscii("variableName"u8), VariantType.String),
            new ReturnInfo(VariantType.Nil)
            {
                Usage = PropertyUsageFlags.NilIsVariant,
            },
            static (InkStory instance, string variableName) =>
            {
                return instance.FetchVariable(variableName);
            });

        context.BindMethod(MethodName.StoreVariable,
            new ParameterInfo(StringName.CreateStaticFromAscii("variableName"u8), VariantType.String),
            new ParameterInfo(StringName.CreateStaticFromAscii("value"u8), VariantType.Nil)
            {
                Usage = PropertyUsageFlags.NilIsVariant,
            },
            static (InkStory instance, string variableName, Variant value) =>
            {
                instance.StoreVariable(variableName, value);
            });

        // NOTE: This method has overloads, it may break source compat for GDScript.
        context.BindMethod(MethodName.ObserveVariable,
            new ParameterInfo(StringName.CreateStaticFromAscii("variableName"u8), VariantType.String),
            new ParameterInfo(StringName.CreateStaticFromAscii("observer"u8), VariantType.Callable),
            static (InkStory instance, string variableName, Callable observer) =>
            {
                instance.ObserveVariable(variableName, observer);
            });

        // NOTE: This method has overloads, it breaks source compat for GDScript in the following cases:
        //         - RemoveVariableObserver(Callable, string)
        context.BindMethod(MethodName.RemoveVariableObserver,
            new ParameterInfo(StringName.CreateStaticFromAscii("callable"u8), VariantType.Callable),
            static (InkStory instance, Callable observer) =>
            {
                instance.RemoveVariableObserver(observer);
            });

        context.BindMethod(MethodName.HasFunction,
            new ParameterInfo(StringName.CreateStaticFromAscii("functionName"u8), VariantType.String),
            new ReturnInfo(VariantType.Bool),
            static (InkStory instance, string functionName) =>
            {
                return instance.HasFunction(functionName);
            });

        // NOTE: This method has overloads, it may break source compat for GDScript.
        context.BindMethod(MethodName.EvaluateFunction,
            new ParameterInfo(StringName.CreateStaticFromAscii("functionName"u8), VariantType.String),
            new ParameterInfo(StringName.CreateStaticFromAscii("arguments"u8), VariantType.Array, VariantTypeMetadata.None, default)
            {
                Hint = PropertyHint.TypeString,
                HintString = "0/0:",
            },
            new ReturnInfo(VariantType.Nil)
            {
                Usage = PropertyUsageFlags.NilIsVariant,
            },
            static (InkStory instance, string functionName, GodotArray<Variant> arguments) =>
            {
                return instance.EvaluateFunction(functionName, arguments?.ToArray() ?? Array.Empty<Variant>());
            });

        // NOTE: This method has overloads, it may break source compat for GDScript.
        context.BindMethod(MethodName.BindExternalFunction,
            new ParameterInfo(StringName.CreateStaticFromAscii("funcName"u8), VariantType.String),
            new ParameterInfo(StringName.CreateStaticFromAscii("callable"u8), VariantType.Callable),
            new ParameterInfo(StringName.CreateStaticFromAscii("lookaheadSafe"u8), VariantType.Bool, VariantTypeMetadata.None, false),
            static (InkStory instance, string funcName, Callable callable, bool lookaheadSafe) =>
            {
                instance.BindExternalFunction(funcName, callable, lookaheadSafe);
            });

        context.BindMethod(MethodName.UnbindExternalFunction,
            new ParameterInfo(StringName.CreateStaticFromAscii("funcName"u8), VariantType.String),
            static (InkStory instance, string funcName) =>
            {
                instance.UnbindExternalFunction(funcName);
            });

        context.BindMethod(MethodName.Error,
            new ParameterInfo(StringName.CreateStaticFromAscii("message"u8), VariantType.String),
            new ParameterInfo(StringName.CreateStaticFromAscii("useEndLineNumber"u8), VariantType.Bool, VariantTypeMetadata.None, false),
            static (InkStory instance, string message, bool useEndLineNumber) =>
            {
                instance.Error(message, useEndLineNumber);
            });

        context.BindMethod(MethodName.Warning,
            new ParameterInfo(StringName.CreateStaticFromAscii("message"u8), VariantType.String),
            static (InkStory instance, string message) =>
            {
                instance.Warning(message);
            });

        context.BindMethod(MethodName.SaveState,
            new ReturnInfo(VariantType.String),
            static (InkStory instance) =>
            {
                return instance.SaveState();
            });

        context.BindMethod(MethodName.SaveStateFile,
            new ParameterInfo(StringName.CreateStaticFromAscii("filePath"u8), VariantType.String),
            static (InkStory instance, string filePath) =>
            {
                instance.SaveStateFile(filePath);
            });

        context.BindMethod(MethodName.LoadState,
            new ParameterInfo(StringName.CreateStaticFromAscii("jsonState"u8), VariantType.String),
            static (InkStory instance, string jsonState) =>
            {
                instance.LoadState(jsonState);
            });

        context.BindMethod(MethodName.LoadStateFile,
            new ParameterInfo(StringName.CreateStaticFromAscii("filePath"u8), VariantType.String),
            static (InkStory instance, string filePath) =>
            {
                instance.LoadStateFile(filePath);
            });

        context.BindMethod(MethodName.OnContinued,
            static (InkStory instance) =>
            {
                instance.OnContinued();
            });
    }
}
#endif
