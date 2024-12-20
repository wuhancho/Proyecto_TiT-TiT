using UnityEngine;

public class Code_vela : MonoBehaviour
{
    //[SerializeField] private GameManager GameManager;
    [SerializeField] private GameObject ChildObjeto;

    [SerializeField] private Code_vela[] velasAManipular;

    private void Start()
    {
        ChildObjeto.SetActive(false);
    }

    //public void Reacciona(int accion)
    //{
    //    GameManager.Order(accion);
    //}

    public void Reacciona()
    {
        for (int i = 0; i < velasAManipular.Length; i++)
        {
            velasAManipular[i].SwitchState();
        }
    }

    private void SwitchState()
    {
        ChildObjeto.SetActive(!IsActive());
    }

    public bool IsActive()
    {
        return ChildObjeto.activeSelf;
    }

    
}
