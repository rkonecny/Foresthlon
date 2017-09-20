using UnityEngine;

namespace Assets.assets.Scripts.Movement
{
    /**
    * <summary>Class for controlling movement speed by setting the moving speed of the background</summary>
    **/
    public class MovementSpeedController : MonoBehaviour
    {

        private ParticleSystem[] ParticleSystems;

        public MovementSpeed ParticleSpeed;

        void Start()
        {
            ParticleSystems = FindObjectsOfType<ParticleSystem>();
        }

        void FixedUpdate()
        {
            foreach (var system in ParticleSystems)
            {
                var systemMain = system.main;
                systemMain.simulationSpeed = ParticleSpeed.Speed;
            }
        }
    }
}