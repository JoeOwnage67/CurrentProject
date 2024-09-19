using HarmonyLib;
using Verse;
using RimWorld;

[HarmonyPatch(typeof(QuestManager), "GenerateQuest")]
public static class QuestManager_GenerateQuest_Patch
{
    static bool Prefix(QuestScriptDef questDef, ref bool __result)
    {
        if (QuestFrequencyMod.settings.questEnabled.TryGetValue(questDef.defName, out bool enabled) && !enabled)
        {
            __result = false;
            return false;
        }
        return true;
    }
}