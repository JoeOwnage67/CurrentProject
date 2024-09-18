public class QuestSettings : ModSettings
{
    public Dictionary<string, bool> questEnabled = new Dictionary<string, bool>();

    public override void ExposeData()
    {
        Scribe_Collections.Look(ref questEnabled, "questEnabled", LookMode.Value, LookMode.Value);
        base.ExposeData();
    }
}
