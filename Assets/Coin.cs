using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Rotate());
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < 0)
            Destroy(this.gameObject);
    }

    private IEnumerator Rotate()
    {
        while (true)
        {
            transform.Rotate(0, 60 * Time.deltaTime, 0);
            yield return null;
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<ThirdPersonPlayerController>().CollectCoin();
            Destroy(this.gameObject);
        }
    }
}
