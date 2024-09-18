public class QuestSettings : ModSettings //This indicates that QuestSettings inherits from the ModSettings class, which is part of RimWorld’s modding API. Inheriting from ModSettings allows this class to handle saving and loading mod settings.
    public Dictionary<string, bool> questEnabled = new Dictionary<string, bool>(); //"public Dictionary<string, bool> questEnabled": This creates a public dictionary named questEnabled. "Dictionary<string, bool>": The dictionary uses string as the key type and bool as the value type. The key represents the quest’s name, and the value indicates whether the quest is enabled (true) or disabled (false).

    public override void ExposeData()//This method overrides the ExposeData method from the ModSettings class. ExposeData is used to save and load data.
    {
        Scribe_Collections.Look(ref questEnabled, "questEnabled", LookMode.Value, LookMode.Value);//This line uses the Scribe_Collections.Look method to handle the serialization and deserialization of the questEnabled dictionary. ref questEnabled: Passes the questEnabled dictionary by reference. “questEnabled”: The label used to identify this data in the save file. LookMode.Value: Specifies that both the keys and values in the dictionary should be treated as simple values (not complex objects).
        base.ExposeData(); //Calls the base class’s ExposeData method to ensure any additional data managed by the base class is also saved and loaded.
    }
}
