using MonotypeUnityTextPlugin;
using NUnit.Framework;
using System;
using System.Collections;
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.TestTools;
using XnaFan.ImageComparison;


public class Alignment : BaseTest
{
    [TearDown]
    public void CleanUp()
    {
        EditorPrefs.SetBool("ShowFontLicense", false);
        foreach (GameObject o in UnityEngine.Object.FindObjectsOfType<GameObject>())
        {
            GameObject.DestroyImmediate(o);
        }
        MPTextComponentNative.Cleanup();
    }

    //test user can change alignment horizontal
    [UnityTest,]
    [Timeout(100000000)]
    public IEnumerator Test_UserCanChangeAlignmentHorizontal()
	//IEnumerator should be the return type of play mode tests in unity
    {
        //Not checking for warning message
        String tcName = "Test_UserCanChangeAlignment";
        setupScene();
        var parent = GameObject.Find("Canvas(Clone)").transform;
        var child = parent.GetChild(0);
        //Applying Horizontal Alignment
        //changing to Default
        fontAlign.setFontHorizontalAlignment(child, "Default");
        yield return screenshotAndCompare(tcName + "Default.png");
        //changing to Left
        fontAlign.setFontHorizontalAlignment(child, "Left");
        yield return screenshotAndCompare(tcName + "Left.png");
        //changing to Right
        fontAlign.setFontHorizontalAlignment(child, "Right");
        yield return screenshotAndCompare(tcName + "Right.png");
        //changing to Center
        fontAlign.setFontHorizontalAlignment(child, "Center");
        yield return screenshotAndCompare(tcName + "Center.png");
    }

    void setupScene()
    {
        MonoBehaviour.Instantiate(Resources.Load<GameObject>("Canvas"));
    }
 }