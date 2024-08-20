using UnityEngine;

public class aktivedAnimatioByVei : MonoBehaviour
{
    public Animator animator;

    void Start()
    {
        if (animator == null)
        {
            Debug.LogError("Animator component not found on " + gameObject.name);
        }
    }

    public void aktivedTheAnimation(){
        if (animator != null)
        {
            animator.SetBool("lookedAt", true);
        }
        
    }

    public void deaktivedTheAnimation(){
       
        if (animator != null)
        {
            animator.SetBool("lookedAt", false);
        }
        
    }
}