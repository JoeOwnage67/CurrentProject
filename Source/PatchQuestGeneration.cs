using HarmonyLib; // For Harmony patches
using Verse; // Ensure this namespace is included

[HarmonyPatch(typeof(QuestManager), "GenerateQuest")] // This attribute indicates that the class QuestManager_GenerateQuest_Patch is a Harmony patch targeting the GenerateQuest method of the QuestManager class.
public static class QuestManager_GenerateQuest_Patch // Defines a static class for the patch.
{
    static bool Prefix(QuestScriptDef questDef, ref bool __result) // This method is a prefix patch for the GenerateQuest method. It runs before the original method and can prevent it from executing.
    {
        if (QuestFrequencyMod.settings.questEnabled.TryGetValue(questDef.defName, out bool enabled) && !enabled)
        // "QuestFrequencyMod.settings.questEnabled.TryGetValue(questDef.defName, out bool enabled)": "QuestFrequencyMod.settings.questEnabled": This accesses the questEnabled dictionary from the settings object of the QuestFrequencyMod class. "TryGetValue(questDef.defName, out bool enabled)": This method tries to get the value associated with the key questDef.defName from the questEnabled dictionary. "questDef.defName": This is the name of the quest definition. "out bool enabled": If the key exists, the value (a boolean indicating whether the quest is enabled) is stored in the enabled variable. If the key does not exist, enabled is set to false. "&& !enabled": &&: This is the logical AND operator. It combines two conditions and returns true only if both conditions are true. "!enabled": This checks if enabled is false. The ! operator negates the value of enabled. Combined Logic - The entire condition checks if: The questEnabled dictionary contains an entry for questDef.defName. The value associated with questDef.defName is false (meaning the quest is disabled). If both conditions are true, the if statement’s body will execute, which in this case prevents the quest from being generated.
        {
            __result = false;
            return false;
        }
        return true;
    }
}
