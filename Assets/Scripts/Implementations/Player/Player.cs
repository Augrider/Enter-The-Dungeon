using Game.Units;
using UnityEngine;

namespace Game.Player.Components
{
    //TODO: Separate Player class from Unit, make player as standalone class in order to simplify save/load between levels
    public class Player : MonoBehaviour, IPlayer
    {
        [SerializeField] private Units.Components.Unit _unit;

        [SerializeField] private PlayerInput _playerInput;

        [SerializeField] private PlayerEvents _playerEvents;

        [SerializeField] private PlayerState _playerState;

        [SerializeField] private PlayerInteractions _playerInteractions;
        [SerializeField] private PlayerInventory _playerInventory;
        [SerializeField] private PlayerWeapons _playerWeapons;

        internal PlayerEvents Events => _playerEvents;

        public bool InputEnabled { get => _playerInput.enabled; set => _playerInput.enabled = value; }

        public IUnit Unit => _unit;
        public IUnitState UnitState => Unit.State;

        public PlayerStats Stats
        {
            get => State.Stats;
            set => State.Stats = value;
        }

        public IPlayerState State => _playerState;

        IPlayerEvents IPlayer.Events => _playerEvents;

        public IPlayerInteractions Interactions => _playerInteractions;
        public IPlayerItems Inventory => _playerInventory;
        public IPlayerWeapons Weapons => _playerWeapons;


        //TODO: Set player from outside
        //TODO: Make Interactions independent from being on player

        void Awake()
        {
            Players.Provide(this);

            Unit.State.MaxHealth = Stats.MaxHealth;
            Unit.State.Health = Stats.MaxHealth;
        }


        void OnEnable()
        {
            Players.Provide(this);
        }

        void OnDisable()
        {
            Players.Provide(null);
        }
    }
}