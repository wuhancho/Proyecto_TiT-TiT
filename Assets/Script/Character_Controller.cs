using Unity.VisualScripting;
using UnityEngine;
using System.Collections;

public class Character_Controller : MonoBehaviour
{
    [SerializeField] private float _speed, _jump;
    [SerializeField] private float distan = 1.5f;
    private Input_Controller input_Controller = null;
    [SerializeField] private GameManager gameManager = null;
    [SerializeField] private Rigidbody rb;
    private Transform cameraTransform;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        input_Controller = GetComponent<Input_Controller>();
        cameraTransform = Camera.main != null ? Camera.main.transform : null;

        if (cameraTransform == null)
        {
            Debug.LogError("No se encontró la cámara principal. Asegúrate de que esté etiquetada como MainCamera.");
        }
    }
    private void Update()
    {
        //input_Controller.Interact();
        input_Controller.InputInventario();

        if (Input.GetMouseButtonDown(0) && (/*!input_Controller.State || !gameManager.NotaHabilitada ||*/ !gameManager.habilitoRaton))
        {

            Debug.Log($"lanza rayo jugador: state ={input_Controller.State},Notahabilitada ={gameManager.NotaHabilitada},habilito ={gameManager.habilitoRaton} ");
            input_Controller.RayCoger(distan);
        }
        if (/*!input_Controller.State || !gameManager.NotaHabilitada || */ !gameManager.habilitoRaton)
        {
            Move();
        }
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            print("entra La mouse1");
            input_Controller.Interact_();
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
    //private void FixedUpdate()
    //{
    //    //if (!input_Controller.State || !gameManager.NotaHabilitada)
    //    //{
    //    //    Move();
    //    //}
    //    //Jump();

    //}
    private void Move()
    {
        #region intento 2 de movimiento
        if (input_Controller.State || gameManager.NotaHabilitada || gameManager.habilitoRaton)
        {
            rb.velocity = new Vector3(0, rb.velocity.y, 0); // Detener movimiento
            return;
        }

        Vector3 forward = cameraTransform.forward;
        Vector3 right = cameraTransform.right;
        Vector3 input = input_Controller.MoveInput();
        forward.Normalize();
        right.Normalize();
        Vector3 moveDirection = forward * input.z + right * input.x;
        Vector3 velocity = moveDirection * _speed;
        velocity.y = rb.velocity.y;
        rb.velocity = velocity;
        #endregion
        #region intento 4 movimiento
        //if (input_Controller.State || gameManager.NotaHabilitada || gameManager.habilito)
        //{
        //    rb.velocity = new Vector3(0, rb.velocity.y, 0); // Detener movimiento
        //    return;
        //}

        //Vector3 forward = cameraTransform.forward;
        //Vector3 right = cameraTransform.right;
        //Vector3 input = input_Controller.MoveInput();
        //forward.y = 0; // Asegurarse de que el movimiento sea en el plano horizontal
        //right.y = 0;

        //forward.Normalize();
        //right.Normalize();

        //Vector3 moveDirection = forward * input.z + right * input.x;
        //Vector3 targetPosition = rb.position + moveDirection * _speed * Time.deltaTime;

        //// Verificar colisiones con un Raycast
        //if (!Physics.Raycast(rb.position, moveDirection.normalized, 0.5f)) // Ajusta la distancia según tu collider
        //{
        //    rb.MovePosition(targetPosition);
        //}
        #endregion
        #region intento3 de movimiento
        //if (input_Controller.State || gameManager.NotaHabilitada || gameManager.habilito)
        //{
        //    rb.velocity = new Vector3(0, rb.velocity.y, 0); // Detener movimiento
        //    return;
        //}

        //Vector3 forward = cameraTransform.forward;
        //Vector3 right = cameraTransform.right;
        //Vector3 input = input_Controller.MoveInput();
        //forward.y = 0; // Asegurarse de que el movimiento sea en el plano horizontal
        //right.y = 0;

        //forward.Normalize();
        //right.Normalize();

        //Vector3 moveDirection = forward * input.z + right * input.x;
        //Vector3 targetPosition = rb.position + moveDirection * _speed * Time.deltaTime;

        //rb.MovePosition(targetPosition); // Mueve el Rigidbody directamente
        #endregion
        #region intento1 de movimiento no usar
        //Vector3 forward = cameraTransform.forward;
        //Vector3 right = cameraTransform.right;
        //Vector3 input = input_Controller.MoveInput();
        //forward.Normalize();
        //right.Normalize();
        ////Vector3 velocity = rb.velocity;
        //Vector3 moveDirection = forward * input.z + right * input.x;
        //Vector3 velocity = moveDirection * _speed;
        //velocity.y = rb.velocity.y;
        //rb.velocity = velocity;

        //velocity.z = input.z * _speed;
        //velocity.x = input.x * _speed;
        //transform.position += transform.forward * input.z * _speed * Time.deltaTime;
        //transform.position += transform.right * input.x * _speed * Time.deltaTime;
        #endregion

    }
    //private void Jump()
    //{
    //    Vector3 input = input_Controller.MoveInput();
    //    transform.position += transform.up * input.y *_jump; 
    //}
}
