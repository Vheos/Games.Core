namespace Vheos.Games.Core
{
    using System;

    public class CustomDisposable : IDisposable
    {
        // Privates
        private readonly Action _onDispose;

        // Initializers
        public CustomDisposable(Action onDispose)
        => _onDispose = onDispose;

        // Finalizers
        public void Dispose()
        => _onDispose();
    }
}