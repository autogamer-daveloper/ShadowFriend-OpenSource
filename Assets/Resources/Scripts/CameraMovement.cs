using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [Header ("Setting")]
    [SerializeField] private float MapBorder = 50f;
    [Range (0.5f, 5f)]
    [SerializeField] private float Sensitivity = 1.5f;
    [SerializeField] private Zavod? SelectedZavod;
    [SerializeField] private PlayerMovement? SelectedPlayer;

    private bool isSelectingPlayer = true;
    private Vector3 startPos;
    private Camera cam;

    private void Start()
    {
        cam = GetComponent<Camera>();
    }

    private void Update()
    {
        if(isSelectingPlayer)
        {
            if (Input.GetMouseButtonDown(0)) CameraSelection();
        }
        else
        {
            CameraMovementt();
        }
    }

    private void CameraMovementt()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos = Input.mousePosition;
            mousePos.z = cam.transform.position.y;
            startPos = cam.ScreenToWorldPoint(mousePos);
        }
        else if (Input.GetMouseButton(0))
        {
            Vector3 mousePos = Input.mousePosition;
            mousePos.z = cam.transform.position.y;
            Vector3 currentPos = cam.ScreenToWorldPoint(mousePos);

            float posX = (currentPos.x - startPos.x);
            float posZ = (currentPos.z - startPos.z);

            Vector3 targetPosition = transform.position - new Vector3(posX, 0, posZ);

            targetPosition.x = Mathf.Clamp(targetPosition.x, -MapBorder, MapBorder);
            targetPosition.z = Mathf.Clamp(targetPosition.z, -MapBorder, MapBorder);

            transform.position = Vector3.MoveTowards(transform.position, targetPosition, Sensitivity);
        }
    }

    private void CameraSelection()
    {
        DeselectPlayer();

#if UNITY_ANDROID
        Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
#else
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
#endif
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            SelectedZavod = hit.collider.GetComponent<Zavod>();
            SelectedPlayer = hit.collider.GetComponent<PlayerMovement>();
        }

        SelectZavod();
        SelectPlayer();
    }

    private void DeselectPlayer()
    {
        if(SelectedPlayer != null)
        {
            SelectedPlayer.DropControl();
            SelectedPlayer = null;
        }
    }

    private void SelectZavod()
    {
        if(SelectedZavod != null && SelectedPlayer != null)
        {
            SelectedZavod.SelectThis();
            SelectedZavod = null;
        }
    }

    private void SelectPlayer()
    {
        if(SelectedPlayer != null)
        {
            SelectedPlayer.ControlThis();
            isSelectingPlayer = false;
        }
    }

    public void SelectNewPlayerMobile()
    {
        isSelectingPlayer = true;
    }
}
