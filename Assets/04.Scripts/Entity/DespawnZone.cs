using System.Collections;
using System.Collections.Generic;
using MondayCatWorld.Managers;
using UnityEngine;

namespace MondayCatWorld.Games
{
    public class DestroyZone : MonoBehaviour
    {
        private void OnCollisionEnter(Collision collision)
        {
            var go = collision.gameObject;
            if (!go.name.Equals("Rubble")) return;
            
            go.name = "Cube";
            PoolManager.Instance.Despawn(collision.gameObject);
        }
    }
}