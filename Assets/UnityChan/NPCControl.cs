using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCControl : MonoBehaviour
{
    //public float roamingRadius = 10;
   // public float roamingTimerMax = 10f;
   // public float roamingTimerMin = 3f;
    public Transform krabs;
    public GameManager manager;
    public AudioClip[] voiceLines;
    
    private AudioSource audioSource;
    private Animator anim;
    private NavMeshAgent nav;
    private Transform target;
    //private float timer;
    private float currentRoamTime;
    private bool isRoaming;

    // DEBUG and Temp stuff that should eventually be removed
    //public Transform destination; // leave this empty for regular behavior
    //public bool startInRandomPosition = true;
   // public float randomPositionStartingRadius = 120;
    
    
    private void OnEnable()
    {
        anim = GetComponent<Animator>();
        nav = GetComponent<NavMeshAgent>();
        audioSource = GetComponent<AudioSource>();
        //timer = roamingTimerMax;
        //currentRoamTime = roamingTimerMin;
        //transform.Find("Body").localEulerAngles = new Vector3(0,0,0);
        StartCoroutine(Roaming());
        StartCoroutine(Speak());

        // Calculate the base offset adjustment using the capsule collider
        CapsuleCollider capsule = GetComponent<CapsuleCollider>();
        nav.baseOffset = - (capsule.center.y - capsule.height / 2) - 0.05f;
        
        // Remove this
        // Randomize starting position
        /*if (startInRandomPosition)
        {
            Vector3 initPos = GetNewDestination(transform.position, randomPositionStartingRadius, -1);
            transform.position = initPos;
        }*/
    }

    IEnumerator Speak()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(4, 15));
            audioSource.PlayOneShot(voiceLines[Random.Range(0, voiceLines.Length)]);
            yield return null;
        }

    }
 
    // Update is called once per frame
    void Update ()
    {
        //timer += Time.deltaTime;
        
        // Calculate angular velocity
        Vector3 s = transform.InverseTransformDirection(nav.velocity).normalized;
        float turn = s.x;

        // let the animator know what's going on
        anim.SetFloat("Speed", nav.velocity.magnitude);
        anim.SetFloat("Turn", turn * 2);
        if (Vector3.Distance(krabs.position, transform.position) < 3) {
            anim.SetLayerWeight(1, 1);
        }
        else {
            anim.SetLayerWeight(1, 0);
        }
        nav.SetDestination(krabs.position);
        

        // When time is up, roam to a new destination
        /*
        if (timer >= currentRoamTime) {
            Vector3 newPos;
            // debug purposes
            if (destination == null)
            {
                newPos = GetNewDestination(transform.position, roamingRadius, -1);
            }
            else
            {
                newPos = destination.position;
            }
            nav.SetDestination(newPos);
            currentRoamTime = Random.Range(roamingTimerMin, roamingTimerMax);
            timer = 0;
        }
        */
    }

    IEnumerator Roaming()
    {
        while (true)
        {
            // Wait till we're at a door
            yield return new WaitUntil(() => nav.isOnOffMeshLink);

            OffMeshLinkData data = nav.currentOffMeshLinkData;

            // data.offMeshLink should never be null... and yet sometimes it is...
            if (data.offMeshLink != null)
            {
                // off mesh link here
            }
            else // This shouldn't be needed but for some reason it (sometimes) is
            {
                Debug.LogWarning(gameObject.name + " encountered a null offMeshLink for no reason!");
                nav.CompleteOffMeshLink();
            }
        }
    }


    // Simply interpolate a straight line from start to end.
    IEnumerator Walk(OffMeshLinkData data)
    {
        float timeLeft = Vector3.Distance(data.offMeshLink.startTransform.position, data.offMeshLink.endTransform.position) * (1 / nav.speed) + 1;
        float passTime = timeLeft;

        Vector3 startPos = transform.position;
        Vector3 endPos = data.endPos + nav.baseOffset * Vector3.up;

        Vector3 lookPos = endPos - startPos;
        lookPos.y = 0;

        while (timeLeft > 0)
        {
            transform.position = Vector3.Lerp(endPos, startPos, timeLeft / passTime);
            anim.SetFloat("Speed", nav.speed - 0.2f);

            timeLeft -= Time.deltaTime;

            yield return null;
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            manager.EndGame();
        }
    }


   /* public static Vector3 GetNewDestination(Vector3 origin, float radius, int layermask) {
        // Get a random new destination
        Vector3 newDirection = Random.insideUnitSphere * radius;

        newDirection += origin;
        
        NavMeshHit navHit;
 
        NavMesh.SamplePosition(newDirection, out navHit, radius, layermask);
    
        return navHit.position;
    }*/
}
