using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Database : MonoBehaviour
{
    public GameObject pocionVidaObject;

    public GameObject pocionXPObject;

    public void SavePlayerData(Player jugador)
    {
        PlayerPrefs.SetString("playerName", jugador.GetName());
        PlayerPrefs.SetInt("playerLevel", jugador.GetLevel());
        PlayerPrefs.SetInt("playerAttack", jugador.GetAttack());
        PlayerPrefs.SetInt("playerHP", jugador.GetHP());
        PlayerPrefs.SetInt("playerMaxHP", jugador.GetMaxHP());
        PlayerPrefs.SetInt("playerXP", jugador.GetXP());
        PlayerPrefs.SetInt("playerMaxXP", jugador.GetMaxXP());

        //Guardar lista objetos
    }

    public string LoadPlayerName()
    {
        return PlayerPrefs.GetString("playerName", "none");
    }

    public int LoadPlayerLevel()
    {
        return PlayerPrefs.GetInt("playerLevel", 1);
    }

    public int LoadPlayerAttack()
    {
        return PlayerPrefs.GetInt("playerAttack", 1);
    }

    public int LoadPlayerHP()
    {
        return PlayerPrefs.GetInt("playerHP", 20);
    }

    public int LoadPlayerMaxHP()
    {
        return PlayerPrefs.GetInt("playerMaxHP", 20);
    }

    public int LoadPlayerXP()
    {
        return PlayerPrefs.GetInt("playerXP", 0);
    }

    public int LoadPlayerMaxXP()
    {
        return PlayerPrefs.GetInt("playerMaxXP", 100);
    }

    public void SavePlayerPosition(GameObject jugadorObject)
    {
        Vector3 playerPosition = jugadorObject.transform.position;
        PlayerPrefs.SetFloat("playerX", playerPosition.x);
        PlayerPrefs.SetFloat("playerY", playerPosition.y);
        PlayerPrefs.SetFloat("playerZ", playerPosition.z);
    }

    public Vector3 LoadPlayerPosition()
    {
        return new Vector3(PlayerPrefs.GetFloat("playerX", 1f), PlayerPrefs.GetFloat("playerY", 4f), PlayerPrefs.GetFloat("playerZ", 1f) );
    }

    public void SavePlayerRotation(GameObject jugadorObject)
    {
        Quaternion playerRotation = jugadorObject.transform.rotation;
        PlayerPrefs.SetFloat("playerRX", playerRotation.x);
        PlayerPrefs.SetFloat("playerRY", playerRotation.y);
        PlayerPrefs.SetFloat("playerRZ", playerRotation.z);
        PlayerPrefs.SetFloat("playerRW", playerRotation.w);
    }

    public Quaternion LoadPlayerRotation()
    {
        return new Quaternion(PlayerPrefs.GetFloat("playerRX", 1f), PlayerPrefs.GetFloat("playerRY", 1f), PlayerPrefs.GetFloat("playerRZ", 1f), PlayerPrefs.GetFloat("playerRW", 1f));
    }

    public void SaveEnemyName(Enemy enemigo)
    {
        PlayerPrefs.SetString("enemyName", enemigo.GetName());
    }

    public string LoadEnemyName()
    {
        return PlayerPrefs.GetString("enemyName", "none");
    }

    public void SaveIsWin(bool iswin)
    {
        if (iswin)
        {
            PlayerPrefs.SetString("iswin", "true");
        }
        else 
        {
            PlayerPrefs.SetString("iswin", "false");
        }
    }

    public bool LoadIsWin()
    {
        if (PlayerPrefs.GetString("iswin", "true").Equals("true"))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void SaveFirstTime(bool ft)
    {
        if (ft)
        {
            PlayerPrefs.SetString("firstTime", "true");
        }
        else
        {
            PlayerPrefs.SetString("firstTime", "false");
        }
    }

    public bool LoadFirstTime()
    {
        if (PlayerPrefs.GetString("firstTime", "true").Equals("true"))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void SaveStopCircles(string value)
    {
        PlayerPrefs.SetString("stopCircles", value);
    }

    public string LoadStopCircles()
    { 
        return PlayerPrefs.GetString("stopCircles", "true");
    }

    public void SaveLastEnemy(string value)
    {
        PlayerPrefs.SetString("lastEnemy", value);
    }

    public string LoadLastEnemy()
    {
        return PlayerPrefs.GetString("lastEnemy", "true");
    }


    public void SaveListaObjetos(List<PickUp> lista)
    {
        for (int i = 0; i < lista.Count; i++)
        {
            PlayerPrefs.SetString("item" + i, lista[i].name);
        }
        PlayerPrefs.SetInt("listaCounter", lista.Count);
    }

    public List<PickUp> LoadListaObjetos()
    {
        List<PickUp> nuevaLista = new List<PickUp>();    
        PickUp nuevoItem;
        int counter = PlayerPrefs.GetInt("listaCounter", 0);
        for (int i = 0; i < counter; i++)
        {
            nuevaLista.Add(LoadItem(PlayerPrefs.GetString("item" + i, "none")));
        }
        return nuevaLista;
    }

    PickUp LoadItem(string nombre)
    {
        if (nombre.Equals("PocionVida"))
        {
            return pocionVidaObject.GetComponent<PickUp>();
        }
        else if (nombre.Equals("PocionXP"))
        {
            return pocionXPObject.GetComponent<PickUp>();
        }
        else
        {
            return null;
        }
    }
    public void ResetListaObjetos()
    {
        PlayerPrefs.SetString("item1", "none");
        PlayerPrefs.SetString("item2", "none");
        PlayerPrefs.SetString("item3", "none");
        PlayerPrefs.SetString("item4", "none");
        PlayerPrefs.SetString("item5", "none");
        PlayerPrefs.SetString("item6", "none");
        PlayerPrefs.SetString("item7", "none");
        PlayerPrefs.SetString("item8", "none");
        PlayerPrefs.SetString("item9", "none");
        PlayerPrefs.SetString("item10", "none");
        PlayerPrefs.SetString("item11", "none");
        PlayerPrefs.SetString("item12", "none");
        PlayerPrefs.SetString("item13", "none");
        PlayerPrefs.SetString("item14", "none");
        PlayerPrefs.SetString("item15", "none");
        PlayerPrefs.SetInt("listaCounter", 0);
    }

    public void SaveSiguienteConversacion(int siguiente)
    {
        PlayerPrefs.SetInt("siguienteConversacion", siguiente);
    }

    public int LoadSiguienteConversacion()
    {
       return PlayerPrefs.GetInt("siguienteConversacion", 0);
    }

    public void SaveMisionCompletada(bool completada)
    {
        if (completada)
        {
            PlayerPrefs.SetString("misionCompletada", "true");
        }
        else 
        {
            PlayerPrefs.SetString("misionCompletada", "false");
        }
        
    }

    public bool LoadMisionCompletada()
    {
        if (PlayerPrefs.GetString("misionCompletada", "true").Equals("true"))
        {
            return true;
        }
        else 
        {
            return false;
        }
    }

    public void SaveObjetosRecogidos(int num)
    {
        PlayerPrefs.SetInt("objetosRecogidos", num);
    }

    public int LoadObjetosRecogidos()
    {
        return PlayerPrefs.GetInt("objetosRecogidos", 0);
    }

    public void SaveDesdeCombate(bool b)
    {
        if (b)
        {
            PlayerPrefs.SetString("desdeCombate", "true");
        }
        else
        {
            PlayerPrefs.SetString("desdeCombate", "false");
        }

    }

    public bool LoadDesdeCombate()
    {
        if (PlayerPrefs.GetString("desdeCombate", "true").Equals("true"))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void SaveMapLevel(string value)
    {
        PlayerPrefs.SetString("mapLevel", value);
    }

    public string LoadMapLevel()
    {
        return PlayerPrefs.GetString("mapLevel", "Malarcier");
    }

    //---------------------------------------------------------------------Pociones de vida
    public void SaveHP1(string value)
    {
        PlayerPrefs.SetString("hp1", value);
    }

    public string LoadHP1()
    {
        return PlayerPrefs.GetString("hp1", "true");
    }

    public void SaveHP2(string value)
    {
        PlayerPrefs.SetString("hp2", value);
    }

    public string LoadHP2()
    {
        return PlayerPrefs.GetString("hp2", "true");
    }

    public void SaveHP3(string value)
    {
        PlayerPrefs.SetString("hp3", value);
    }

    public string LoadHP3()
    {
        return PlayerPrefs.GetString("hp3", "true");
    }



    //---------------------------------------------------------------------Pociones de experiencia
    public void SaveXP1(string value)
    {
        PlayerPrefs.SetString("xp1", value);
    }

    public string LoadXP1()
    {
        return PlayerPrefs.GetString("xp1", "true");
    }

    public void SaveXP2(string value)
    {
        PlayerPrefs.SetString("xp2", value);
    }

    public string LoadXP2()
    {
        return PlayerPrefs.GetString("xp2", "true");
    }

    //---------------------------------------------------------------------Champimudos
    public void SaveCh1(string value)
    {
        PlayerPrefs.SetString("ch1", value);
    }

    public string LoadCh1()
    {
        return PlayerPrefs.GetString("ch1", "true");
    }

    public void SaveCh2(string value)
    {
        PlayerPrefs.SetString("ch2", value);
    }

    public string LoadCh2()
    {
        return PlayerPrefs.GetString("ch2", "true");
    }

    public void SaveCh3(string value)
    {
        PlayerPrefs.SetString("ch3", value);
    }

    public string LoadCh3()
    {
        return PlayerPrefs.GetString("ch3", "true");
    }

    public void SaveCh4(string value)
    {
        PlayerPrefs.SetString("ch4", value);
    }

    public string LoadCh4()
    {
        return PlayerPrefs.GetString("ch4", "true");
    }

    public void SaveCh5(string value)
    {
        PlayerPrefs.SetString("ch5", value);
    }

    public string LoadCh5()
    {
        return PlayerPrefs.GetString("ch5", "true");
    }


    //---------------------------------------------------------------------Champimudos furiosos
    public void SaveChF1(string value)
    {
        PlayerPrefs.SetString("chf1", value);
    }

    public string LoadChF1()
    {
        return PlayerPrefs.GetString("chf1", "true");
    }

    public void SaveChF2(string value)
    {
        PlayerPrefs.SetString("chf2", value);
    }

    public string LoadChF2()
    {
        return PlayerPrefs.GetString("chf2", "true");
    }

    public void SaveChF3(string value)
    {
        PlayerPrefs.SetString("chf3", value);
    }

    public string LoadChF3()
    {
        return PlayerPrefs.GetString("chf3", "true");
    }

    public void SaveChF4(string value)
    {
        PlayerPrefs.SetString("chf4", value);
    }

    public string LoadChF4()
    {
        return PlayerPrefs.GetString("chf4", "true");
    }

    public void SaveChF5(string value)
    {
        PlayerPrefs.SetString("chf5", value);
    }

    public string LoadChF5()
    {
        return PlayerPrefs.GetString("chf5", "true");
    }


    //---------------------------------------------------------------------Florhadas
    public void SaveFh1(string value)
    {
        PlayerPrefs.SetString("fh1", value);
    }

    public string LoadFh1()
    {
        return PlayerPrefs.GetString("fh1", "true");
    }

    public void SaveFh2(string value)
    {
        PlayerPrefs.SetString("fh2", value);
    }

    public string LoadFh2()
    {
        return PlayerPrefs.GetString("fh2", "true");
    }

    public void SaveFh3(string value)
    {
        PlayerPrefs.SetString("fh3", value);
    }

    public string LoadFh3()
    {
        return PlayerPrefs.GetString("fh3", "true");
    }

    public void SaveFh4(string value)
    {
        PlayerPrefs.SetString("fh4", value);
    }

    public string LoadFh4()
    {
        return PlayerPrefs.GetString("fh4", "true");
    }

    public void SaveFh5(string value)
    {
        PlayerPrefs.SetString("fh5", value);
    }

    public string LoadFh5()
    {
        return PlayerPrefs.GetString("fh5", "true");
    }


    //---------------------------------------------------------------------Trifauces
    public void SaveTr1(string value)
    {
        PlayerPrefs.SetString("tr1", value);
    }

    public string LoadTr1()
    {
        return PlayerPrefs.GetString("tr1", "true");
    }

    //---------------------------------------------------------------------Scarab
    public void SaveSc1(string value)
    {
        PlayerPrefs.SetString("sc1", value);
    }

    public string LoadSc1()
    {
        return PlayerPrefs.GetString("sc1", "true");
    }



    //-------------------------------------------------------------------------Desierto Espejismo-------------------------------------------------------------------------------------------------------

    public void SaveFirstTime2(bool ft)
    {
        if (ft)
        {
            PlayerPrefs.SetString("firstTime2", "true");
        }
        else
        {
            PlayerPrefs.SetString("firstTime2", "false");
        }
    }

    public bool LoadFirstTime2()
    {
        if (PlayerPrefs.GetString("firstTime2", "true").Equals("true"))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void SaveDesiertoAMalarcier(bool b)
    {
        if (b)
        {
            PlayerPrefs.SetString("desiertoAMalarcier", "true");
        }
        else
        {
            PlayerPrefs.SetString("desiertoAMalarcier", "false");
        }
    }

    public bool LoadDesiertoAMalarcier()
    {
        if (PlayerPrefs.GetString("desiertoAMalarcier", "true").Equals("true"))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void SaveMalarcierADesierto(bool b)
    {
        if (b)
        {
            PlayerPrefs.SetString("malarcierADesierto", "true");
        }
        else
        {
            PlayerPrefs.SetString("malarcierADesierto", "false");
        }
    }

    public bool LoadMalarcierADesierto()
    {
        if (PlayerPrefs.GetString("malarcierADesierto", "true").Equals("true"))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    //---------------------------------------------------------------------Pociones de vida
    public void SaveHP21(string value)
    {
        PlayerPrefs.SetString("hp21", value);
    }

    public string LoadHP21()
    {
        return PlayerPrefs.GetString("hp21", "true");
    }

    public void SaveHP22(string value)
    {
        PlayerPrefs.SetString("hp22", value);
    }

    public string LoadHP22()
    {
        return PlayerPrefs.GetString("hp22", "true");
    }

    public void SaveHP23(string value)
    {
        PlayerPrefs.SetString("hp23", value);
    }

    public string LoadHP23()
    {
        return PlayerPrefs.GetString("hp23", "true");
    }


    //---------------------------------------------------------------------Pociones de experiencia
    public void SaveXP21(string value)
    {
        PlayerPrefs.SetString("xp21", value);
    }

    public string LoadXP21()
    {
        return PlayerPrefs.GetString("xp21", "true");
    }

    public void SaveXP22(string value)
    {
        PlayerPrefs.SetString("xp22", value);
    }

    public string LoadXP22()
    {
        return PlayerPrefs.GetString("xp22", "true");
    }

    //---------------------------------------------------------------------Alaquijones
    public void SaveAl1(string value)
    {
        PlayerPrefs.SetString("al1", value);
    }

    public string LoadAl1()
    {
        return PlayerPrefs.GetString("al1", "true");
    }

    public void SaveAl2(string value)
    {
        PlayerPrefs.SetString("al2", value);
    }

    public string LoadAl2()
    {
        return PlayerPrefs.GetString("al2", "true");
    }

    public void SaveAl3(string value)
    {
        PlayerPrefs.SetString("al3", value);
    }

    public string LoadAl3()
    {
        return PlayerPrefs.GetString("al3", "true");
    }

    public void SaveAl4(string value)
    {
        PlayerPrefs.SetString("al4", value);
    }

    public string LoadAl4()
    {
        return PlayerPrefs.GetString("al4", "true");
    }

    public void SaveAl5(string value)
    {
        PlayerPrefs.SetString("al5", value);
    }

    public string LoadAl5()
    {
        return PlayerPrefs.GetString("al5", "true");
    }

    public void SaveAl6(string value)
    {
        PlayerPrefs.SetString("al6", value);
    }

    public string LoadAl6()
    {
        return PlayerPrefs.GetString("al6", "true");
    }

    public void SaveAl7(string value)
    {
        PlayerPrefs.SetString("al7", value);
    }

    public string LoadAl7()
    {
        return PlayerPrefs.GetString("al7", "true");
    }

    public void SaveAl8(string value)
    {
        PlayerPrefs.SetString("al8", value);
    }

    public string LoadAl8()
    {
        return PlayerPrefs.GetString("al8", "true");
    }

    //---------------------------------------------------------------------Absorbedoras

    public void SaveAb1(string value)
    {
        PlayerPrefs.SetString("ab1", value);
    }

    public string LoadAb1()
    {
        return PlayerPrefs.GetString("ab1", "true");
    }

    public void SaveAb2(string value)
    {
        PlayerPrefs.SetString("ab2", value);
    }

    public string LoadAb2()
    {
        return PlayerPrefs.GetString("ab2", "true");
    }

    public void SaveAb3(string value)
    {
        PlayerPrefs.SetString("ab3", value);
    }

    public string LoadAb3()
    {
        return PlayerPrefs.GetString("ab3", "true");
    }

    public void SaveAb4(string value)
    {
        PlayerPrefs.SetString("ab4", value);
    }

    public string LoadAb4()
    {
        return PlayerPrefs.GetString("ab4", "true");
    }

    //---------------------------------------------------------------------CabezaCarnivora
    public void SaveCa1(string value)
    {
        PlayerPrefs.SetString("ca1", value);
    }

    public string LoadCa1()
    {
        return PlayerPrefs.GetString("ca1", "true");
    }
}