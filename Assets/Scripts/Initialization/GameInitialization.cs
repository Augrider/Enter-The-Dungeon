using System.Collections;
using Game.Items.Components;
using Game.State;
using UnityEngine;

namespace Game.Initialization
{
    public class GameInitialization : MonoBehaviour
    {
        [SerializeField] private ItemDatabaseSO _itemDatabase;


        void Awake()
        {
            ItemDatabase.Provide(_itemDatabase);
        }

        IEnumerator Start()
        {
            yield return new WaitForSeconds(0.5f);

            GameStateLocator.Service.Control.ToMainMenu();
        }
    }
}