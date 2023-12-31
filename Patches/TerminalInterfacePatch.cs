using BepInEx.Logging;
using HarmonyLib;
using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

namespace AdminMode.Patches
{
    [HarmonyPatch(typeof(Terminal))]
    internal class TerminalInterfacePatch
    {
        public static bool changeText = false;

        [HarmonyPatch("Update")]
        [HarmonyPostfix]
        static void AddMenu(ref TextMeshProUGUI ___topRightText, ref int ___groupCredits)
        {
            if (changeText)
            {
                ___topRightText.text += "ADMIN";
                ___groupCredits = 999;

            }
            else
            {
                ___topRightText.text += "USER";
                
            }
        }

        [HarmonyPatch("LoadNewNodeIfAffordable")]
        [HarmonyPrefix]
        static void AlwaysPurchase(ref int ___groupCredits, ref int ___totalCostOfItems)
        {
            if (changeText)
            {
                ___groupCredits = Mathf.Clamp(___groupCredits + ___totalCostOfItems, 0, 10000000);

            }
        }




        [HarmonyPatch("ParsePlayerSentence")]
        [HarmonyPrefix]
        static bool DisplayCommands(ref int ___textAdded, ref TMP_InputField ___screenText, ref TerminalNodesList ___terminalNodes, ref TerminalNode __result)
        {
            TerminalNode terminalNode = ScriptableObject.CreateInstance<TerminalNode>();
            TerminalKeyword terminalKey = ScriptableObject.CreateInstance<TerminalKeyword>();
            terminalNode.clearPreviousText = true;
            terminalNode.displayText = "ADMIN MODE ACTIVATED:PLACEHOLDER TEXT BELOW --IGNORE\n----------\n>FEATURES INCLUDED:\n-Infinite Sprint\n-Infinite Battery\n-Instant Kill\n-Infinite Credits\n-Infinite Scan\n-Untargetabble\n-Unkillable\n-Slower Days\n";
            terminalNode.terminalEvent = "admin";
            terminalNode.name = "Admin";
            terminalKey.word = "admin";
            ___terminalNodes.terminalNodes.Add(terminalNode);
            ___terminalNodes.allKeywords.AddToArray(terminalKey);
            ___terminalNodes.allKeywords.Append(terminalKey);
            ___terminalNodes.terminalNodes.Append(terminalNode);
            ___terminalNodes.specialNodes.Add(terminalNode);

            TerminalNode terminalNode1 = ScriptableObject.CreateInstance<TerminalNode>();
            terminalNode1.clearPreviousText = true;
            terminalNode1.displayText = "ADMIN MODE ALREADY ENABLED";
            terminalNode1.terminalEvent = "admin";


           
            
            string s = ___screenText.text.Substring(___screenText.text.Length - ___textAdded);

            if (s == "admin")
            {
                if (changeText == false)
                {
                    changeText = true;
                    Console.WriteLine("Admin mode has been activated!");
                    Console.WriteLine(___terminalNodes.specialNodes[24].name);
                    __result = ___terminalNodes.specialNodes[24];
                    return false;
                }
                

            }
            if (s == "user")
            {
                Console.WriteLine("Admin mode has been de-activated!");

                changeText = false;
            }
            return true;
        }

    }
}
