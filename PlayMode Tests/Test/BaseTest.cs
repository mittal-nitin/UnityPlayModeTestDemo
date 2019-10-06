using NUnit.Framework;
using System;
using System.Collections;
using System.IO;
using System.Text;
using UnityEngine;
using XnaFan.ImageComparison;

public class BaseTest
{
    public int waitingTime = 1;
    protected bool isCreateBaseImages = bool.Parse(readFile());
    protected String baseImageDirectory= Environment.CurrentDirectory + "\\Assets\\Resources\\Base\\Images\\";
    protected String refactorImageDirectory = Environment.CurrentDirectory + "\\Assets\\Resources\\Test\\Images\\";

    public IEnumerator screenshotAndCompare(String fileName)
    {
        string baseImageCompletePath = baseImageDirectory + fileName;
        string newImageCompletePath = refactorImageDirectory + fileName;
        if (isCreateBaseImages)
        {   //Creating base image 
            
            yield return new WaitForSeconds(waitingTime);
            ScreenCapture.CaptureScreenshot(baseImageCompletePath);
            yield return new WaitForSeconds(waitingTime);
            Assert.IsTrue(File.Exists(baseImageCompletePath), "Base Image should exist");
        }
        else
        {

            yield return new WaitForSeconds(waitingTime);
            ScreenCapture.CaptureScreenshot(newImageCompletePath);
            yield return new WaitForSeconds(waitingTime);
            float difference = ImageTool.GetPercentageDifference(baseImageCompletePath, newImageCompletePath) * 100;
            // We don't require image if it is a mtach, so delete it
            if (difference == 0)
            {
                if (File.Exists(newImageCompletePath))
                    File.Delete(newImageCompletePath);
            }

            Assert.IsTrue(difference == 0, "Difference of " + difference + " % found in " + baseImageCompletePath + " vs " + newImageCompletePath);

        }
       
    }
    public IEnumerator WaitAndScreenshots(String fileName)
    {
        yield return new WaitForSeconds(waitingTime);
    }
    public static string readFile()
    {
        string text=null;
        FileStream fileStream = new FileStream(Environment.CurrentDirectory+@"\\Assets\\Resources\\Mode.txt", FileMode.Open, FileAccess.Read);
        using (var streamReader = new StreamReader(fileStream, Encoding.UTF8))
        {
            if(streamReader!=null)
                {
                    text = streamReader.ReadToEnd();
                }
       
        }
        text=text.Substring(text.LastIndexOf('=') + 1);
        return text;
    }
}