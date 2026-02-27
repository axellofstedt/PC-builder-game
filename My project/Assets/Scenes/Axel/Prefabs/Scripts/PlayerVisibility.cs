using UnityEngine;

public class PlayerVisibility : MonoBehaviour
{
    private SkinnedMeshRenderer skinnedRenderer;
    private CapsuleCollider capsuleCollider;
    private Rigidbody rb;

    void Awake()
    {
        skinnedRenderer = GetComponent<SkinnedMeshRenderer>();
        capsuleCollider = transform.parent.parent.gameObject.GetComponent<CapsuleCollider>();
        rb = transform.parent.parent.gameObject.GetComponent<Rigidbody>();
    }

    public void SetVisibility(bool visible)
    {
        Debug.Log("Setting player visibility to " + visible);
        if (skinnedRenderer != null) 
        {
            rb.isKinematic = !visible;
            skinnedRenderer.enabled = visible;
            capsuleCollider.enabled = visible;
            
        }
    }
}
