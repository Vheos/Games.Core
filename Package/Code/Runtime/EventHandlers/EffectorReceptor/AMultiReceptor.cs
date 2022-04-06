namespace Vheos.Games.Core
{
    using System;
    using System.Collections.Generic;
    using UnityEngine;
    using Tools.Extensions.Collections;
    using Tools.Extensions.General;

    public abstract class AMultiReceptor<TReceptor, TEffector> : AReceptor<TReceptor, TEffector>
        where TReceptor : AMultiReceptor<TReceptor, TEffector>
        where TEffector : AEffector<TEffector, TReceptor>
    {
        // Overrides
        override public void Clear()
        {
            if (!IsEffected)
                return;

            foreach (var effector in _effectors.MakeCopy())
                effector.Clear();
        }
        override internal bool IsEffected
        => _effectors.Count > 0;
        override internal bool IsEffectedBy(TEffector effector)
        => _effectors.Contains(effector);
        override internal void GainEffectFrom(TEffector effector)
        {
            _effectors.Add(effector);
            OnGainEffect.Invoke(effector, _effectors.Count == 1);   // is first
        }
        override internal void LoseEffectFrom(TEffector effector)
        {
            _effectors.Remove(effector);
            OnLoseEffect.Invoke(effector, _effectors.Count == 0);   // was last
        }

        // Privates
        protected readonly AutoEvent<TEffector, bool> OnGainEffect = new();
        protected readonly AutoEvent<TEffector, bool> OnLoseEffect = new();
        protected readonly HashSet<TEffector> _effectors = new();
    }
}