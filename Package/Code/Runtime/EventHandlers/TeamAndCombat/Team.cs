namespace Vheos.Games.Core
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using UnityEngine;

    public class Team : AComponentGroup<Teamable>
    {
        // Public
        static public Team GetPredefinedTeam(PredefinedTeam predefinedTeamEnum)
        => predefinedTeamEnum switch
        {
            PredefinedTeam.Allies => _allies,
            PredefinedTeam.Enemies => _enemies,
            _ => null,
        };
        public string Name
        { get; private set; }
        public Color Color
        { get; private set; }

        // Privates
        static private Team _allies;
        static private Team _enemies;

        // Initializers
        [SuppressMessage("CodeQuality", "IDE0051")]
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        static private void StaticInitialize()
        {
            _allies = new Team
            {
                Name = nameof(_allies),
                Color = new Color(0.5f, 0.75f, 1f, 1f),
            };
            _enemies = new Team()
            {
                Name = nameof(_enemies),
                Color = new Color(1f, 0.75f, 0.5f, 1f),
            };
        }
    }
}