using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

using System.ComponentModel;

namespace Mercury
{
    /// <summary>
    /// An emitter controller that position an emitter at the mouse coordinates
    /// and, optionally, triggers the emitter when the LMB is clicked
    /// </summary>
    public class MouseController : GameComponent
    {
        /// <summary>
        /// Emitter to control
        /// </summary>
        private Emitter _emitter;

        /// <summary>
        /// Gets or sets the controlled emitter
        /// </summary>
        [Category("Controller")]
        [Description("The emitter that this controller will control")]
        public Emitter ControlledEmitter
        {
            get { return _emitter; }
            set { _emitter = value; }
        }

        /// <summary>
        /// Current position of the mouse
        /// </summary>
        private Vector2 _mousePosition;

        /// <summary>
        /// Flag true to trigger the emitter when the LMB is clicked
        /// </summary>
        private bool _triggerOnClick = false;

        /// <summary>
        /// Gets or sets wether or not TriggerOnClick is enabled
        /// </summary>
        [Category("Controller")]
        [Description("Flag true to trigger the emitter when the left mouse button is clicked")]
        [DefaultValue(false)]
        public bool TriggerOnClick
        {
            get { return _triggerOnClick; }
            set { _triggerOnClick = value; }
        }

        /// <summary>
        /// Flag true to enable the emitter only when the LMB is clicked
        /// </summary>
        private bool _enableOnClick = false;

        /// <summary>
        /// Gets or sets wether or not EnableOnClick is enabled
        /// </summary>
        [Category("Controller")]
        [Description("Flag true to enable the emitter only when the left mouse button is clicked")]
        [DefaultValue(false)]
        public bool EnableOnClick
        {
            get { return _enableOnClick; }
            set { _enableOnClick = value; }
        }

        /// <summary>
        /// Updates the controller
        /// </summary>
        public override void Update()
        {
            MouseState mouse = Mouse.GetState();

            _mousePosition.X = mouse.X;
            _mousePosition.Y = mouse.Y;

            _emitter.Position = _mousePosition;

            if (_enableOnClick)
            {
                if (mouse.LeftButton == ButtonState.Pressed)
                {
                    _emitter.Enabled = true;
                }
                else
                {
                    _emitter.Enabled = false;
                }
            }

            if (_triggerOnClick)
            {
                if (mouse.LeftButton == ButtonState.Pressed)
                {
                        _emitter.Trigger();
                }
            }
        }
    }
}