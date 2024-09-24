//QuestControlMod.cs

using UnityEngine;
using Verse;
using RimWorld;

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

        if (QuestControl.QuestControlQuestList.Count == 0)
        {
            listingStandard.Label("No quests found.");
        }
        else
        {
            foreach (var quest in QuestControl.QuestControlQuestList)
            {
                bool enabled = settings.questEnabled.ContainsKey(quest.Key) ? settings.questEnabled[quest.Key] : true;
                listingStandard.CheckboxLabeled(quest.Key, ref enabled);
                settings.questEnabled[quest.Key] = enabled;
            }

            if (listingStandard.ButtonText("Save"))
            {
                settings.Save();
            }
        }

        listingStandard.End();
        base.DoSettingsWindowContents(inRect);
    }

    public override string SettingsCategory() => "Quest Control";
}