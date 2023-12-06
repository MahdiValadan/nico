using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    //Canvases
    public Canvas prologue;
    public Canvas level1;
    public Canvas level2;
    public Canvas level3;
    public Canvas epilogue;
    
    //Cubes
    public GameObject cubeSet1;
    public GameObject cubeSet2;
    public GameObject cubeSet3;

    public void DropdownHandler(int index)
    {
        switch (index)
        {
            case 0:
                prologue.gameObject.SetActive(true);
                level1.gameObject.SetActive(false);
                level2.gameObject.SetActive(false);
                level3.gameObject.SetActive(false);
                epilogue.gameObject.SetActive(false);

                cubeSet1.SetActive(false);
                cubeSet2.SetActive(false);
                cubeSet3.SetActive(false);
                break;
            case 1:
                prologue.gameObject.SetActive(false);
                level1.gameObject.SetActive(true);
                level2.gameObject.SetActive(false);
                level3.gameObject.SetActive(false);
                epilogue.gameObject.SetActive(false);

                cubeSet1.SetActive(true);
                cubeSet2.SetActive(false);
                cubeSet3.SetActive(false);
                break;
            case 2:
                prologue.gameObject.SetActive(false);
                level1.gameObject.SetActive(false);
                level2.gameObject.SetActive(true);
                level3.gameObject.SetActive(false);
                epilogue.gameObject.SetActive(false);

                cubeSet1.SetActive(false);
                cubeSet2.SetActive(true);
                cubeSet3.SetActive(false);
                break;
            case 3:
                prologue.gameObject.SetActive(false);
                level1.gameObject.SetActive(false);
                level2.gameObject.SetActive(false);
                level3.gameObject.SetActive(true);
                epilogue.gameObject.SetActive(false);

                cubeSet1.SetActive(false);
                cubeSet2.SetActive(false);
                cubeSet3.SetActive(true);
                break;
            case 4:
                prologue.gameObject.SetActive(false);
                level1.gameObject.SetActive(false);
                level2.gameObject.SetActive(false);
                level3.gameObject.SetActive(false);
                epilogue.gameObject.SetActive(true);

                cubeSet1.SetActive(false);
                cubeSet2.SetActive(false);
                cubeSet3.SetActive(false);
                break;
            default:
                prologue.gameObject.SetActive(true);
                level1.gameObject.SetActive(false);
                level2.gameObject.SetActive(false);
                level3.gameObject.SetActive(false);
                epilogue.gameObject.SetActive(false);

                cubeSet1.SetActive(false);
                cubeSet2.SetActive(false);
                cubeSet3.SetActive(false);
                break;
        }
    }
}
