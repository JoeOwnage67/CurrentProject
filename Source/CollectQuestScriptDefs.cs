using System.Collections.Generic;
using RimWorld;
using Verse;

public class QuestControl : Mod
{
    public static Dictionary<string, QuestScriptDef> QuestControlQuestList = new Dictionary<string, QuestScriptDef>();

    public QuestControl(ModContentPack content) : base(content)
    {
        foreach (var def in DefDatabase<QuestScriptDef>.AllDefs)
        {
            QuestControlQuestList[def.defName] = def;
        }
    }
}
