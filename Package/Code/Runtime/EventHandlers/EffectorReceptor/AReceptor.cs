namespace Vheos.Games.Core
{
    using System;
    using System.Collections.Generic;
    using UnityEngine;
    using Tools.Extensions.Collections;
    using Tools.Extensions.General;

    public abstract class AReceptor<TReceptor, TEffector> : ABaseComponent
        where TReceptor : AReceptor<TReceptor, TEffector>
        where TEffector : AEffector<TEffector, TReceptor>
    {
        // Abstract
        abstract public void Clear();
        abstract internal bool IsEffected
        { get; }
        abstract internal bool IsEffectedBy(TEffector effector);
        abstract internal void GainEffectFrom(TEffector effector);
        abstract internal void LoseEffectFrom(TEffector effector);

        // Common
        public void AddTest(Func<TEffector, bool> test)
        => _effectorTests.Add(test);
        public void RemoveTest(Func<TEffector, bool> test)
        => _effectorTests.Remove(test);
        internal bool CanGainEffectFrom(TEffector effector)
        {
            foreach (var test in _effectorTests)
                if (!test(effector))
                    return false;
            return true;
        }
        protected readonly HashSet<Func<TEffector, bool>> _effectorTests = new();

        // Play
        protected override void PlayDisable()
        {
            base.PlayDisable();
            Clear();
        }
    }
}