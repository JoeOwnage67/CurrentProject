public class QuestFrequencyMod : Mod
{
    public static QuestSettings settings;

    public QuestFrequencyMod(ModContentPack content) : base(content)
    {
        settings = GetSettings<QuestSettings>();
    }

    public override void DoSettingsWindowContents(Rect inRect)
    {
        Listing_Standard listingStandard = new Listing_Standard();
        listingStandard.Begin(inRect);

        var allQuestDefs = QuestLoader.GetAllQuestDefs();
        foreach (var questDef in allQuestDefs)
        {
            bool enabled = settings.questEnabled.TryGetValue(questDef.defName, out bool value) ? value : true;
            listingStandard.CheckboxLabeled(questDef.label, ref enabled);
            settings.questEnabled[questDef.defName] = enabled;
        }

        listingStandard.End();
        base.DoSettingsWindowContents(inRect);
    }

    public override string SettingsCategory() => "Quest Frequency Control";
}
