public static class QuestLoader
{
    public static List<QuestScriptDef> GetAllQuestDefs()
    {
        return DefDatabase<QuestScriptDef>.AllDefsListForReading;
    }
}
