using UnityEngine;
using UnityEngine.UI;
using System.Collections;


public class FPSInteractionManager : MonoBehaviour
{
    [SerializeField] private Transform _fpsCameraT;
    [SerializeField] private bool _debugRay;
    [SerializeField] private float _interactionDistance;

    //private PhysicsGrabbable rif;
    //creata io per mettere le mani come padre e non più la camera
    [SerializeField] private Transform _grabbing;

    [SerializeField] private Image _target;

    //private GameObject _noMovement;

    private Interactable _pointingInteractable;
    private Grabbable _pointingGrabbable;

    private CharacterController _fpsController;
    private Vector3 _rayOrigin;

    private Grabbable _grabbedObject = null;


    void Start()
    {
       
        _fpsController = GetComponent<CharacterController>();
    }

    void Update()
    {
        _rayOrigin = _fpsCameraT.position + _fpsController.radius * _fpsCameraT.forward;

        if (_grabbedObject == null)
            CheckInteraction();

        if (_grabbedObject != null && Input.GetMouseButtonDown(0))
            Drop();

        UpdateUITarget();

        if (_debugRay)
            DebugRaycast();
    }

    private void CheckInteraction()
    {
        Ray ray = new Ray(_rayOrigin, _fpsCameraT.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, _interactionDistance))
        {
            //Check if is interactable
            _pointingInteractable = hit.transform.GetComponent<Interactable>();
            if (_pointingInteractable)
            {
                if (Input.GetMouseButtonDown(0))
                    _pointingInteractable.Interact(gameObject);
            }

            //Check if is grabbable
            _pointingGrabbable = hit.transform.GetComponent<Grabbable>();
            if (_grabbedObject == null && _pointingGrabbable)
            {
                if (Input.GetMouseButtonDown(1))
                {
                    _pointingGrabbable.Grab(gameObject);
                    Grab(_pointingGrabbable);
                }
                /*if (Input.GetMouseButtonDown(0)) {
                    _pointingGrabbable.Grab_(gameObject);
                    Grab_(_pointingGrabbable);
                }*/
            }
        }
        //If NOTHING is detected set all to null
        else
        {
            _pointingInteractable = null;
            _pointingGrabbable = null;
        }
    }

    private void UpdateUITarget()
    {
        if (_pointingInteractable)
            _target.color = Color.green;
        else if (_pointingGrabbable)
            _target.color = Color.yellow;
        else
            _target.color = Color.red;
    }

    private void Drop()
    {
        if (_grabbedObject == null)
            return;

        _grabbedObject.transform.parent = _grabbedObject.OriginalParent;
        _grabbedObject.Drop();

        _target.enabled = true;
        _grabbedObject = null;
    }

    private void Grab(Grabbable grabbable)
    {
        _grabbedObject = grabbable;
        /*_noMovement = GameObject.Find("carro");
        if (_noMovement = GameObject.Find("carro")) {
            grabbable.transform.SetParent(_grabbing);
       }
          else
        {
            grabbable.transform.SetParent(_fpsCameraT);
       }
        if(_upDown == true) { 
        grabbable.transform.SetParent(_grabbing);
            //qui c'era _fpsCameraT, ma con quella potevo spostare ad esempio il carrello anche in alto e in basso. Invece ora l'ho imparentato con le mani, creando un empty, e quindi può essere solo spostato in avanti e indietro 
        }
        else
        {
            grabbable.transform.SetParent(_fpsCameraT);
        }
        */

        //if (grabbable == GameObject.Find("carro"))
        //    grabbable.transform.SetParent(_grabbing);
        // else
        grabbable.transform.SetParent(_grabbing); 

        _target.enabled = false;
    }

    /*private void Grab_(Grabbable grabbable_)
    {
        _grabbedObject = grabbable_;
        grabbable_.transform.SetParent(_fpsCameraT);
    }*/
    private void DebugRaycast()
        {
            Debug.DrawRay(_rayOrigin, _fpsCameraT.forward * _interactionDistance, Color.red);
        }
    
}
