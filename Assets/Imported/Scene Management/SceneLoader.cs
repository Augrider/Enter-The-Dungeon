using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

namespace Developed.SceneManagement
{
    /// <summary>
    /// Base component for scene controllers that are able to do scene loading operations
    /// </summary>
    public abstract class SceneLoader : MonoBehaviour
    {
        private static Scene loaderScene;


        protected virtual void Awake()
        {
            loaderScene = SceneManager.GetActiveScene();
        }


        protected virtual Coroutine LoadFromScenePackage(ScenePackage scenePackage, Action doBeforeLoad = null, Action doAfterLoad = null)
        {
            return StartCoroutine(scenePackage.Load(doBeforeLoad, doAfterLoad));
        }

        protected virtual Coroutine LoadFromSceneData(SceneData sceneData, Action doBeforeLoad = null, Action doAfterLoad = null)
        {
            return StartCoroutine(sceneData.Load(doBeforeLoad, doAfterLoad));
        }


        protected virtual Coroutine UnloadFromScenePackage(ScenePackage scenePackage, Action doBeforeLoad = null, Action doAfterLoad = null)
        {
            return StartCoroutine(scenePackage.Unload(doBeforeLoad, doAfterLoad));
        }

        protected virtual Coroutine UnloadFromSceneData(SceneData sceneData, Action doBeforeLoad = null, Action doAfterLoad = null)
        {
            return StartCoroutine(sceneData.Unload(doBeforeLoad, doAfterLoad));
        }


        protected static void UnloadAll()
        {
            for (int i = 0; i < SceneManager.sceneCount; i++)
            {
                var scene = SceneManager.GetSceneAt(i);

                if (scene.name != loaderScene.name)
                    SceneManager.UnloadSceneAsync(scene);
            }
        }
    }
}