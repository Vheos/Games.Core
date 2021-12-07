namespace Vheos.Tools.UnityCore
{
    using System;
    using System.Collections.Generic;
    using UnityEngine;

    public struct OptionalParameters
    {
        // Publics
        public AnimationCurve Curve;
        public CurveFuncType? CurveFuncType;
        public TimeDeltaType? TimeDeltaType;
        public AssignmentType? AssignmentType;
        public EventInfo[] EventInfo;
        public ConflictResolution? ConflictResolution;
        public object GUID;

        // Publics (new)
        public OptionalParameters New(AnimationCurve curve)
        {
            Curve = curve;
            return this;
        }
        public OptionalParameters New(CurveFuncType funcType)
        {
            CurveFuncType = funcType;
            return this;
        }
        public OptionalParameters New(TimeDeltaType? timeDeltaType)
        {
            TimeDeltaType = timeDeltaType;
            return this;
        }
        public OptionalParameters New(AssignmentType? assignmentType)
        {
            AssignmentType = assignmentType;
            return this;
        }
        public OptionalParameters New(EventInfo[] eventInfo)
        {
            EventInfo = eventInfo;
            return this;
        }
        public OptionalParameters New(ConflictResolution? conflictResolution, object guid)
        {
            ConflictResolution = conflictResolution;
            GUID = guid;
            return this;
        }

        // Operators
        static public implicit operator OptionalParameters(AnimationCurve t)
        => new OptionalParameters { Curve = t };
        static public implicit operator OptionalParameters(CurveFuncType t)
        => new OptionalParameters { CurveFuncType = t };
        static public implicit operator OptionalParameters(TimeDeltaType t)
        => new OptionalParameters { TimeDeltaType = t };
        static public implicit operator OptionalParameters(AssignmentType t)
        => new OptionalParameters { AssignmentType = t };
        static public implicit operator OptionalParameters(EventInfo t)
        => new OptionalParameters { EventInfo = new[] { t } };
        static public implicit operator OptionalParameters((ConflictResolution conflictResolution, object guid) t)
        => new OptionalParameters { ConflictResolution = t.conflictResolution, GUID = t.guid };
    }
}