namespace Vheos.Games.Core
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.Pool;
    using Tools.Extensions.General;
    using Tools.Extensions.UnityObjects;
    using Tools.Extensions.Math;

    [DefaultExecutionOrder(-1)]
    abstract public class AComponentPool<TPool, TComponent> : AStaticComponent<TPool>
        where TPool : AComponentPool<TPool, TComponent>
        where TComponent : Component
    {
        // Inspector
        [field: SerializeField] public TComponent Prefab { get; private set; }

        // Publics
        static public TComponent Get()
        => _pool.Get();
        static public void Release(TComponent component)
        => _pool.Release(component);
        static public int Count
        => _pool.CountAll;
        static public int CountActive
        => _pool.CountActive;

        virtual protected TComponent CreateComponent()
        {
            TComponent newComponent = Instantiate(Prefab);
            newComponent.name = $"{typeof(TComponent).Name}_{Count}";
            return newComponent;
        }
        virtual protected void OnGetComponent(TComponent t)
        => t.Activate();
        virtual protected void OnReleaseComponent(TComponent t)
        => t.Deactivate();
        virtual protected void OnDestroyComponent(TComponent t)
        => t.DestroyObject();

        // Privates
        static private ObjectPool<TComponent> _pool;

        // Initializers
        protected override void PlayAwake()
        {
            base.PlayAwake();
            _pool = new(
                createFunc: CreateComponent,
                actionOnGet: OnGetComponent,
                actionOnRelease: OnReleaseComponent,
                actionOnDestroy: OnDestroyComponent,
                defaultCapacity: 1000,
                maxSize: 1000,
                collectionCheck: true);
        }
    }
}