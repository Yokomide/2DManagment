using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GridProgram : MonoBehaviour
{
    public static GridProgram Instance;

    public Tilemap MovingTilemap;

    public GameObject Robot;
    public Vector3Int RobotStartPosition = Vector3Int.zero;
    public Directions RobotStartDirection = Directions.Down;
    
    [HideInInspector] public Vector3Int RobotPosition;
    [HideInInspector] public Directions RobotDirection;

    private Vector3 offset;

    [Space]
    [HideInInspector] public List<Point> Points;
    [SerializeField] private Sprite[] _pointSprites = new Sprite[3];
    [SerializeField] private Sprite[] _hostSprites = new Sprite[3];
    [SerializeField] private GameObject _pointReference;
    [SerializeField] private GameObject _hostReference;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        Points = new List<Point>()
        {
            new Point(new Vector3Int(2, 1, 0), Vector3Int.zero, _pointSprites[0], _hostSprites[0], _pointReference, _hostReference, MovingTilemap),
            new Point(new Vector3Int(0, 1, 0), new Vector3Int(2, 2, 0), _pointSprites[1], _hostSprites[1], _pointReference, _hostReference, MovingTilemap)
        };

        offset = MovingTilemap.CellToWorld(new Vector3Int(1, 1, 0)) / 2;

        SetRobotOnStartPosition();
    }

    Dictionary<Directions, Vector3Int> DirectionMoveVectors = new Dictionary<Directions, Vector3Int>()
    {
        {Directions.Up, new Vector3Int(0, 1, 0) },
        {Directions.Down, new Vector3Int(0, -1, 0) },
        {Directions.Right, new Vector3Int(1, 0, 0) },
        {Directions.Left, new Vector3Int(-1, 0, 0) }
    };
    
    Dictionary<Directions, Vector3Int> DirectionRotateVectors = new Dictionary<Directions, Vector3Int>()
    {
        {Directions.Up, new Vector3Int(0, 0, 0) },
        {Directions.Down, new Vector3Int(0, 0, 180) },
        {Directions.Right, new Vector3Int(1, 0, -90) },
        {Directions.Left, new Vector3Int(-1, 0, 90) }
    };

    public enum Directions
    {
        Up,
        Down,
        Left,
        Right
    }

    public enum Rotation
    {
        Left,
        Right
    }

    public void SetRobotOnStartPosition()
    {
        RobotPosition = RobotStartPosition;
        RobotDirection = RobotStartDirection;
        Robot.transform.position = MovingTilemap.CellToWorld(RobotPosition) + offset;
        Robot.transform.rotation = Quaternion.Euler(DirectionRotateVectors[RobotDirection]);
    }

    public void RotateRobot(Rotation r)
    {
        StartCoroutine(RobotRotating(r));
    }

    public void TryMoveRobot()
    {
        Vector3Int targetPos = RobotPosition + DirectionMoveVectors[RobotDirection];
        if (MovingTilemap.HasTile(targetPos))
        {
            StartCoroutine(RobotMoving(targetPos));
        }
    }

    IEnumerator RobotMoving(Vector3Int targetPos)
    {
        float progress = 0;
        Vector3 trueTargetPos = MovingTilemap.CellToWorld(targetPos) + offset;
        Vector3 trueStartPos = Robot.transform.position;
        while (progress < 1)
        {
            progress += Time.deltaTime * 3;
            Robot.transform.position = Vector3.Lerp(trueStartPos, trueTargetPos, progress);
            yield return null;
        }
        RobotPosition = targetPos;
    }

    IEnumerator RobotRotating(Rotation r)
    {
        Vector3 startRot = DirectionRotateVectors[RobotDirection];
        Vector3 targetRot = Vector3.zero;
        Directions newDirection = Directions.Down;

        Directions[] directions = new Directions[4]
        {
            Directions.Down, Directions.Left, Directions.Up, Directions.Right
        };

        for (int i = 0; i < directions.Length; i++)
        {
            if (RobotDirection == directions[i])
            {
                int newIndex = i + (r == Rotation.Right ? 1 : -1);
                if (newIndex == 4)
                {
                    newIndex = 0;
                }
                else if (newIndex == -1)
                {
                    newIndex = 3;
                }
                newDirection = directions[newIndex];
                targetRot = DirectionRotateVectors[newDirection];

                break;
            }
        }

        float progress = 0;
        while (progress < 1)
        {
            progress += Time.deltaTime * 3;
            Robot.transform.rotation = Quaternion.Euler(new Vector3(0, 0, Mathf.LerpAngle(startRot.z, targetRot.z, progress)));
            yield return null;
        }

        RobotDirection = newDirection;
    }

    public class Point
    {
        private Vector3 _offset;

        public Vector3Int StartPointPos;
        public Vector3Int PointPos;
        public Vector3Int HostPos;

        public bool Active;
        private bool _pointOnGround = true;
        public bool PointOnGround
        {
            get
            {
                return _pointOnGround;
            }

            set
            {
                _pointOnGround = value;
                Active = _pointOnGround && PointPos == HostPos;
            }
        }

        public Tilemap tilemap;
        public GameObject PointObject;

        public void Restart()
        {
            PointPos = StartPointPos;
            PointObject.transform.parent = null;
            PointObject.transform.position = tilemap.CellToWorld(PointPos) + _offset;
            PointObject.transform.rotation = Quaternion.identity;
            PointObject.GetComponent<SpriteRenderer>().sortingOrder = 3;

            PointOnGround = true;
        }

        public Point(Vector3Int pointPos, Vector3Int hostPos, 
            Sprite PointSprite, Sprite HostSprite,
            GameObject pointReference, GameObject hostReference,
            Tilemap tilemap)
        {
            this.tilemap = tilemap;
            _offset = this.tilemap.CellToWorld(new Vector3Int(1, 1, 0)) / 2;

            StartPointPos = pointPos;
            PointPos = StartPointPos;
            HostPos = hostPos;

            PointObject = Instantiate(pointReference, this.tilemap.CellToWorld(pointPos) + _offset, Quaternion.identity);
            PointObject.GetComponent<SpriteRenderer>().sprite = PointSprite;
            Instantiate(hostReference, this.tilemap.CellToWorld(hostPos) + _offset, Quaternion.identity).GetComponent<SpriteRenderer>().sprite = HostSprite;
        }
    }
}
