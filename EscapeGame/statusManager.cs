using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class statusManager : MonoBehaviour
{
    Vector3[] vertices; //頂点
    int[] triangles;    //index
    int nPoly; //頂点数
    float max; //最大値

    public GameObject statusWindowParent;
    public GameObject statusWindowParentPrologue;
    GameObject OldstatusChart;

    public Text[] stText;
    public Text[] stTextPrologue;
    public Text stTextPrologueTwo;
    public Text stTextPrologueThree;
    public Image departImage;
    public Sprite[] departImageSprite;

    // Start is called before the first frame update
    void Start()
    {
        
        max = 15;
        nPoly = 6;
        makeParams(new float[] { 5, 5, 5, 5, 5, 5 });
        setParams(GameObject.CreatePrimitive(PrimitiveType.Quad), new Color(1, 0, 0, 0.5f), 0);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StatusChartUpdate(int HP,int STR, int VIT,int TAC,int COM,int INT)
    {
        Destroy(OldstatusChart);
        max = 15;
        nPoly = 6;
        makeParams(new float[] { HP, STR, VIT, TAC, COM, INT });
        setParams(GameObject.CreatePrimitive(PrimitiveType.Quad), new Color(1, 0, 0, 0.5f), 0);
        stText[0].text = "健康\n" + HP;
        stText[1].text = "筋肉\n" + STR;
        stText[2].text = "  体力\n" + VIT;
        stText[3].text =  TAC + "\n戦略";
        stText[4].text = "話術  \n" + COM;
        stText[5].text = "知恵\n" + INT;
    }

    public void StatusChartUpdatePrologue(int HP, int STR, int VIT, int TAC, int COM, int INT, string departName, int departID)
    {
        stTextPrologueTwo.text = "健康  " + HP + "\n筋肉  " + STR + "\n体力  " + VIT + "\n戦略  " + TAC + "\n話術  " + COM + "\n知恵  " + INT;
        stTextPrologueThree.text = departName;
        switch (departID)
        {
            case 1:
                stTextPrologueThree.color = new Color(1f, 0.6f, 0.6f, 1);
                break;
            case 2:
                stTextPrologueThree.color = new Color(1f, 1f, 0.6f, 1);
                break;
            case 3:
                stTextPrologueThree.color = new Color(0.6f, 1f, 0.6f, 1);
                break;
            case 4:
                stTextPrologueThree.color = new Color(0.6f, 1f, 1f, 1);
                break;
            case 5:
                stTextPrologueThree.color = new Color(0.6f, 0.6f, 1f, 1);
                break;
            case 6:
                stTextPrologueThree.color = new Color(1f, 0.6f, 1f, 1);
                break;
            default:
                break;
        }
        departImage.sprite = departImageSprite[departID - 1];
    }


    //多角形メッシュ
    void makeParams(float[] values)
    {
        List<Vector3> vertList = new List<Vector3>();
        List<int> triList = new List<int>();
        vertList.Add(new Vector3(0, 0, 0));  //原点
        for (int n = 0; n < nPoly; n++)
        {
            float _x = values[n] / max * Mathf.Cos(n * 2 * Mathf.PI / nPoly);
            float _y = values[n] / max * Mathf.Sin(n * 2 * Mathf.PI / nPoly);
            vertList.Add(new Vector3(_x, _y));
            if (n != nPoly - 1)
            {
                triList.Add(0); triList.Add(n + 2); triList.Add(n + 1);
            }
            else
            {
                triList.Add(0); triList.Add(1); triList.Add(n + 1);
            }
        }
        vertices = vertList.ToArray();
        triangles = triList.ToArray();
    }

    //GameObjectにメッシュを適用
    void setParams(GameObject t, Color color, int order)
    {
        Mesh mesh = new Mesh();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();
        mesh.RecalculateBounds();
        t.GetComponent<MeshFilter>().sharedMesh = mesh;
        t.GetComponent<MeshCollider>().sharedMesh = mesh;
        t.GetComponent<MeshRenderer>().material.shader = Shader.Find("Sprites/Default");
        t.GetComponent<MeshRenderer>().material.color = color;
        t.transform.localScale = new Vector3(-1.7f, 1.7f, 1);
        t.transform.rotation = Quaternion.Euler(0, 0, -90);

        t.transform.SetParent(statusWindowParent.transform);
        t.transform.localPosition = new Vector3(0,0,-1000);

        OldstatusChart = t;
    }

    void setParamsPrologue(GameObject t, Color color, int order)
    {
        Mesh mesh = new Mesh();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();
        mesh.RecalculateBounds();
        t.GetComponent<MeshFilter>().sharedMesh = mesh;
        t.GetComponent<MeshCollider>().sharedMesh = mesh;
        t.GetComponent<MeshRenderer>().material.shader = Shader.Find("Sprites/Default");
        t.GetComponent<MeshRenderer>().material.color = color;
        t.transform.localScale = new Vector3(-1.7f, 1.7f, 1);
        t.transform.rotation = Quaternion.Euler(0, 0, -90);

        t.transform.SetParent(statusWindowParentPrologue.transform);
        t.transform.localPosition = new Vector3(0, 0, -1);

        OldstatusChart = t;
    }
}
