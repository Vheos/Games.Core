namespace Vheos.Games.Core
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using UnityEngine;
    using Tools.Extensions.General;
    using Tools.Extensions.UnityObjects;

    [DisallowMultipleComponent]
    sealed public class Combatable : ABaseComponent
    {
        // Events
        public readonly AutoEvent<Combat, Combat> OnChangeCombat = new();

        // Publics
        public Combat Combat
        {
            get => _combat;
            set
            {
                if (value == _combat)
                    return;

                Combat previousCombat = _combat;
                if (previousCombat != null)
                    previousCombat.TryRemoveMember(this);

                _combat = value;
                if (_combat != null)
                    _combat.TryAddMember(this);

                OnChangeCombat.Invoke(previousCombat, _combat);
            }
        }
        public bool IsInCombat
        => Combat != null;
        public bool IsInCombatWith(Combatable other)
        => this != other && Combat == other.Combat;
        public void TryStartCombatWith(Combatable target)
        {
            if (!this.isActiveAndEnabled
            || !target.isActiveAndEnabled
            || target == this)
                return;

            Combat combat;
            if (!this.IsInCombat && !target.IsInCombat)
                combat = new();
            else if (this.IsInCombat)
                combat = this.Combat;
            else
                combat = target.Combat;

            this.Combat = target.Combat = combat;
        }

        // Publics (team-related)
        public IEnumerable<Combatable> Allies
        {
            get
            {
                if (Combat == null
                || !this.TryGetTeam(out _))
                    yield break;

                foreach (var combatable in Combat.Members)
                    if (combatable.IsAllyOf(this))
                        yield return combatable;
            }
        }
        public IEnumerable<Combatable> Enemies
        {
            get
            {
                if (Combat == null)
                    yield break;

                foreach (var combatable in Combat.Members)
                    if (combatable.IsEnemyOf(this))
                        yield return combatable;
            }
        }
        public Vector3 AllyMidpoint
        => Allies.Midpoint();
        public Vector3 EnemyMidpoint
        => Enemies.Midpoint();
        public bool HasAnyAllies
        => Allies.Any();
        public bool HasAnyEnemies
        => Enemies.Any();

        // Privates
        private Combat _combat;
    }
}