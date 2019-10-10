using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Targeting<T> where T : MonoBehaviour {
    private const float SECTOR_SIZE = 1f;

    private List<T>[,] sectors;

    public Targeting (int mapsize){
        sectors = new List<T>[mapsize,mapsize];
        initialize();
    }

    // should be called every frame
    // resets and reassigns targetable objects to sectors
    public void UpdateSectors(List<T> targetableObjects) {
        clear();
        for(int i = 0; i < targetableObjects.Count; i++) {
            T t = targetableObjects[i];
            int x = (int)(t.transform.position.x / SECTOR_SIZE);
            int y = (int)(t.transform.position.z / SECTOR_SIZE);
            sectors[x,y].Add(t);
        }
    }

    // searches sectors around t within range and returns the closest T found
    public T ScanForClosest(T t, float range,bool draw = false) {
        int scanRadius = (int)(range / SECTOR_SIZE + 1f);
        int xStart = (int)(t.transform.position.x / SECTOR_SIZE) - scanRadius + 1;
        xStart = (xStart < 0) ? 0 : xStart;
        int xEnd = (int)(t.transform.position.x / SECTOR_SIZE) + scanRadius;
        xEnd = (xEnd > sectors.GetLength(0)) ? sectors.GetLength(0) : xEnd;
        int zStart = (int)(t.transform.position.z / SECTOR_SIZE) - scanRadius + 1;
        zStart = (zStart < 0) ? 0 : zStart;
        int zEnd = (int)(t.transform.position.z / SECTOR_SIZE) + scanRadius;
        zEnd = (zEnd > sectors.GetLength(1)) ? sectors.GetLength(1) : zEnd;

        T closest = null;
        float dist = Mathf.Infinity;
        Vector3 posA = t.transform.position;
        for(int x = xStart;  x < xEnd; x++) {
            for(int z = zStart; z < zEnd; z++) {
                if (draw) {
                    Gizmos.color = Color.grey;
                    Gizmos.DrawCube(new Vector3(x * SECTOR_SIZE + SECTOR_SIZE * 0.5f,0,z * SECTOR_SIZE + SECTOR_SIZE * 0.5f),Vector3.one * SECTOR_SIZE / 4f);
                }

                for (int i = 0; i < sectors[x,z].Count; i++) {
                    if (sectors[x,z][i] == t)
                        continue;

                    Vector3 posB = sectors[x,z][i].transform.position;
                    if (Vector3.SqrMagnitude(posA - posB) <= range * range && Vector3.SqrMagnitude(posA-posB) < dist) {
                        dist = Vector3.SqrMagnitude(posA - posB);
                        closest = sectors[x,z][i];
                    }
                    if (draw) {
                        Gizmos.color = Color.red;
                        Gizmos.DrawSphere(posB + Vector3.up,0.1f);
                    }
                }
            }            
        }
        if (draw && closest != null) {
            Gizmos.color = Color.green;
            Gizmos.DrawSphere(closest.transform.position + Vector3.up,0.2f);
        }
        return closest;
    }

    // initializes all lists in sectors
    private void initialize() {
        for (int i = 0; i < sectors.GetLength(0); i++) {
            for(int j = 0; j < sectors.GetLength(1); j++) {
                sectors[i,j] = new List<T>();
            }
        }
    }

    // clears all lists in sectors
    private void clear() {
        for (int i = 0; i < sectors.GetLength(0); i++) {
            for (int j = 0; j < sectors.GetLength(1); j++) {
                sectors[i,j].Clear();
            }
        }
    }

    // draws the sector grids onto the scene
    public void DrawSectors() {
        for(int x = 0; x <= sectors.GetLength(0); x++) {
            Debug.DrawLine(new Vector3(x * SECTOR_SIZE,0,0),new Vector3(x * SECTOR_SIZE,0,sectors.GetLength(1) * SECTOR_SIZE),Color.black);
        }

        for (int y = 0; y <= sectors.GetLength(1); y++) {
            Debug.DrawLine(new Vector3(0,0,y * SECTOR_SIZE),new Vector3(sectors.GetLength(1) * SECTOR_SIZE,0,y * SECTOR_SIZE),Color.black);
        }
    }    
}
