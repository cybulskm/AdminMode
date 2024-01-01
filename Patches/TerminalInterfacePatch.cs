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
        public static bool AdminMode = false;
        public static bool TwoHands = false;
        public static bool CantDie = false;
        public static bool BuyingPower = false;
        public static bool ImprovedStats = false;
        public static bool HandOfGod = false;
        public static bool OpenDoors = false;
        public static bool[] patchBoolList = new bool[] { AdminMode, TwoHands, CantDie, BuyingPower, ImprovedStats, HandOfGod, OpenDoors};


        [HarmonyPatch("Update")]
        [HarmonyPostfix]
        static void AddMenu(ref TextMeshProUGUI ___topRightText, ref int ___groupCredits)
        {
            if (AdminMode || ___groupCredits == 999)
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
        [HarmonyPostfix]
        static void AlwaysPurchase(ref int ___groupCredits, ref int ___totalCostOfItems)
        {
            if (AdminMode)
            {
                ___groupCredits = ___groupCredits + ___totalCostOfItems;

            }
        }




        [HarmonyPatch("ParsePlayerSentence")]
        [HarmonyPrefix]
        static bool DisplayCommands(ref int ___textAdded, ref TMP_InputField ___screenText, ref TerminalNodesList ___terminalNodes, ref TerminalNode __result)
        {
            



            string s = ___screenText.text.Substring(___screenText.text.Length - ___textAdded);

            if (s == "admin")
            {
                __result = ___terminalNodes.specialNodes[24];
                if (AdminMode == false)
                {
                    AdminMode = true;
                    
                }
                return false;

            }
            if (s == "user")
            {
                Console.WriteLine("Admin mode has been de-activated!");

                AdminMode = false;
            }
            if (AdminMode == true)
            {
                //Ill make an array of words and bools later
                if (s == "unkillable")
                {
                    CantDie = true;
                    __result = ___terminalNodes.specialNodes[25];
                    return false;
                }
                if (s == "two hands")
                {
                    TwoHands = true;
                    __result = ___terminalNodes.specialNodes[26];
                    return false;
                }
                if (s == "improved stats")
                {
                    ImprovedStats = true;
                    __result = ___terminalNodes.specialNodes[27];
                    return false;
                }
                if (s == "open doors")
                {
                    OpenDoors = true;
                    __result = ___terminalNodes.specialNodes[28];
                    return false;
                }
                if (s == "hand of god")
                {
                    HandOfGod = true;
                    __result = ___terminalNodes.specialNodes[29];
                    return false;
                }
                if (s == "buying power")
                {
                    BuyingPower = true;
                    __result = ___terminalNodes.specialNodes[30];
                    return false;
                }
                if (s == "all")
                {
                    CantDie = true;
                    TwoHands = true;
                    ImprovedStats = true;
                    OpenDoors = true;
                    HandOfGod = true; 
                    BuyingPower = true;
                    __result = ___terminalNodes.specialNodes[31];
                    return false;
                }

            }

            return true;
        }
        [HarmonyPatch("Start")]
        [HarmonyPrefix]
        static void GenerateCommands( ref TerminalNodesList ___terminalNodes)
        {
            string[] nodeDisplayText = new string[] {"ADMIN MODE ACTIVATED:\n----------" +
                "\n\n>All\n-Activate all admin abilities" +
                "\n\n>Unkillable\n-Player can't die (works for the most part)" +
                "\n\n>Two Hands\n-Player can interact with objects while both hands are full" +
                "\n\n>Improved Stats\n-Player stats are improved. Infnite sprint, big jump, infnite battery, better vision" +
                "\n\n>Open Doors\n-All locked doors are open (doesn't work for double doors)" +
                "\n\n>Hand of God\n-Player can one shot enimies" +
                "\n\n>Buying Power\n-Company buying rate set to 0 + Infinite Spending\n" ,
                "UNKILLABLE ENABLED:\n\n>You are now unkillable\n",
                "TWO HANDS ENABLED\n\n>You can now do things with two hands\n",
                "YOU NOW HAVE:\nInfinite Sprint\nBig Jumps\nInfinite Battery\nHead flashlight\nMore features soon\n",
                 "ALL DOORS UNLOCKED\n\n>Still working on double doors :(\n",
                  "HAND OF GOD ENABLED\n\n>You can now one shot things\n",
                   "BUYING POWER ENABLED\n\n>Spend away!!\n",
                   "ALL MODS ACTIVATED\n"
            };
            string[] nodeTerminalEvent = new string[] { "Admin", "Unkillable", "Two Hands", "Improved Stats", "Open Doors", "Hand of God", "Buying Power", "All" };
            string[] terminalKeyWord = new string[] { "admin", "unkillable", "two hands", "improved stats", "open doors", "hand of god", "buying power", "All" };



            for (int i = 0; i < terminalKeyWord.Length; i++)
            {
                TerminalNode node = ScriptableObject.CreateInstance<TerminalNode>();
                TerminalKeyword key = ScriptableObject.CreateInstance<TerminalKeyword>();
                node.clearPreviousText = true;
                node.displayText = nodeDisplayText[i];
                node.terminalEvent = nodeTerminalEvent[i];
                node.name = nodeTerminalEvent[i];
                key.name = nodeTerminalEvent[i];
                key.word = terminalKeyWord[i];
                ___terminalNodes.terminalNodes.Add(node);
                ___terminalNodes.specialNodes.Add(node);
                ___terminalNodes.allKeywords.AddToArray(key);

            }
        }
    }
}
