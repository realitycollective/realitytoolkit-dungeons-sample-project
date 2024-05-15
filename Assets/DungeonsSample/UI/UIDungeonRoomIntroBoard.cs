using DungeonsSample.Dungeons;
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

        private void DungeonService_DungeonEntered(DungeonRoomController room)
        {
            titleText.text = room.Title;
            descriptionText.text = room.Description;
            transform.SetPositionAndRotation(room.IntroBoardAnchor.position, room.IntroBoardAnchor.rotation);
            root.SetActive(true);
        }

        private void DungeonService_DungeonCleared(DungeonRoomController room) => Close();

        private void Close() => root.SetActive(false);
    }
}