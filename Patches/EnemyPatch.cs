using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminMode.Patches
{
    [HarmonyPatch(typeof(EnemyAI))]
    internal class EnemyPatch
    {
        [HarmonyPatch(nameof(EnemyAI.PlayerIsTargetable))]
        [HarmonyPrefix]
        static bool DontTargetPlayer(ref bool __result)
        {
            __result = false;
            return false;
        }

        [HarmonyPatch(nameof(EnemyAI.HitEnemy))]
        [HarmonyPostfix]
        static void InstantKill(ref int ___enemyHP)
        {
            UnityEngine.Debug.Log("Player killed enemy using ADMIN MODE");
            ___enemyHP = 0;
        }

        [HarmonyPatch(nameof(EnemyAI.KillEnemyOnOwnerClient))]
        [HarmonyPrefix]
        static void EnemyCanDie(ref EnemyType ___enemyType)
        {
            ___enemyType.canDie = true;
            ___enemyType.destroyOnDeath = true;
        }
    }
}
