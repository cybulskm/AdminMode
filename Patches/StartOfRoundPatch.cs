using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminMode.Patches
{
    [HarmonyPatch(typeof(StartOfRound))]

    internal class StartOfRoundPatch
    {
        [HarmonyPatch("LoadUnlockables")]
        [HarmonyPrefix]
        static void SpawnShotGun(ref List<UnlockableItem> ___unlockables)
        {
            for (int i = 0; i< ___unlockables.Count; i++)
            {
                ___unlockables[i].hasBeenUnlockedByPlayer = true;
                Debug.WriteLine(___unlockables[i].unlockableName);
            }
        }

    }
}
