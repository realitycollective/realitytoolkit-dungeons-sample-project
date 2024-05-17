// Copyright (c) Reality Collective. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using RealityCollective.ServiceFramework.Services;
using RealityToolkit.Locomotion;
using RealityToolkit.Locomotion.Teleportation;

namespace DungeonsSample.Quests
{
    /// <summary>
    /// For this quest, the user has to teleport at least once using the <see cref="BlinkTeleportLocomotionProvider"/>s.
    /// </summary>
    public class BlinkTeleportQuest : Quest, ILocomotionServiceHandler
    {
        private ILocomotionService locomotionService;

        /// <inheritdoc/>
        protected override async void Awake()
        {
            base.Awake();

            await ServiceManager.WaitUntilInitializedAsync();

            locomotionService = ServiceManager.Instance.GetService<ILocomotionService>();
            locomotionService.Register(gameObject);
        }

        /// <inheritdoc/>
        protected override void OnDestroy()
        {
            if (locomotionService != null)
            {
                locomotionService.Unregister(gameObject);
            }

            base.OnDestroy();
        }

        /// <inheritdoc/>
        protected override void OnComplete()
        {
            locomotionService.MovementEnabled = true;
            ServiceManager.Instance.GetService<ITeleportValidationServiceModule>().AnchorsOnly = false;
        }

        /// <inheritdoc/>
        public void OnMoving(LocomotionEventData eventData) { }

        /// <inheritdoc/>
        public void OnTeleportTargetRequested(LocomotionEventData eventData) { }

        /// <inheritdoc/>
        public void OnTeleportStarted(LocomotionEventData eventData) { }

        /// <inheritdoc/>
        public void OnTeleportCompleted(LocomotionEventData eventData)
        {
            if (eventData.LocomotionProvider is BlinkTeleportLocomotionProvider)
            {
                IsComplete = true;
            }
        }

        /// <inheritdoc/>
        public void OnTeleportCanceled(LocomotionEventData eventData) { }
    }
}
