// Copyright (c) Reality Collective. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using RealityCollective.ServiceFramework.Services;
using RealityCollective.Utilities.Extensions;
using UnityEngine;

namespace DungeonsSample.Dungeons
{
    [System.Runtime.InteropServices.Guid("f978dcf9-b1da-4246-b671-fed7220226d5")]
    public class DungeonsService : BaseServiceWithConstructor, IDungeonsService
    {
        /// <inheritdoc/>
        public DungeonsService(string name, uint priority, DungeonsServiceProfile profile)
            : base(name, priority) { }

        private DungeonFeatureControlServiceModule featureControlServiceModule;
        private IGameService gameService;

        /// <inheritdoc/>
        public DungeonController CurrentDungeon { get; private set; }

        /// <inheritdoc/>
        public bool IsCleared { get; private set; }

        /// <inheritdoc/>
        public event OnDungeonDelegate DungeonEntered;

        /// <inheritdoc/>
        public event OnDungeonDelegate DungeonCleared;

        /// <inheritdoc/>
        public override void Initialize()
        {
            if (!Application.isPlaying)
            {
                return;
            }

            gameService = ServiceManager.Instance.GetService<IGameService>();
            featureControlServiceModule = ServiceManager.Instance.GetService<IDungeonsServiceModule>() as DungeonFeatureControlServiceModule;
        }

        /// <inheritdoc/>
        public void EnterDungeon(DungeonController dungeon)
        {
            if (CurrentDungeon.IsNotNull())
            {
                gameService.UnloadLevel(CurrentDungeon.gameObject.scene);
            }

            CurrentDungeon = dungeon;
            IsCleared = false;
            featureControlServiceModule.UpdateFeatures(CurrentDungeon.Data);
            DungeonEntered?.Invoke(dungeon);
        }

        /// <inheritdoc/>
        public void ClearDungeon(DungeonController dungeon)
        {
            if (dungeon != CurrentDungeon || IsCleared)
            {
                return;
            }

            IsCleared = true;
            DungeonCleared?.Invoke(dungeon);
        }
    }
}
