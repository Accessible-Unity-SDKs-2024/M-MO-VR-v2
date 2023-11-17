using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEditor;
using System;

[ExecuteInEditMode]
public class ReadActionLogs : MonoBehaviour
{
    private List<ActionLog> logs;
    private bool updated = false;

    [SerializeField]
    private bool showSonarCasts = true;
    [SerializeField]
    private bool showSonarHits = true;
    [SerializeField]
    private bool showHapticWaypoints = true;
    [SerializeField]
    private bool showHapticImpulses = true;
    [SerializeField]
    private GameObject[] waypoints;
    

    void ReadActionLog(string filePath, ref List<ActionLog> logs)
    {
        string[] lines = System.IO.File.ReadAllLines(filePath);
        List<ActionLog> actionLogs = new();

        if (logs.Last() != null && JsonUtility.FromJson<ActionLog>(lines.Last()) == logs.Last())
            return;

        foreach (string line in lines)
        {
            actionLogs.Add(JsonUtility.FromJson<ActionLog>(line));
        }

        updated = true;
    }

    private void OnDrawGizmosSelected()
    {
        if (updated)
        {
            Debug.Log("Draw Heatmaps");
            if (showSonarCasts) DrawSonarCasts();
            if (showSonarHits) DrawSonarHits();
            if (showHapticWaypoints) DrawHapticWaypoints();
            if (showHapticImpulses) DrawHapticImpulse();
            updated = false;
            HandleUtility.Repaint();
        }
    }

    private void DrawHapticImpulse()
    {
        var impulses =
            from x in logs
            where String.Equals(x.loggedAction, ActionLog.ACTIONS[2])
            select x;

        //DrawDensityHeatmap(impulses, Color.red);
        DrawHeatmap(impulses, Color.red);
    }

    private void DrawHapticWaypoints()
    {
        var waypoints = GameObject.FindGameObjectsWithTag("Waypoint");

        foreach (var wp in waypoints)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawSphere(wp.transform.position, 0.1f);
        }
    }

    private void DrawSonarHits()
    {
        var hits =
            from x in logs
            where String.Equals(x.loggedAction, ActionLog.ACTIONS[1])
            select x;

        //DrawDensityHeatmap(hits, Color.white);
        DrawHeatmap(hits, Color.gray);
    }

    private void DrawSonarCasts()
    {
        var casts =
            from x in logs
            where String.Equals(x.loggedAction, ActionLog.ACTIONS[0])
            select x;

        //DrawDensityHeatmap(casts, Color.blue);
        DrawHeatmap(casts, Color.blue);
    }

    private void DrawDensityHeatmap(IEnumerable<ActionLog> list, Color color)
    {
        for (int x = 0; x < DensityHeatmap.gridSize; x++)
        {
            for (int y = 0; y < DensityHeatmap.gridSize; y++)
            {
                float[,] density = DensityHeatmap.CalculateDensity(list);

                for (int i = 0; i < density.GetLength(0); i++)
                {
                    for (int j = 0; j < density.GetLength(1); j++)
                    {
                        // Convert the density to a color
                        color.a = density[i, j];

                        // Draw a Gizmo cube at the grid position with the calculated color
                        Gizmos.color = color;
                        Gizmos.DrawCube(new Vector3(i * (1f / (DensityHeatmap.gridSize - 1)), 0f, j * (1f / (DensityHeatmap.gridSize - 1))), Vector3.one * 0.1f);
                    }
                }
            }
        }
    }

    private void DrawHeatmap(IEnumerable<ActionLog> list, Color color)
    {
        color.a = 0.1f;
        Gizmos.color = color;
        foreach (var log in list)
        {
            Gizmos.DrawCube(log.loggedPosition, new(0.1f, 0.01f, 0.1f));
        }
    }

    void Start()
    {
        ReadActionLog("Assets/Tests/log.json", ref logs);
        Debug.Log("Initial Log Load");
        waypoints = GameObject.FindGameObjectsWithTag("Waypoint");
        Debug.Log("Waypoints Loaded");
    }

    void Update()
    {
        ReadActionLog("Assets/Tests/log.json", ref logs);
        Debug.Log("Logs Updated: " + updated);
    }
}
