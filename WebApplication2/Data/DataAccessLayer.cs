using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using WebApplication2.Shared;

namespace WebApplication2.API.Data
{
    public class DataAccessLayer
    {
        private string connectionString = "Server=localhost;User Id=root;Password=default22;Database=encuesta;SslMode=none";
        public Encuesta getEncuesta()
        {
            Encuesta encuesta = new Encuesta();
            using (MySqlConnection con = new MySqlConnection(connectionString))
            {
                MySqlCommand cmd = new MySqlCommand("select id, nombre from encuesta where activa=1", con);
                cmd.CommandType = CommandType.Text;

                con.Open();
                MySqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    encuesta.id = Convert.ToInt32(rdr[0]);
                    encuesta.nombre = rdr[1].ToString();
                    break; //toma la primera encuesta activa
                }
                rdr.Close(); con.Close();
            }
            return encuesta;
        }
        public List<Pregunta> getPreguntas(int idEncuesta, string nombre)
        {
            List<Pregunta> preguntas = new List<Pregunta>();
            using (MySqlConnection con = new MySqlConnection(connectionString))
            {
                MySqlCommand cmd = new MySqlCommand("select id,pregunta,esOpcional,tipo from pregunta where idEncuesta=" + idEncuesta, con);
                cmd.CommandType = CommandType.Text;

                con.Open();
                MySqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    Pregunta pregunta = new Pregunta();
                    pregunta.id = Convert.ToInt32(rdr[0]);
                    pregunta.pregunta = rdr[1].ToString();
                    pregunta.esOpcional = Convert.ToBoolean(rdr[2]);
                    pregunta.tipo = rdr[3].ToString();

                    preguntas.Add(pregunta);
                }
                rdr.Close(); con.Close();
            }
            return preguntas;
        }

        public List<Respuesta> getPreguntas(int idEncuesta)
        {
            List<Respuesta> respuestas = new List<Respuesta>();
            using (MySqlConnection con = new MySqlConnection(connectionString))
            {
                MySqlCommand cmd = new MySqlCommand("select id from pregunta where idEncuesta=" + idEncuesta, con);
                cmd.CommandType = CommandType.Text;

                con.Open();
                MySqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    Respuesta respuesta = new Respuesta();
                    respuesta.id = Convert.ToInt32(rdr[0]);

                    respuestas.Add(respuesta);
                }
                rdr.Close(); con.Close();
            }
            return respuestas;
        }

        public void setRespuestas(int idPregunta, string respuesta)
        {
            using (MySqlConnection con = new MySqlConnection(connectionString))
            {
                MySqlCommand cmd = new MySqlCommand("sp_setRespuesta", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new MySqlParameter("@idPregunta", idPregunta));
                cmd.Parameters.Add(new MySqlParameter("@respuesta", respuesta));

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }
    }
}
