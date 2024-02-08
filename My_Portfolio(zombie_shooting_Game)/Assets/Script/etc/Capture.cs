using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class Capture : MonoBehaviour
{
    public Camera cam;
    public RenderTexture rt;
    public Image bg;
    public new GameObject gameObject;

    private void Start()
    {
        cam = Camera.main;   
    }

    public void Create()
    {
        StartCoroutine(CaptureImage());
    }
    IEnumerator CaptureImage()
    {
        yield return null;

        Texture2D tex = new Texture2D(rt.width, rt.height, TextureFormat.ARGB32, false, true);
        RenderTexture.active = rt;
        tex.ReadPixels(new Rect(0, 0, rt.width, rt.height), 0, 0); // �о����

        yield return null;

        File.WriteAllBytes(gameObject.name + ".png", tex.EncodeToPNG()); // png�� ����

        yield return null;
    }
}
