using System.Reflection;
using Aki.Reflection.Patching;
using EFT;
using UnityEngine;
using HarmonyLib;

namespace NoSensPenalty
{
    public class NoSensPenalty : ModulePatch
    {
        protected override MethodBase GetTargetMethod()
        {
            NoSensPenalty.moveContext = AccessTools.Field(typeof(MovementState), "MovementContext");
            NoSensPenalty.rotationClamp = AccessTools.Field(typeof(MovementState), "RotationSpeedClamp");
            return typeof(MovementState).GetMethod("ClampRotation", BindingFlags.Instance | BindingFlags.Public);
        }

        [PatchPrefix]
        static bool PatchPrefix(ref Vector3 __result, MovementState __instance, Vector3 deltaRotation)
        {
            MovementContext movementContext = (MovementContext) NoSensPenalty.moveContext.GetValue(__instance);
            float clamp = (float) rotationClamp.GetValue(__instance);
            if (clamp <= 0f)
            {
                __result = Vector3.zero;
                return false;
            }
            deltaRotation = movementContext.ApplyExternalSense(deltaRotation);
            __result = deltaRotation;
            return false;
        }

        private static FieldInfo rotationClamp;
        private static FieldInfo moveContext;
    }
}