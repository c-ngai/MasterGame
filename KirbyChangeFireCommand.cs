﻿using MasterGame.Entities.Players;
namespace MasterGame.Commands
{
    public class KirbyChangeFireCommand : ICommand
    {
        private IPlayer kirby;
        public KirbyChangeFireCommand(IPlayer player)
        {
            kirby = player;
        }

        public void Execute()
        {
            kirby.ChangeToFire();
        }

        public void Undo()
        {

        }
    }
}
