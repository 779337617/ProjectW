using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class light_shadows : MonoBehaviour
{
    // Start is called before the first frame update
    void OnEnable()
    {

        transform.GetComponent<SpriteRenderer>().receiveShadows = true;
        transform.GetComponent<SpriteRenderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On;

    }
}
