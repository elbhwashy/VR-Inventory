using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GVRI
{
    public static class Utils
    {
        public static void AddSphereTriggerColliderIfNotAvailable(GameObject go)
        {
            // attach Sphere Trigger Collider if none is attached
            var colliders = go.GetComponents<Collider>();
            bool triggerColliderAvailable = false;
            foreach (Collider col in colliders)
            {
                if (col.isTrigger == true)
                {
                    triggerColliderAvailable = true;
                }
            }
            if (!triggerColliderAvailable)
            {
                SphereCollider sc = go.AddComponent<SphereCollider>();
                sc.isTrigger = true;
            }
        }
    }
}

