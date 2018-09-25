using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Minimax
{
    interface IState
    {
        int currentValue { get; set; }
        List<IState> Expand(IPlayer player, IPlayer otherPlayer);
        int Value(IPlayer player);
    }

    interface IPlayer
    {

    }

    class MiniMax
    {
        public static IState Select(IState state, IPlayer player, IPlayer otherPlayer, int depth, bool maximising)
        {
            IState nextState = state;
            state.currentValue = state.Value(player);
            if (depth == 0 || state.currentValue == Int32.MaxValue || state.currentValue == Int32.MinValue)
                return (state);

            IState childState;
            if (maximising)
            {
                state.currentValue = Int32.MinValue;
                nextState = null;
                List<IState> childstates = state.Expand(player, otherPlayer);
                if (childstates.Count == 0)
                    return (state);

                foreach (IState s in childstates)
                {
                    childState = Select(s, otherPlayer, player, depth - 1, false);
                    if (childState != null && childState.currentValue >= state.currentValue)
                    {
                        nextState = s;
                        state.currentValue = childState.currentValue;
                    }
                }
            }
            else
            {
                state.currentValue = Int32.MaxValue;
                nextState = null;
                List<IState> childstates = state.Expand(player, otherPlayer);
                if (childstates.Count == 0)
                    return (state);

                foreach (IState s in childstates)
                {
                    childState = Select(s, otherPlayer, player, depth - 1, true);

                    if (childState != null && childState.currentValue <= state.currentValue)
                    {
                        nextState = s;
                        state.currentValue = childState.currentValue;
                    }

                }
            }
            return (nextState);
        }


    }
}