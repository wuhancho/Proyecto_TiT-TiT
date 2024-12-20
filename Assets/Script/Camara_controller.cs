using System.Net;
using UnityEngine;

public class Camara_controller : MonoBehaviour
{
    [SerializeField] private float _senseCamara = 0f;
    [SerializeField] float _maxAngleUp = 50f;
    [SerializeField] float _maxAngleDown = 50f;
    [SerializeField] Transform _camaraAnchor = null;
    [SerializeField] GameManager _gameManager = null;
    private Input_Controller _controller;
    Quaternion _initialRotation;
    void Awake()
    {
        //_gameManager = GetComponent<GameManager>();
        _controller = GetComponent<Input_Controller>();
    }
    void Start()
    {// bloqueo del cursor
        Cursor.lockState = CursorLockMode.Locked;
        _initialRotation = _camaraAnchor.localRotation;
        Cursor.visible = false;
    }
    private void Update()
    {
        Vector2 input = _controller.MouseInput();

        //// Rotación horizontal
        //Quaternion horizontalRotation = Quaternion.Euler(0f, input.x * _senseCamara * Time.deltaTime, 0f);
        //transform.localRotation *= horizontalRotation;

        //// Rotación vertical con restricciones de ángulo
        //Quaternion verticalRotation = Quaternion.Euler(input.y * _senseCamara * Time.deltaTime, 0f, 0f);
        //Quaternion newRotation = _camaraAnchor.localRotation * verticalRotation;
        //float angleX = Quaternion.Angle(newRotation, _initialRotation);
        //if (angleX <= _maxAngleDown || angleX >= 360f - _maxAngleUp)
        //{
        //    _camaraAnchor.localRotation = newRotation;
        //}
        //if(!_controller.State || _gameManager.NotaHabilitada)
        //{
        //    // Rotación horizontal
        //    Quaternion horizontalRotation = Quaternion.Euler(0f, input.x * _senseCamara * Time.deltaTime, 0f);
        //    transform.localRotation *= horizontalRotation;

        //    // Rotación vertical con restricciones de ángulo
        //    Quaternion verticalRotation = Quaternion.Euler(input.y * _senseCamara * Time.deltaTime, 0f, 0f);
        //    Quaternion newRotation = _camaraAnchor.localRotation * verticalRotation;
        //    float angleX = Quaternion.Angle(newRotation, _initialRotation);
        //    if (angleX <= _maxAngleDown || angleX >= 360f - _maxAngleUp)
        //    {
        //        _camaraAnchor.localRotation = newRotation;
        //    }
        //}

        if (_controller.State || _gameManager.NotaHabilitada || _gameManager.habilitoRaton)
        {
            return;
        }



        // Rotación horizontal
        Quaternion horizontalRotation = Quaternion.Euler(0f, input.x * _senseCamara * Time.deltaTime, 0f);
        transform.localRotation *= horizontalRotation;

        // Rotación vertical con restricciones de ángulo
        Quaternion verticalRotation = Quaternion.Euler(input.y * _senseCamara * Time.deltaTime, 0f, 0f);
        Quaternion newRotation = _camaraAnchor.localRotation * verticalRotation;
        float angleX = Quaternion.Angle(newRotation, _initialRotation);
        if (angleX <= _maxAngleDown || angleX >= 360f - _maxAngleUp)
        {
            _camaraAnchor.localRotation = newRotation;
        }

    }
}