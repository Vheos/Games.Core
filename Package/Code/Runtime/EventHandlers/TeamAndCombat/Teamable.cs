namespace Vheos.Games.Core
{
    using System;
    using System.Collections.Generic;
    using UnityEngine;
    using Tools.Extensions.General;

    [DisallowMultipleComponent]
    sealed public class Teamable : ABaseComponent
    {
        // Events
        public readonly AutoEvent<Team, Team> OnChangeTeam = new();

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
            if (!isActiveAndEnabled
            || newTeam == Team)
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
            OnChangeTeam.Invoke(previousTeam, Team);
        }
        public void TryLeaveTeam()
        => TryChangeTeam(null);
    }
}