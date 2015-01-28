using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;
using System.Xml;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Diagnostics;
using System.Resources;
using System.Data;
using System.Text;
namespace WebApplication1
{
    public class Language 
    {
        private static MyHashTable _Hash = null;

        public static MyHashTable Selected
        {
            get
            {
                if(_Hash != null)
                {
                    return _Hash;
                }


                _Hash = new MyHashTable();
                XmlReader reader = null;
                
                string str = System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase + @"Language.xml";

                FileInfo fi = new FileInfo(str);
                if (!fi.Exists)
                {
                    return new MyHashTable();
                }
                else
                {
                    reader = new XmlTextReader(str);
                }

                XmlDocument doc = new XmlDocument();
                doc.Load(reader);

                XmlNode root = doc.DocumentElement;
                XmlNodeList nodelist = root.SelectNodes("type");
                XmlNode xnode = null;
                for (int i = 0; i < nodelist.Count; i++)
                {
                    if (nodelist[i].Attributes[0].Value == DefaultLanguage)
                    {
                        xnode = nodelist[i];
                    }
                }

                if (xnode == null)
                {
                    throw new Exception("Language not found!");
                }

                nodelist = xnode.ChildNodes;

                foreach (XmlNode node in nodelist)
                {
                    try
                    {
                        if (node.NodeType == XmlNodeType.Element)
                        {
                            XmlNode node1 = node.SelectSingleNode("@Key");
                            XmlNode node2 = node.SelectSingleNode("@Text");
                            if (node1 != null)
                            {
                                _Hash.Add(node1.InnerText, node2.InnerText);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        throw new Exception(ex.Message);
                    }
                }
                reader.Close();

                return _Hash;
            }

        }

        public static string DefaultLanguage
        {
            get
            {
                string filePath = System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase + @"Language.xml";

                try
                {
                    XmlDocument doc = new XmlDocument();
                    doc.Load(filePath);

                    XmlNodeList list = doc.GetElementsByTagName("Default");
                    return list[0].InnerText;
                }
                catch (System.Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }

            set
            {
                string filePath = System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase + @"Language.xml";

                try
                {
                    XmlDocument doc = new XmlDocument();
                    doc.Load(filePath);

                    XmlNodeList nodeList = doc.GetElementsByTagName("Default");
                    nodeList[0].InnerText = value;
                    doc.Save(filePath);

                    _Hash = null;
                }
                catch (System.Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }
        public static void change_calender(string ch,string en)
        {
            string path = System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase + @"\Scripts\My97DatePicker\WdatePicker.js";
            
            string s11 = File.ReadAllText(path);    
            
            s11 = s11.Replace(ch, en);
            FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Write);
            
            StreamWriter write = new StreamWriter(fs, Encoding.Default);
            write.Write(s11);
            write.Close();
            fs.Close();
        }
    }

    public class MyHashTable
    {
        private Hashtable _hash = new Hashtable();

        public void Add(string key, string value)
        {
            _hash.Add(key, value);
        }

        public int Count
        {
            get
            {
                return _hash.Count;
            }
        }

        public string this[string key]
        {
            get
            {
                if (_hash[key] == null)
                {
                    return "Err";
                }

                return _hash[key].ToString();
            }

            set
            {
                if (_hash[key] == null)
                {
                    _hash.Add(key, value);
                }
                else
                {
                    _hash[key] = value;
                }
            }
        }
    }
}
