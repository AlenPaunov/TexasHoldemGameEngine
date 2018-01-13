﻿namespace TexasHoldem.Logic.GameMechanics
{
    using System.Collections.Generic;
    using System.Linq;

    using TexasHoldem.Logic.Players;

    internal class MultiplePlayersBettingLogic : BaseBettingLogic
    {
        public MultiplePlayersBettingLogic(IList<IInternalPlayer> players, int smallBlind)
            : base(players, smallBlind)
        {
        }

        public override void Bet(GameRoundType gameRoundType)
        {
            this.RoundBets.Clear();
            var playerIndex = 1;

            if (gameRoundType == GameRoundType.PreFlop)
            {
                this.PlaceBlinds();
                playerIndex = 3;
            }

            while (this.AllPlayers.Count(x => x.PlayerMoney.InHand) >= 2
                   && this.AllPlayers.Any(x => x.PlayerMoney.ShouldPlayInRound))
            {
                var player = this.AllPlayers[playerIndex % this.AllPlayers.Count];
                if (!player.PlayerMoney.InHand || !player.PlayerMoney.ShouldPlayInRound)
                {
                    if (player.PlayerMoney.InHand == player.PlayerMoney.ShouldPlayInRound)
                    {
                        playerIndex++;
                    }

                    continue;
                }

                var maxMoneyPerPlayer = this.AllPlayers.Max(x => x.PlayerMoney.CurrentRoundBet);
                var action =
                    player.GetTurn(
                        new GetTurnContext(
                            gameRoundType,
                            this.RoundBets.AsReadOnly(),
                            this.SmallBlind,
                            player.PlayerMoney.Money,
                            this.Pot,
                            player.PlayerMoney.CurrentRoundBet,
                            maxMoneyPerPlayer));

                action = player.PlayerMoney.DoPlayerAction(action, maxMoneyPerPlayer);
                this.RoundBets.Add(new PlayerActionAndName(player.Name, action));

                if (action.Type == PlayerActionType.Raise)
                {
                    // When raising, all players are required to do action afterwards in current round
                    foreach (var playerToUpdate in this.AllPlayers)
                    {
                        playerToUpdate.PlayerMoney.ShouldPlayInRound = playerToUpdate.PlayerMoney.InHand ? true : false;
                    }
                }

                player.PlayerMoney.ShouldPlayInRound = false;
                playerIndex++;
            }

            this.ReturnMoneyInCaseOfAllIn();
        }

        protected override void PlaceBlinds()
        {
            // Small blind
            this.RoundBets.Add(
                new PlayerActionAndName(
                    this.AllPlayers[1].Name,
                    this.AllPlayers[1].PlayerMoney.DoPlayerAction(
                        this.AllPlayers[1].ToPostBlind(this.AllPlayers[1].PlayerMoney.Money, 1, 0), 0)));

            // Big blind
            this.RoundBets.Add(
                new PlayerActionAndName(
                    this.AllPlayers[2].Name,
                    this.AllPlayers[2].PlayerMoney.DoPlayerAction(
                        this.AllPlayers[2].ToPostBlind(this.AllPlayers[2].PlayerMoney.Money, 2, 1), 0)));
        }
    }
}