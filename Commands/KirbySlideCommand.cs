using KirbyNightmareInDreamLand.Controllers;
using KirbyNightmareInDreamLand.Entities.Players;
using KirbyNightmareInDreamLand.Time;
using Microsoft.Xna.Framework.Input;
using System;
using System.Linq;

namespace KirbyNightmareInDreamLand.Commands
{
    public class KirbySlideCommand : ICommand
    {
        private Game1 game;
        private KeyboardController keyboard;
        private Keys slideKey;
        private Keys crouchKey;
        private TimeCalculator timer;
        private double startingTime;
        private IPlayer kirby;
        private bool isSliding;
        public KirbySlideCommand(Game1 newGame, KeyboardController newKeyboard, Keys newCrouchKey, Keys newSlideKey, IPlayer player)
        {
            game = newGame;
            keyboard = newKeyboard;
            crouchKey = newCrouchKey;
            slideKey = newSlideKey;
            kirby = player;
            timer = new TimeCalculator();
            startingTime = 0;
            isSliding = false;
        }

        public void Execute()
        {
            // Determines if a timer needs to be set to keep track of slide time when the attack key is also pressed and if the timer needs to be reset
            if (keyboard.currentState.Contains(slideKey) && startingTime == 0)
            {
                startingTime = timer.GetCurrentTimeInMS(game.time);
                isSliding = true;
            }
            else if (keyboard.currentState.Contains(slideKey) && startingTime != 0)
            {
                isSliding = timer.GetCurrentTimeInMS(game.time) - startingTime < Constants.Controller.SLIDE_TIME;
            }
            else 
            {
                startingTime = 0;
                isSliding = false;
            }

            if (isSliding)
            {
                kirby.Slide();
            }
            else if (!keyboard.currentState.Contains(crouchKey))
            {
                kirby.EndCrouch();
            }
        }
    }
}
