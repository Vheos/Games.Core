namespace Vheos.Games.Core
{
    using System;
    using System.Linq;
    using UnityEngine;
    using Games.Core;
    using Tools.Extensions.UnityObjects;
    using Tools.Extensions.Math;
    using System.Collections.Generic;

    [DisallowMultipleComponent]
    public class RaycastableManager : AStaticManager<RaycastableManager, Raycastable>
    {
        static public IEnumerable<T> ScreenRaycast<T>(Vector2 screenPosition, Camera camera, bool includeInactive = false) where T : Component
        {
            foreach (var raycastable in (includeInactive ? Components : ActiveComponents).Where(t => t.Has<T>()))
                if (raycastable.Raycast(camera.ScreenPointToRay(screenPosition), out _))
                    yield return raycastable.Get<T>();
        }
        static public IEnumerable<T> ScreenRaycast<T>(Component cursor, Camera camera, bool includeInactive = false) where T : Component
        => ScreenRaycast<T>(camera.WorldToScreenPoint(cursor.transform.position), camera, includeInactive);
        static public T ScreenRaycastClosest<T>(Vector2 screenPosition, Camera camera, bool includeInactive = false) where T : Component
        => ScreenRaycast<T>(screenPosition, camera, includeInactive).OrderBy(t => t.DistanceTo(camera)).FirstOrDefault();
        static public T ScreenRaycastClosest<T>(Component cursor, Camera camera, bool includeInactive = false) where T : Component
        => ScreenRaycastClosest<T>(camera.WorldToScreenPoint(cursor.transform.position), camera, includeInactive);
    }
}