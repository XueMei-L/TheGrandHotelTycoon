using UnityEngine;

public class RestaurantChair : MonoBehaviour
{
    public GameObject currentGuest = null;

    public GameObject GetServiceArea()
    {
        Transform serviceTransform = transform.Find("ServiceArea");
        Debug.Log("here");
        Debug.Log("currentGuest is " + currentGuest);
        return serviceTransform != null ? serviceTransform.gameObject : this.gameObject;
    }
}