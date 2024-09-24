//QuestControlPatcher.cs
using HarmonyLib;
using Verse;
using RimWorld;
using RimWorld.QuestGen;

[StaticConstructorOnStartup]
public static class QuestControlPatcher
{
    static QuestControlPatcher()
    {
        var harmony = new Harmony("com.joeownage.questcontrol");
        harmony.PatchAll();
    }
}