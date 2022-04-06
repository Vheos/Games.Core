namespace Vheos.Games.Core
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using UnityEngine;
    using Tools.Extensions.General;
    using Tools.Extensions.Math;
    using Tools.Extensions.UnityObjects;

    public abstract class AMultiEffector<TEffector, TReceptor> : AEffector<TEffector, TReceptor>
        where TEffector : AMultiEffector<TEffector, TReceptor>
        where TReceptor : AReceptor<TReceptor, TEffector>
    {
        // Overrides
        override public void Clear()
        {
            if (!IsEffectingAny)
                return;

            foreach (var receptor in _receptors.MakeCopy())
                RemoveReceptor(receptor);
        }
        override protected bool IsEffectingAny
        => _receptors.Count > 0;
        override protected bool IsEffecting(TReceptor receptor)
        => _receptors.Contains(receptor);
        override protected bool IsEffecting<T>()
        => _receptors.Any(t => t.Has<T>());

        // Privates
        protected readonly AutoEvent<TReceptor> OnAddReceptor = new();
        protected readonly AutoEvent<TReceptor> OnRemoveReceptor = new();
        protected HashSet<TReceptor> _receptors = new();
        protected bool AddReceptor(TReceptor receptor)
        {
            if (receptor == null
            || _receptors.Contains(receptor)
            || !this.TryAs(out TEffector @this)
            || !PerformAllTests(@this, receptor))
                return false;

            _receptors.Add(receptor);
            OnAddReceptor.Invoke(receptor);
            receptor.GainEffectFrom(@this);

            return true;
        }
        protected bool RemoveReceptor(TReceptor receptor)
        {
            if (receptor == null
            || !_receptors.Contains(receptor)
            || !this.TryAs(out TEffector @this))
                return false;

            _receptors.Remove(receptor);
            OnRemoveReceptor.Invoke(receptor);
            receptor.LoseEffectFrom(@this);
            return true;
        }
    }
}