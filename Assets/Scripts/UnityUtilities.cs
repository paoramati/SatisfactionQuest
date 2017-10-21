using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnityUtilities {

    public static UnityEngine.UI.InputField AssignInputField(string pObjectName)
    {
        InputField lcInputField;
        GameObject lcGameObject = GameObject.Find(pObjectName);
        return lcInputField = lcGameObject.GetComponent<InputField>();
    }

    public static UnityEngine.UI.Button AssignButton(string pObjectName)
    {
        Button lcButton;
        GameObject lcGameObject = GameObject.Find(pObjectName);
        return lcButton = lcGameObject.GetComponent<Button>();
    }

    public static T AssignGameObject<T>(string pObjectName) where T:new ()
    {
        T lcObject;
        GameObject lcGameObject = GameObject.Find(pObjectName);
        lcObject = lcGameObject.GetComponent<T>();
        return lcObject;
    }


}
