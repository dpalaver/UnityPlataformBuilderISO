namespace Assets.MobileOptimizedWater.Scripts
{
    using UnityEngine;

    public class AnimationStarter : MonoBehaviour
    {
        [SerializeField] private Animator ani;
        [SerializeField] private Motion anim;

        public void Awake()
        {
            ani.Play(anim.name);
        }
    }
}
