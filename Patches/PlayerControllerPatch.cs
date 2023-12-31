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
            ref Light ___nightVision, ref float ___insanityLevel, ref float ___sinkingValue, ref GrabbableObject[] ___ItemSlots,
            ref TextMeshProUGUI ___cursorTip, ref Camera ___gameplayCamera, ref RaycastHit ___hit, ref float ___jumpForce)
        {
            //ShotgunItem shotgun = new ShotgunItem();
            //ShotgunItem shotgun1 = UnityEngine.Object.Instantiate(shotgun);

            //shotgun1.EquipItem();
            if (TerminalInterfacePatch.ImprovedStats)
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
                ___jumpForce = 10f;
                int layer = (!true) ? 23 : 19;
                Ray ray = new Ray(___gameplayCamera.transform.position, ___gameplayCamera.transform.forward);
                if (Physics.Raycast(ray, out ___hit, 30f))
                {
                    ___cursorTip.text = ___hit.collider.gameObject.layer.ToString();
                    if (___hit.collider.gameObject.layer == layer)
                    {
                        Console.WriteLine("WHAT2");
                        ___cursorTip.text = "KILL";
                        UnityEngine.Debug.Log(___hit.collider.name);
                        PlayerControllerB playerwhohit = new PlayerControllerB();
                        ___hit.collider.transform.TryGetComponent<IHittable>(out IHittable component);
                        Vector3 vector3 = new Vector3(-0.0555f, 0.1469f, -0.0655f);
                        component.Hit(10, vector3, playerwhohit, playHitSFX: true);
                    }
                }
                foreach (var item in ___ItemSlots)
                {
                    if (item is null ){

                    }
                }
                
               


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
