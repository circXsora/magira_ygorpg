using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;


public static class FileUtility
{

    /// <summary>
    /// 可以在文件被其他应用程序占用的情况下读取文件所有行
    /// </summary>
    /// <param name="fileName"></param>
    /// <returns></returns>
    public static List<string> ReadAllLinesShared(string fileName)
    {
        using FileStream fileStream = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
        using StreamReader streamReader = new StreamReader(fileStream, System.Text.Encoding.Default);
        List<string> lines = new List<string>();
        while (true)
        {
            string line = streamReader.ReadLine();
            if (line == null)
                break;
            lines.Add(line);
        }
        return lines;
    }
}

