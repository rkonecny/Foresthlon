using System.Collections.Generic;
using UnityEngine;

namespace Assets.assets.Scripts.Environment.ShootingRange
{
    /**
    * <summary> Controls emmiting of shooting range </summary>
    **/
    public class ShootingRangeEmitter : MonoBehaviour
    {

        public ParticleSystem Line;
        public ParticleSystem Board;

        private IList<ParticleSystem> shootingRangeEmitters;

        public void Start()
        {
            shootingRangeEmitters = new List<ParticleSystem> { Line, Board };
        }

        /**
        * <summary> Spawns a shooting range </summary>
        **/
        public void SpawnShootingRange()
        {
            foreach (var emmiter in shootingRangeEmitters)
            {
                emmiter.Emit(1);
            }

            //only one range should be spawned
            StopEmmiting();
        }

        /**
        * <summary> Stops emmitting of shooting ranges </summary>
        **/
        private void StopEmmiting()
        {
            foreach (var emmiter in shootingRangeEmitters)
            {
                emmiter.Stop();
            }
        }
    }
}