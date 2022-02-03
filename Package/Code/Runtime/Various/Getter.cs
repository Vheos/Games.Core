namespace Vheos.Games.Core
{
    using System;
    using UnityEngine;

    /// <summary> Wraps <c><see cref="Func{TResult}"/></c> </summary>
    sealed public class Getter<TReturn>
    {
        // Publics
        /// <summary> Defines the getter function </summary>
        /// <remarks> By default, the getter function returns <c><see langword="default"/>(<typeparamref name="TReturn"/>)</c> </remarks>
        public void Set(Func<TReturn> getFunction)
        => _getFunction = getFunction;
        /// <summary> Invokes the getter function and returns its value </summary>
        public TReturn Value
        => _getFunction();
        /// <inheritdoc cref="Getter{TReturn}.Value"/>
        public static implicit operator TReturn(Getter<TReturn> t)
        => t._getFunction();

        // Privates   
        private Func<TReturn> _getFunction = () => default;
    }

    /// <summary> Wraps <c><see cref="Func{T1, TResult}"/></c> </summary>
    sealed public class Getter<T1, TReturn>
    {
        // Publics
        /// <inheritdoc cref="Getter{TReturn}.Set(Func{TReturn})"/>
        public void Set(Func<T1, TReturn> getFunction)
         => _getFunction = getFunction;
        /// <inheritdoc cref="Getter{TReturn}.Value"/>
        public TReturn Value(T1 arg1)
        => _getFunction(arg1);
        /// <inheritdoc cref="Getter{TReturn}.Value"/>
        public TReturn this[T1 arg1]
        => _getFunction(arg1);

        // Privates   
        private Func<T1, TReturn> _getFunction = (arg1) => default;
    }

    /// <summary> Wraps <c><see cref="Func{T1, T2, TResult}"/></c> </summary>
    sealed public class Getter<T1, T2, TReturn>
    {
        // Publics
        /// <inheritdoc cref="Getter{TReturn}.Set(Func{TReturn})"/>
        public void Set(Func<T1, T2, TReturn> getFunction)
        => _getFunction = getFunction;
        /// <inheritdoc cref="Getter{TReturn}.Value"/>
        public TReturn Value(T1 arg1, T2 arg2)
        => _getFunction(arg1, arg2);
        /// <inheritdoc cref="Getter{TReturn}.Value"/>
        public TReturn this[T1 arg1, T2 arg2]
        => _getFunction(arg1, arg2);

        // Privates   
        private Func<T1, T2, TReturn> _getFunction = (arg1, arg2) => default;

    }
}

/*
/// <summary> Base class for generic getters </summary>
abstract public class AGetter
{
    private protected bool _hasBeenSet;
    private protected bool TestForWarnings(Type type)
    {
        if (_hasBeenSet)
            return WarningInputAlreadySet(type);
        return false;
    }
    private protected bool WarningInputAlreadySet(Type type)
    {
        Debug.LogWarning($"InputAlreadySet:\ttrying to override an already defined getter of type {type.Name}\n" +
        $"Fallback:\treturn without changing anything");
        return true;
    }
}
*/