using Microsoft.Xna.Framework.Input;
using System.Linq;
using KirbyNightmareInDreamLand.Time;
using KirbyNightmareInDreamLand.Entities.Players;
using KirbyNightmareInDreamLand.Controllers;

namespace KirbyNightmareInDreamLand.Commands
{
    public class KirbyCrouchCommand : ICommand
    {
        private IPlayer kirby;

        private KeyboardController keyboard;
        private Keys key;

        public KirbyCrouchCommand(IPlayer player, Keys keyMapped, KeyboardController currentKeyboard)
        {
            kirby = player;
            key = keyMapped;
            keyboard = currentKeyboard;
        }
        public void Execute()
        {
            kirby.Crouch();

            // Calls corresponding stop key to deal with sliding/stopping mechanic
            keyboard.stopKeys[key].Execute();
        }
    }
}