namespace Vheos.Tools.UnityCore
{
    abstract public class AUpdatable :
#if UNITY_EDITOR
        AEditable
#else
        APlayable
#endif
    {
        // Virtuals
        virtual public void PlayUpdate()
        { }
        virtual public void PlayUpdateLate()
        { }
        virtual public void PlayUpdateFixed()
        { }

        // Mono
        override public void PlayEnable()
        => AUpdateManager.RegisterComponentDelayed(this);
        override public void PlayDisable()
        => AUpdateManager.UnregisterComponentDelayed(this);
    }
}