﻿namespace TexasHoldem.Logic.GameMechanics
{
    using System.Collections.Generic;

    public struct Pot
    {
        public Pot(int amountOfMoney, IReadOnlyList<string> participants)
        {
            this.AmountOfMoney = amountOfMoney;
            this.Participants = participants;
        }

        public int AmountOfMoney { get; }

        public IReadOnlyList<string> Participants { get; }
    }
}
