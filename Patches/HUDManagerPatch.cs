using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace AdminMode.Patches
{
    [HarmonyPatch(typeof(HUDManager))]

    //Set company buying rate to 100 always
    internal class HUDManagerPatch
    {
        [HarmonyPrefix]
        [HarmonyPatch("MeetsScanNodeRequirements")]
        static bool InfiniteScan(ref bool __result)
        {
            if (TerminalInterfacePatch.ImprovedStats)
            {
                __result = true;
                return false;
            }
            return true;

        }
    

    }
}
