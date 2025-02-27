//-------------------------------------------------------------------------------
// <copyright file="TransitionInfo.cs" company="Appccelerate">
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
    using System.Collections.Generic;
    using ActionHolders;
    using GuardHolders;
    using States;

    /// <summary>
    /// Describes a transition.
    /// </summary>
    /// <typeparam name="TState">Type fo the states.</typeparam>
    /// <typeparam name="TEvent">Type of the events.</typeparam>
    public class TransitionInfo<TState, TEvent>
        where TState : IComparable
        where TEvent : IComparable
    {
        public TransitionInfo(TEvent eventId, IStateDefinition<TState, TEvent> source, IStateDefinition<TState, TEvent> target, IGuardHolder guard, IEnumerable<IActionHolder> actions)
        {
            this.EventId = eventId;
            this.Source = source;
            this.Target = target;
            this.Guard = guard;
            this.Actions = actions;
        }

        /// <summary>
        /// Gets the event id.
        /// </summary>
        /// <value>The event id.</value>
        public TEvent EventId { get; }

        /// <summary>
        /// Gets the source.
        /// </summary>
        /// <value>The source.</value>
        public IStateDefinition<TState, TEvent> Source { get; }

        /// <summary>
        /// Gets the target.
        /// </summary>
        /// <value>The target.</value>
        public IStateDefinition<TState, TEvent> Target { get; }

        /// <summary>
        /// Gets the guard.
        /// </summary>
        /// <value>The guard.</value>
        public IGuardHolder Guard { get; }

        /// <summary>
        /// Gets the actions.
        /// </summary>
        /// <value>The actions.</value>
        public IEnumerable<IActionHolder> Actions { get; }
    }
}