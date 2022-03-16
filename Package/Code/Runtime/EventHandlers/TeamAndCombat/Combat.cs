namespace Vheos.Games.Core
{
    using System;
    using UnityEngine;
    using Tools.Extensions.Collections;
    using Tools.Extensions.General;

    sealed public class Combat : AComponentGroup<Combatable>
    {
        // Publics
        public override void TryRemoveMember(Combatable member)
        {
            base.TryRemoveMember(member);

            if (!_isEnding
            && _members.TryGetAny(out var anyMember)
            && !anyMember.HasAnyEnemies)
                End();
        }

        // Privates
        private bool _isEnding;
        private void End()
        {
            _isEnding = true;
            foreach (var member in _members.MakeCopy())
                member.Combat = null;
            _isEnding = false;
        }

        // Initializers
        public Combat(params Combatable[] combatables) : base()
        => _members.Add(combatables);
    }
}