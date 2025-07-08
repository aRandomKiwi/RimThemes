using RimWorld;
using RimWorld.Planet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using UnityEngine;
using Verse;
using System.IO;
using Verse.Sound;
using HarmonyLib;

namespace aRandomKiwi.RimThemes
{
    /*[HarmonyPatch(typeof(GlobalControlsUtility), "DoDate")]
    class GlobalControlsUtility_DoDate_Patch
    {
        [HarmonyPrefix]
        static bool ListenerPrefix()
        {
            if (Settings.enableAccessibilityMode)
                Utils.tempEnableAccessibilityMode = true;
            return true;
        }

        [HarmonyPostfix]
        static void ListenerPostfix()
        {
            if (Settings.enableAccessibilityMode)
                Utils.tempEnableAccessibilityMode = false;
        }
    }

    [HarmonyPatch(typeof(GlobalControlsUtility), "DoRealtimeClock")]
    class GlobalControlsUtility_DoRealtimeClock_Patch
    {
        [HarmonyPrefix]
        static bool ListenerPrefix()
        {
            if (Settings.enableAccessibilityMode)
                Utils.tempEnableAccessibilityMode = true;
            return true;
        }

        [HarmonyPostfix]
        static void ListenerPostfix()
        {
            if (Settings.enableAccessibilityMode)
                Utils.tempEnableAccessibilityMode = false;
        }
    }

    [HarmonyPatch(typeof(GlobalControlsUtility), "DoPlaySettings")]
    class GlobalControlsUtility_DoPlaySettings_Patch
    {
        [HarmonyPrefix]
        static bool ListenerPrefix()
        {
            if(Settings.enableAccessibilityMode)
                Utils.tempEnableAccessibilityMode = true;
            return true;
        }

        [HarmonyPostfix]
        static void ListenerPostfix()
        {
            if (Settings.enableAccessibilityMode)
                Utils.tempEnableAccessibilityMode = false;
        }
    }*/

    [HarmonyPatch(typeof(ResourceReadout), "ResourceReadoutOnGUI")]
    class ResourceReadout_ResourceReadoutOnGUI_Patch
    {
        [HarmonyPrefix]
        static bool ListenerPrefix()
        {
            if (Settings.enableAccessibilityMode)
                Utils.tempEnableAccessibilityMode = true;
            return true;
        }

        [HarmonyPostfix]
        static void ListenerPostfix()
        {
            if (Settings.enableAccessibilityMode)
                Utils.tempEnableAccessibilityMode = false;
        }
    }

    [HarmonyPatch(typeof(GlobalControls), "GlobalControlsOnGUI")]
    class GlobalControls_GlobalControlsOnGUI_Patch
    {
        [HarmonyPrefix]
        static bool ListenerPrefix()
        {
            if (Settings.enableAccessibilityMode)
                Utils.tempEnableAccessibilityMode = true;
            return true;
        }

        [HarmonyPostfix]
        static void ListenerPostfix()
        {
            if (Settings.enableAccessibilityMode)
                Utils.tempEnableAccessibilityMode = false;
        }
    }

    /*[HarmonyPatch(typeof(MouseoverReadout), "MouseoverReadoutOnGUI")]
    class MouseoverReadout_MouseoverReadoutOnGUI_Patch
    {
        [HarmonyPrefix]
        static bool ListenerPrefix()
        {
            if (Settings.enableAccessibilityMode)
                Utils.tempEnableAccessibilityMode = true;
            return true;
        }

        [HarmonyPostfix]
        static void ListenerPostfix()
        {
            if (Settings.enableAccessibilityMode)
                Utils.tempEnableAccessibilityMode = false;
        }
    }*/
    

}
