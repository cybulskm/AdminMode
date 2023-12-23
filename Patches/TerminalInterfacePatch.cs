﻿using BepInEx.Logging;
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
        static bool changeText = false;

        [HarmonyPatch("Update")]
        [HarmonyPostfix]
        static void AddMenu(ref TextMeshProUGUI ___topRightText)
        {
            if (changeText)
            {
                ___topRightText.text += "ADMIN";

            }
            else
            {
                ___topRightText.text += "USER";
            }
        }




        [HarmonyPatch("ParsePlayerSentence")]
        [HarmonyPrefix]
        static bool DisplayCommands(ref int ___textAdded, ref TMP_InputField ___screenText, ref TerminalNodesList ___terminalNodes, ref TerminalNode __result)
        {
            Console.WriteLine("-------------------------------");
            TerminalNode terminalNode = ScriptableObject.CreateInstance<TerminalNode>();
            TerminalKeyword terminalKey = ScriptableObject.CreateInstance<TerminalKeyword>();

            terminalNode.displayText = "ADMIN MODE ACTIVATED:\nQUOTA:\nSet your quoate amount\nSCRAP:\nSet the amount of scrap you have\n";
            terminalNode.terminalEvent = "admin";
            terminalNode.name = "Admin";
            terminalKey.word = "admin";


            ___terminalNodes.terminalNodes.Add(terminalNode);
            ___terminalNodes.allKeywords.AddToArray(terminalKey);
            ___terminalNodes.allKeywords.Append(terminalKey);
            ___terminalNodes.terminalNodes.Append(terminalNode);
            ___terminalNodes.specialNodes.Add(terminalNode);

            /*

            for (int i = 0; i < ___terminalNodes.allKeywords.Length; i++)
            {
                Console.WriteLine("Keyword: " + i + " "+ ___terminalNodes.allKeywords[i].word);
            }
            for (int d = 0; d < ___terminalNodes.specialNodes.Count; d++)
            {
                Console.WriteLine("Special Node:" + d + " "+ ___terminalNodes.specialNodes[d].name);

            }
            */
            string s = ___screenText.text.Substring(___screenText.text.Length - ___textAdded);

            if (s == "admin")
            {
                changeText = true;
                Console.WriteLine("Admin mode has been activated!");
                Console.WriteLine("TERMINAL EVENT:");
                Console.WriteLine(___terminalNodes.specialNodes[13].terminalEvent.ToString());
                Console.WriteLine("TERMINAL TEXT:");
                Console.WriteLine(___terminalNodes.specialNodes[13].displayText);
                Console.WriteLine("TERMINAL OPTIONS:");
                Console.WriteLine(___terminalNodes.specialNodes[13].terminalOptions.ToString());


                __result = ___terminalNodes.specialNodes[24];
                return false;

            }
            if (s == "user")
            {
                Console.WriteLine("Admin mode has been de-activated!");

                changeText = false;
            }


            Console.WriteLine("-------------------------------");
            return true;
        }

    }
}