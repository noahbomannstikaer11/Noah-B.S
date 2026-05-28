using UnityEngine;

public class WorldBuilder : MonoBehaviour
{
    [SerializeField] private Material groundMaterial;
    [SerializeField] private Material roadMaterial;
    [SerializeField] private Material sidewalkMaterial;
    
    private void Start()
    {
        BuildWorld();
    }

    private void BuildWorld()
    {
        // Create ground
        CreateGround();
        
        // Create roads and sidewalks
        CreateRoads();
        
        // Create buildings
        CreateBusinessBuildings();
        
        // Add decoration
        CreateTrees();
        CreateBushes();
        CreateStreetLights();
    }

    private void CreateGround()
    {
        GameObject ground = new GameObject("Ground");
        MeshFilter mf = ground.AddComponent<MeshFilter>();
        MeshRenderer mr = ground.AddComponent<MeshRenderer>();
        ground.AddComponent<BoxCollider>();
        
        // Create simple plane mesh
        Mesh mesh = new Mesh();
        Vector3[] vertices = new Vector3[4]
        {
            new Vector3(-50, 0, -50),
            new Vector3(50, 0, -50),
            new Vector3(50, 0, 50),
            new Vector3(-50, 0, 50)
        };
        
        int[] triangles = new int[6] { 0, 2, 1, 0, 3, 2 };
        
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();
        
        mf.mesh = mesh;
        
        // Create green material for grass
        Material grassMat = new Material(Shader.Find("Standard"));
        grassMat.color = new Color(0.2f, 0.7f, 0.2f); // Green
        mr.material = grassMat;
        
        ground.transform.position = new Vector3(0, -1, 0);
        ground.transform.localScale = new Vector3(1, 1, 1);
    }

    private void CreateRoads()
    {
        // Horizontal road
        CreateRoad(new Vector3(0, 0, 0), new Vector3(60, 1, 8), "Road_Horizontal");
        
        // Vertical road
        CreateRoad(new Vector3(0, 0, 0), new Vector3(8, 1, 60), "Road_Vertical");
        
        // Left sidewalk
        CreateSidewalk(new Vector3(-8, 0.01f, 0), new Vector3(4, 0.5f, 60), "Sidewalk_Left");
        
        // Right sidewalk
        CreateSidewalk(new Vector3(8, 0.01f, 0), new Vector3(4, 0.5f, 60), "Sidewalk_Right");
        
        // Top sidewalk
        CreateSidewalk(new Vector3(0, 0.01f, -8), new Vector3(60, 0.5f, 4), "Sidewalk_Top");
        
        // Bottom sidewalk
        CreateSidewalk(new Vector3(0, 0.01f, 8), new Vector3(60, 0.5f, 4), "Sidewalk_Bottom");
    }

    private void CreateRoad(Vector3 position, Vector3 scale, string name)
    {
        GameObject road = GameObject.CreatePrimitive(PrimitiveType.Cube);
        road.name = name;
        road.transform.position = position;
        road.transform.localScale = scale;
        
        // Remove collider, we'll add box collider instead
        DestroyImmediate(road.GetComponent<Collider>());
        road.AddComponent<BoxCollider>();
        
        Material roadMat = new Material(Shader.Find("Standard"));
        roadMat.color = new Color(0.3f, 0.3f, 0.3f); // Dark gray
        road.GetComponent<Renderer>().material = roadMat;
    }

    private void CreateSidewalk(Vector3 position, Vector3 scale, string name)
    {
        GameObject sidewalk = GameObject.CreatePrimitive(PrimitiveType.Cube);
        sidewalk.name = name;
        sidewalk.transform.position = position;
        sidewalk.transform.localScale = scale;
        
        DestroyImmediate(sidewalk.GetComponent<Collider>());
        sidewalk.AddComponent<BoxCollider>();
        
        Material sidewalkMat = new Material(Shader.Find("Standard"));
        sidewalkMat.color = new Color(0.7f, 0.7f, 0.7f); // Light gray
        sidewalk.GetComponent<Renderer>().material = sidewalkMat;
    }

    private void CreateBusinessBuildings()
    {
        // Building 1: Lemonade Stand (small kiosk)
        CreateBuilding(new Vector3(-20, 0, -20), new Vector3(4, 3, 4), "Lemonade Stand", new Color(1f, 1f, 0.5f));
        
        // Building 2: Hot Dog Stand (small kiosk)
        CreateBuilding(new Vector3(-20, 0, 15), new Vector3(4, 3, 4), "Hot Dog Stand", new Color(1f, 0.6f, 0f));
        
        // Building 3: Small Shop
        CreateBuilding(new Vector3(20, 0, -20), new Vector3(6, 4, 6), "Small Shop", new Color(0.8f, 0.2f, 0.2f));
        
        // Building 4: Supermarket
        CreateBuilding(new Vector3(20, 0, 15), new Vector3(10, 5, 8), "Supermarket", new Color(0.2f, 0.2f, 0.8f));
        
        // Building 5: Restaurant
        CreateBuilding(new Vector3(-35, 0, 0), new Vector3(7, 4.5f, 7), "Restaurant", new Color(0.8f, 0.5f, 0.2f));
    }

    private void CreateBuilding(Vector3 position, Vector3 scale, string name, Color color)
    {
        GameObject building = GameObject.CreatePrimitive(PrimitiveType.Cube);
        building.name = name;
        building.transform.position = position + Vector3.up * (scale.y / 2);
        building.transform.localScale = scale;
        
        DestroyImmediate(building.GetComponent<Collider>());
        building.AddComponent<BoxCollider>();
        
        Material buildingMat = new Material(Shader.Find("Standard"));
        buildingMat.color = color;
        building.GetComponent<Renderer>().material = buildingMat;
        
        // Add a roof
        GameObject roof = GameObject.CreatePrimitive(PrimitiveType.Cube);
        roof.name = name + "_Roof";
        roof.transform.parent = building.transform;
        roof.transform.localPosition = new Vector3(0, scale.y / 2 + 0.3f, 0);
        roof.transform.localScale = new Vector3(1.1f, 0.6f, 1.1f);
        
        DestroyImmediate(roof.GetComponent<Collider>());
        
        Material roofMat = new Material(Shader.Find("Standard"));
        roofMat.color = new Color(0.2f, 0.2f, 0.2f); // Dark gray
        roof.GetComponent<Renderer>().material = roofMat;
    }

    private void CreateTrees()
    {
        // Tree positions around the map
        Vector3[] treePositions = new Vector3[]
        {
            new Vector3(-40, 0, -35),
            new Vector3(-40, 0, 35),
            new Vector3(40, 0, -35),
            new Vector3(40, 0, 35),
            new Vector3(-15, 0, -40),
            new Vector3(15, 0, -40),
            new Vector3(-15, 0, 40),
            new Vector3(15, 0, 40),
        };
        
        foreach (Vector3 pos in treePositions)
        {
            CreateTree(pos);
        }
    }

    private void CreateTree(Vector3 position)
    {
        GameObject tree = new GameObject("Tree");
        tree.transform.position = position;
        
        // Trunk
        GameObject trunk = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
        trunk.name = "Trunk";
        trunk.transform.parent = tree.transform;
        trunk.transform.localPosition = Vector3.zero;
        trunk.transform.localScale = new Vector3(0.5f, 3f, 0.5f);
        
        DestroyImmediate(trunk.GetComponent<Collider>());
        trunk.AddComponent<CapsuleCollider>();
        
        Material trunkMat = new Material(Shader.Find("Standard"));
        trunkMat.color = new Color(0.4f, 0.2f, 0f); // Brown
        trunk.GetComponent<Renderer>().material = trunkMat;
        
        // Foliage (sphere)
        GameObject foliage = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        foliage.name = "Foliage";
        foliage.transform.parent = tree.transform;
        foliage.transform.localPosition = new Vector3(0, 2.5f, 0);
        foliage.transform.localScale = new Vector3(4f, 4f, 4f);
        
        DestroyImmediate(foliage.GetComponent<Collider>());
        foliage.AddComponent<SphereCollider>();
        
        Material foliageMat = new Material(Shader.Find("Standard"));
        foliageMat.color = new Color(0.1f, 0.6f, 0.1f); // Dark green
        foliage.GetComponent<Renderer>().material = foliageMat;
    }

    private void CreateBushes()
    {
        // Bush positions
        Vector3[] bushPositions = new Vector3[]
        {
            new Vector3(-12, 0, -35),
            new Vector3(12, 0, -35),
            new Vector3(-12, 0, 35),
            new Vector3(12, 0, 35),
            new Vector3(-35, 0, -12),
            new Vector3(-35, 0, 12),
            new Vector3(35, 0, -12),
            new Vector3(35, 0, 12),
        };
        
        foreach (Vector3 pos in bushPositions)
        {
            CreateBush(pos);
        }
    }

    private void CreateBush(Vector3 position)
    {
        GameObject bush = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        bush.name = "Bush";
        bush.transform.position = position + Vector3.up * 0.8f;
        bush.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
        
        DestroyImmediate(bush.GetComponent<Collider>());
        bush.AddComponent<SphereCollider>();
        
        Material bushMat = new Material(Shader.Find("Standard"));
        bushMat.color = new Color(0.2f, 0.5f, 0.2f); // Medium green
        bush.GetComponent<Renderer>().material = bushMat;
    }

    private void CreateStreetLights()
    {
        // Street light positions
        Vector3[] lightPositions = new Vector3[]
        {
            new Vector3(-25, 0, -8),
            new Vector3(-15, 0, -8),
            new Vector3(-5, 0, -8),
            new Vector3(5, 0, -8),
            new Vector3(15, 0, -8),
            new Vector3(25, 0, -8),
            
            new Vector3(-25, 0, 8),
            new Vector3(-15, 0, 8),
            new Vector3(-5, 0, 8),
            new Vector3(5, 0, 8),
            new Vector3(15, 0, 8),
            new Vector3(25, 0, 8),
            
            new Vector3(-8, 0, -25),
            new Vector3(-8, 0, -15),
            new Vector3(-8, 0, -5),
            new Vector3(-8, 0, 5),
            new Vector3(-8, 0, 15),
            new Vector3(-8, 0, 25),
            
            new Vector3(8, 0, -25),
            new Vector3(8, 0, -15),
            new Vector3(8, 0, -5),
            new Vector3(8, 0, 5),
            new Vector3(8, 0, 15),
            new Vector3(8, 0, 25),
        };
        
        foreach (Vector3 pos in lightPositions)
        {
            CreateStreetLight(pos);
        }
    }

    private void CreateStreetLight(Vector3 position)
    {
        GameObject light = new GameObject("StreetLight");
        light.transform.position = position;
        
        // Post
        GameObject post = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
        post.name = "Post";
        post.transform.parent = light.transform;
        post.transform.localPosition = Vector3.zero;
        post.transform.localScale = new Vector3(0.2f, 4f, 0.2f);
        
        DestroyImmediate(post.GetComponent<Collider>());
        post.AddComponent<CapsuleCollider>();
        
        Material postMat = new Material(Shader.Find("Standard"));
        postMat.color = new Color(0.2f, 0.2f, 0.2f); // Dark gray
        post.GetComponent<Renderer>().material = postMat;
        
        // Light bulb
        GameObject bulb = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        bulb.name = "Bulb";
        bulb.transform.parent = light.transform;
        bulb.transform.localPosition = new Vector3(0, 4f, 0);
        bulb.transform.localScale = new Vector3(0.4f, 0.4f, 0.4f);
        
        DestroyImmediate(bulb.GetComponent<Collider>());
        
        Material bulbMat = new Material(Shader.Find("Standard"));
        bulbMat.color = new Color(1f, 1f, 0.8f); // Warm yellow
        bulbMat.SetFloat("_Emission", 1f);
        bulb.GetComponent<Renderer>().material = bulbMat;
        
        // Add light component
        Light lightComponent = light.AddComponent<Light>();
        lightComponent.type = LightType.Point;
        lightComponent.range = 15f;
        lightComponent.intensity = 0.8f;
        lightComponent.color = new Color(1f, 1f, 0.8f);
    }
}
