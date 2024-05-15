using RealityCollective.ServiceFramework.Definitions;
using RealityCollective.ServiceFramework.Services;

namespace DungeonsSample.Dungeons
{
    [System.Runtime.InteropServices.Guid("f978dcf9-b1da-4246-b671-fed7220226d5")]
    public class DungeonsService : BaseServiceWithConstructor, IDungeonsService
    {
        public DungeonsService(string name, uint priority, BaseProfile profile)
            : base(name, priority)
        {
            // The constructor should be used to gather required properties from the profile (or cache the profile) and to ready any components needed.
            // Note, during this call, not all services will be registered with the Service Framework, so this should only be used to ready this individual service.
        }

        /// <inheritdoc/>
        public DungeonController CurrentDungeon { get; private set; }

        /// <inheritdoc/>
        public bool IsCleared { get; private set; }

        /// <inheritdoc/>
        public event OnDungeonDelegate DungeonEntered;

        /// <inheritdoc/>
        public event OnDungeonDelegate DungeonCleared;

        /// <inheritdoc/>
        public void EnterDungeon(DungeonController dungeon)
        {
            CurrentDungeon = dungeon;
            IsCleared = false;
            DungeonEntered?.Invoke(dungeon);
        }

        /// <inheritdoc/>
        public void ClearDungeon(DungeonController dungeon)
        {
            if (dungeon != CurrentDungeon || IsCleared)
            {
                return;
            }

            IsCleared = true;
            DungeonCleared?.Invoke(dungeon);
        }
    }
}
