using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Xml;
using System.Text;

public static class XMLSerialization
{
    public static bool LoadXMLData()
    {        
        if (File.Exists(Application.persistentDataPath + "/GameData.xml"))
        {
            XmlDocument xmldoc = new XmlDocument();
            xmldoc.Load(Application.persistentDataPath + "/GameData.xml");
            XmlElement doc = xmldoc["Save"];
            if (doc == null)
            {
                Debug.LogWarning("Save node not found");
                return false;
            }
            XmlElement node;

            ////Settings////  
            node = doc["Settings"];
            if (node != null)
            {
                //if (node["Music"] != null) Globals.musicOn = bool.Parse(node["Music"].GetAttribute("value"));
            }
            else Debug.LogWarning("Settings node not found");

            //////////////

            ////Player////

            node = doc["Player"];
            if (node == null) Debug.LogWarning("Player node not found");          
            else
            {
                if (node["Stars"] != null) GLOBALS.player.stars = int.Parse(node["Stars"].GetAttribute("value"));
                if (node["Calculator"] != null) GLOBALS.player.calculatorPower = int.Parse(node["Calculator"].GetAttribute("value"));
                if (node["Crono"] != null) GLOBALS.player.cronoPower = int.Parse(node["Crono"].GetAttribute("value"));
                if (node["Lifes"] != null) GLOBALS.player.lifes = int.Parse(node["Lifes"].GetAttribute("value"));
                if (node["LifeUpgrades"] != null) GLOBALS.player.lifeUpgrades = int.Parse(node["LifeUpgrades"].GetAttribute("value"));
                if (node["ActiveCD"] != null) GLOBALS.player.activeCd = int.Parse(node["ActiveCD"].GetAttribute("value"));
                if (node["LastDate"] != null)
                {
                    string[] date = node["LastDate"].GetAttribute("value").Split("/");
                    GLOBALS.player.SetLastTime(int.Parse(date[0]), int.Parse(date[1]), int.Parse(date[2]), int.Parse(date[3]), int.Parse(date[5]), int.Parse(date[5]));
                }
            }

            //////////////
            
            ////Adventure////

            node = doc["Adventure"];
            if (node == null) Debug.LogWarning("Adventure node not found");
            else
            {
                XmlElement levelNode;
                foreach (KeyValuePair<int, NodeInfo> adNode in GLOBALS.infoNodes)
                {
                    levelNode = node[adNode.Key.ToString()];
                    if (levelNode != null)
                    {
                        GLOBALS.infoNodes[adNode.Key].UpdateNode(
                            (MissionState)int.Parse(node["State"].GetAttribute("value")),
                            bool.Parse(node["Star1"].GetAttribute("value")),
                            bool.Parse(node["Star2"].GetAttribute("value")),
                            bool.Parse(node["Star3"].GetAttribute("value")));
                        levelNode = null;
                    }                   
                }                
            }

            //////////////

            Debug.Log("XML data loaded");
            doc = null;
            xmldoc = null;
            return true;
        }
        else
        {
            Debug.LogError("SAVE FILE NOT FOUND!");
            SaveXMLData();
            return false;
        }      
    }

    public static void SaveXMLData()
    {
        XmlDocument xmlDocument = new XmlDocument();

        XmlDeclaration xmldecl = xmlDocument.CreateXmlDeclaration("1.0", null, null);
        xmldecl.Encoding = "UTF-8";
        xmldecl.Standalone = "yes";

        XmlElement doc = xmlDocument.DocumentElement;
        xmlDocument.InsertBefore(xmldecl, doc);

        XmlElement root = xmlDocument.CreateElement("Save");
        root.SetAttribute("FileName", "SaveFile");

        ////Settings///
        XmlElement settings = xmlDocument.CreateElement("Settings");

        /*XmlElement music = xmlDocument.CreateElement("Music");
        music.SetAttribute("value", GLOBALS.musicOn.ToString());
        settings.AppendChild(music);*/

        root.AppendChild(settings);       
        //////////////
      
        ////Player////

        XmlElement player = xmlDocument.CreateElement("Player");

        XmlElement elem = xmlDocument.CreateElement("Stars");
        elem.SetAttribute("value", GLOBALS.player.stars.ToString());
        player.AppendChild(elem);

        elem = xmlDocument.CreateElement("Calculator");
        elem.SetAttribute("value", GLOBALS.player.calculatorPower.ToString());
        player.AppendChild(elem);

        elem = xmlDocument.CreateElement("Crono");
        elem.SetAttribute("value", GLOBALS.player.cronoPower.ToString());
        player.AppendChild(elem);

        elem = xmlDocument.CreateElement("Lifes");
        elem.SetAttribute("value", GLOBALS.player.lifes.ToString());
        player.AppendChild(elem);

        elem = xmlDocument.CreateElement("LifeUpgrades");
        elem.SetAttribute("value", GLOBALS.player.lifeUpgrades.ToString());
        player.AppendChild(elem);

        elem = xmlDocument.CreateElement("ActiveCD");
        elem.SetAttribute("value", GLOBALS.player.activeCd.ToString());
        player.AppendChild(elem);

        elem = xmlDocument.CreateElement("LastDate");
        DateTime time = GLOBALS.player.GetLastTime();
        string date = time.Day+"/"+time.Month+"/"+time.Year+"/"+time.Hour+"/"+time.Minute+"/"+time.Second;
        elem.SetAttribute("value", date);
        player.AppendChild(elem);

        root.AppendChild(player);
        //////////////
        
        ////Adventure////

        XmlElement adventure = xmlDocument.CreateElement("Adventure");
        XmlElement adNode, aux;

        foreach (KeyValuePair<int, NodeInfo> node in GLOBALS.infoNodes)
        {
            adNode = xmlDocument.CreateElement(node.Value.level.ToString());

            aux = xmlDocument.CreateElement("State");
            aux.SetAttribute("value", ((int)node.Value.state).ToString());
            adNode.AppendChild(aux);

            aux = xmlDocument.CreateElement("Star1");
            aux.SetAttribute("value", node.Value.star1.ToString());
            adNode.AppendChild(aux);

            aux = xmlDocument.CreateElement("Star2");
            aux.SetAttribute("value", node.Value.star2.ToString());
            adNode.AppendChild(aux);

            aux = xmlDocument.CreateElement("Star3");
            aux.SetAttribute("value", node.Value.star3.ToString());
            adNode.AppendChild(aux);

            adventure.AppendChild(adNode);
        }

        root.AppendChild(adventure);
        //////////////

        xmlDocument.AppendChild(root);//Add the root and its children elements to the XML Document

        xmlDocument.Save(Application.persistentDataPath + "/GameData.xml");
        if (File.Exists(Application.persistentDataPath + "/GameData.xml")) Debug.Log("XML FILE SAVED");
        else Debug.Log("Error saving XML file");

        doc = null;
        xmldecl = null;
    }
}