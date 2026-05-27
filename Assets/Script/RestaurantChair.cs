using UnityEngine;

public class RestaurantChair : MonoBehaviour
{
    public GameObject currentGuest = null;

    public GameObject GetServiceArea()
    {
        Transform serviceTransform = transform.Find("ServiceArea");
        Debug.Log("currentGuest: " + currentGuest + ", serviceTransform: " + serviceTransform);
        return serviceTransform != null ? serviceTransform.gameObject : this.gameObject;
    }
}