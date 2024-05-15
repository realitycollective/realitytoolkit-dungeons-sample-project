// Copyright (c) Reality Collective. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using RealityCollective.ServiceFramework.Interfaces;

namespace DungeonsSample.Dungeons
{
    public delegate void OnDungeonDelegate(DungeonController dungeon);

    public interface IDungeonsService : IService
    {
        /// <summary>
        /// The most recent <see cref="DungeonRoom"/> the player has progressed to.
        /// </summary>
        DungeonController CurrentDungeon { get; }

        /// <summary>
        /// Has <see cref="DungeonRoom"/> been cleared?
        /// </summary>
        bool IsCleared { get; }

        /// <summary>
        /// A <see cref="DungeonRoom"/> has been entered.
        /// </summary>
        event OnDungeonDelegate DungeonEntered;

        /// <summary>
        /// A <see cref="DungeonRoom"/> has been cleared.
        /// </summary>
        event OnDungeonDelegate DungeonCleared;

        /// <summary>
        /// Makes the <paramref name="dungeon"/> the <see cref="CurrentDungeon"/>.
        /// </summary>
        /// <param name="dungeon">The <see cref="DungeonRoom"/> entered.</param>
        void EnterDungeon(DungeonController dungeon);

        /// <summary>
        /// Clears the <paramref name="dungeon"/> and unlocks the next <see cref="DungeonRoom"/>.
        /// </summary>
        /// <param name="dungeon">The <see cref="DungeonRoom"/> cleared.</param>
        void ClearDungeon(DungeonController dungeon);
    }
}