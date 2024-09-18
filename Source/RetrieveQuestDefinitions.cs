using System.Collections.Generic;
using Verse; // Ensure this namespace is included

public static class QuestLoader // This is a static class, meaning it cannot be instantiated and all its members must also be static.
{
    public static List<QuestScriptDef> GetAllQuestDefs() // This static method returns a list of QuestScriptDef objects.
    {
        return DefDatabase<QuestScriptDef>.AllDefsListForReading; // This line accesses the DefDatabase class, which is a generic class that manages definitions (Defs) in RimWorld. DefDatabase<QuestScriptDef> specifically refers to the database of QuestScriptDef objects. AllDefsListForReading is a property that returns a list of all QuestScriptDef objects available in the game.
    }
}
