using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Developed.SceneManagement
{
    /// <summary>
    /// Contains reference to scene asset and handles it's load/unload
    /// </summary>

    [CreateAssetMenu(fileName = "NewSceneData", menuName = "Scene Data", order = 152)]
    public class SceneData : ScriptableObject
    {
        [SerializeField] private SceneReference sceneReference;


        public IEnumerator Load(Action doBeforeLoad = null, Action doAfterLoad = null)
        {
            doBeforeLoad?.Invoke();
            yield return null;

            yield return SceneHandleFunctions.Load(sceneReference);

            doAfterLoad?.Invoke();
            yield return null;
        }

        public IEnumerator Unload(Action doBeforeUnload = null, Action doAfterUnload = null)
        {
            doBeforeUnload?.Invoke();
            yield return null;

            yield return SceneHandleFunctions.Unload(sceneReference);

            doAfterUnload?.Invoke();
            yield return null;
        }
    }
}