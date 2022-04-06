namespace Vheos.Games.Core
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using UnityEngine;
    using Tools.Extensions.General;
    using Tools.Extensions.Math;
    using Tools.Extensions.UnityObjects;

    public abstract class ASingleEffector<TEffector, TReceptor> : AEffector<TEffector, TReceptor>
        where TEffector : ASingleEffector<TEffector, TReceptor>
        where TReceptor : AReceptor<TReceptor, TEffector>
    {
        // Overrides
        override public void Clear()
        => SetReceptor(null);
        override protected bool IsEffectingAny
        => _receptor != null;
        override protected bool IsEffecting(TReceptor receptor)
        => _receptor == receptor;
        override protected bool IsEffecting<T>()
        => _receptor != null && _receptor.Has<T>();
        override protected bool TryGetReceptor(out TReceptor receptor)
        => _receptor.TryNonNull(out receptor);
        override protected bool TryGetReceptor<T>(out T component)
        {
            if (_receptor != null
            && _receptor.TryGet(out component))
                return true;

            component = default;
            return false;
        }

        // Privates
        protected readonly AutoEvent<TReceptor, TReceptor> OnChangeReceptor = new();
        protected TReceptor _receptor;
        protected bool SetReceptor(TReceptor newReceptor)
        {
            if (newReceptor == _receptor
            || !this.TryAs(out TEffector @this)
            || newReceptor != null && !PerformAllTests(@this, newReceptor))
                return false;

            TReceptor previousReceptor = _receptor;
            _receptor = newReceptor;
            OnChangeReceptor.Invoke(previousReceptor, _receptor);

            if (previousReceptor != null)
                previousReceptor.LoseEffectFrom(@this);
            if (_receptor != null)
                _receptor.GainEffectFrom(@this);

            return true;
        }
    }
}