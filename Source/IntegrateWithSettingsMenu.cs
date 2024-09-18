public class QuestFrequencyMod : Mod//QuestFrequencyMod: This class inherits from Mod, which is likely a base class provided by RimWorld for creating mods.
{
    public static QuestSettings settings;//settings: A static variable of type QuestSettings that will hold the mod’s settings.

    public QuestFrequencyMod(ModContentPack content) : base(content)//Takes a ModContentPack object as a parameter and passes it to the base class constructor.
    {
        settings = GetSettings<QuestSettings>();//Initializes the settings variable with the mod’s settings.
    }

    public override void DoSettingsWindowContents(Rect inRect)// This method is overridden to provide custom settings UI.
    {
        Listing_Standard listingStandard = new Listing_Standard();// Used to create a list of UI elements.
        listingStandard.Begin(inRect);//Starts the listing within the given rectangle.

        var allQuestDefs = QuestLoader.GetAllQuestDefs();//Retrieves all quest definitions.
        foreach (var questDef in allQuestDefs)//foreach loop: Iterates through each quest definition.   
        {
            bool enabled = settings.questEnabled.TryGetValue(questDef.defName, out bool value) ? value : true;//TryGetValue: Checks if the quest is enabled in the settings.
            listingStandard.CheckboxLabeled(questDef.label, ref enabled);//CheckboxLabeled: Creates a checkbox for each quest.
            settings.questEnabled[questDef.defName] = enabled;//settings.questEnabled[questDef.defName]: Updates the settings based on the checkbox state.
        }

        listingStandard.End();//End(): Ends the listing.
        base.DoSettingsWindowContents(inRect);//Calls the base class method.
    }

    public override string SettingsCategory() => "Quest Frequency Control";//Returns the name of the settings category as “Quest Frequency Control”.
}
