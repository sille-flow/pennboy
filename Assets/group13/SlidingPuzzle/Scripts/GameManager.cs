using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace group13 {
public class GameManager : MonoBehaviour
{
    [SerializeField] private Transform gameTransform;
    [SerializeField] private Transform piecePrefab;

    private List<Transform> pieces;
    private int emptyLocation;
    private int size;
    private bool shuffling = false;

    [SerializeField] private Material pickDoor;
    [SerializeField] private Material quaker;

    private bool pickingDoors;

    private int[] doorsPicked = new int[3];
    private int numDoorsPicked = 0;

    // Create the game setup with size x size pieces.
    private void CreateGamePieces(float gapThickness)
    {
        // This is the width of each tile.
        float width = 1 / (float)size;
        for (int row = 0; row < size; row++)
        {
            for (int col = 0; col < size; col++)
            {
                Transform piece = Instantiate(piecePrefab, gameTransform);
                pieces.Add(piece);
                // Pieces will be in a game board going from -1 to +1.
                piece.localPosition = new Vector3(-1 + (2 * width * col) + width,
                                                  0,
                                                  +1 - (2 * width * row) - width);
                piece.localScale = ((2 * width) - gapThickness) * Vector3.one;
                piece.name = $"{(row * size) + col}";

                piece.GetComponent<MeshRenderer>().material = quaker;

                // We want to map the UV coordinates appropriately, they are 0->1.
                float gap = gapThickness / 2;
                Mesh mesh = piece.GetComponent<MeshFilter>().mesh;
                Vector2[] uv = new Vector2[4];
                // UV coord order: (0, 1), (1, 1), (0, 0), (1, 0)
                uv[0] = new Vector2((width * col) + gap, 1 - ((width * (row + 1)) - gap));
                uv[1] = new Vector2((width * (col + 1)) - gap, 1 - ((width * (row + 1)) - gap));
                uv[2] = new Vector2((width * col) + gap, 1 - ((width * row) + gap));
                uv[3] = new Vector2((width * (col + 1)) - gap, 1 - ((width * row) + gap));
                // Assign our new UVs to the mesh.
                mesh.uv = uv;

                // We want an empty space in the bottom right.
                if ((row == size - 1) && (col == size - 1))
                {
                    emptyLocation = (size * size) - 1;
                    piece.gameObject.SetActive(false);

                }
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        pieces = new List<Transform>();
        size = 3;
        CreateGamePieces(0.01f);
        Shuffle();
    }

    // Update is called once per frame
    void Update()
    {
        // Check for completion.
        if (!shuffling && CheckCompletion())
        {

            shuffling = true;
            StartCoroutine(WaitShuffle(0.5f));
        }

        if (!pickingDoors)
        {

            // On click send out ray to see if we click a piece.
            if (Input.GetMouseButtonDown(0))
            {
                //Vector2 vecTest = new Vector2(0, -1);
                //RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), vecTest);
                //if (hit)
                //{
                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out hit))
                {
                    // Handle hit

                    // Go through the list, the index tells us the position.
                    for (int i = 0; i < pieces.Count; i++)
                    {
                        if (pieces[i] == hit.transform)
                        {
                            // Check each direction to see if valid move.
                            // We break out on success so we don't carry on and swap back again.
                            if (SwapIfValid(i, -size, size)) { break; }
                            if (SwapIfValid(i, +size, size)) { break; }
                            if (SwapIfValid(i, -1, 0)) { break; }
                            if (SwapIfValid(i, +1, size - 1)) { break; }
                        }
                    }
                }
            }

        }

        else // picking doors
        {

            // On click send out ray to see if we click a piece.
            if (Input.GetMouseButtonDown(0))
            {
                //Vector2 vecTest = new Vector2(0, -1);
                //RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), vecTest);
                //if (hit)
                //{
                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out hit))
                {
                    // Handle hit

                    // Go through the list, the index tells us the position.
                    for (int i = 0; i < pieces.Count; i++)
                    {
                        if (pieces[i] == hit.transform)
                        {
                            int[] doorsToDoors = { 1, -1, 4, 2, -1, 5, 3, -1, 6};
                            int door = doorsToDoors[i];
                            if (door != -1)
                            {
                                doorsPicked[numDoorsPicked] = door;
                                numDoorsPicked++;
                            }
                        }
                    }
                }
            }

            if (numDoorsPicked == 3)
            {

                Debug.Log("3 doors picked, loading SampleScene...");

                //GameData.doorsPicked = this.doorsPicked;
                GameData.doorsPicked[0] = this.doorsPicked[0];
                GameData.doorsPicked[1] = this.doorsPicked[1];
                GameData.doorsPicked[2] = this.doorsPicked[2];

                SceneManager.LoadScene("SampleScene");

            }



        }
    }

    // colCheck is used to stop horizontal moves wrapping.
    private bool SwapIfValid(int i, int offset, int colCheck)
    {
        if (((i % size) != colCheck) && ((i + offset) == emptyLocation))
        {
            // Swap them in game state.
            (pieces[i], pieces[i + offset]) = (pieces[i + offset], pieces[i]);
            // Swap their transforms.
            (pieces[i].localPosition, pieces[i + offset].localPosition) = ((pieces[i + offset].localPosition, pieces[i].localPosition));
            // Update empty location.
            emptyLocation = i;
            return true;
        }
        return false;
    }

    // We name the pieces in order so we can use this to check completion.
    private bool CheckCompletion()
    {
        for (int i = 0; i < pieces.Count; i++)
        {
            if (pieces[i].name != $"{i}")
            {
                return false;
            }
        }
        return true;
    }

    private IEnumerator WaitShuffle(float duration)
    {

        showDoorSelect();

        yield return new WaitForSeconds(duration);
        // Shuffle();
        shuffling = false;
    }

    // Brute force shuffling.
    private void Shuffle()
    {
        int count = 0;
        int last = 0;
        while (count < (size * size * size))
        {
            // Pick a random location.
            int rnd = Random.Range(0, size * size);
            // Only thing we forbid is undoing the last move.
            if (rnd == last) { continue; }
            last = emptyLocation;
            // Try surrounding spaces looking for valid move.
            if (SwapIfValid(rnd, -size, size))
            {
                count++;
            }
            else if (SwapIfValid(rnd, +size, size))
            {
                count++;
            }
            else if (SwapIfValid(rnd, -1, 0))
            {
                count++;
            }
            else if (SwapIfValid(rnd, +1, size - 1))
            {
                count++;
            }
        }
    }

    private void showDoorSelect()
    {

        foreach (var piece in pieces)
        {
            piece.GetComponent<MeshRenderer>().material = pickDoor;
            piece.gameObject.SetActive(true);
            pickingDoors = true;
        }
    }
}
}