using Game.Player;
using UnityEngine;

namespace Game.UI.HUD
{
    public class HudComponent : MonoBehaviour
    {
        [SerializeField] private WeaponIndicator weaponIndicator;
        [SerializeField] private CurrencyIndicator currencyIndicator;
        [SerializeField] private HealthIndicator healthIndicator;


        public void SetPlayer(IPlayer player)
        {
            weaponIndicator.SetPlayer(player);
            currencyIndicator.SetPlayer(player);
            healthIndicator.SetPlayer(player);
        }

        public void ResetPlayer()
        {
            weaponIndicator.ClearPlayer();
            currencyIndicator.ClearPlayer();
            healthIndicator.ClearPlayer();
        }
    }
}