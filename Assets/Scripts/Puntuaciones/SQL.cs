//Código creado por Aarón Angulo

using UnityEngine;
using Mono.Data.Sqlite;
using System.Data;
using System.IO;

public class SQL : MonoBehaviour
{
    private IDbConnection dbconn;
    private IDbCommand dbcmd;
    private string conn;
    private string path;

    void Awake()
    {
    }
    /**
     *  Se crea el path completo para conexión y se revisa que la base de datos exista, si no existe
     *  se crea una. Si ya existe se crea la conexión y se abre.
     */
    public void Iniciar ()
    {
        path = Application.persistentDataPath + "/Binary.PdV";
        conn = "URI=file:" + path;

        if (!File.Exists(path))
            CrearBaseDeDatos();
        else
            dbconn = (IDbConnection)new SqliteConnection(conn);
    }
	
    /**
     * Crea la base de datos con sus pertinentes tablas y se setean las puntuaciones de todos los modos
     * de juego con las diferentes variantes en 0
     */
    public void CrearBaseDeDatos()
    {
        dbconn = (IDbConnection) new SqliteConnection(conn);
        dbconn.Open();
        dbcmd = dbconn.CreateCommand();
        dbcmd.CommandText = "CREATE TABLE reloj(variante varchar(15), valor int)";
        dbcmd.ExecuteScalar();
        dbcmd.CommandText = "CREATE TABLE suma(variante varchar(15), valor int)";
        dbcmd.ExecuteScalar();
        dbcmd.CommandText = "CREATE TABLE conversor(variante varchar(15), valor int)";
        dbcmd.ExecuteScalar();

        dbcmd.CommandText = "INSERT INTO  reloj VALUES ('normal', 0)";
        dbcmd.ExecuteScalar();
        dbcmd.CommandText = "INSERT INTO  reloj VALUES ('survival', 0)";
        dbcmd.ExecuteScalar();
        dbcmd.CommandText = "INSERT INTO  reloj VALUES ('contrareloj', 0)";
        dbcmd.ExecuteScalar();

        dbcmd.CommandText = "INSERT INTO  suma VALUES ('normal', 0)";
        dbcmd.ExecuteScalar();
        dbcmd.CommandText = "INSERT INTO  suma VALUES ('survival', 0)";
        dbcmd.ExecuteScalar();
        dbcmd.CommandText = "INSERT INTO  suma VALUES ('contrareloj', 0)";
        dbcmd.ExecuteScalar();

        dbcmd.CommandText = "INSERT INTO  conversor VALUES ('normal', 0)";
        dbcmd.ExecuteScalar();
        dbcmd.CommandText = "INSERT INTO  conversor VALUES ('survival', 0)";
        dbcmd.ExecuteScalar();
        dbcmd.CommandText = "INSERT INTO  conversor VALUES ('contrareloj', 0)";
        dbcmd.ExecuteScalar();

        dbcmd.Dispose();
        dbconn.Close();
    }

    /**
     * Borra y crea una base de datos
     */
     public void BorrarBdd()
    {
        File.Delete(path);
        conn = "URI=file:" + path;
        CrearBaseDeDatos();
    }

    /**
     * Devuelve la mayor puntuación realizada en el modo de juego y variante especificado
     * @param modo Es el modo de juego del que se quiere consultar la puntuación
     * @param variante Es la variante de juego de la que se quiere consultar la puntuación
     */
    public int MayorPuntuacion(string modo, string variante)
    {
        dbconn.Open();
        dbcmd = dbconn.CreateCommand();
        dbcmd.CommandText = "SELECT valor FROM " + modo + " WHERE variante='" + variante + "'";
        
        IDataReader reader = dbcmd.ExecuteReader();
        reader.Read();
        int valor = reader.GetInt32(0);
        reader.Close();
        dbcmd.Dispose();
        dbconn.Close();
        return valor;
    }

    /**
     * Actualiza el valor de la puntuación del modo de juego y variante proporcionado
     * @param modo Es el modo de juego del que se quiere actualizar la puntuación
     * @param variante Es la variante de juego que la cual se quiere actualizar la puntuación
     * @param valor Es la puntuación realizada que sustituirá a la anterior.
     */
    public void Actualizar(string modo, string variante, int valor)
    {
        dbconn.Open();
        dbcmd = dbconn.CreateCommand();
        dbcmd.CommandText = "UPDATE " + modo + " SET valor=" + valor + " WHERE variante='" + variante + "'";
        dbcmd.ExecuteNonQuery();
        dbcmd.Dispose();
        dbconn.Close();
    }
}
