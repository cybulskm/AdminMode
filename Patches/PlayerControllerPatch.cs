using GameNetcodeStuff;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.HID;

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
            ref Light ___nightVision, ref float ___insanityLevel, ref float ___sinkingValue,
            ref float ___jumpForce)
        {
            
            if (TerminalInterfacePatch.ImprovedStats)
            {
                ___sprintMeter = 1f;
                ___isPlayerDead = false;
                ___drunkness = 0;
                //___jetpackControls = true;
                ___isSinking = false;
                ___allHelmetLights[0].enabled = true;
                ___helmetLight = ___allHelmetLights[1];
                ___nightVision.enabled = true;
                ___insanityLevel = 0;
                ___sinkingValue = 0;
                ___jumpForce = 20f;
            }
        }

        [HarmonyPatch("Interact_performed")]
        [HarmonyPrefix]
        static void HandsNeverFull(ref bool ___twoHanded)
        {
            if (TerminalInterfacePatch.TwoHands)
            {
                ___twoHanded = false;
            }

        }

        

        [HarmonyPatch("SetFaceUnderwaterFilters")]
        [HarmonyPostfix]
        static void DrownPatch()
        {
            if (TerminalInterfacePatch.CantDie)
            {
                StartOfRound.Instance.drowningTimer = 1;

            }
        }
        


        [HarmonyPatch(nameof(PlayerControllerB.DamagePlayer))]
        [HarmonyPrefix]
        static void InfiniteHealthPatch(ref int ___health, ref int damageNumber)
        {
            if (TerminalInterfacePatch.CantDie)
            {
                ___health = 100;
                damageNumber = 0;
            }
            
            

        }
        [HarmonyPatch(nameof(PlayerControllerB.KillPlayer))]
        [HarmonyPrefix]
        static bool StopDeathPatch(ref bool ___isPlayerDead)
        {
            if (TerminalInterfacePatch.CantDie)
            {
                ___isPlayerDead = false;
                return false;

            }
            return true;
           

        }

        

    }
}
