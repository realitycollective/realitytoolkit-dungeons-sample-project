// Copyright (c) Reality Collective. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using UnityEngine;

namespace DungeonsSample.Dungeons
{
    /// <summary>
    /// The sample experience consists of multiple rooms. Each room is dedicated
    /// to a specific feature or feature group of the toolkit.
    /// </summary>
    [CreateAssetMenu(fileName = "DungeonRoom", menuName = "Dungeons Sample/Dungeon Room", order = 0)]
    public class DungeonRoom : ScriptableObject
    {
        /// <summary>
        /// The dungeon's identifier.
        /// </summary>
        public string Id => name;

        [SerializeField, Tooltip("The room intro title.")]
        private string title = null;

        /// <summary>
        /// The room intro title.
        /// </summary>
        public string Title => title;

        [SerializeField, Multiline, Tooltip("The room intro description.")]
        private string description = null;

        /// <summary>
        /// The room intro description.
        /// </summary>
        public string Description => description;

        [SerializeField]
        private DungeonRoom previousRoom = null;

        /// <summary>
        /// The preceding <see cref="DungeonRoom"/>.
        /// </summary>
        public DungeonRoom PreviousRoom => previousRoom;
    }
}
