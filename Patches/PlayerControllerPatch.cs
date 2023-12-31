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

        [HarmonyPatch("Interact_performed")]
        [HarmonyPrefix]
        static void HandsNeverFull(ref bool ___twoHanded)
        {
            ___twoHanded = false;

        }

        [HarmonyPatch("SetHoverTipAndCurrentInteractTrigger")]
        [HarmonyPrefix]
        static void HandofGodPatch(ref TextMeshProUGUI ___cursorTip, ref Camera ___gameplayCamera, ref Ray ___interactRay, ref RaycastHit ___hit, ref float ___grabDistance, ref int ___interactableObjectsMask)
        {
            
            ___interactRay = new Ray(___gameplayCamera.transform.position, ___gameplayCamera.transform.forward);

            if (Physics.Raycast(___interactRay, out ___hit, ___grabDistance, ___interactableObjectsMask) && ___hit.collider.gameObject.layer != 8)
            {
                Console.WriteLine(___hit.collider.gameObject.layer);
                Console.WriteLine("LOOKING AT OBJECTT" + ___hit.collider.gameObject.name);
                int layer = ((!true) ? 23 : 19);
                if (___hit.collider.gameObject.layer == layer)
                {
                    ___cursorTip.text = "KILL";
                    RaycastHit[] enemyColliders = new RaycastHit[10];
                    UnityEngine.Debug.Log(___hit.collider.name);
                    IHittable component;
                    PlayerControllerB playerwhohit = new PlayerControllerB();
                    ___hit.collider.transform.TryGetComponent<IHittable>(out component);
                    Vector3 vector3 = new Vector3(-0.0555f, 0.1469f, -0.0655f);
                    component.Hit(10, vector3, playerwhohit, playHitSFX: true);
                }
                
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
                ___health = 100;
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

        [HarmonyPatch("Awake")]
        [HarmonyPostfix]
        static void BornWithShotGun(ref GrabbableObject[] ___ItemSlots)
        {
            ShotgunItem shotgun = new ShotgunItem();
            ___ItemSlots[0] = shotgun;
        }

    }
}
