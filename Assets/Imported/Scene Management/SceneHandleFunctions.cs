using System;
using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Developed.SceneManagement
{
    internal static class SceneHandleFunctions
    {
        public static string GetSceneName(SceneReference sceneReference)
        {
            var SceneName = sceneReference.ScenePath.Split('/').Last();
            var extension = SceneName.Split('.').Last();

            string SceneNameWithOutExtension = SceneName.Substring(0, SceneName.Length - extension.Length - 1);
            return SceneNameWithOutExtension;
        }


        public static AsyncOperation Load(SceneReference sceneReference)
        {
            if (sceneReference == null)
                throw new ArgumentNullException("Scene reference is empty!");

            return SceneManager.LoadSceneAsync(GetSceneName(sceneReference), LoadSceneMode.Additive);
        }

        public static AsyncOperation Unload(SceneReference sceneReference)
        {
            if (sceneReference == null)
                throw new ArgumentNullException("Scene reference is empty!");

            var scene = SceneManager.GetSceneByPath(sceneReference.ScenePath);

            return SceneManager.UnloadSceneAsync(scene);
        }
    }
}