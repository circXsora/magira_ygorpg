//-------------------------------------------------------------------------------
// <copyright file="TransitionResult.cs" company="Appccelerate">
//   Copyright (c) 2008-2019 Appccelerate
//
//   Licensed under the Apache License, Version 2.0 (the "License");
//   you may not use this file except in compliance with the License.
//   You may obtain a copy of the License at
//
//       http://www.apache.org/licenses/LICENSE-2.0
//
//   Unless required by applicable law or agreed to in writing, software
//   distributed under the License is distributed on an "AS IS" BASIS,
//   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//   See the License for the specific language governing permissions and
//   limitations under the License.
// </copyright>
//-------------------------------------------------------------------------------

namespace Appccelerate.StateMachine.AsyncMachine.Transitions
{
    using System;

    public class TransitionResult<TState>
        : ITransitionResult<TState>
        where TState : IComparable
    {
        public static readonly ITransitionResult<TState> NotFired = new TransitionResult<TState>(false, default(TState));

        public TransitionResult(bool fired, TState newState)
        {
            this.Fired = fired;
            this.NewState = newState;
        }

        /// <summary>
        /// Gets a value indicating whether this <see cref="ITransitionResult{TState}"/> is fired.
        /// </summary>
        /// <value><c>true</c> if fired; otherwise, <c>false</c>.</value>
        public bool Fired { get; }

        /// <summary>
        /// Gets the new state the state machine is in.
        /// </summary>
        /// <value>The new state.</value>
        public TState NewState { get; }
    }
}