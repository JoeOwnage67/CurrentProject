//QuestUtilityPatch.cs

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