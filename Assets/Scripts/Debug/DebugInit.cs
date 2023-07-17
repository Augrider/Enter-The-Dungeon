using System.Collections;
using System.Collections.Generic;
using Game.Items.Components;
using Game.Player.Components;
using UnityEngine;

namespace Game.Debug
{
    public class DebugInit : MonoBehaviour
    {
        [SerializeField] private PlayerInput _playerInput;
        [SerializeField] private GameObject _UiObject;

        [SerializeField] private ItemDatabaseSO _itemDatabase;


        void Awake()
        {
            ItemDatabase.Provide(_itemDatabase);
        }

        IEnumerator Start()
        {
            yield return null;

            _playerInput.enabled = true;
            _UiObject.SetActive(true);
        }
    }
}