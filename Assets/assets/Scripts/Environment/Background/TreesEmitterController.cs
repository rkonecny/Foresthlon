using System.Collections.Generic;
using UnityEngine;

namespace Assets.assets.Scripts.Environment.Background
{
    /**
    * <summary> Controls emmiting of trees, used for making sure no trees are emmitted while shooting at the shooting range </summary>
    **/
    public class TreesEmitterController
    {
        private IList<ParticleSystem> treesEmitters;

        public TreesEmitterController()
        {
            treesEmitters = new List<ParticleSystem>();
            foreach (var tree in GameObject.FindGameObjectsWithTag("Trees"))
            {
                treesEmitters.Add(tree.GetComponent<ParticleSystem>());
            }
        }

        /**
         * <summary> Start emmitting trees </summary>
         **/
        public void StartEmitting()
        {
            foreach (var treeEmitter in treesEmitters)
            {
                var emission = treeEmitter.emission;
                emission.enabled = true;
            }
        }

        /**
         * <summary> Stop emmitting trees </summary>
         **/
        public void StopEmitting()
        {
            foreach (var treeEmitter in treesEmitters)
            {
                var emission = treeEmitter.emission;
                emission.enabled = false;
            }
        }
    }
}