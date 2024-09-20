//QuestControl
//I want you to create a new mod called Quest Control for rimworld using C# and XML as necessary.
//Here is what it should do:
//1. I want the mod to collect all of the different QuestScriptDefs from rimworld's def dictionary and put them in a dictionary called QuestControlQuestList.
//2. I want mod options labeled "Quest Control" that will appear in the Rimworld game menu, that will list all of the different QuestScriptDefs by their label, with a radio button next to each different one allowing the user to enable or disable that type of quest. 
//3. I want the mod options written to the config file when the user clicks on a save button at the bottom of the options.
//4. I want to make a harmony prefix patch that will disable or enable the QuestScriptDefs according to the config file using "com.joeownage.questcontrol".

//CollectQuestScriptDefs.cs
using System.Collections.Generic;
using RimWorld;
using Verse;

public class QuestControl : Mod
{
    public static Dictionary<string, QuestScriptDef> QuestControlQuestList = new Dictionary<string, QuestScriptDef>();

    public QuestControl(ModContentPack content) : base(content)
    {
        foreach (var def in DefDatabase<QuestScriptDef>.AllDefs)
        {
            QuestControlQuestList[def.defName] = def;
        }
    }
}

//CreateModOptions.cs
using UnityEngine;
using Verse;

public class QuestControlSettings : ModSettings
{
    public Dictionary<string, bool> questEnabled = new Dictionary<string, bool>();

    public override void ExposeData()
    {
        base.ExposeData();
        Scribe_Collections.Look(ref questEnabled, "questEnabled", LookMode.Value, LookMode.Value);
    }

    public void Save()
    {
        LoadedModManager.GetMod<QuestControlMod>().WriteSettings();
    }
}

public class QuestControlMod : Mod
{
    public static QuestControlSettings settings;

    public QuestControlMod(ModContentPack content) : base(content)
    {
        settings = GetSettings<QuestControlSettings>();
    }

    public override void DoSettingsWindowContents(Rect inRect)
    {
        Listing_Standard listingStandard = new Listing_Standard();
        listingStandard.Begin(inRect);
        listingStandard.Label("Quest Control");

        foreach (var quest in QuestControl.QuestControlQuestList)
        {
            bool enabled = settings.questEnabled.ContainsKey(quest.Key) ? settings.questEnabled[quest.Key] : true;
            listingStandard.CheckboxLabeled(quest.Value.label, ref enabled);
            settings.questEnabled[quest.Key] = enabled;
        }

        if (listingStandard.ButtonText("Save"))
        {
            settings.Save();
        }

        listingStandard.End();
        base.DoSettingsWindowContents(inRect);
    }

    public override string SettingsCategory() => "Quest Control";
}


//HarmonyPrefixPatch.cs
using HarmonyLib;
using Verse;

[StaticConstructorOnStartup]
public static class QuestControlPatcher
{
    static QuestControlPatcher()
    {
        var harmony = new Harmony("com.joeownage.questcontrol");
        harmony.PatchAll();
    }
}

[HarmonyPatch(typeof(QuestUtility), "GenerateQuestAndMakeAvailable")]
public static class QuestUtility_GenerateQuestAndMakeAvailable_Patch
{
    public static bool Prefix(QuestScriptDef questDef)
    {
        if (QuestControlMod.settings.questEnabled.TryGetValue(questDef.defName, out bool enabled))
        {
            return enabled;
        }
        return true;
    }
}



