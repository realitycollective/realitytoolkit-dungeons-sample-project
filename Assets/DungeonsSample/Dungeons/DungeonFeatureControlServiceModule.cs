// Copyright (c) Reality Collective. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using RealityCollective.ServiceFramework.Definitions;
using RealityCollective.ServiceFramework.Modules;
using RealityCollective.ServiceFramework.Services;
using RealityToolkit.Locomotion;
using RealityToolkit.Locomotion.Teleportation;

namespace DungeonsSample.Dungeons
{
    [System.Runtime.InteropServices.Guid("007c5bea-da49-4da9-94d8-7ed45059afcb")]
    public class DungeonFeatureControlServiceModule : BaseServiceModule, IDungeonsServiceModule
    {
        /// <inheritdoc/>
        public DungeonFeatureControlServiceModule(string name, uint priority, BaseProfile profile, IDungeonsService parentService)
            : base(name, priority, profile, parentService)
        { }

        /// <summary>
        /// Makes sure only features allowed within the <paramref name="dungeon"/> are enabled.
        /// </summary>
        /// <param name="dungeon">The <see cref="DungeonRoom"/>.</param>
        public void UpdateFeatures(DungeonRoom dungeon)
        {
            var locomotionService = ServiceManager.Instance.GetService<ILocomotionService>();
            locomotionService.MovementEnabled = dungeon.FreeMovement;
            locomotionService.TeleportationEnabled = dungeon.Teleportation;

            var teleportValidationServiceModule = ServiceManager.Instance.GetService<ITeleportValidationServiceModule>();
            teleportValidationServiceModule.AnchorsOnly = dungeon.AnchorsOnly;
        }
    }
}
