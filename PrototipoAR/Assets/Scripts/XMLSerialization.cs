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
            XmlElement doc;
            try
            {
                Debug.LogWarning("Loading xml document...");
                XmlDocument xmldoc = new XmlDocument();
                xmldoc.Load(Application.persistentDataPath + "/GameData.xml");
                doc = xmldoc["Save"];
                if (doc == null)
                {
                    Debug.LogWarning("Save node not found");
                    return false;
                }
            }
            catch(Exception e)
            {
                Debug.LogError(e);
                return false;
            }


            ////Settings////  
            XmlElement node;
            Debug.LogWarning("Loading settings...");
            node = doc["Settings"];
            if (node != null)
            {
                if (node["Sound"] != null) GLOBALS.soundOn = bool.Parse(node["Sound"].GetAttribute("value"));
                if (node["Fill"] != null) GLOBALS.selectionFill = bool.Parse(node["Fill"].GetAttribute("value"));
            }
            else Debug.LogWarning("Settings node not found");
            //////////////

            ////Player////
            Debug.LogWarning("Loading player...");
            node = doc["Player"];
            if (node == null) Debug.LogWarning("Player node not found");          
            else
            {
                if (node["Stars"] != null) GLOBALS.player.stars = int.Parse(node["Stars"].GetAttribute("value"));
                if (node["Calculator"] != null) GLOBALS.player.calculatorPower = int.Parse(node["Calculator"].GetAttribute("value"));
                if (node["Crono"] != null) GLOBALS.player.cronoPower = int.Parse(node["Crono"].GetAttribute("value"));
                if (node["Lifes"] != null) GLOBALS.player.lifes = int.Parse(node["Lifes"].GetAttribute("value"));
                if (node["LifeUpgrades"] != null) GLOBALS.player.lifeUpgrades = int.Parse(node["LifeUpgrades"].GetAttribute("value"));
                if (node["ShopLifes"] != null) GLOBALS.player.shopLifes = int.Parse(node["ShopLifes"].GetAttribute("value"));
                if (node["ActiveCD"] != null) GLOBALS.player.activeCd = float.Parse(node["ActiveCD"].GetAttribute("value"));
                if (node["Challenge"] != null) GLOBALS.player.challengeDone = bool.Parse(node["Challenge"].GetAttribute("value"));
                if (node["LastDate"] != null)
                {
                    string[] date = node["LastDate"].GetAttribute("value").Split("/");
                    GLOBALS.player.SetLastTime(int.Parse(date[0]), int.Parse(date[1]), int.Parse(date[2]), int.Parse(date[3]), int.Parse(date[5]), int.Parse(date[5]));
                }
            }
            //////////////

            ////Adventure////
            Debug.LogWarning("Loading adventure...");
            node = doc["Adventure"];
            if (node == null) Debug.LogWarning("Adventure node not found");
            else
            {
                XmlNode levelNode;
                foreach (KeyValuePair<int, NodeInfo> adNode in GLOBALS.infoNodes)
                {
                    levelNode = node.SelectSingleNode("/Node[@value='"+adNode.Key.ToString()+"']");
                    if (levelNode != null)
                    {
                        GLOBALS.infoNodes[adNode.Key].UpdateNode(
                            (MissionState)int.Parse(node["State"].GetAttribute("value")),
                            bool.Parse(node["StarComplete"].GetAttribute("value")),
                            bool.Parse(node["StarTime"].GetAttribute("value")),
                            bool.Parse(node["StarError"].GetAttribute("value")));
                        levelNode = null;
                    }            
                }                
            }
            //////////////

            Debug.Log("XML data loaded");
            return true;
        }
        else
        {
            Debug.LogError("SAVE FILE NOT FOUND!");
            return false;
        }      
    }

    public static void SaveXMLData()
    {
        if(File.Exists(Application.persistentDataPath + "/GameData.xml")) File.Delete(Application.persistentDataPath + "/GameData.xml");

        XmlDocument xmlDocument = new XmlDocument();

        XmlDeclaration xmldecl = xmlDocument.CreateXmlDeclaration("1.0", null, null);
        xmldecl.Encoding = "UTF-8";
        xmldecl.Standalone = "yes";

        XmlElement doc = xmlDocument.DocumentElement;
        xmlDocument.InsertBefore(xmldecl, doc);

        XmlElement root = xmlDocument.CreateElement("Save");
        root.SetAttribute("FileName", "SaveFile");

        ////Settings///
        Debug.LogWarning("Saving settings...");
        XmlElement settings = xmlDocument.CreateElement("Settings");

        XmlElement Sound = xmlDocument.CreateElement("Sound");
        Sound.SetAttribute("value", GLOBALS.soundOn.ToString());
        settings.AppendChild(Sound);

        XmlElement fill = xmlDocument.CreateElement("Fill");
        fill.SetAttribute("value", GLOBALS.selectionFill.ToString());
        settings.AppendChild(fill);

        root.AppendChild(settings);
        //////////////

        ////Player////
        Debug.LogWarning("Saving player...");
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

        elem = xmlDocument.CreateElement("ShopLifes");
        elem.SetAttribute("value", GLOBALS.player.shopLifes.ToString());
        player.AppendChild(elem);

        elem = xmlDocument.CreateElement("ActiveCD");
        elem.SetAttribute("value", GLOBALS.player.activeCd.ToString());
        player.AppendChild(elem);

        elem = xmlDocument.CreateElement("Challenge");
        elem.SetAttribute("value", GLOBALS.player.challengeDone.ToString());
        player.AppendChild(elem);

        elem = xmlDocument.CreateElement("LastDate");
        DateTime time = GLOBALS.player.GetLastTime();
        string date = time.Day+"/"+time.Month+"/"+time.Year+"/"+time.Hour+"/"+time.Minute+"/"+time.Second;
        elem.SetAttribute("value", date);
        player.AppendChild(elem);

        root.AppendChild(player);
        //////////////

        ////Adventure////
        Debug.LogWarning("Saving adventure...");
        XmlElement adventure = xmlDocument.CreateElement("Adventure");
        XmlElement adNode, aux;

        foreach (KeyValuePair<int, NodeInfo> node in GLOBALS.infoNodes)
        {
            adNode = xmlDocument.CreateElement("Node");
            adNode.SetAttribute("value", node.Key.ToString());

            aux = xmlDocument.CreateElement("State");
            aux.SetAttribute("value", ((int)node.Value.state).ToString());
            adNode.AppendChild(aux);

            aux = xmlDocument.CreateElement("StarComplete");
            aux.SetAttribute("value", node.Value.starCompleted.ToString());
            adNode.AppendChild(aux);

            aux = xmlDocument.CreateElement("StarTime");
            aux.SetAttribute("value", node.Value.starTime.ToString());
            adNode.AppendChild(aux);

            aux = xmlDocument.CreateElement("StarError");
            aux.SetAttribute("value", node.Value.starError.ToString());
            adNode.AppendChild(aux);

            adventure.AppendChild(adNode);
        }

        root.AppendChild(adventure);
        //////////////

        xmlDocument.AppendChild(root);//Add the root and its children elements to the XML Document

        xmlDocument.Save(Application.persistentDataPath + "/GameData.xml");
        if (File.Exists(Application.persistentDataPath + "/GameData.xml")) Debug.Log("XML FILE SAVED");
        else Debug.Log("Error saving XML file");
    }
}