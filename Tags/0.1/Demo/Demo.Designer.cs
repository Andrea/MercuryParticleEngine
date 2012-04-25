using System;

namespace WindowsGame3
{
    partial class Demo
    {
        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.graphics = new Microsoft.Xna.Framework.Components.GraphicsComponent();
            this.testParticleSystem = new Mercury.ParticleSystem();
            this.testEmitter1 = new Mercury.SpiralEmitter();
            this.testEmitter2 = new Mercury.SpiralEmitter();
            this.testEmitter3 = new Mercury.PointEmitter();
            this.testController = new Mercury.MouseController();
            // 
            // graphics
            // 
            this.graphics.AllowMultiSampling = false;
            this.graphics.BackBufferHeight = 480;
            this.graphics.BackBufferWidth = 640;
            // 
            // testEmitter1
            // 
            this.testEmitter1.AutoTrigger = true;
            this.testEmitter1.AutoTriggerFrequency = ((uint)(10u));
            this.testEmitter1.Direction = Mercury.SpiralDirection.Clockwise;
            this.testEmitter1.EmitQuantity = ((byte)(1));
            this.testEmitter1.FinalAlpha = 0F;
            this.testEmitter1.FinalColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.testEmitter1.FinalScale = 0.5F;
            this.testEmitter1.InitialColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.testEmitter1.MidAlphaSweep = 0.8F;
            this.testEmitter1.MidColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.testEmitter1.MidScale = 1F;
            this.testEmitter1.MidScaleSweep = 0F;
            this.testEmitter1.ParticleLifespan = ((uint)(1800u));
            this.testEmitter1.ParticleSpeed = -50F;
            this.testEmitter1.ParticleSystem = this.testParticleSystem;
            this.testEmitter1.Radius = 100F;
            this.testEmitter1.Segments = ((byte)(32));
            this.testEmitter1.X = 150;
            this.testEmitter1.Y = 240;
            // 
            // testEmitter2
            // 
            this.testEmitter2.AutoTrigger = true;
            this.testEmitter2.AutoTriggerFrequency = ((uint)(1000u));
            this.testEmitter2.Direction = Mercury.SpiralDirection.AntiClockwise;
            this.testEmitter2.EmitQuantity = ((byte)(32));
            this.testEmitter2.FinalAlpha = 0F;
            this.testEmitter2.FinalColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.testEmitter2.InitialColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.testEmitter2.InitialScale = 0.5F;
            this.testEmitter2.MidAlphaSweep = 0.8F;
            this.testEmitter2.MidColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.testEmitter2.MidScale = 0.5F;
            this.testEmitter2.MidScaleSweep = 0F;
            this.testEmitter2.ParticleLifespan = ((uint)(1800u));
            this.testEmitter2.ParticleSpeed = 50F;
            this.testEmitter2.ParticleSystem = this.testParticleSystem;
            this.testEmitter2.Radius = 0F;
            this.testEmitter2.Segments = ((byte)(32));
            this.testEmitter2.X = 490;
            this.testEmitter2.Y = 240;
            // 
            // testEmitter3
            // 
            this.testEmitter3.AutoTriggerFrequency = ((uint)(1000u));
            this.testEmitter3.EmitQuantity = ((byte)(1));
            this.testEmitter3.FinalAlpha = 0F;
            this.testEmitter3.FinalColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.testEmitter3.FinalScale = 2F;
            this.testEmitter3.InitialColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.testEmitter3.MidColor = System.Drawing.Color.Yellow;
            this.testEmitter3.MidColorSweep = 0.25F;
            this.testEmitter3.MidScale = 1F;
            this.testEmitter3.MidScaleSweep = 0F;
            this.testEmitter3.ParticleLifespan = ((uint)(1000u));
            this.testEmitter3.ParticleLifespanVariation = 0.5F;
            this.testEmitter3.ParticleSpeed = 150F;
            this.testEmitter3.ParticleSpeedDelta = -300F;
            this.testEmitter3.ParticleSpeedMin = 0F;
            this.testEmitter3.ParticleSpeedVariation = 0.5F;
            this.testEmitter3.ParticleSystem = this.testParticleSystem;
            this.testEmitter3.Revolutions = 1F;
            // 
            // testController
            // 
            this.testController.ControlledEmitter = this.testEmitter3;
            this.testController.TriggerOnClick = true;
            // 
            // Demo
            // 
            this.IsMouseVisible = true;
            this.GameComponents.Add(this.graphics);
            this.GameComponents.Add(this.testParticleSystem);
            this.GameComponents.Add(this.testEmitter1);
            this.GameComponents.Add(this.testEmitter2);
            this.GameComponents.Add(this.testEmitter3);
            this.GameComponents.Add(this.testController);

        }

        private Microsoft.Xna.Framework.Components.GraphicsComponent graphics;
        private Mercury.ParticleSystem testParticleSystem;
        private Mercury.SpiralEmitter testEmitter1;
        private Mercury.SpiralEmitter testEmitter2;
        private Mercury.PointEmitter testEmitter3;
        private Mercury.MouseController testController;
    }
}
