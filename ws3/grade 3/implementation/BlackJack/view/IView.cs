﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlackJack.view
{
    interface IView : model.rules.IRulesFactoryVisitor
    {
        void DisplayWelcomeMessage();
        void DisplayCardIsBeingDealt();
        void DisplayNewGameSetup();
        void CollectDesiredPlayerAction();
        bool WantsToPlay();
        bool WantsToHit();
        bool WantsToStand();
        bool WantsToQuit();
        void DisplayCard(model.Card a_card);
        void DisplayPlayerHand(IEnumerable<model.Card> a_hand, int a_score);
        void DisplayDealerHand(IEnumerable<model.Card> a_hand, int a_score);
        void DisplayGameOver(bool a_dealerIsWinner);
    }
}
