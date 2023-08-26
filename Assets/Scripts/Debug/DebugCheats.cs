using UnityEngine;
using Game.Player;

namespace Game.Debug
{
    [DisallowMultipleComponent]
    public class DebugCheats : MonoBehaviour
    {
        [SerializeField] private Rect _menuArea;


        void OnGUI()
        {
            GUILayout.BeginArea(_menuArea);

            if (Players.IsCurrentAlive)
                PlayerCheatsButtons();

            GUILayout.EndArea();
        }



        private void PlayerCheatsButtons()
        {
            GUILayout.Label($"Player cheats");
            GUILayout.BeginHorizontal();

            if (GUILayout.Button("Heal"))
            {
                Players.Current.State.Health = Players.Current.Stats.MaxHealth;
            }

            if (GUILayout.Button("Refill Ammo"))
            {
                Players.Current.Weapons.Current.State.TotalAmmo = Players.Current.Weapons.Current.Stats.MaxAmmo;
            }

            if (GUILayout.Button("Add 10 Currency"))
            {
                Players.Current.State.Currency += 10;
            }

            GUILayout.EndHorizontal();
        }
    }
}