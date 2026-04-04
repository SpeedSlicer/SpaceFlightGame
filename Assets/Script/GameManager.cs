using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    GameObject interactGO;
    void Start()
    {
        SetInteractActive(false);
    }

    void Update()
    {

    }
    public void SetInteractActive(bool interact)
    {
        interactGO.transform.LeanScale(
            interact ? 
            new Vector3(0.35f, 0.35f, 0.35f) :
            new Vector3(0, 0, 0), 
        0.2f);
    }
}
