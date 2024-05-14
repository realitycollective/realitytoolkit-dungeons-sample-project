// Copyright (c) Reality Collective. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using RealityToolkit.Player.Rigs;
using RealityToolkit.Player.UX;
using UnityEngine;

namespace DungeonsSample.Quests
{
    /// <summary>
    /// For this quest the player has to reach a target area as defined by a trigger <see cref="Collider"/>.
    /// </summary>
    [RequireComponent(typeof(Collider))]
    public class ReachAreaQuest : Quest
    {
        /// <inheritdoc />
        protected override void Awake()
        {
            base.Awake();

            var collider = GetComponent<Collider>();
            if (!collider.isTrigger)
            {
                collider.isTrigger = true;
                Debug.LogWarning($"{nameof(PlayerTrigger)} requires the attached {nameof(Collider)} to be a trigger and has auto configured it.", this);
            }
        }

        /// <summary>
        /// <see cref="MonoBehaviour"/>.
        /// </summary>
        private void OnTriggerEnter(Collider other)
        {
            if (!other.TryGetComponent<IPlayerRig>(out _))
            {
                return;
            }

            IsComplete = true;
        }
    }
}