using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class PlayerMovement : MonoBehaviour
{
    [Header ("Player Settings")]
    [SerializeField] private NavMeshAgent Agent;
    [SerializeField] private GameObject Arrow;
    [SerializeField] private Animator _Animator;
    [SerializeField] private GameObject Selected;
    [SerializeField] private GameObject Portrait;
    [SerializeField] private GameObject Panel;

    private bool CanWalking = false;
    private Quaternion zero = Quaternion.Euler(0, 0, 0);
    private GameObject? UsingArrow;

    private Zavod? ZXC;

    private void Update()
    {
        if(!CanWalking) return;

#if UNITY_ANDROID
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            Click();
        }
#else
        if (Input.GetMouseButtonDown(0))
        {
            Click();
        }
#endif

        _Animator.SetBool("isWalking", Agent.velocity.magnitude > 0.1f);
    }

    private void Click()
    {
#if UNITY_ANDROID
        Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
#else
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
#endif
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            if(hit.collider.CompareTag("Zavod"))
            {
                ZXC = hit.collider.GetComponent<Zavod>();
                ZXC.GoingToZavod(true);
                Agent.SetDestination(hit.point);
                return;
            }
            else
            {
                ZXC?.GoingToZavod(false);
                ZXC = null;
            }

            Agent.SetDestination(hit.point);
            
            if(UsingArrow == null)
            {
                UsingArrow = Instantiate(Arrow, hit.point, zero);
            }
            else
            {
                UsingArrow.SetActive(false);
                var anim = UsingArrow.GetComponent<Animation>();
                anim.Stop();
                anim.Play("Arrow-Animation");
                UsingArrow.transform.position = hit.point;
                UsingArrow.SetActive(true);
            }
        }
    }

    public void ControlThis()
    {
        CanWalking = true;
        Selected.SetActive(true);
        Portrait.SetActive(true);
        Panel.SetActive(true);
    }

    public void DropControl()
    {
        CanWalking = false;
        Selected.SetActive(false);
        Portrait.SetActive(false);
        Panel.SetActive(false);
    }
}
