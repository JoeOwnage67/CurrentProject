using HarmonyLib;
using Verse;
using RimWorld;

[HarmonyPatch(typeof(QuestManager))]
[HarmonyPatch("GenerateQuest")]
public static class QuestManager_GenerateQuest_Patch
{
    static bool Prefix(QuestScriptDef questDef, ref bool __result)
    {
        Log.Message($"Patching GenerateQuest for {questDef.defName}");
        if (QuestFrequencyMod.settings.questEnabled.TryGetValue(questDef.defName, out bool enabled) && !enabled)
        {
            __result = false;
            return false;
        }
        return true;
    }
}
