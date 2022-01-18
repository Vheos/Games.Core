namespace Vheos.Tools.UnityCore
{
    using System;
    using System.Collections.Generic;
    using UnityEngine;
        using Tools.Extensions.General;

    [DisallowMultipleComponent]
    sealed public class Teamable : ABaseComponent
    {
        // Events
        public AutoEvent<Team, Team> OnChangeTeam
        { get; } = new AutoEvent<Team, Team>();

        // Publics
        public Team Team
        { get; private set; }
        public IEnumerable<Teamable> Allies
        {
            get
            {
                if (Team == null)
                    yield break;

                foreach (var other in Team.Members)
                    if (other != this)
                        yield return other;
            }
        }
        public bool HasAnyAllies
        => Team != null && Team.Members.Count >= 2;
        public bool IsAllyOf(Teamable other)
        => other != this && Team != null && Team == other.Team;
        public bool IsEnemyOf(Teamable other)
        => other != this && (Team == null || Team != other.Team);
        public void TryChangeTeam(Team newTeam)
        {
            if (!enabled || newTeam == Team)
                return;

            Team previousTeam = Team;
            if (Team != null)
            {
                Team.TryRemoveMember(this);
                Team = null;
            }
            if (newTeam != null)
            {
                newTeam.TryAddMember(this);
                Team = newTeam;
            }
            OnChangeTeam?.Invoke(previousTeam, Team);
        }
        public void TryLeaveTeam()
        => TryChangeTeam(null);
    }

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
        => t != a
        && t.TryGetTeam(out var tTeam)
        && a.TryGetTeam(out var aTeam)
        && tTeam == aTeam;
        static public bool IsEnemyOf(this ABaseComponent t, ABaseComponent a)
        => t != a
        && (!t.TryGetTeam(out var tTeam)
            || !a.TryGetTeam(out var aTeam)
            || tTeam != aTeam);
    }
}