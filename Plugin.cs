using AdminMode.Patches;
using BepInEx;
using BepInEx.Logging;
using GameNetcodeStuff;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace AdminMode
{
    [BepInPlugin(modGUID, modName, modVersion)]

    public class AdminBase : BaseUnityPlugin
    {
        private const string modGUID = "AdminMode";
        private const string modName = "Testing AdminMode";
        private const string modVersion = "1.0.0";

        private readonly Harmony harmony = new Harmony(modGUID);

        private static AdminBase Instance;

        internal ManualLogSource mls;

        public static Texture2D mainLogo;
        void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            mls = BepInEx.Logging.Logger.CreateLogSource(modGUID);
            mls.LogInfo("This mod has started");
            harmony.PatchAll(typeof(AdminBase));
            harmony.PatchAll(typeof(TerminalInterfacePatch));
            harmony.PatchAll(typeof(PlayerControllerPatch));
            harmony.PatchAll(typeof(TimeOfDayPatch));
            harmony.PatchAll(typeof(KillPlayerPatch));
            harmony.PatchAll(typeof(EnemyPatch));
            harmony.PatchAll(typeof(ScanPatch));
            harmony.PatchAll(typeof(BatteryUpdate));
            mls.LogInfo("Mod completed loading patches");
        }
    }
}
