using Game.Items.Components;
using Game.Plugins.ObjectPool;
using UnityEngine;

namespace Game.Map.Components
{
    public class ItemShop : MonoBehaviour
    {
        [SerializeField] private ItemTable _shopItems;
        [SerializeField] private ItemShopSpot[] _itemPlaces;
        [SerializeField] private int _price;

        //When spawned, place random items on provided spots with prices
        //Spot is interactable which takes item price and subtracts it from player money if he have enough and gives that item
        //Item shouldn't be interactable when placed here
        //Also, item rarities are used when choosing items for sale

        //Required systems: money, interactable toggle, item rarities

        void Start()
        {
            //Fill places with items from Item table
            foreach (var spot in _itemPlaces)
            {
                var item = ObjectPoolLocator.Service.GetItem(_shopItems.GetRandomPrefab());

                spot.PlaceItem(item, _price);
            }
        }
    }
}