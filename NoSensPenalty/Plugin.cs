using BepInEx;
using System;

namespace NoSensPenalty
{
    [BepInPlugin("com._1nco.noSensPenalty", "NoSensPenalty", "1.0.0")]

    public class Plugin : BaseUnityPlugin
    {
        private void Awake()
        {
            // Plugin startup logic
            new NoSensPenalty().Enable();
            Logger.LogInfo($"Plugin NoSensPenalty is loaded!");

        }
    }
}
