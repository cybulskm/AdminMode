using DunGen;
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
        [HarmonyPatch(nameof(DoorLock.LockDoor))]
        [HarmonyPrefix]

        static bool LockDoor(ref InteractTrigger ___doorTrigger, ref bool ___isLocked, ref DoorLock ___twinDoor)
        {
            
            ___doorTrigger.interactable = true;
            ___doorTrigger.hoverTip = "No longer locked";
            ___isLocked = false;
            //not set to insance of object
            if (___twinDoor != null)
            {
                ___twinDoor.isLocked = false;
            }
            return false;

        }

    }
}
