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
        public readonly AutoEvent<Combat> OnChangeCombat = new();

        // Publics
        public Combat Combat
        { get; private set; }
        public Vector3 AnchorPosition
        { get; private set; }
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
                combat = new Combat();
            else if (this.IsInCombat)
                combat = this.Combat;
            else
                combat = target.Combat;

            this.TryJoinCombat(combat);
            target.TryJoinCombat(combat);
        }
        public void TryJoinCombat(Combat combat)
        {
            if (IsInCombat || combat == Combat)
                return;

            Combat = combat;
            combat.TryAddMember(this);
            AnchorPosition = transform.position;
            OnChangeCombat?.Invoke(combat);
        }
        public void TryLeaveCombat()
        {
            if (Combat == null)
                return;

            Combat.TryRemoveMember(this);
            Combat = null;
            OnChangeCombat?.Invoke(null);
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
    }
}