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
        //Set company buying rate to 100 always

        [HarmonyPatch(nameof(TimeOfDay.SetBuyingRateForDay))]
        [HarmonyPostfix]
        static void BuyingRate()
        {
            if (TerminalInterfacePatch.changeText)
            {
                StartOfRound.Instance.companyBuyingRate = 1;

            }
        }
        /*
        [HarmonyPatch("Start")]
        [HarmonyPostfix]
        static void SyncNewProfitQuotaClientRpc(ref int ___quotaFulfilled, ref float ___timeUntilDeadline) 
        {
            
            ___quotaFulfilled = 999;
            ___timeUntilDeadline = 999;
            
            
        }
        */
        /*
        [HarmonyPatch("MoveGlobalTime")]
        [HarmonyPrefix]
        static void InfiniteDay(ref float ___globalTimeSpeedMultiplier)
        {

            if (TerminalInterfacePatch.changeText)
            {
                ___globalTimeSpeedMultiplier = 0.1f;

            }
           
        }
        */
        
        
    }
 }
