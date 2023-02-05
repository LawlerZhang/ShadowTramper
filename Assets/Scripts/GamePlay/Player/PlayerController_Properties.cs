using UnityEngine;

namespace Lfish
{
    public partial class PlayerController
    {
        public new Rigidbody2D rigidbody2D => GetComponent<Rigidbody2D>();
        public Animator animator => GetComponent<Animator>();
        public Shadow shadow => GetComponentInChildren<Shadow>();
        public SpriteRenderer render => GetComponent<SpriteRenderer>();
    }
}
