namespace Vheos.Games.Core
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using UnityEngine;
    using Tools.Extensions.General;
    using Tools.Extensions.Math;
    using Tools.Extensions.UnityObjects;

    public abstract class AEffector<TEffector, TReceptor> : ABaseComponent
        where TEffector : AEffector<TEffector, TReceptor>
        where TReceptor : AReceptor<TReceptor, TEffector>
    {
        // Abstract
        abstract public void Clear();
        abstract protected bool IsEffectingAny
        { get; }
        abstract protected bool IsEffecting(TReceptor receptor);
        abstract protected bool IsEffecting<T>() where T : Component;
        abstract protected bool TryGetReceptor(out TReceptor receptor);
        abstract protected bool TryGetReceptor<T>(out T component) where T : Component;

        // Common
        public void AddTest(Func<TReceptor, bool> test)
        => _receptorTests.Add(test);
        public void RemoveTest(Func<TReceptor, bool> test)
        => _receptorTests.Remove(test);
        private bool CanEffect(TReceptor receptor)
        {
            foreach (var test in _receptorTests)
                if (!test(receptor))
                    return false;
            return true;
        }
        static protected bool PerformAllTests(TEffector effector, TReceptor receptor)
        => effector.isActiveAndEnabled && receptor.isActiveAndEnabled
        && effector.CanEffect(receptor) && receptor.CanGainEffectFrom(effector);
        private readonly HashSet<Func<TReceptor, bool>> _receptorTests = new();

        // Play
        protected override void PlayDisable()
        {
            base.PlayDisable();
            Clear();
        }
    }
}