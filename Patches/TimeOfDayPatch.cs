using GameNetcodeStuff;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminMode.Patches
{
    [HarmonyPatch(typeof(TimeOfDay))]

    internal class TimeOfDayPatch
    {
        [HarmonyPatch(nameof(TimeOfDay.SetBuyingRateForDay))]
        [HarmonyPostfix]
        static void BuyingRate()
        {
            if (TerminalInterfacePatch.changeText)
            {
                StartOfRound.Instance.companyBuyingRate = 1;

            }
        }
        
    }
 }
