
using UnityEngine;

public class GameOfLife : MonoBehaviour
{
    [SerializeField] private int size;
    [SerializeField][Range(0, 80)] private int startAlivePercentage;
    [SerializeField] private float timeBetweenGenerations;
    [SerializeField] private bool startPaused;
    [SerializeField] private GameObject cellPrefab;

    private float timeSinceLastGeneration = 0;
    private cell[,] cellGrid;
    private bool[,] nextGeneration;

    private void Start()
    {
        if (PlayerPrefs.HasKey("size"))
            size = PlayerPrefs.GetInt("size");
        if (PlayerPrefs.HasKey("startAlivePercentage"))
            startAlivePercentage = PlayerPrefs.GetInt("startAlivePercentage");
        if (PlayerPrefs.HasKey("timeBetweenGenerations"))
            timeBetweenGenerations = PlayerPrefs.GetFloat("timeBetweenGenerations");

        if (PlayerPrefs.HasKey("startPaused"))
        {
            if (PlayerPrefs.GetInt("startPaused") == 1)
                startPaused = true;
            else
                startPaused = false;
        }

        GenerateGrid();
        SetCellsActiveByPercentage(startAlivePercentage);

        if (startPaused)
            Time.timeScale = 0;
    }

    private void Update()
    {
        timeSinceLastGeneration += Time.deltaTime;

        if (timeSinceLastGeneration > timeBetweenGenerations)
        {
            CalculateNextGeneration();
            UpdateCurrentGeneration();

            timeSinceLastGeneration = 0;
        }

        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePos;
            mousePos.x = (Mathf.Floor(Camera.main.ScreenToWorldPoint(Input.mousePosition).x) + size/2);
            mousePos.y = (Mathf.Floor(Camera.main.ScreenToWorldPoint(Input.mousePosition).y) + size/2);

            if(cellGrid[(int)mousePos.x, (int)mousePos.y].isAlive)
                cellGrid[(int)mousePos.x, (int)mousePos.y].isAlive = false;
            else
                cellGrid[(int)mousePos.x, (int)mousePos.y].isAlive = true;
            UpdateCell(cellGrid[(int)mousePos.x, (int)mousePos.y]);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (Time.timeScale == 1f)
                Time.timeScale = 0;
            else
                Time.timeScale = 1;
        }
    }

    private void GenerateGrid()
    {
        cellGrid = new cell[size, size];
        nextGeneration = new bool[size, size];
        Camera.main.orthographicSize = size / 2;

        for (int x = 0; x < size; x++)
        {
            for (int y = 0; y < size; y++)
            {
                cellGrid[x, y].cellRenderer = 
                    Instantiate(cellPrefab, new Vector2(x - size / 2 + 0.5f, y - size / 2 + 0.5f), Quaternion.identity).GetComponent<SpriteRenderer>();
                cellGrid[x, y].isAlive = false;
                UpdateCell(cellGrid[x, y]);
            }
        }
    }

    private void CalculateNextGeneration()
    {
        for (int x = 0; x < size; x++)
        {
            for (int y = 0; y < size; y++)
            {
                nextGeneration[x, y] = ShouldBeAlive(x, y);
            }
        }
    }

    private void UpdateCurrentGeneration()
    {
        for (int x = 0; x < size; x++)
        {
            for (int y = 0; y < size; y++)
            {
                cellGrid[x, y].isAlive = nextGeneration[x, y];
                UpdateCell(cellGrid[x, y]);
            }
        }
    }

    private void SetCellsActiveByPercentage(int percentage)
    {
        Mathf.Clamp(percentage, 1, 100);

        int numberOfCellsActive = ((size * size) / 100) * percentage;

        for (int i = 0; i < numberOfCellsActive; i++)
        {
            Vector2Int position = new Vector2Int(Random.Range(0, size), Random.Range(0, size));

            while (cellGrid[position.x, position.y].isAlive)
                position = new Vector2Int(Random.Range(0, size), Random.Range(0, size));

            cellGrid[position.x, position.y].isAlive = true;

            UpdateCell(cellGrid[position.x, position.y]);
        }
    }

    public virtual void UpdateCell(cell cell)
    {
        if (cell.isAlive)
            cell.cellRenderer.color = Color.white;
        else
            cell.cellRenderer.color = Color.black;
    }

    private bool ShouldBeAlive(int x, int y)
    {
        int aliveNeighbours = 0;

        for (int x2 = -1; x2 < 2; x2++)
        {
            for (int y2 = -1; y2 < 2; y2++)
            {
                if (new Vector2(x2, y2) != Vector2.zero)
                {
                    int x3 = Wrap(x + x2);
                    int y3 = Wrap(y + y2);

                    if (cellGrid[x3, y3].isAlive)
                        aliveNeighbours++;
                }
            }
        }

        if (cellGrid[x,y].isAlive)
        {
            if (aliveNeighbours < 2)
                return false;
            else if (aliveNeighbours < 4)
                return true;
            else
                return false;
        }
        else
        {
            if (aliveNeighbours == 3)
                return true;
            else
                return false;
        }
    }

    private int Wrap(int i)
    {
        if (i < 0)
            i = size -1;
        else if (i > size -1)
            i = 0;

        return i;
    }

    public struct cell
    {
        public bool isAlive;
        public SpriteRenderer cellRenderer;
    }
}
