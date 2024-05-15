// Copyright (c) Reality Collective. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using RealityCollective.ServiceFramework.Definitions;
using RealityCollective.ServiceFramework.Interfaces;
using System.Collections.Generic;
using UnityEngine;

namespace DungeonsSample
{
    public class GameServiceProfile : BaseServiceProfile<IServiceModule>
    {
        /// <summary>
        /// Scenes to load upon application launch.
        /// </summary>
        [field: SerializeField]
        public List<string> ScenesToLoad { get; private set; } = null;
    }
}
