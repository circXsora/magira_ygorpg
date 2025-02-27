//-------------------------------------------------------------------------------
// <copyright file="StateBuilder.cs" company="Appccelerate">
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

namespace Appccelerate.StateMachine.Machine
{
    using System;
    using System.Linq;
    using Events;
    using States;
    using Syntax;
    using Transitions;

    /// <summary>
    /// Provides operations to build a state machine.
    /// </summary>
    /// <typeparam name="TState">The type of the state.</typeparam>
    /// <typeparam name="TEvent">The type of the event.</typeparam>
    public sealed class StateBuilder<TState, TEvent> :
        IEntryActionSyntax<TState, TEvent>,
        IGotoInIfSyntax<TState, TEvent>,
        IOtherwiseSyntax<TState, TEvent>,
        IIfOrOtherwiseSyntax<TState, TEvent>,
        IGotoSyntax<TState, TEvent>,
        IIfSyntax<TState, TEvent>,
        IOnSyntax<TState, TEvent>
        where TState : IComparable
        where TEvent : IComparable
    {
        private readonly StateDefinition<TState, TEvent> stateDefinition;
        private readonly IImplicitAddIfNotAvailableStateDefinitionDictionary<TState, TEvent> stateDefinitionDictionary;
        private readonly IFactory<TState, TEvent> factory;

        private TransitionDefinition<TState, TEvent> currentTransitionDefinition;

        private TEvent currentEventId;

        public StateBuilder(
            TState stateId,
            IImplicitAddIfNotAvailableStateDefinitionDictionary<TState, TEvent> stateDefinitionDictionary,
            IFactory<TState, TEvent> factory)
        {
            this.stateDefinitionDictionary = stateDefinitionDictionary;
            this.factory = factory;
            this.stateDefinition = this.stateDefinitionDictionary[stateId];
        }

        /// <summary>
        /// Defines entry actions.
        /// </summary>
        /// <param name="action">The action.</param>
        /// <returns>Exit action syntax.</returns>
        IEntryActionSyntax<TState, TEvent> IEntryActionSyntax<TState, TEvent>.ExecuteOnEntry(Action action)
        {
            Guard.AgainstNullArgument("action", action);

            this.stateDefinition.EntryActionsModifiable.Add(this.factory.CreateActionHolder(action));

            return this;
        }

        public IEntryActionSyntax<TState, TEvent> ExecuteOnEntry<T>(Action<T> action)
        {
            Guard.AgainstNullArgument("action", action);

            this.stateDefinition.EntryActionsModifiable.Add(this.factory.CreateActionHolder(action));

            return this;
        }

        /// <summary>
        /// Defines an entry action.
        /// </summary>
        /// <typeparam name="T">Type of the parameter of the entry action method.</typeparam>
        /// <param name="action">The action.</param>
        /// <param name="parameter">The parameter that will be passed to the entry action.</param>
        /// <returns>Exit action syntax.</returns>
        IEntryActionSyntax<TState, TEvent> IEntryActionSyntax<TState, TEvent>.ExecuteOnEntryParametrized<T>(Action<T> action, T parameter)
        {
            this.stateDefinition.EntryActionsModifiable.Add(this.factory.CreateActionHolder(action, parameter));

            return this;
        }

        /// <summary>
        /// Defines an exit action.
        /// </summary>
        /// <param name="action">The action.</param>
        /// <returns>Event syntax.</returns>
        IExitActionSyntax<TState, TEvent> IExitActionSyntax<TState, TEvent>.ExecuteOnExit(Action action)
        {
            Guard.AgainstNullArgument("action", action);

            this.stateDefinition.ExitActionsModifiable.Add(this.factory.CreateActionHolder(action));

            return this;
        }

        public IExitActionSyntax<TState, TEvent> ExecuteOnExit<T>(Action<T> action)
        {
            Guard.AgainstNullArgument("action", action);

            this.stateDefinition.ExitActionsModifiable.Add(this.factory.CreateActionHolder(action));

            return this;
        }

        /// <summary>
        /// Defines an exit action.
        /// </summary>
        /// <typeparam name="T">Type of the parameter of the exit action method.</typeparam>
        /// <param name="action">The action.</param>
        /// <param name="parameter">The parameter that will be passed to the exit action.</param>
        /// <returns>Exit action syntax.</returns>
        IExitActionSyntax<TState, TEvent> IExitActionSyntax<TState, TEvent>.ExecuteOnExitParametrized<T>(Action<T> action, T parameter)
        {
            this.stateDefinition.ExitActionsModifiable.Add(this.factory.CreateActionHolder(action, parameter));

            return this;
        }

        /// <summary>
        /// Builds a transition.
        /// </summary>
        /// <param name="eventId">The event that triggers the transition.</param>
        /// <returns>Syntax to build the transition.</returns>
        IOnSyntax<TState, TEvent> IEventSyntax<TState, TEvent>.On(TEvent eventId)
        {
            this.currentEventId = eventId;

            this.CreateTransition();

            return this;
        }

        private void CreateTransition()
        {
            this.currentTransitionDefinition = new TransitionDefinition<TState, TEvent>();
            this.stateDefinition.TransitionsModifiable.Add(this.currentEventId, this.currentTransitionDefinition);
        }

        /// <summary>
        /// Defines where to go in response to an event.
        /// </summary>
        /// <param name="target">The target.</param>
        /// <returns>Execute syntax.</returns>
        IGotoSyntax<TState, TEvent> IOnSyntax<TState, TEvent>.Goto(TState target)
        {
            this.SetTargetState(target);

            return this;
        }

        IGotoSyntax<TState, TEvent> IOtherwiseSyntax<TState, TEvent>.Goto(TState target)
        {
            this.SetTargetState(target);

            return this;
        }

        IGotoInIfSyntax<TState, TEvent> IIfSyntax<TState, TEvent>.Goto(TState target)
        {
            this.SetTargetState(target);

            return this;
        }

        IOtherwiseExecuteSyntax<TState, TEvent> IOtherwiseExecuteSyntax<TState, TEvent>.Execute(Action action)
        {
            return this.ExecuteInternal(action);
        }

        IOtherwiseExecuteSyntax<TState, TEvent> IOtherwiseExecuteSyntax<TState, TEvent>.Execute<T>(Action<T> action)
        {
            return this.ExecuteInternal(action);
        }

        IGotoInIfSyntax<TState, TEvent> IGotoInIfSyntax<TState, TEvent>.Execute(Action action)
        {
            return this.ExecuteInternal(action);
        }

        IGotoInIfSyntax<TState, TEvent> IGotoInIfSyntax<TState, TEvent>.Execute<T>(Action<T> action)
        {
            return this.ExecuteInternal(action);
        }

        IGotoSyntax<TState, TEvent> IGotoSyntax<TState, TEvent>.Execute(Action action)
        {
            return this.ExecuteInternal(action);
        }

        IGotoSyntax<TState, TEvent> IGotoSyntax<TState, TEvent>.Execute<T>(Action<T> action)
        {
            return this.ExecuteInternal(action);
        }

        IIfOrOtherwiseSyntax<TState, TEvent> IIfSyntax<TState, TEvent>.Execute(Action action)
        {
            return this.ExecuteInternal(action);
        }

        IIfOrOtherwiseSyntax<TState, TEvent> IIfSyntax<TState, TEvent>.Execute<T>(Action<T> action)
        {
            return this.ExecuteInternal(action);
        }

        IOnExecuteSyntax<TState, TEvent> IOnExecuteSyntax<TState, TEvent>.Execute(Action action)
        {
            return this.ExecuteInternal(action);
        }

        IOnExecuteSyntax<TState, TEvent> IOnExecuteSyntax<TState, TEvent>.Execute<T>(Action<T> action)
        {
            return this.ExecuteInternal(action);
        }

        IIfSyntax<TState, TEvent> IGotoInIfSyntax<TState, TEvent>.If<T>(Func<T, bool> guard)
        {
            this.CreateTransition();

            this.SetGuard(guard);

            return this;
        }

        IIfSyntax<TState, TEvent> IGotoInIfSyntax<TState, TEvent>.If(Func<bool> guard)
        {
            this.CreateTransition();

            this.SetGuard(guard);

            return this;
        }

        IOtherwiseSyntax<TState, TEvent> IGotoInIfSyntax<TState, TEvent>.Otherwise()
        {
            this.CreateTransition();

            return this;
        }

        IIfOrOtherwiseSyntax<TState, TEvent> IIfOrOtherwiseSyntax<TState, TEvent>.Execute(Action action)
        {
            return this.ExecuteInternal(action);
        }

        IIfOrOtherwiseSyntax<TState, TEvent> IIfOrOtherwiseSyntax<TState, TEvent>.Execute<T>(Action<T> action)
        {
            return this.ExecuteInternal(action);
        }

        private StateBuilder<TState, TEvent> ExecuteInternal(Action action)
        {
            this.currentTransitionDefinition.ActionsModifiable.Add(this.factory.CreateTransitionActionHolder(action));

            this.CheckGuards();

            return this;
        }

        private StateBuilder<TState, TEvent> ExecuteInternal<T>(Action<T> action)
        {
            this.currentTransitionDefinition.ActionsModifiable.Add(this.factory.CreateTransitionActionHolder(action));

            this.CheckGuards();

            return this;
        }

        IIfSyntax<TState, TEvent> IOnSyntax<TState, TEvent>.If<T>(Func<T, bool> guard)
        {
            this.SetGuard(guard);

            return this;
        }

        IIfSyntax<TState, TEvent> IOnSyntax<TState, TEvent>.If(Func<bool> guard)
        {
            this.SetGuard(guard);

            return this;
        }

        IIfSyntax<TState, TEvent> IIfOrOtherwiseSyntax<TState, TEvent>.If<T>(Func<T, bool> guard)
        {
            this.CreateTransition();

            this.SetGuard(guard);

            return this;
        }

        IIfSyntax<TState, TEvent> IIfOrOtherwiseSyntax<TState, TEvent>.If(Func<bool> guard)
        {
            this.CreateTransition();

            this.SetGuard(guard);

            return this;
        }

        IOtherwiseSyntax<TState, TEvent> IIfOrOtherwiseSyntax<TState, TEvent>.Otherwise()
        {
            this.CreateTransition();

            return this;
        }

        private void SetGuard<T>(Func<T, bool> guard)
        {
            this.currentTransitionDefinition.Guard = this.factory.CreateGuardHolder(guard);
        }

        private void SetGuard(Func<bool> guard)
        {
            this.currentTransitionDefinition.Guard = this.factory.CreateGuardHolder(guard);
        }

        private void SetTargetState(TState target)
        {
            this.currentTransitionDefinition.Target = this.stateDefinitionDictionary[target];

            this.CheckGuards();
        }

        private void CheckGuards()
        {
            var transitionsByEvent = this.stateDefinition.TransitionsModifiable.GetTransitions().GroupBy(t => t.EventId).ToList();
            var withMoreThenOneTransitionWithoutGuard = transitionsByEvent.Where(g => g.Count(t => t.Guard == null) > 1);

            if (withMoreThenOneTransitionWithoutGuard.Any())
            {
                throw new InvalidOperationException(ExceptionMessages.OnlyOneTransitionMayHaveNoGuard);
            }

            if ((from grouping in transitionsByEvent
                 let transition = grouping.SingleOrDefault(t => t.Guard == null)
                 where transition != null && grouping.LastOrDefault() != transition
                 select grouping).Any())
            {
                throw new InvalidOperationException(ExceptionMessages.TransitionWithoutGuardHasToBeLast);
            }
        }
    }
}