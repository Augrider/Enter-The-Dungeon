using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Developed.Database
{
    [CustomEditor(typeof(BaseScriptableDatabase<>), editorForChildClasses: true)]
    internal class BaseScriptableDatabaseDrawer : Editor
    {
        private bool _displayContents = false;


        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            var editorHandler = target as IDatabaseEditorHandler;

            GUILayout.Space(10);

            GUI.enabled = !Application.isPlaying;

            if (GUILayout.Button("Update Database"))
                editorHandler.UpdateDatabase();

            GUILayout.Space(10);
            _displayContents = GUILayout.Toggle(_displayContents, "Show content");
            if (_displayContents)
                GUILayout.Label(editorHandler.GetContents());
        }
    }
}