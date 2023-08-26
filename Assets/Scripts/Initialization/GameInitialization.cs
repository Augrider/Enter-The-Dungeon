using System.Collections;
using Game.Items.Components;
using Game.Player;
using Game.Player.Components;
using Game.State;
using UnityEngine;

namespace Game.Initialization
{
    public class GameInitialization : MonoBehaviour
    {
        [SerializeField] private ItemDatabaseSO _itemDatabase;
        [SerializeField] private PlayerDatabaseSO _playerDatabase;


        void Awake()
        {
            ItemDatabase.Provide(_itemDatabase);
            PlayerDatabase.Provide(_playerDatabase);
            
            Players.Provide(new Player.Components.Player());
        }

        IEnumerator Start()
        {
            yield return new WaitForSeconds(0.5f);

            GameStateLocator.Service.Control.ToMainMenu();
        }
    }
}