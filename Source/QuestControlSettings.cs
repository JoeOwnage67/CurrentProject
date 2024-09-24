// QuestControlSettings.cs
using UnityEngine;
using Verse;
using RimWorld;
using System.Collections.Generic;

public class QuestControlSettings : ModSettings
{
    public Dictionary<string, bool> questEnabled = new Dictionary<string, bool>();

    public override void ExposeData()
    {
        base.ExposeData();
        Scribe_Collections.Look(ref questEnabled, "questEnabled", LookMode.Value, LookMode.Value);
    }

    public void Save()
    {
        Write();
    }
}

