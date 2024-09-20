//CurrentProject
//the current thing I am developing

//QuestSettings.cs
using System.Collections.Generic;
using Verse;

public class QuestSettings : ModSettings
{
    public Dictionary<string, bool> questEnabled = new Dictionary<string, bool>(); 

    public override void ExposeData() 
    {
        Scribe_Collections.Look(ref questEnabled, "questEnabled", LookMode.Value, LookMode.Value);
        base.ExposeData();
    }
}

// /QuestFrequencyMod.cs
using System.Collections.Generic;
using UnityEngine;
using Verse;
using HarmonyLib;

public class QuestFrequencyMod : Mod
{
    public static QuestSettings settings;

    public QuestFrequencyMod(ModContentPack content) : base(content)
    {
        settings = GetSettings<QuestSettings>();
        
        var harmony = new Harmony("com.joeownage.questcontrol");
        harmony.PatchAll();
		
		// Log the methods of QuestManager
		MethodLogger.LogMethods();
    }

    public override void DoSettingsWindowContents(Rect inRect)
    {
        Listing_Standard listingStandard = new Listing_Standard();
        listingStandard.Begin(inRect);

        var allQuestDefs = QuestLoader.GetAllQuestDefs();
        foreach (var questDef in allQuestDefs) 
        {
            if (!settings.questEnabled.TryGetValue(questDef.defName, out bool enabled))
            {
                enabled = true;
            }
            listingStandard.CheckboxLabeled(questDef.label, ref enabled);
            settings.questEnabled[questDef.defName] = enabled;
        }

        listingStandard.End();
        base.DoSettingsWindowContents(inRect);
    }

    public override string SettingsCategory() => "Quest Control";
}

//QuestLoader.cs
using System.Collections.Generic;
using Verse;
using RimWorld;

public static class QuestLoader
{
    public static List<QuestScriptDef> GetAllQuestDefs()
    {
        return DefDatabase<QuestScriptDef>.AllDefsListForReading;
    }
}

//CustomQuestGen.cs
using RimWorld;
using Verse;

public static class CustomQuestGen
{
    public static void GenerateQuest(QuestScriptDef questScriptDef, Slate slate)
    {
        if (QuestFrequencyMod.settings.questEnabled.TryGetValue(questScriptDef.defName, out bool enabled) && !enabled)
        {
            Log.Message($"Quest {questScriptDef.defName} is disabled by settings.");
            return;
        }

        Quest quest = QuestGen.Generate(questScriptDef, slate);
        Find.QuestManager.Add(quest);
    }
}

//QuestManager_Add_Patch.cs
using HarmonyLib;
using Verse;
using RimWorld;

[HarmonyPatch(typeof(QuestManager))]
[HarmonyPatch("Add")]
public static class QuestManager_Add_Patch
{
    static bool Prefix(Quest quest)
    {
        var slate = new Slate();
        CustomQuestGen.GenerateQuest(quest.def, slate);
        return false; // Prevent the original method from running
    }
}


