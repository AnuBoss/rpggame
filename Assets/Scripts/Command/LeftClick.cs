using UnityEngine;

public class LeftClick : MonoBehaviour
{
    public static LeftClick instance;

    private Camera cam;

    [SerializeField] 
    private Character curChar;
    public Character CurChar
    {
        get { return curChar; }
    }

    [SerializeField] 
    private LayerMask layerMask;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        instance = this;
        cam =Camera.main;
        layerMask = LayerMask.GetMask("Ground", "Character", "Building", "Item");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SelectCharacter(RaycastHit hit)
    {
        curChar = hit.collider.GetComponent<Character>();
        Debug.Log("Selest Char: " + hit.collider.gameObject);
    }
}
