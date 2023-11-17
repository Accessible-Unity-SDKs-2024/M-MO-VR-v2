using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DensityHeatmap : MonoBehaviour
{
    public static int gridSize = 10;

    public static float[,] CalculateDensity(IEnumerable<ActionLog> logs)
    {
        float[,] densityGrid = new float[gridSize, gridSize];

        foreach (ActionLog log in logs)
        {
            Vector3 position = log.loggedPosition;
            
            // Map the position to grid coordinates
            int x = Mathf.FloorToInt(Mathf.Clamp01(position.x) * (gridSize - 1));
            int y = Mathf.FloorToInt(Mathf.Clamp01(position.z) * (gridSize - 1));

            // Increment the density at the corresponding grid cell
            densityGrid[x, y]++;
        }

        // Normalize the density values
        NormalizeDensity(densityGrid);

        return densityGrid;
    }

    static float[,] NormalizeDensity(float[,] densityGrid)
    {
        float maxDensity = Mathf.NegativeInfinity;

        // Find the maximum density value
        for (int x = 0; x < gridSize; x++)
        {
            for (int y = 0; y < gridSize; y++)
            {
                if (densityGrid[x, y] > maxDensity)
                {
                    maxDensity = densityGrid[x, y];
                }
            }
        }

        // Normalize the density values to be in the range [0, 1]
        for (int x = 0; x < gridSize; x++)
        {
            for (int y = 0; y < gridSize; y++)
            {
                densityGrid[x, y] /= maxDensity;
            }
        }

        return densityGrid;
    }

}
