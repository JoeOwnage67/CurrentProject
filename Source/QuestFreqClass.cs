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