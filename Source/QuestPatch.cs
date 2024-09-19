using HarmonyLib;
using Verse;
using RimWorld;

[HarmonyPatch(typeof(QuestManager))]
[HarmonyPatch("Add")]
public static class QuestManager_Add_Patch
{
    static bool Prefix(Quest quest)
    {
        Log.Message($"Patching Add for {quest.ToStringSafe()}");
        if (QuestFrequencyMod.settings.questEnabled.TryGetValue(quest.def.defName, out bool enabled) && !enabled)
        {
            Log.Message($"Quest {quest.def.defName} is disabled by settings.");
            return false;
        }
        return true;
    }
}
