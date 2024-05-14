// Copyright (c) Reality Collective. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System;
using UnityEngine;

namespace DungeonsSample.Quests
{
    /// <summary>
    /// Abstract base for any kind of quest the user has to perform within the sample experience.
    /// </summary>
    public abstract class Quest : MonoBehaviour
    {
        [SerializeField, Tooltip("The instruction text to complete the quest.")]
        private string instruction = null;

        /// <summary>
        /// The instruction text to complete the quest.
        /// </summary>
        public string Instruction => instruction;

        private bool isActive;
        /// <summary>
        /// Is this quest currently active and being tracked?
        /// </summary>
        public bool IsActive
        {
            get => isActive;
            set
            {
                if (isActive == value)
                {
                    return;
                }

                isActive = value;
                OnActivated();
            }
        }

        private bool isComplete;
        /// <summary>
        /// Is this quest complete / finished?
        /// </summary>
        public bool IsComplete
        {
            get => isComplete;
            protected set
            {
                if (isComplete == value || !IsActive)
                {
                    return;
                }

                isComplete = value;

                if (isComplete)
                {
                    OnComplete();
                    Completed?.Invoke();
                }
            }
        }

        /// <summary>
        /// The quest has been completed.
        /// </summary>
        public event Action Completed;

        /// <summary>
        /// <see cref="MonoBehaviour"/>.
        /// </summary>
        protected virtual void Awake() { }

        /// <summary>
        /// <see cref="MonoBehaviour"/>.
        /// </summary>
        protected virtual void OnDestroy() { }

        /// <summary>
        /// The <see cref="Quest"/> has been activated.
        /// </summary>
        protected virtual void OnActivated() { }

        /// <summary>
        /// The <see cref="Quest"/> was completed.
        /// </summary>
        protected virtual void OnComplete() { }
    }
}