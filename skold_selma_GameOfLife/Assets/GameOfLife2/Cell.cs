using UnityEngine;

public class Cell : MonoBehaviour
{
    private Animator animator;
    private SpriteRenderer spriterenderer;

    private void Start()
    {
        animator = GetComponent<Animator>();
        spriterenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (spriterenderer.color.a == 255)
            animator.SetTrigger("Die");
    }
}
