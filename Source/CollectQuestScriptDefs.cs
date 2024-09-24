// CollectQuestScriptDefs.cs
using System.Collections.Generic;
using RimWorld;
using Verse;

[StaticConstructorOnStartup]
public static class QuestControl
{
    public static Dictionary<string, QuestScriptDef> QuestControlQuestList = new Dictionary<string, QuestScriptDef>();

    static QuestControl()
    {
        foreach (var def in DefDatabase<QuestScriptDef>.AllDefs)
        {
            QuestControlQuestList[def.defName] = def;
        }
    }
}