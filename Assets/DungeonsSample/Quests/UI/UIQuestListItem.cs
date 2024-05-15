// Copyright (c) Reality Collective. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using RealityCollective.Utilities.Extensions;
using UnityEngine;
using UnityEngine.UI;

namespace DungeonsSample.Quests.UI
{
    /// <summary>
    /// UI controller for the quest list itme in the  <see cref="UIQuestsList"/>.
    /// </summary>
    public class UIQuestListItem : MonoBehaviour
    {
        [SerializeField]
        private Toggle toggle = null;

        [SerializeField]
        private TMPro.TextMeshProUGUI text = null;

        private Quest data;

        /// <summary>
        /// See <see cref="MonoBehaviour"/>.
        /// </summary>
        private void OnDisable()
        {
            if (data.IsNotNull())
            {
                data.Completed -= Data_Completed;
                data = null;
            }
        }

        /// <summary>
        /// Binds the <paramref name="quest"/> to the UI control.
        /// </summary>
        /// <param name="quest">The <see cref="Quest"/>.</param>
        public void Bind(Quest quest)
        {
            if (data.IsNotNull())
            {
                data.Completed -= Data_Completed;
            }

            data = quest;
            toggle.isOn = data.IsComplete;
            text.text = data.Instruction;
            data.Completed += Data_Completed;
        }

        private void Data_Completed() => toggle.isOn = true;
    }
}
