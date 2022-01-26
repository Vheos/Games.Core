namespace Vheos.Games.Core
{
    using System;
    using UnityEngine;
    using Tools.Extensions.General;

    [RequireComponent(typeof(Updatable))]
    abstract public class AAutoMProps : ABaseComponent
    {
        // MProps
        protected float GetFloat(string name)
        => _mprops.GetFloat(name);
        protected Color GetColor(string name)
        => _mprops.GetColor(name);
        protected Texture GetTexture(string name)
        => _mprops.GetTexture(name);
        protected void SetFloat(string name, float value)
        {
            if (_mprops.GetFloat(name) == value)
                return;

            _mprops.SetFloat(name, value);
            _hasDirtyMProps = true;
        }
        protected void SetColor(string name, Color value)
        {
            if (_mprops.GetColor(name) == value)
                return;

            _mprops.SetColor(name, value);
            _hasDirtyMProps = true;
        }
        protected void SetTexture(string name, Texture value)
        {
            if (_mprops.GetTexture(name) == value)
                return;

            _mprops.SetTexture(name, value);
            _hasDirtyMProps = true;
        }

        // Privates
        private Renderer _renderer;
        private MaterialPropertyBlock _mprops;
        private bool _hasDirtyMProps;
        private void UpdateDirtyMProps()
        {
            if (_hasDirtyMProps.Consume())
                _renderer.SetPropertyBlock(_mprops);
        }

        // Play
        public void Initialize(Renderer renderer)
        {
            _renderer = renderer;
            _mprops = new MaterialPropertyBlock();
            UpdateDirtyMProps();
            Get<Updatable>().OnUpdateLate.SubscribeAuto(this, UpdateDirtyMProps);
        }
    }
}