using UnityEngine;
using Verse;
using RimWorld;
using RimWorld.QuestGen;

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
        Write();
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
