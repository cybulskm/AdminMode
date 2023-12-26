using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminMode.Patches
{
    [HarmonyPatch(typeof(GrabbableObject))]

    //Set company buying rate to 100 always
    internal class BatteryUpdate
    {
        [HarmonyPatch(nameof(GrabbableObject.Update))]
        [HarmonyPrefix]
        static void InfiniteBattery(ref Battery ___insertedBattery)
        {
            if (TerminalInterfacePatch.changeText)
            {
                ___insertedBattery.charge = 1;
            }
        }

    }
}
