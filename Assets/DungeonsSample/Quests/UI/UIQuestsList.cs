// Copyright (c) Reality Collective. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using RealityCollective.Utilities.Extensions;
using System.Collections.Generic;
using UnityEngine;

namespace DungeonsSample.Quests.UI
{
    /// <summary>
    /// Displsys a collection of <see cref="UIQuestListItem"/>s.
    /// </summary>
    public class UIQuestsList : MonoBehaviour
    {
        [SerializeField]
        private List<UIQuestListItem> items = null;

        [SerializeField]
        private GameObject listItemPrefab = null;

        /// <summary>
        /// Binds the <paramref name="quests"/> to the UI control.
        /// </summary>
        /// <param name="quests">List of <see cref="Quest"/>s.</param>
        public void Bind(IReadOnlyList<Quest> quests)
        {
            var requiredItems = quests != null ? quests.Count : 0;
            var existingItems = items.Count;
            var delta = requiredItems - existingItems;

            while (delta > 0)
            {
                var newItem = Instantiate(listItemPrefab).GetComponent<UIQuestListItem>();
                items.Add(newItem);
                delta--;
            }

            for (var i = 0; i < items.Count; i++)
            {
                var item = items[i];

                if (i < requiredItems)
                {
                    item.Bind(quests[i]);
                    item.SetActive(true);
                    continue;
                }

                item.SetActive(false);
            }
        }
    }
}
