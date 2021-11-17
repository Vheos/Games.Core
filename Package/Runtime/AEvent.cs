namespace Vheos.Tools.UnityCore
{
    using System;

    abstract public class AEvent
    {
        // Public
        public int ActionsCount
        => InternalEvent == null ? 0 : InternalEvent.GetInvocationList().Length;
        public bool IsEmpty
        => InternalEvent == null;

        // Privates
        abstract internal void SubscribeAuto(AEventSubscriber subscriber);
        abstract internal void UnsubscribeAuto(AEventSubscriber subscriber);
        abstract protected Delegate InternalEvent
        { get; }
    }
}