using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    #region public Variables

    [HideInInspector] public int inputIndex;
    #endregion

    #region _private Variables

    #endregion

    void Start()
    {

    }

    void Update()
    {
        //Il suffit d'utiliser le nom de la touche + l'index 
        //pour utiliser les inputs liés à ce joueur en particulier
        if (Input.GetButton("Push_" + inputIndex))
        {
            transform.Translate(new Vector3(0, 0, 1));
        }
    }
}
