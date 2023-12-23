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
        [HarmonyPatch("Update")]
        [HarmonyPostfix]
        static void TimeofDayPatch(ref int ___hour, ref float ___currentDayTime, ref int ___daysUntilDeadline, ref int ___quotaFulfilled)
        {
            
            ___daysUntilDeadline = 999;
            ___quotaFulfilled = 999;
        }
        [HarmonyPatch(nameof(TimeOfDay.SetBuyingRateForDay))]
        [HarmonyPostfix]
        static void BuyingRate()
        {
            StartOfRound.Instance.companyBuyingRate = 1;
        }
        
    }
 }
