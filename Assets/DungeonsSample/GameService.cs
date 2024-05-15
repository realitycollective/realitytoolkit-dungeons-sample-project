// Copyright (c) Reality Collective. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using RealityCollective.ServiceFramework.Services;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

namespace DungeonsSample
{
    [System.Runtime.InteropServices.Guid("2cd645ac-a682-4493-8f4d-3e7293dd1038")]
    public class GameService : BaseServiceWithConstructor, IGameService
    {
        public GameService(string name, uint priority, GameServiceProfile profile)
            : base(name, priority)
        {
            scenesToLoad = profile.ScenesToLoad;
        }

        private readonly List<string> scenesToLoad;

        /// <inheritdoc/>
        public override void Start() => LoadScenes();

        private void LoadScenes()
        {
            foreach (var scene in scenesToLoad)
            {
                if (!IsSceneLoaded(scene))
                {
                    SceneManager.LoadScene(scene, LoadSceneMode.Additive);
                }
            }
        }

        private bool IsSceneLoaded(string sceneName)
        {
            for (var i = 0; i < SceneManager.loadedSceneCount; i++)
            {
                var loadedScene = SceneManager.GetSceneAt(i);
                if (string.Equals(loadedScene.name, sceneName))
                {
                    return true;
                }
            }

            return false;
        }
    }
}
