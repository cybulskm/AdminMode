using GameNetcodeStuff;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminMode.Patches
{
    [HarmonyPatch(typeof(PlayerControllerB))]
    internal class MakeUserAdminPatch
    {
        [HarmonyPatch("Update")]
        [HarmonyPostfix]
        static void infiniteUpdatePatch(ref float ___sprintMeter, ref bool ___isPlayerDead, ref float ___drunkness)
        {
            Console.WriteLine("Set sprint to infinite");
            ___sprintMeter = 1f;
            Console.WriteLine("Never let player die");
            ___isPlayerDead = false;
            Console.WriteLine("Set drunkness to 0");
            ___drunkness = 0;

        }
        [HarmonyPatch(nameof(PlayerControllerB.DamagePlayer)]
        [HarmonyPrefix]
        static void infiniteHealthPatch(ref int ___health, ref bool ___AllowPlayerDeath, ref int ___damageNumber)
        {
            Console.WriteLine("Set health to infinite");
            ___health = 100;
            Console.WriteLine("Set damage to 0");
            ___damageNumber = 0;
            Console.WriteLine($"Damage taken: {___damageNumber}");

        }

    }
}
