// Copyright (c) Reality Collective. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using RealityCollective.ServiceFramework.Services;
using RealityToolkit.Player.Bounds;

namespace DungeonsSample.Quests
{
    /// <summary>
    /// This quest requires the user to be reset back into bounds by auto reset.
    /// </summary>
    public class PlayerAutoBackInBounds : Quest
    {
        private IPlayerBoundsModule playerBoundsModule;

        /// <inheritdoc/>
        protected override async void Awake()
        {
            base.Awake();

            await ServiceManager.WaitUntilInitializedAsync();
            playerBoundsModule = ServiceManager.Instance.GetService<IPlayerBoundsModule>();
            playerBoundsModule.PlayerBackInBounds += PlayerBoundsModule_PlayerBackInBounds;
        }

        /// <inheritdoc/>
        protected override void OnDestroy()
        {
            if (playerBoundsModule != null)
            {
                playerBoundsModule.PlayerBackInBounds -= PlayerBoundsModule_PlayerBackInBounds;
            }

            base.OnDestroy();
        }

        private void PlayerBoundsModule_PlayerBackInBounds(bool didAutoReset)
        {
            if (!didAutoReset)
            {
                return;
            }

            IsComplete = true;
        }
    }
}