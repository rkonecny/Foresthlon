using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.assets.Scripts.Environment.ShootingRange
{
    /**
    * <summary> Sends notification to GameController that player has reached the shooting range </summary>
    **/
    public class LineController : MonoBehaviour
    {
        public ParticleSystem LineParticleSystem;

        public event EventHandler<EventArgs> LineTriggered;

        private List<ParticleSystem.Particle> triggeredParticles = new List<ParticleSystem.Particle>();

        /**
         * <summary>Raises OnLineTriggered event if a line was triggered by the player</summary>
        **/ 
        public void OnParticleTrigger()
        {
            int triggeredParticlesCount = LineParticleSystem.GetTriggerParticles(ParticleSystemTriggerEventType.Enter, triggeredParticles);

            if (triggeredParticlesCount > 0)
            {
                OnLineTriggered();
            }
        }

        protected virtual void OnLineTriggered()
        {
            if (LineTriggered != null)
            {
                LineTriggered(this, new EventArgs());
            }
        }
    }
}
