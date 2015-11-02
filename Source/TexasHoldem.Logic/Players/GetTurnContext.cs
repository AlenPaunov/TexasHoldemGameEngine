﻿namespace TexasHoldem.Logic.Players
{
    using System.Collections.Generic;

    public class GetTurnContext
    {
        public GetTurnContext(
            GameRoundType roundType,
            IReadOnlyCollection<PlayerActionAndName> previousRoundActions,
            int smallBlind,
            int moneyLeft,
            int currentPot,
            int myMoneyInThePot,
            int maxMoneyPerPlayerInThePot)
        {
            this.RoundType = roundType;
            this.PreviousRoundActions = previousRoundActions;
            this.SmallBlind = smallBlind;
            this.MoneyLeft = moneyLeft;
            this.CurrentPot = currentPot;
            this.MyMoneyInThePot = myMoneyInThePot;
            this.MaxMoneyPerPlayerInThePot = maxMoneyPerPlayerInThePot;
        }

        public GameRoundType RoundType { get; }

        public IReadOnlyCollection<PlayerActionAndName> PreviousRoundActions { get; }

        public int SmallBlind { get; }

        public int MoneyLeft { get; }

        public int CurrentPot { get; }

        public int MyMoneyInThePot { get; }

        public int MaxMoneyPerPlayerInThePot { get; }

        public bool CanCheck => this.MyMoneyInThePot == this.MaxMoneyPerPlayerInThePot;
    }
}
