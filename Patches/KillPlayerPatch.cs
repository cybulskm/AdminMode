using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminMode.Patches
{
    [HarmonyPatch(typeof(KillLocalPlayer))]
    internal class KillPlayerPatch
    {
        [HarmonyPatch(nameof(KillLocalPlayer.KillPlayer))]
        [HarmonyPrefix]
        static void DontKillPlayer(ref int __return)
        {
            if (1 == 1)
            {
                __return = 0;
                return;
            }
        }
    }
}
