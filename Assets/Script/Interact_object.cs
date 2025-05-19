using UnityEngine;

public class Interact_object : MonoBehaviour
{
    [SerializeField] private GameManager GameManager;
    [SerializeField] private Input_Controller Controller;
    [SerializeField] private Code_vela Code_Vela;
    [SerializeField] private int candleAmount = 5;
    private string candleName = "vela_";
    bool canInteract = false;

    public bool CanInteract { get => canInteract; set => canInteract = value; }

    //private bool colision;
    //private string[] nombre = { "vela_1", "vela_2", "vela_3", "vela_4", "vela_5" };

    //private void Awake()
    //{
    //    GameManager = GetComponent<GameManager>();
    //    Controller = GetComponent<Input_Controller>();
    //}
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
         print($"canInteract: {canInteract},controller:{Controller.Interact()}, velasN:{GameManager.VelasN}" );
        }
        if (canInteract && Controller.Interact() && GameManager.VelasN)
        {
            for (int i = 0; i < candleAmount; i++)
            {
                if (gameObject.name == candleName + (i + 1))
                {
                    //colision = true
                    Code_Vela.Reacciona();
                    GameManager.ComprobarPuzzleVelaCompleto();
                    //Code_Vela.Reacciona(i + 1);
                    break;
                }
            }
        }
        #region intento de velas 1:
        //for (int i = 0; i < nombre.Length; i++)
        //{
        //    if (gameObject.name == nombre[i])
        //    {
        //        //colision = true
        //        Code_Vela.Reacciona(i+1);
        //        break;
        //    }
        //}

        //if (gameObject.name == nombre[0])
        //{
        //    //colision = true
        //    Code_Vela.Reacciona(1);
        //}
        //else if (gameObject.name == nombre[1])
        //{
        //    //colision = true
        //    Code_Vela.Reacciona(2);
        //}
        //else if (gameObject.name == nombre[2])
        //{
        //    //colision = true
        //    Code_Vela.Reacciona(3);
        //}
        //else if (gameObject.name == nombre[3])
        //{
        //    //colision = true
        //    Code_Vela.Reacciona(4);
        //}
        //else if (gameObject.name == nombre[4])
        //{
        //    //colision = true
        //    Code_Vela.Reacciona(5);
        //}
        #endregion


    }
    private void OnTriggerStay(Collider other)
    {
        if (GameManager.VelasN)
        {
            if (GameManager.SomethingClose(0) == true)
                canInteract = true;
        }
        #region intento1 velas;
        //if (GameManager.SomethingClose(0)==true)
        //{

        //    if (canInteract)
        //    {
        //        for (int i = 0; i < candleAmount; i++)
        //        {
        //            if (gameObject.name == candleName + (i + 1))
        //            {
        //                //colision = true
        //                Code_Vela.Reacciona();
        //                GameManager.ComprobarPuzzleVelaCompleto();
        //                //Code_Vela.Reacciona(i + 1);
        //                break;
        //            }
        //        }
        //    }
        //    #region intento de velas 1:
        //    //for (int i = 0; i < nombre.Length; i++)
        //    //{
        //    //    if (gameObject.name == nombre[i])
        //    //    {
        //    //        //colision = true
        //    //        Code_Vela.Reacciona(i+1);
        //    //        break;
        //    //    }
        //    //}

        //    //if (gameObject.name == nombre[0])
        //    //{
        //    //    //colision = true
        //    //    Code_Vela.Reacciona(1);
        //    //}
        //    //else if (gameObject.name == nombre[1])
        //    //{
        //    //    //colision = true
        //    //    Code_Vela.Reacciona(2);
        //    //}
        //    //else if (gameObject.name == nombre[2])
        //    //{
        //    //    //colision = true
        //    //    Code_Vela.Reacciona(3);
        //    //}
        //    //else if (gameObject.name == nombre[3])
        //    //{
        //    //    //colision = true
        //    //    Code_Vela.Reacciona(4);
        //    //}
        //    //else if (gameObject.name == nombre[4])
        //    //{
        //    //    //colision = true
        //    //    Code_Vela.Reacciona(5);
        //    //}
        //    #endregion
        //} 
        #endregion

        canInteract = true;

    }
    private void OnTriggerExit(Collider other)
    {
        GameManager.SomethingClose(1);
        canInteract = false;
    }
}