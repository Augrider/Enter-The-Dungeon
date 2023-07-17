using System;
using System.Collections;
using System.Linq;
using UnityEngine;

namespace Developed.SceneManagement
{
    /// <summary>
    /// Contains scene data collection and handles it's load/unload
    /// </summary>

    [CreateAssetMenu(fileName = "NewScenePackage", menuName = "Scene Package", order = 151)]
    public class ScenePackage : ScriptableObject
    {
        [SerializeField] private SceneReference[] sceneReferences;


        public IEnumerator Load(Action doBeforeLoad = null, Action doAfterLoad = null)
        {
            if (sceneReferences.Length <= 0)
                throw new MissingFieldException("No scenes found in package!");

            doBeforeLoad?.Invoke();
            yield return null;

            var loadingProcesses = new AsyncOperation[sceneReferences.Length];

            for (int i = 0; i < sceneReferences.Length; i++)
                loadingProcesses[i] = SceneHandleFunctions.Load(sceneReferences[i]);

            yield return new WaitUntil(() => loadingProcesses.All(t => t.isDone));
            yield return null;

            doAfterLoad?.Invoke();
            yield return null;
        }

        public IEnumerator Unload(Action doBeforeUnload = null, Action doAfterUnload = null)
        {
            if (sceneReferences.Length <= 0)
                throw new MissingFieldException("No scenes found in package!");

            doBeforeUnload?.Invoke();
            yield return null;

            var loadingProcesses = new AsyncOperation[sceneReferences.Length];

            for (int i = 0; i < sceneReferences.Length; i++)
                loadingProcesses[i] = SceneHandleFunctions.Unload(sceneReferences[i]);

            yield return new WaitUntil(() => loadingProcesses.All(t => t.isDone));
            yield return null;

            doAfterUnload?.Invoke();
            yield return null;
        }
    }
}