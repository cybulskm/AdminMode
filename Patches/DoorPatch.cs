using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminMode.Patches
{
    [HarmonyPatch(typeof(DoorLock))]

    //Set company buying rate to 100 always
    internal class DoorPatch
    {
        [HarmonyPostfix]
        [HarmonyPatch("Awake")]
        static void UnlockAllDoors(ref float ___lockPickTimeLeft)
        {
            ___lockPickTimeLeft = 0;   
        }

    }
}
