using UnityEngine;

public class RestaurantChair : MonoBehaviour
{
    // 🌟 核心：记录这把椅子有没有被人坐
    public GameObject currentGuest = null;

    // 🌟 方便服务员过去：通过代码直接获取子物体 ServiceArea
    public GameObject GetServiceArea()
    {
        Transform serviceTransform = transform.Find("ServiceArea");
        Debug.Log("currentGuest: " + currentGuest + ", serviceTransform: " + serviceTransform);
        return serviceTransform != null ? serviceTransform.gameObject : this.gameObject;
    }
}