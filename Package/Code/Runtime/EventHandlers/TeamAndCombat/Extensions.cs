namespace Vheos.Games.Core
{
    using System;
    using UnityEngine;
    using Tools.Extensions.General;

    static public class Teamable_Extensions
    {
        static public bool TryGetTeam(this ABaseComponent t, out Team team)
        {
            if (t.TryGet(out Teamable teamable)
            && teamable.Team.TryNonNull(out team))
                return true;

            team = null;
            return false;
        }
        static public bool IsAllyOf(this ABaseComponent t, ABaseComponent a)
        => t.gameObject != a.gameObject
        && t.TryGetTeam(out var tTeam)
        && a.TryGetTeam(out var aTeam)
        && tTeam == aTeam;
        static public bool IsEnemyOf(this ABaseComponent t, ABaseComponent a)
        => t.gameObject != a.gameObject
        && (!t.TryGetTeam(out var tTeam)
            || !a.TryGetTeam(out var aTeam)
            || tTeam != aTeam);
    }

    static public class Combatable_Extensions
    {
        static public bool TryGetCombat(this ABaseComponent t, out Combat combat)
        {
            if (t.TryGet(out Combatable combatable)
            && combatable.Combat.TryNonNull(out combat))
                return true;

            combat = null;
            return false;
        }
        static public bool IsInCombatWith(this ABaseComponent t, ABaseComponent a)
        => t.gameObject != a.gameObject
        && t.TryGetCombat(out var tCombat)
        && a.TryGetCombat(out var aCombat)
        && tCombat == aCombat;
        static public bool IsInCombat(this ABaseComponent t)
        => TryGetCombat(t, out _);
    }
}