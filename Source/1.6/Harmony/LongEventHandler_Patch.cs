using RimWorld;
using RimWorld.Planet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using UnityEngine;
using Verse;
using Verse.Sound;
using HarmonyLib;

namespace aRandomKiwi.RimThemes
{
    [HarmonyPatch(typeof(ModSummaryWindow), "DrawWindow", new Type[] { typeof(Vector2), typeof(bool) }), StaticConstructorOnStartup]
    class ModSummaryWindow_DrawWindow_Patch
    {

        [HarmonyPrefix]
        static bool Prefix(Vector2 offset, bool useWindowStack)
        {
            if (Settings.disableCustomLoader)
                return true;
            else
                return false;
        }
    }
}
