using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminMode.Patches
{
    [HarmonyPatch(typeof(HUDManager))]

    //Set company buying rate to 100 always
    internal class ScanPatch
    {
        [HarmonyPrefix]
        [HarmonyPatch("MeetsScanNodeRequirements")]
        static bool InfiniteScan(ref bool __result)
        {
            __result = true;
            return false;
        }

    }
}
