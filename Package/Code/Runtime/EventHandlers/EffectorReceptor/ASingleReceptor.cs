namespace Vheos.Games.Core
{
    using System;
    using System.Collections.Generic;
    using UnityEngine;
    using Tools.Extensions.Collections;
    using Tools.Extensions.General;

    public abstract class ASingleReceptor<TReceptor, TEffector> : AReceptor<TReceptor, TEffector>
        where TReceptor : ASingleReceptor<TReceptor, TEffector>
        where TEffector : AEffector<TEffector, TReceptor>
    {
        // Overrides
        override public void Clear()
        {
            if (!IsEffected)
                return;

            _effector.Clear();
        }
        override internal bool IsEffected
        => _effector != null;
        override internal bool IsEffectedBy(TEffector effector)
        => _effector == effector;
        override internal void GainEffectFrom(TEffector effector)
        {
            _effector = effector;
            OnGainEffect.Invoke(effector);
        }
        override internal void LoseEffectFrom(TEffector effector)
        {
            _effector = null;
            OnLoseEffect.Invoke(effector);
        }

        // Privates
        protected readonly AutoEvent<TEffector> OnGainEffect = new();
        protected readonly AutoEvent<TEffector> OnLoseEffect = new();
        protected TEffector _effector;
    }
}