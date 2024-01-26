using System.Collections;
using UnityEngine;

namespace Assets.Scripts
{
    public class Breakable : AbstractHealth
    {
        protected override void Die()
        {
            Destroy(gameObject);
        }
    }
}