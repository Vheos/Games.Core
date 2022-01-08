namespace Vheos.Tools.UnityCore
{
    using System;
    using UnityEngine;

    /// <summary> Holds data required for adding conditional events to <c><see cref="Tween"/></c>s </summary> 
    /// <remarks> Used in <c><see cref="Tween.AddEvents(EventInfo[])"/></c> </remarks>
    public struct EventInfo
    {
        // Publics
        /// <summary> This <c><see cref="Tween"/></c>'s property will be tested against the <c><see cref="Threshold"/></c><br/> </summary> 
        /// <remarks> See <c><see cref="EventThresholdVariable"/></c>'s names for more info and examples </remarks> 
        public EventThresholdVariable ThresholdVariable;
        /// <summary> This threshold must be reached for <c><see cref="Action"/></c> to get invoked </summary> 
        public float Threshold;
        /// <summary> This action will be invoked when the <c><see cref="Threshold"/></c> is reached </summary> 
        public Action Action;

        // Initializers
        /// <summary> Creates a new <c><see cref="EventInfo"/></c> from given <c><paramref name="threshold"/></c> and <c><paramref name="action"/></c><br/>
        /// <c><see cref="ThresholdVariable"/></c> will be set to <c><see cref="EventThresholdVariable.Progress"/></c> </summary> 
        public EventInfo(float threshold, Action action)
        {
            ThresholdVariable = EventThresholdVariable.Progress;
            Threshold = threshold;
            Action = action;
        }
        /// <summary> Creates a new <c><see cref="EventInfo"/></c> from given <c><paramref name="thresholdVariable"/></c>, <c><paramref name="threshold"/></c> and <c><paramref name="action"/></c> </summary> 
        public EventInfo(EventThresholdVariable thresholdVariable, float threshold, Action action)
        {
            ThresholdVariable = thresholdVariable;
            Threshold = threshold;
            Action = action;
        }
    }
}