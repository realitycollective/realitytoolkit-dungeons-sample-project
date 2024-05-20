// Copyright (c) Reality Collective. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using RealityCollective.ServiceFramework.Interfaces;
using UnityEngine.SceneManagement;

namespace DungeonsSample
{
    public interface IGameService : IService
    {
        /// <summary>
        /// Unloads the <paramref name="scene"/>.
        /// </summary>
        /// <param name="scene">The scene that is safe to unload.</param>
        void UnloadLevel(Scene scene);
    }
}