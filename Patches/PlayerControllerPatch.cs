using GameNetcodeStuff;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace AdminMode.Patches
{
    [HarmonyPatch(typeof(PlayerControllerB))]
    internal class PlayerControllerPatch
    {
        [HarmonyPatch("Update")]
        [HarmonyPostfix]
        static void InfiniteUpdatePatch(ref float ___sprintMeter, ref bool ___isPlayerDead, 
            ref float ___drunkness, ref bool ___jetpackControls, ref bool ___isSinking, 
            ref Light ___helmetLight, ref Light[] ___allHelmetLights,
            ref Light ___nightVision, ref float ___insanityLevel, ref float ___sinkingValue)
        {
            if (TerminalInterfacePatch.changeText)
            {
                //Console.WriteLine("Set sprint to infinite");
                ___sprintMeter = 1f;
                //Console.WriteLine("Never let player die");
                ___isPlayerDead = false;
                //Console.WriteLine("Set drunkness to 0");
                ___drunkness = 0;
                //___jetpackControls = true;
                ___isSinking = false;
                ___allHelmetLights[0].enabled = true;
                ___helmetLight = ___allHelmetLights[1];
                ___nightVision.enabled = true;
                ___insanityLevel = 0;
                ___sinkingValue = 0;
            }
            
            

        }

        [HarmonyPatch("SetFaceUnderwaterFilters")]
        [HarmonyPostfix]
        static void DrownPatch()
        {
            if (TerminalInterfacePatch.changeText)
            {
                StartOfRound.Instance.drowningTimer = 1;

            }
        }
        


        [HarmonyPatch(nameof(PlayerControllerB.DamagePlayer))]
        [HarmonyPrefix]
        static void InfiniteHealthPatch(ref int ___health, ref int damageNumber)
        {
            if (TerminalInterfacePatch.changeText)
            {
                //Console.WriteLine("Set health to infinite");
                ___health = 100;
                //Console.WriteLine("Set damage to 0");
                damageNumber = 0;
            }
            
            

        }
        [HarmonyPatch(nameof(PlayerControllerB.KillPlayer))]
        [HarmonyPrefix]
        static bool StopDeathPatch(ref bool ___isPlayerDead)
        {
            if (TerminalInterfacePatch.changeText)
            {
                UnityEngine.Debug.Log("Killed Player?!");

                ___isPlayerDead = false;
                return false;

            }
            return true;
           

        }

    }
}
