// Copyright (c) Reality Collective. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using DungeonsSample.Dungeons;
using DungeonsSample.Quests.UI;
using RealityCollective.ServiceFramework.Services;
using UnityEngine;

namespace DungeonsSample.UI
{
    public class UIDungeonRoomIntroBoard : MonoBehaviour
    {
        [SerializeField]
        private GameObject root = null;

        [SerializeField]
        private TMPro.TextMeshProUGUI titleText = null;

        [SerializeField]
        private TMPro.TextMeshProUGUI descriptionText = null;

        [SerializeField]
        private UIQuestsList questList = null;

        private IDungeonsService dungeonsService;

        private async void Awake()
        {
            root.SetActive(false);

            await ServiceManager.WaitUntilInitializedAsync();
            dungeonsService = ServiceManager.Instance.GetService<IDungeonsService>();
            dungeonsService.DungeonEntered += DungeonService_DungeonEntered;
            dungeonsService.DungeonCleared += DungeonService_DungeonCleared;
        }

        private void OnDestroy()
        {
            if (dungeonsService != null)
            {
                dungeonsService.DungeonEntered -= DungeonService_DungeonEntered;
                dungeonsService.DungeonCleared -= DungeonService_DungeonCleared;
            }
        }

        private void DungeonService_DungeonEntered(DungeonController room)
        {
            titleText.text = room.Title;
            descriptionText.text = room.Description;
            transform.SetPositionAndRotation(room.IntroBoardAnchor.position, room.IntroBoardAnchor.rotation);
            questList.Bind(room.Quests);
            root.SetActive(true);
        }

        private void DungeonService_DungeonCleared(DungeonController room) => Close();

        private void Close() => root.SetActive(false);
    }
}