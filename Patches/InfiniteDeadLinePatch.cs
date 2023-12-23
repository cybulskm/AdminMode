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

    internal class InfiniteDeadLinePatch
    {
        [HarmonyPatch("Update")]
        [HarmonyPostfix]
        static void TimeofDayPatch(ref int ___hour, ref float ___currentDayTime, ref int ___daysUntilDeadline, ref int ___quotaFulfilled)
        {
            ___currentDayTime = 8;
            ___hour = 1;
            ___daysUntilDeadline = 999;
            ___quotaFulfilled = 999;
        }
        [HarmonyPatch(nameof(TimeOfDay.SetBuyingRateForDay))]
        [HarmonyPostfix]
        static void BuyingRate(ref float ___companyBuyingRate)
        {
            ___companyBuyingRate = 100;
        }
        
    }
 }
