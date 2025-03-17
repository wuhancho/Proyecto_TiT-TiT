using Unity.VisualScripting;
using UnityEngine;
using System.Collections;
using static UnityEditor.PlayerSettings;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine.Rendering;

public class Character_Controller : MonoBehaviour
{
    [SerializeField] private float _speed, _jump;
    [SerializeField] private float distan = 1.5f;
    private Input_Controller input_Controller = null;
    [SerializeField] private GameManager gameManager = null;
    [SerializeField] private Rigidbody rb;
    private Transform cameraTransform;
    private CapsuleCollider capCol;
    private float minMoveDistance = 0.01f;
    private CapsuleCollider charCol;
    [SerializeField] private LayerMask validLayers;
    [SerializeField] private float bias = 0.05f;
   
    private RaycastHit[] hitBuffer = new RaycastHit[10];


    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        input_Controller = GetComponent<Input_Controller>();
        cameraTransform = Camera.main != null ? Camera.main.transform : null;

        if (cameraTransform == null)
        {
            Debug.LogError("No se encontró la cámara principal. Asegúrate de que esté etiquetada como MainCamera.");
        }
        capCol = GetComponent<CapsuleCollider>();
        charCol = GetComponent<CapsuleCollider>();

    }
    private void Update()
    {
        //input_Controller.Interact();
        input_Controller.InputInventario();

        if (Input.GetMouseButtonDown(0) && !gameManager.habilitoRaton)
        {
            Debug.Log($"lanza rayo jugador: state ={input_Controller.State}, Notahabilitada ={gameManager.NotaHabilitada}, habilito ={gameManager.habilitoRaton}");
            input_Controller.RayCoger(distan);
        }

        if (Input.GetKeyDown(KeyCode.Mouse1) && !input_Controller.State)
        {
            print("entra La mouse1");
            input_Controller.Interact_();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }

        if (!gameManager.habilitoRaton)
        {
            Vector3 inputDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

            if (inputDirection.magnitude > 0)
            {
                Vector3 moveDirection = cameraTransform.TransformDirection(inputDirection);
                moveDirection.y = 0; // Evita que se mueva en el eje Y involuntariamente
                moveDirection.Normalize();

                Vector3 velocity = moveDirection * _speed * Time.deltaTime;
                Move(velocity); // ✅ Ahora Move recibe el argumento correcto.
            }
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
    //int maxBounces = 55;
    //float skinWidth = 0.015f;
    //float maxSlopeAngle = 55;
    //Bounds bounds;
    //Bounds = Collider.bouns;
    //    bounds.Expand(-2*skinWidth);



    //    private Vector3 Move(Vector3 vel, Vector3 pos, int depth, bool gravityPass, Vector3 velInit)
    //{
    //    if (depth >= maxBounces) {
    //        return Vector3.zero;
    //    }

    //    float dist = vel.magnitude + skinWidth;

    //    RaycastHit hit;
    //    if (
    //         Physics.SphereCast(pos, bounds.extents.x, vel.normalized, out hit, dist)) {


    //        Vector3 snapToSurface = vel.normalized * (hit.distance - skinWidth);
    //        Vector3 leftover = vel - snapToSurface;
    //        float angle = Vector3.Angle(Vector3.up, hit.normal);

    //        if (snapToSurface.magnitude <= skinWidth) {
    //            snapToSurface = Vector3.zero;

    //        }

    //        // normal ground / slope
    //        if (angle <= maxSlopeAngle) {
    //            if (gravityPass) {
    //                return snapToSurface; }

    //            leftover = ProjectandScale(leftover, hit.normal);
    //        }
    //        // wall or steep slope
    //        else {

    //            float scale = 1 - Vector3.Dot(
    //             new Vector3(hit.normal.x, 0, hit.normal.z).normalized,
    //             -new Vector3(velInit.x, 0, velInit.z).normalized
    //       );

    //            if (isGrounded && !gravityPass) {
    //                leftover = ProjectandScale(
    //                new Vector3(leftover.x, 0, leftover.z),
    //                new Vector3(hit.normal.x, 0, hit.normal.z)
    //                ).normalized;
    //                leftover *= scale;
    //                }
    //    else {
    //                leftover = ProjectAndScale(leftover, hit.normal) * scale;
    //            }
    //        }

    //        return snapToSurface + Move(leftover, pos + snapToSurface, depth + 1, gravityPass, velInit);

    //    }

    //    return vel;
    //}


    // bias = 0.05f;

    void Move(Vector3 velocity)
    {
        Vector3 position = transform.position;
        Vector3 trajectory = velocity;
        int bounces = 0;

        while (trajectory.magnitude > minMoveDistance && bounces < 5)
        {
            if (CapsuleTrace(position, trajectory, transform.rotation, validLayers,
                hitBuffer, QueryTriggerInteraction.UseGlobal, bias))
            {
                float hitDistance = GetClosestHit(hitBuffer).distance;
                float fraction = (hitDistance / trajectory.magnitude);
                position += hitDistance * trajectory;
                trajectory *= (1 - fraction);

                for (int i = 0; i < hitBuffer.Length; i++)
                {
                    RaycastHit thisHit = hitBuffer[i];
                    if (thisHit.distance == 0) break;
                    Vector3 projected = Vector3.ProjectOnPlane(trajectory, thisHit.normal);
                    trajectory = projected;
                }
            }
            else
            {
                break;
            }
            bounces++;
        }
        transform.position = position + trajectory;
    }

    public bool CapsuleTrace(
        Vector3 position,
        Vector3 direction,
        Quaternion orientation,
        LayerMask validLayers,
        RaycastHit[] hitBuffer,
        QueryTriggerInteraction triggers = QueryTriggerInteraction.UseGlobal,
        float bias = float.Epsilon
    )
    {
        position += orientation * charCol.center;
        Vector3 capsuleLineSegment = orientation * Vector3.up * ((charCol.height / 2) - (charCol.radius));

        Vector3 safeDirection = direction.magnitude > 0 ? direction.normalized : Vector3.forward;

        int traceHits = Physics.CapsuleCastNonAlloc(
            position + capsuleLineSegment,
            position - capsuleLineSegment,
            charCol.radius + bias,
            safeDirection,
            hitBuffer,
            direction.magnitude,
            validLayers,
            triggers
        );

        return traceHits > 0;
    }

    private RaycastHit GetClosestHit(RaycastHit[] hitBuffer)
    {
        RaycastHit closestHit = new RaycastHit();
        float minDistance = float.MaxValue;

        foreach (var hit in hitBuffer)
        {
            if (hit.distance > 0 && hit.distance < minDistance)
            {
                closestHit = hit;
                minDistance = hit.distance;
            }
        }
        return closestHit;
    }
}

    




























    //#region intento 2 de movimiento
    //if (input_Controller.State || gameManager.NotaHabilitada || gameManager.habilitoRaton)
    //{
    //    rb.velocity = new Vector3(0, rb.velocity.y, 0); // Detener movimiento
    //    return;
    //}

    //Vector3 forward = cameraTransform.forward;
    //Vector3 right = cameraTransform.right;
    //Vector3 input = input_Controller.MoveInput();
    //forward.Normalize();
    //right.Normalize();
    //Vector3 moveDirection = forward * input.z + right * input.x;
    //Vector3 velocity = moveDirection * _speed;
    //velocity.y = rb.velocity.y;
    //rb.velocity = velocity;
    //#endregion
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


//private void Jump()
//{
//    Vector3 input = input_Controller.MoveInput();
//    transform.position += transform.up * input.y *_jump; 
//}

