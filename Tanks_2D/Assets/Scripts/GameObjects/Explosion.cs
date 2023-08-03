using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //Destroy ourself in 2 seconds
        StartCoroutine(DestroyAfter(0.5f));
    }

    private IEnumerator DestroyAfter(float waitTime)
    {
        while (true)
        {
            yield return new WaitForSeconds(waitTime);

            //Destroy self
            Destroy(gameObject);
        }
    }
}