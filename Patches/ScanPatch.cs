using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminMode.Patches
{
    [HarmonyPatch(typeof(ScanNodeProperties))]

    //Set company buying rate to 100 always
    internal class ScanPatch
    {
        [HarmonyPatch("ScanNodeProperties")]
        [HarmonyPostfix]
        static void InfiniteScan(ref int ___maxRange, ref int __minRange, ref bool ___requiresLineOfSight)
        {
            ___maxRange = 100;
            ___maxRange = 100;
            ___requiresLineOfSight = false;
        }

    }
}
