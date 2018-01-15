﻿namespace TexasHoldem.Logic.Players
{
    using System.Collections.Generic;

    public class GetTurnContext : IGetTurnContext
    {
        public GetTurnContext(
            GameRoundType roundType,
            IReadOnlyCollection<PlayerActionAndName> previousRoundActions,
            int smallBlind,
            int moneyLeft,
            int currentPot,
            int myMoneyInTheRound,
            int currentMaxBet,
            int minRaise)
        {
            this.RoundType = roundType;
            this.PreviousRoundActions = previousRoundActions;
            this.SmallBlind = smallBlind;
            this.MoneyLeft = moneyLeft;
            this.CurrentPot = currentPot;
            this.MyMoneyInTheRound = myMoneyInTheRound;
            this.CurrentMaxBet = currentMaxBet;
            this.MinRaise = minRaise;
        }

        public GameRoundType RoundType { get; }

        public IReadOnlyCollection<PlayerActionAndName> PreviousRoundActions { get; }

        public int SmallBlind { get; }

        public int MoneyLeft { get; }

        public int CurrentPot { get; }

        public int MyMoneyInTheRound { get; }

        public int CurrentMaxBet { get; }

        public bool CanCheck => this.MyMoneyInTheRound == this.CurrentMaxBet;

        public int MoneyToCall => this.CurrentMaxBet - this.MyMoneyInTheRound;

        public bool IsAllIn => this.MoneyLeft <= 0;

        public int MinRaise { get; }
    }
}
