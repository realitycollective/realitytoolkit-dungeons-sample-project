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
    public class DungeonRoomController : MonoBehaviour
    {
        [SerializeField]
        private DungeonRoom room = null;

        [SerializeField]
        private List<Quest> quests = null;

        [SerializeField, Tooltip("This transform defines the pose of the room intro board.")]
        private Transform introBoardAnchor = null;

        [SerializeField]
        private AudioSource successAudioSource = null;

        [SerializeField, Tooltip("The door used to exit the room.")]
        private DungeonRoomDoor exitDoor = null;

        private IDungeonsService sampleProjectService;

        /// <summary>
        /// THe room intro title.
        /// </summary>
        public string Title => room.Title;

        /// <summary>
        /// The room intro description.
        /// </summary>
        public string Description => room.Description;

        /// <summary>
        /// This transform defines the pose of the room intro board.
        /// </summary>
        public Transform IntroBoardAnchor => introBoardAnchor;

        /// <summary>
        /// See <see cref="MonoBehaviour"/>.
        /// </summary>
        private async void Awake()
        {
            await ServiceManager.WaitUntilInitializedAsync();

            sampleProjectService = ServiceManager.Instance.GetService<IDungeonsService>();
            sampleProjectService.DungeonEntered += SampleProjectService_DungeonEntered;
            sampleProjectService.DungeonCleared += SampleProjectService_DungeonCleared;

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

            if (sampleProjectService != null)
            {
                sampleProjectService.DungeonEntered -= SampleProjectService_DungeonEntered;
                sampleProjectService.DungeonCleared -= SampleProjectService_DungeonCleared;
            }
        }

        /// <summary>
        /// The player has entered the room.
        /// </summary>
        public void OnRoomEntered() => sampleProjectService.EnterDungeon(this);

        private void Quest_Completed()
        {
            foreach (var quest in quests)
            {
                if (!quest.IsComplete)
                {
                    return;
                }
            }

            sampleProjectService.ClearDungeon(this);
        }

        private void SampleProjectService_DungeonEntered(DungeonRoomController room)
        {
            if (this != room)
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

        private void SampleProjectService_DungeonCleared(DungeonRoomController room)
        {
            if (this != room || exitDoor.IsNull())
            {
                return;
            }

            StartCoroutine(OpenExitDelayed());
        }

        private IEnumerator OpenExitDelayed()
        {
            successAudioSource.Play();
            yield return new WaitForSeconds(2f);
            exitDoor.Open();
        }
    }
}
