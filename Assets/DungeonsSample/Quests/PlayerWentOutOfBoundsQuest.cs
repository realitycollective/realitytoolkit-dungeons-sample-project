// Copyright (c) Reality Collective. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using RealityCollective.ServiceFramework.Services;
using RealityToolkit.Player.Bounds;

namespace DungeonsSample.Quests
{
    /// <summary>
    /// This quest requires the user to purposefully go out of level bounds.
    /// </summary>
    public class PlayerWentOutOfBoundsQuest : Quest
    {
        private IPlayerBoundsModule playerBoundsModule;

        /// <inheritdoc/>
        protected override async void Awake()
        {
            base.Awake();

            await ServiceManager.WaitUntilInitializedAsync();
            playerBoundsModule = ServiceManager.Instance.GetService<IPlayerBoundsModule>();
            playerBoundsModule.PlayerOutOfBounds += PlayerBoundsModule_PlayerOutOfBounds;
        }

        /// <inheritdoc/>
        protected override void OnDestroy()
        {
            if (playerBoundsModule != null)
            {
                playerBoundsModule.PlayerOutOfBounds -= PlayerBoundsModule_PlayerOutOfBounds;
            }

            base.OnDestroy();
        }

        private void PlayerBoundsModule_PlayerOutOfBounds(float severity, UnityEngine.Vector3 returnToBoundsDirection)
        {
            if (severity >= .5f)
            {
                IsComplete = true;
            }
        }
    }
}