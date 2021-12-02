namespace Vheos.Tools.UnityCore
{
    using System;
    using System.Collections.Generic;
    using UnityEngine;

    public struct OptionalParameters
    {
        // Publics
        public AnimationCurve Curve;
        public GUID GUID;
        public TimeDeltaType? TimeDeltaType;
        public EventInfo[] EventInfo;
        public AssignmentType? AssignmentType;
        public ConflictResolution? ConflictResolution;

        // Publics (new)
        public OptionalParameters Set(AnimationCurve curve)
        {
            Curve = curve;
            return this;
        }
        public OptionalParameters Set(GUID guid)
        {
            GUID = guid;
            return this;
        }
        public OptionalParameters Set(TimeDeltaType? timeDeltaType)
        {
            TimeDeltaType = timeDeltaType;
            return this;
        }
        public OptionalParameters Set(EventInfo[] eventInfo)
        {
            EventInfo = eventInfo;
            return this;
        }
        public OptionalParameters Set(AssignmentType? assignmentType)
        {
            AssignmentType = assignmentType;
            return this;
        }
        public OptionalParameters Set(ConflictResolution? conflictResolution)
        {
            ConflictResolution = conflictResolution;
            return this;
        }
    }
}