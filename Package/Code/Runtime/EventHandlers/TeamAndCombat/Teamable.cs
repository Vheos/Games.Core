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
        public readonly AutoEvent<Teamable> OnChangeTeam = new();

        // Publics
        public Team Team
        {
            get => _team;
            set
            {
                if (value == _team)
                    return;

                Team previousTeam = _team;
                if (previousTeam != null)
                    previousTeam.TryRemoveMember(this);

                _team = value;
                if (_team != null)
                    _team.TryAddMember(this);

                OnChangeTeam.Invoke(this);
            }
        }
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

        // Privates
        private Team _team;

        // Play
        protected override void PlayDisable()
        {
            base.PlayDisable();
            Team = null;
        }
    }
}