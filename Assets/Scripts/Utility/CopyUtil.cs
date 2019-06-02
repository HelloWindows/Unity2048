using System;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;

public class CopyUtil  {

    public static T DeepCopyByReflect<T>(T obj)
    {
        if (obj is string || obj.GetType().IsValueType) return obj;
        // end if
        object retval = Activator.CreateInstance(obj.GetType());
        FieldInfo[] fields = obj.GetType().GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static);
        foreach (FieldInfo field in fields)
        {
            try { field.SetValue(retval, DeepCopyByReflect(field.GetValue(obj))); }
            catch { } // end try
        } // end foreach
        return (T)retval;
    } // end DeepCopyByReflect

    public static T DeepCopyByXml<T>(T obj)
    {
        object retval;
        using (MemoryStream ms = new MemoryStream())
        {
            XmlSerializer xml = new XmlSerializer(typeof(T));
            xml.Serialize(ms, obj);
            ms.Seek(0, SeekOrigin.Begin);
            retval = xml.Deserialize(ms);
            ms.Close();
        } // end using
        return (T)retval;
    } // end DeepCopyByXml

    public static T DeepCopyByBin<T>(T obj)
    {
        object retval = null;
        using (MemoryStream ms = new MemoryStream())
        {
            try
            {
                BinaryFormatter bf = new BinaryFormatter();
                bf.Serialize(ms, obj);
                ms.Seek(0, SeekOrigin.Begin);
                retval = bf.Deserialize(ms);
                ms.Close();
            } catch { } // end try
        } // end using
        return (T)retval;
    } // end DeepCopyByBin

    //需要silverlight支持
    public static T DeepCopy<T>(T obj)
    {
        object retval = null;
        using (MemoryStream ms = new MemoryStream())
        {
            try
            {
                DataContractSerializer ser = new DataContractSerializer(typeof(T));
                ser.WriteObject(ms, obj);
                ms.Seek(0, SeekOrigin.Begin);
                retval = ser.ReadObject(ms);

            } catch { } // end try
            ms.Close();
        } // end using
        return (T)retval;
    } // end DeepCopy
} // end class CopyUtil
