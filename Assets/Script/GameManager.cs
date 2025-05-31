using System;
using System.Collections;
using System.Runtime.CompilerServices;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;


public class GameManager : MonoBehaviour
{

    //[SerializeField] TextMeshProUGUI texto_pantalla;
    //[SerializeField] GameObject[] llamas;
    [SerializeField] public bool habilitoRaton = false;
    private float deltaTime= 0f;
   

    [Header("Inventario y coger")]
    [SerializeField] GameObject inventario;
    [SerializeField] private InventoryManager inventory;
    //ItemPickUp PickUp;

    [Header("entradas")]
    [SerializeField] Input_Controller _Controller;

    [Header("puntero")]
    [SerializeField] private GameObject puntero;
    //private bool isInventoryOpen = false;

    [Header("Puzle entrada")]
    [SerializeField] Animator globo_anim;

    [Header("Puzle nave")]
    [SerializeField] Code_vela[] llamas;
    private bool velasN = true;

    [Header("Puzle Despacho1")]
    [SerializeField] private GameObject cajonera;

    [Header("Puzle despacho2")]
    private int contEngranajes;
    [SerializeField]public GameObject[] zonewin, zoneinit, engranajes;

    // puzle del planeta
    //[Header ("Puzle entrada")]
    //[SerializeField] private Chincheta chincheta;
    [Header("Puzle Oficinas")]

    [SerializeField] private GameObject caja_Fuertenumber;

    [Header("Pasillo 1")]

    [SerializeField] private GameObject humo;
    [Header("Puzle SalaEspera")]
    [SerializeField] private int cantidad = 0;

    [Header("llaves")]
    [SerializeField] private GameObject[] llaves;

    [Header("Reconpensas")]
    [SerializeField] private bool notaHabilitada = false;
    //private bool estadoRecompensa = false;
    [SerializeField] GameObject[] recompensas;

    [Header("Salir")]
    [SerializeField] private GameObject zoneSalir;

    //public bool EstadoRecompensa { get => estadoRecompensa; set => estadoRecompensa = value; }
    public bool NotaHabilitada { get => notaHabilitada; set => notaHabilitada = value; }
    public bool VelasN { get => velasN; set => velasN = value; }

    public void Awake()
    {
        for (int i = 0; i < zoneinit.Length; i++)
        {
            engranajes[i].transform.position = zoneinit[i].transform.position;
        }
        for (int i = 0; i < recompensas.Length; i++)
        {
            recompensas[i].SetActive(false);
        }
        _Controller.GetComponent<Input_Controller>();
        //PickUp = GetComponent<ItemPickUp>();
        //texto_pantalla.enabled = false;
        inventario.SetActive(false);
        zoneSalir.SetActive(false);

    }
    private void Update()
    {
        //if (contEngranajes <= 5)
        //{
        //    DetectTruePosicionEngranaje();
        //}
        //if (contEngranajes == 5)
        //{
        //    ActivarTriunfo(1);
        //    //print(contEngranajes);
        //    //contEngranajes = 6;
        //}
        notaHabilitada = NotaHabilitada;
        velasN = VelasN;
        deltaTime += (Time.unscaledDeltaTime - deltaTime) * 0.1f;
    }
    public bool PausaActiva => zoneSalir.activeSelf;
    public void returnGame()
    {
        HabilitarRaton(false);
        zoneSalir.SetActive(false);
        _Controller.CerrarMenuPausa();
        Time.timeScale = 1f;
    }
    public void salirJuego()
    {
        HabilitarRaton(true);
        zoneSalir.SetActive(true);
        Time.timeScale = 0f;
    }
    //public IEnumerator MostrarEstado(bool Cambool, bool Moving)
    //{
    //    //texto_pantalla.enabled = true;
    //    //texto_pantalla.text = $"StateCam: {Cambool} IsMoving: {Moving}";
    //    //yield return new WaitForSeconds(3);
    //    //texto_pantalla.enabled = false;
    //}
    private void OnGUI()
    {
        int w = Screen.width, h = Screen.height;
        GUIStyle style = new GUIStyle();
        Rect rect = new Rect(0, 0, w, h*2/100);
        style.alignment = TextAnchor.UpperLeft;
        style.fontSize =h*2/100;
        style.normal.textColor = Color.red;
        float msec = deltaTime * 1000f;
        float fps = 1.0f / deltaTime;
        string text = string.Format("{0:0.0} ms ({1:0.} fps)", msec, fps);
        GUI.Label(rect, text, style);
    }
    public void HabilitarRaton(bool habilitado)
    {
        //print($"valor del {habilitado}");
        if (habilitado)
        {
            //print("habilitado");
            habilitoRaton = true;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            puntero.SetActive(false);
        }
        else if (!habilitado)
        {
            habilitoRaton = false;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            puntero.SetActive(true);
        }
    }

    public bool SomethingClose(int algo)// detector de proximidad
    {
        if (algo == 0)
        {

            //texto_pantalla.enabled = true;
            //texto_pantalla.text = $"pulsa E";

            return true;
        }
        else
        {
            //texto_pantalla.enabled = false;
            return false;
        }
    }

    public void ComprobarPuzzleVelaCompleto()
    {
        bool completado = true;

        for (int i = 0; i < llamas.Length; i++)
        {
            if (!llamas[i].IsActive())
            {

                completado = false;
                break;
            }
        }

        if (completado)
        {
            //tirar confeti
            velasN = false;
            ActivarTriunfo(2);
            print("completado");
        }


        //GameObject engranajeObjeto = GameObject.FindWithTag("Engranaje");
        //Engranaje engranaje = engranajeObjeto.GetComponent<Engranaje>();

    }

    #region intento de velas 1;
    //public void Order(int vela)
    //{
    //    //switch (vela)
    //    //{
    //    //    case 1:
    //    //        Comprobar();
    //    //        break;
    //    //    case 2:
    //    //        Comprobar();
    //    //        break;
    //    //    case 3:
    //    //        Comprobar();
    //    //        break;
    //    //    case 4:
    //    //        Comprobar();
    //    //        break;
    //    //    case 5:
    //    //        Comprobar();
    //    //        break;
    //    //    default:
    //    //        break;
    //    //}

    //    if (vela == 1)
    //    {
    //        llamas[0].SetActive(true);
    //        llamas[2].SetActive(true);
    //    }
    //    if (vela == 2)
    //    {
    //        llamas[1].SetActive(true);
    //        llamas[3].SetActive(true);
    //    }
    //    if (vela == 3)
    //    {
    //        llamas[2].SetActive(true);
    //        llamas[3].SetActive(true);
    //    }
    //    if (vela == 4)
    //    {
    //        llamas[3].SetActive(true);
    //        llamas[1].SetActive(true);
    //    }
    //    if (vela == 5)
    //    {
    //        llamas[4].SetActive(true);
    //    }
    //}


    //public void Comprobar()
    //{
    //    foreach (GameObject llama in llamas)
    //    {
    //        if(!llama.activeSelf)
    //            llama.SetActive(true);

    //        else if (llama.activeSelf)
    //            llama.SetActive(false);
    //        else 
    //            break;
    //    }
    //}
    #endregion
    #region intento inventario;
    //public void AbrirInventario()
    //{
    //    if (_Controller.InputInventario())
    //    {
    //        inventario.SetActive(true);
    //        Cursor.lockState = CursorLockMode.None;
    //        Cursor.visible = true;

    //    }
    //}
    //public void AbrirInventario()
    //{
    //    Debug.Log("Abrir Inventario");
    //    if (_Controller.InputInventario())
    //    {
    //        // Alterna la visibilidad del inventario según el estado
    //        bool isInventoryOpen = _Controller.InputInventario();

    //        // Invierte el estado actual del inventario
    //        Debug.Log("abriendo inventario");
    //        inventario.SetActive(!isInventoryOpen);

    //        // Cambia el estado del cursor según si el inventario está abierto o cerrado
    //        if (!isInventoryOpen)
    //        {
    //            Cursor.lockState = CursorLockMode.None;
    //            Cursor.visible = true;
    //        }
    //        else
    //        {
    //            Cursor.lockState = CursorLockMode.Locked;
    //            Cursor.visible = false;
    //        }
    //    }
    //}
    #endregion

    public void AbrirInventario(bool isInventoryOpen)
    {
        // Cambia el estado del cursor según si el inventario está abierto o cerrado
        if (isInventoryOpen == true)
        {
            HabilitarRaton(isInventoryOpen);
            inventario.SetActive(isInventoryOpen);
            //puntero.SetActive(false);
            //inventory.ListItems();
            //Cursor.lockState = CursorLockMode.None;
            //Cursor.visible = true;
        }
        else
        {
            HabilitarRaton(isInventoryOpen);
            inventario.SetActive(isInventoryOpen);
            //puntero.SetActive(true);
            //Cursor.lockState = CursorLockMode.Locked;
            //Cursor.visible = false;
        }
    }

    //public void AbrirInventario()
    //{
    //    isInventoryOpen = !isInventoryOpen; // Alterna el estado del inventario
    //    inventario.SetActive(isInventoryOpen);

    //    // Ajusta el estado del cursor
    //    if (isInventoryOpen)
    //    {
    //        Cursor.lockState = CursorLockMode.None;
    //        Cursor.visible = true;
    //        Debug.Log("INVENTARIO ABIERTO");
    //    }
    //    else
    //    {
    //        Cursor.lockState = CursorLockMode.Locked;
    //        Cursor.visible = false;
    //        Debug.Log("INVENTARIO CERRADO");
    //    }
    //}

    public bool DetectTruePosicionEngranaje()
    {
        int[][] validPositions = new int[][]
    {
        new int[] { 1, 3 },      // engranajes[0]
        new int[] { 0, 2, 4 },   // engranajes[1]
        new int[] { 0, 2, 4 },   // engranajes[2]
        new int[] { 1, 3 },      // engranajes[3]
        new int[] { 0, 2, 4 }    // engranajes[4]
    };

        int engranajesCorrectos = 0;

        for (int i = 0; i < engranajes.Length; i++)
        {
            bool enPosicion = false;
            foreach (int pos in validPositions[i])
            {
                if (engranajes[i].transform.position == zonewin[pos].transform.position)
                {
                    enPosicion = true;
                    break;
                }
            }
            if (enPosicion)
            {
                engranajesCorrectos++;
                engranajes[i].GetComponent<Collider>().enabled = false;
                print($"Engranaje {i} está en la posición correcta.");
            }
            else
            {
                engranajes[i].GetComponent<Collider>().enabled = true; // Permite recolocar si está mal
            }
        }

        print($"Engranajes correctos: {engranajesCorrectos}");

        if (engranajesCorrectos == engranajes.Length)
        {
            llaves[1].SetActive(true);
            ActivarTriunfo(1);
            print("¡Todos los engranajes están en su posición correcta!");
            return true;
        }
        else
        {
            print("No todos los engranajes están en su posición correcta.");
            return false;
        }
        #region intento 2
        //// Define las posiciones válidas para cada engranaje
        //int[][] validPositions = new int[][]
        //{
        //new int[] { 1, 3 }, // Posiciones válidas para engranajes[0]
        //new int[] { 0, 2, 4 }, // Posiciones válidas para engranajes[1]
        //new int[] { 0, 2, 4 }, // Posiciones válidas para engranajes[2]
        //new int[] { 1, 3 }, // Posiciones válidas para engranajes[3]
        //new int[] { 0, 2, 4 } // Posiciones válidas para engranajes[4]
        //};
        //for (int i = 0; i < engranajes.Length; i++)
        //{
        //    foreach (int pos in validPositions[i])
        //    {
        //        if (engranajes[i].transform.position == zonewin[pos].transform.position)
        //        {
        //            contEngranajes++;
        //            print($"valor de {contEngranajes}");
        //            engranajes[i].GetComponent<Collider>().enabled = false;
        //            print($"Engranaje {i} está en la posición correcta.");
        //            return true; // Salir del bucle si se encuentra una posición correcta
        //        }
        //    }
        //}
        //if (contEngranajes == 15)
        //{
        //    llaves[1].SetActive(true);
        //    ActivarTriunfo(1);
        //    print("¡Todos los engranajes están en su posición correcta!");
        //    return true;
        //}
        //else
        //{
        //    print("No todos los engranajes están en su posición correcta.");
        //    return false;
        //}
        #endregion
        #region intento1 de detectar engranajes
        // intento 1 Si todos los engranajes están en las posiciones correctas

        //if (engranajes[4].transform.position == zonewin[0].transform.position || engranajes[4].transform.position == zonewin[2].transform.position || engranajes[4].transform.position == zonewin[4].transform.position)
        //{
        //    contEngranajes++;
        //    engranajes[4].GetComponent<Collider>().enabled = false;
        //    print("tas correcto");
        //}
        //else if (engranajes[2].transform.position == zonewin[0].transform.position || engranajes[2].transform.position == zonewin[2].transform.position || engranajes[2].transform.position == zonewin[4].transform.position)
        //{
        //    contEngranajes++;
        //    engranajes[2].GetComponent<Collider>().enabled = false;
        //    print("tas correcto");
        //}
        //else if (engranajes[1].transform.position == zonewin[0].transform.position || engranajes[1].transform.position == zonewin[2].transform.position || engranajes[1].transform.position == zonewin[4].transform.position)
        //{
        //    contEngranajes++;
        //    engranajes[1].GetComponent<Collider>().enabled = false;
        //    print("tas correcto");
        //}
        //else if (engranajes[3].transform.position == zonewin[1].transform.position || engranajes[3].transform.position == zonewin[3].transform.position)
        //{
        //    contEngranajes++;
        //    engranajes[3].GetComponent<Collider>().enabled = false;
        //    print("tas correcto");
        //}
        //else if (engranajes[0].transform.position == zonewin[1].transform.position || engranajes[0].transform.position == zonewin[3].transform.position)
        //{
        //    contEngranajes++;
        //    engranajes[0].GetComponent<Collider>().enabled = false;
        //    print("tas correcto");
        //}
        ////if (contEngranajes == 5)
        ////{
        ////   // print(contengranajes);
        ////    ActivarTriunfo(1);
        ////   // contengranajes = 6;
        ////   // contengranajes = 6;
        ////}
        #endregion
    }
    public void PuzleMapaCompletado()
    {

        print("felas");
        globo_anim.enabled = true;
        ActivarTriunfo(3);
        // chincheta.GetComponent<Collider>().enabled = false;
    }
    public void PuzleMaquinaEscribirCompletado()
    {
        Vector3 position = cajonera.transform.position;
        cajonera.transform.position = new Vector3(position.x, position.y, position.z- 0.150f);
        ActivarTriunfo(4);
    }
    public void ComprobarPuzzleSalaEspera()
    {
        print(cantidad+ " cantidad");
        cantidad++;
        if (cantidad == 3)
        {

            //print("completado");
            llaves[0].SetActive(true);
            ActivarTriunfo(5);
        }
        
    }
    public void ComprobarPuzzleCajaFuerte()
    {
        caja_Fuertenumber.tag = "Untagged";
        //print("completado");
        ActivarTriunfo(6);
    }
    public void ComprobarTuberia()
    {
        ActivarTriunfo(7);
    } 
    internal void Pzlemaquina()
    {
        ActivarTriunfo(8);
    }
    public void DesabilitarNota(int numNota)
    {
        recompensas[numNota].SetActive(false);
        HabilitarRaton(false);
        print("bye bye ");
        //print("estado de la recompensa presionando E " + estadoRecompensa);
        //puntero.SetActive(true);
        //estadoRecompensa = false;
        //Cursor.lockState = CursorLockMode.Locked;
        //Cursor.visible = false;
        //notaHabilitada = false;
    }
    private void ActivarTriunfo(int puzle)
    {
        if (puzle == 1) // comprobar engranajes
        {
            print("completado");
            HabilitarRaton(true);
            recompensas[0].SetActive(true);
            //notaHabilitada = true;
            //puntero.SetActive(false);
            //Cursor.lockState = CursorLockMode.None;
            //Cursor.visible = true;
            //estadoRecompensa = true;
            //print("estado de la recompensa completado" + estadoRecompensa);
            //if (notaHabilitada)
            //{
            //    //print("estado de la recompensa presionando E " + estadoRecompensa);
            //    puntero.SetActive(true);
            //    recompensas[0].SetActive(false);
            //    //estadoRecompensa = false;
            //    Cursor.lockState = CursorLockMode.Locked;
            //    Cursor.visible = false;
            //    contEngranajes = 6;
            //    notaHabilitada = false;
            //}

        }
        if (puzle == 2) // comprobar velas
        {
            HabilitarRaton(true);
            recompensas[1].SetActive(true);
            //notaHabilitada = true;
            //puntero.SetActive(false);
            //Cursor.lockState = CursorLockMode.None;
            //Cursor.visible = true;
            //estadoRecompensa = true;
            //print("estado de la recompensa completado" + estadoRecompensa);
            //if (notaHabilitada)
            //{
            //    print("estado de la recompensa presionando E " + estadoRecompensa);
            //    puntero.SetActive(false);
            //    recompensas[1].SetActive(false);
            //    estadoRecompensa = false;
            //    Cursor.lockState = CursorLockMode.Locked;
            //    Cursor.visible = false;
            //    notaHabilitada = false;
            //}
        }
        if (puzle == 3)
        {
            HabilitarRaton(true);
            recompensas[2].SetActive(true);
            //notaHabilitada = true;
            //puntero.SetActive(false);
            //Cursor.lockState = CursorLockMode.None;
            //Cursor.visible = true;

        }
        if (puzle == 4)
        {
            HabilitarRaton(true);
            recompensas[3].SetActive(true);
            //notaHabilitada = true;
            //puntero.SetActive(false);
            //Cursor.lockState = CursorLockMode.None;
            //Cursor.visible = true;
        }
        if (puzle == 5)
        {
            HabilitarRaton(true);
            recompensas[4].SetActive(true);
        }
        if (puzle == 6)
        {
            HabilitarRaton(true);
            recompensas[5].SetActive(true);
        }
        if(puzle == 7)
        {
            HabilitarRaton(true);
            humo.SetActive(false);
            recompensas[6].SetActive(true);
        }
        if (puzle == 8)
        {
            HabilitarRaton(true);
            recompensas[7].SetActive(true);
        }

    }


}