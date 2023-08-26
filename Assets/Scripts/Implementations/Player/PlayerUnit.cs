using Game.Units.Components;
using UnityEngine;

namespace Game.Player.Components
{
    /// <summary>
    /// A type of unit used by players.
    /// Contains references to player components
    /// </summary>
    public class PlayerUnit : Unit, IPlayerUnit
    {
        //Unit shouldn't have default state?
        private PlayerUnitState _playerState;

        [SerializeField] private Interactions _playerInteractions;
        [SerializeField] private ItemsInventory _playerInventory;
        [SerializeField] private WeaponsInventory _playerWeapons;

        [SerializeField] private PlayerUnitActions _playerActions;

        public static event System.Action<PlayerUnit> PlayerUnitSpawned;
        public static event System.Action PlayerUnitDied;

        public string CharacterID { get; internal set; }

        public Vector3 Position => Transform.Position;

        public PlayerStats Stats { get => _playerState.Stats; set => _playerState.Stats = value; }
        public IPlayerState State => _playerState;

        public Interactions Interactions => _playerInteractions;
        public PlayerUnitActions Actions => _playerActions;

        IPlayerItems IPlayer.Inventory => _playerInventory;
        IPlayerWeapons IPlayer.Weapons => _playerWeapons;

        public ItemsInventory Inventory => _playerInventory;
        public WeaponsInventory Weapons => _playerWeapons;


        void Awake()
        {
            _playerState = new PlayerUnitState(this);
        }

        void OnEnable()
        {
            PlayerUnitSpawned?.Invoke(this);
        }

        void OnDisable()
        {
            PlayerUnitDied?.Invoke();
        }


        public void Import(PlayerFullData playerData)
        {
            CharacterID = playerData.CharacterID;

            _playerState.SetDefaultStats(PlayerDatabase.GetDefaultStats(CharacterID));
            _playerState.ClearStatsDelta();

            _playerState.Health = playerData.State.Health;
            _playerState.Currency = playerData.State.Currency;

            Inventory.Import(playerData.State);
            Weapons.Import(playerData.State);
        }

        public PlayerFullData Export()
        {
            var playerState = new PlayerStateData();

            playerState.Health = State.Health;
            playerState.Currency = _playerState.Currency;

            playerState.Items = Inventory.Export();
            playerState.WeaponData = Weapons.Export();

            return new PlayerFullData(CharacterID, playerState);
        }
    }
}