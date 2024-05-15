// Copyright (c) Reality Collective. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using DungeonsSample.Quests;
using RealityCollective.ServiceFramework.Services;
using RealityCollective.Utilities.Extensions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DungeonsSample.Dungeons
{
    public class DungeonController : MonoBehaviour
    {
        [SerializeField]
        private DungeonRoom dungeon = null;

        [SerializeField]
        private List<Quest> quests = null;

        [SerializeField, Tooltip("This transform defines the pose of the dungeon intro board.")]
        private Transform introBoardAnchor = null;

        [SerializeField]
        private AudioSource successAudioSource = null;

        [SerializeField, Tooltip("The door used to enter this room's corridor.")]
        private DungeonRoomDoor corridorEntranceDoor = null;

        private IDungeonsService dungeonService;

        /// <summary>
        /// The dungeon's identifier.
        /// </summary>
        public string Id => dungeon.Id;

        /// <summary>
        /// THe dungeon intro title.
        /// </summary>
        public string Title => dungeon.Title;

        /// <summary>
        /// The dungeon intro description.
        /// </summary>
        public string Description => dungeon.Description;

        /// <summary>
        /// All <see cref="Quest"/>s in this dungeon.
        /// </summary>
        public IReadOnlyList<Quest> Quests => quests;

        /// <summary>
        /// This transform defines the pose of the dungeon intro board.
        /// </summary>
        public Transform IntroBoardAnchor => introBoardAnchor;

        /// <summary>
        /// See <see cref="MonoBehaviour"/>.
        /// </summary>
        private async void Awake()
        {
            await ServiceManager.WaitUntilInitializedAsync();

            dungeonService = ServiceManager.Instance.GetService<IDungeonsService>();
            dungeonService.DungeonEntered += DungeonService_DungeonEntered;
            dungeonService.DungeonCleared += DungeonService_DungeonCleared;

            if (quests != null)
            {
                foreach (var quest in quests)
                {
                    quest.IsActive = false;
                    quest.Completed += Quest_Completed;
                }
            }
        }

        /// <summary>
        /// See <see cref="MonoBehaviour"/>.
        /// </summary>
        private void OnDestroy()
        {
            if (quests != null)
            {
                foreach (var quest in quests)
                {
                    if (quest.IsNotNull())
                    {
                        quest.Completed -= Quest_Completed;
                    }
                }
            }

            if (dungeonService != null)
            {
                dungeonService.DungeonEntered -= DungeonService_DungeonEntered;
                dungeonService.DungeonCleared -= DungeonService_DungeonCleared;
            }
        }

        /// <summary>
        /// The player has entered the room.
        /// </summary>
        public void OnRoomEntered() => dungeonService.EnterDungeon(this);

        private void Quest_Completed()
        {
            foreach (var quest in quests)
            {
                if (!quest.IsComplete)
                {
                    return;
                }
            }

            dungeonService.ClearDungeon(this);
        }

        private void DungeonService_DungeonEntered(DungeonController dungeon)
        {
            if (this != dungeon)
            {
                return;
            }

            if (quests != null)
            {
                foreach (var quest in quests)
                {
                    quest.IsActive = true;
                }
            }
        }

        private void DungeonService_DungeonCleared(DungeonController dungeon)
        {
            if (this.dungeon.PreviousRoom.IsNull() ||
                !string.Equals(this.dungeon.PreviousRoom.Id, dungeon.Id) ||
                corridorEntranceDoor.IsNull())
            {
                return;
            }

            StartCoroutine(OpenCorridorDelayed());
        }

        private IEnumerator OpenCorridorDelayed()
        {
            successAudioSource.Play();
            yield return new WaitForSeconds(2f);
            corridorEntranceDoor.Open();
        }
    }
}
