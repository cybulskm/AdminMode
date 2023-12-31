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
            __result = true;
            return false;
        }
        /*
        [HarmonyPostfix]
        [HarmonyPatch("UpdateScanNodes")]
        static void HandOfGOd(ref RectTransform[] ___scanElements, ref Dictionary<RectTransform, ScanNodeProperties> ___scanNodes)
        {
            for (int i = 0;  i < ___scanElements.Length; i++)
            {
                if (___scanNodes.TryGetValue(___scanElements[i], out var value) && value != null)
                {
                    Console.WriteLine(value.gameObject.name);
                }

            }

        }
        */

    }
}
