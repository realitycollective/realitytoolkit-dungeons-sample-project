// Copyright (c) Reality Collective. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using RealityCollective.ServiceFramework.Services;
using RealityToolkit.Locomotion;
using RealityToolkit.Locomotion.Teleportation;
using UnityEngine;

namespace DungeonsSample.Quests
{
    public class DashTeleportActivator : MonoBehaviour
    {
        public void Activate()
        {
            ServiceManager.Instance.GetService<ILocomotionService>().EnableLocomotionProvider(typeof(DashTeleportLocomotionProvider));
        }
    }
}