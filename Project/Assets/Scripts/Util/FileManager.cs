using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

/// <summary>
/// 文件读取
/// </summary>
public class FileManager
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="fileName"></param>
    /// <param name="file"></param>
    public void DataSave(string fileName,String file)
    {
        using (StreamWriter output = new StreamWriter(Application.dataPath + fileName))
        {
            output.Write(file);
        }
    }
    
    public String DataLoad(string fileName)
    {
        //文件判断，无文件返回空
        if (!File.Exists(Application.dataPath + fileName))
        {
            return null;
        }

        using (StreamReader input = new StreamReader(Application.dataPath + fileName))
        {
            return input.ReadToEnd();
        }
    }
}
