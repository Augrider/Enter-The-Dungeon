using System.Collections;
using Game.Items.Components;
using Game.Player;
using Game.Player.Components;
using Game.State;
using UnityEngine;

namespace Game.Debug
{
    public class DebugInit : MonoBehaviour
    {
        [SerializeField] private ItemDatabaseSO _itemDatabase;
        [SerializeField] private PlayerDatabaseSO _playerDatabase;

        [SerializeField] private PlayerData _selectedCharacter;
        [SerializeField] private GameObject _playerObject;


        void Awake()
        {
            ItemDatabase.Provide(_itemDatabase);
            PlayerDatabase.Provide(_playerDatabase);

            Players.Provide(new Player.Components.Player());
        }

        // Start is called before the first frame update
        IEnumerator Start()
        {
            yield return null;

            Players.Current.Import(_selectedCharacter.GetFullData());
            _playerObject.SetActive(true);

            yield return null;

            // Players.Current.InputEnabled = true;
            GameStateLocator.Service.Control.TogglePause(false);
        }
    }
}