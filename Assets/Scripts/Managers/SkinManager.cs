using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinManager : MonoBehaviour
{
    public static SkinManager Instance;
    [SerializeField] private Mesh[] skinMeshes;
    [SerializeField] private Material[] skinMaterials;
    
    void Start()
    {
        if (Instance == null)
            Instance = this; //If this is the first instance, set itself as the main instance
        else 
            Destroy(gameObject); //If there is another instance, destroy this one
    }

    public static SkinManager instance {
        get { return Instance; }
    }

    /**
     * <summary>Apply a skin to a character model.</summary>
     * <param name="go">The player game object</param>
     * <param name="i">The index of the selected skin</param>
     * <example>
     * 0. Money Man     1. Foreman
     * 2. Suffragette   3. Dutchess
     * 4. Animal Lover  5. Flower Child
     * 6. Activist      7. Pacifist
     * </example>
     */
    public void applySkin(GameObject go, int i)
    {
        SkinnedMeshRenderer m = go.GetComponent<SkinnedMeshRenderer>();
        m.sharedMesh = skinMeshes[i];
        m.material = skinMaterials[i];
    }
}
