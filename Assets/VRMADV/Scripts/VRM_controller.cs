﻿using System;
using UnityEngine;
using VRM;
using UnityEngine.Networking;
using System.Collections;
using UniRx.Async;

public class VRM_controller : MonoBehaviour
{
    public GameObject obj;

    async void Start()
    {
        var fileName = GameObject.Find("VRMPath").GetComponent<DontDestroy>().Path;
#if UNITY_EDITOR
        var path = Application.persistentDataPath + "/ModelData/" + fileName;
#elif UNITY_ANDROID
        var path = "file://"+ Application.persistentDataPath + "/ModelData/" + fileName;
#endif
        var www = new WWW(path);

        await www;
        
        var go = await VRMImporter.LoadVrmAsync(www.bytes);

        go.transform.position = new Vector3(0, -1, 0);
        Camera_controller CameraController = obj.GetComponent<Camera_controller>();
        Animator humanoid = go.GetComponent<Animator>();
        CameraController.offset = humanoid.GetBoneTransform(HumanBodyBones.Spine).position;
        CameraController.target = go;

    }
}