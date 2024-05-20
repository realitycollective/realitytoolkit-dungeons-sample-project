using RealityCollective.ServiceFramework.Services;
using RealityToolkit.Locomotion;
using RealityToolkit.Locomotion.Teleportation;
using UnityEngine;

namespace DungeonsSample.Quests
{
    public class TeleportAnchorQuest : Quest, ILocomotionServiceHandler
    {
        [SerializeField]
        private TeleportAnchor targetAnchor = null;

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
        public void OnMoving(LocomotionEventData eventData) { }

        /// <inheritdoc/>
        public void OnTeleportTargetRequested(LocomotionEventData eventData) { }

        /// <inheritdoc/>
        public void OnTeleportStarted(LocomotionEventData eventData) { }

        /// <inheritdoc/>
        public void OnTeleportCompleted(LocomotionEventData eventData)
        {
            if (eventData.Anchor == (ITeleportAnchor)targetAnchor)
            {
                IsComplete = true;
            }
        }

        /// <inheritdoc/>
        public void OnTeleportCanceled(LocomotionEventData eventData) { }
    }
}
