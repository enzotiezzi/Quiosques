using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using Tell_you_story.Models;
using System.Web;

namespace Tell_you_story
{
    public class DBClass
    {
        private SqlConnection connection;

        public DBClass()
        {
            this.connection = new SqlConnection("workstation id=contesuahistoriadb.mssql.somee.com;packet size=4096;user id=enzotiezzi_SQLLogin_2;pwd=x73r3noddc;data source=contesuahistoriadb.mssql.somee.com;persist security info=False;initial catalog=contesuahistoriadb");
        }

        public String TesteConnection()
        {
            try
            {
                connection.Open();
                connection.Close();
            }
            catch (Exception e)
            {
                return "Error";
            }
            return "OK";
        }

        #region Praias do litoral

        #region Registros

        public bool RegistraLitoral(Litoral l)
        {
            String sql_RegistraLitoral = "INSERT INTO TB_PLITORAL(NOME, IMAGEURL) VALUES(@NOME, @IMAGEURL);";
            try
            {
                connection.Open();
                using (SqlCommand _command = new SqlCommand(sql_RegistraLitoral, connection))
                {
                    _command.Parameters.AddWithValue("@NOME", l.Nome);
                    _command.Parameters.AddWithValue("@IMAGEURL", l.ImageURL);

                    _command.ExecuteNonQuery();
                }
            }
            catch (Exception)
            {
                return false;
            }
            finally
            {
                connection.Close();
            }
            return true;
        }

        public bool RegistraCidade(Cidade c)
        {
            String sql_RegistraCidade = "INSERT INTO TB_PCIDADE(ID, NOME, ESTADO, IMAGEURL) VALUES(@ID, @NOME, @ESTADO, @IMAGEURL);";
            try
            {
                connection.Open();

                using (SqlCommand _command = new SqlCommand(sql_RegistraCidade, connection))
                {
                    _command.Parameters.AddWithValue("@ID", c.IDLirotal);
                    _command.Parameters.AddWithValue("@NOME", c.Nome);
                    _command.Parameters.AddWithValue("@ESTADO", c.Estado);
                    _command.Parameters.AddWithValue("@IMAGEURL", c.ImageURL);

                    _command.ExecuteNonQuery();
                }
            }
            catch (Exception)
            {
                return false;
            }
            finally
            {
                connection.Close();
            }
            return true;
        }

        public bool RegistraPraia(Praia p)
        {
            String sql_RegistraPraia = "INSERT INTO TB_PPRAIA(IDCIDADE, NOME, IMAGEURL) VALUES(@IDCIDADE, @NOME, @IMAGEURL);";
            try
            {
                connection.Open();
                using (SqlCommand _command = new SqlCommand(sql_RegistraPraia, connection))
                {
                    _command.Parameters.AddWithValue("@IDCIDADE", p.IDCidade);
                    _command.Parameters.AddWithValue("@NOME", p.Nome);
                    _command.Parameters.AddWithValue("@IMAGEURL", p.ImageURL);

                    _command.ExecuteNonQuery();
                }
            }
            catch (Exception)
            {
                return false;
            }
            finally
            {
                connection.Close();
            }
            return true;
        }

        public bool RegistraQuiosque(Quiosque q)
        {
            String sql_RegistraQuiosque = "INSERT INTO TB_PQUIOSQUES(IDPRAIA, NOME, NUMEROQUIOSQUE, RUA, BAIRRO, LAT, LNG, IMAGEURL)" + 
            " VALUES(@IDPRAIA, @NOME, @NUMEROQUIOSQUE, @RUA, @BAIRRO, @LAT, @LNG, @IMAGEURL);";
            try
            {
                connection.Open();
                using (SqlCommand _command = new SqlCommand(sql_RegistraQuiosque, connection))
                {
                    _command.Parameters.AddWithValue("@IDPRAIA", q.IDPraia);
                    _command.Parameters.AddWithValue("@NOME", q.Nome);
                    _command.Parameters.AddWithValue("@NUMEROQUIOSQUE", q.NumeroQuiosque);
                    _command.Parameters.AddWithValue("@RUA", q.Rua);
                    _command.Parameters.AddWithValue("@BAIRRO", q.Bairro);
                    _command.Parameters.AddWithValue("@LAT",     q.Lat);
                    _command.Parameters.AddWithValue("@LNG", q.Lng);
                    _command.Parameters.AddWithValue("@IMAGEURL", q.ImageURL);
                }
            }
            catch(Exception)
            {
                return false;
            }
            finally
            {
                connection.Close();
            }
            return true;
        }

        #endregion

        #region Listagem

        public List<Litoral> ListarLitorais()
        {
            List<Litoral> litorais = new List<Litoral>();
            String sql_ListarLitorais = "SELECT TB_PLITORAL.ID, TB_PLITORAL.NOME, TB_PLITORAL.IMAGEURL, COUNT(TB_PCIDADES.IDCIDADE) FROM TB_PLITORAL, TB_PCIDADES GROUP BY TB_PLITORAL.ID, TB_PLITORAL.NOME, TB_PLITORAL.IMAGEURL;";
            try
            {
                connection.Open();
                using (SqlCommand _command = new SqlCommand(sql_ListarLitorais, connection))
                {
                    using (SqlDataReader _reader = _command.ExecuteReader())
                    {
                        while (_reader.Read())
                        {
                            Litoral l = new Litoral();

                            l.ID = (int)_reader.GetValue(0);
                            l.Nome = (String)_reader.GetValue(1);
                            l.ImageURL = (String)_reader.GetValue(2);
                            l.Qtd = (int)_reader.GetValue(3);

                            litorais.Add(l);
                        }
                    }
                }
            }
            catch (Exception)
            {
                return litorais;
            }
            finally
            {
                connection.Close();
            }
            return litorais;
        }

        public List<Cidade> ListaCidades(int id, HttpContext context)
        {
            List<Cidade> cidades = new List<Cidade>();
            String sql_ListaCidades = "SELECT TB_PCIDADES.IDCIDADE, TB_PCIDADES.NOME, TB_PCIDADES.ESTADO, TB_PCIDADES.IMAGEURL, COUNT(IDPRAIA)"+ 
            " FROM TB_PCIDADES, TB_PPRAIA WHERE ID = @ID GROUP BY TB_PCIDADES.IDCIDADE, TB_PCIDADES.ID, TB_PCIDADES.NOME, TB_PCIDADES.ESTADO, TB_PCIDADES.IMAGEURL, TB_PPRAIA.IDPRAIA, TB_PPRAIA.IDCIDADE, TB_PPRAIA.NOME, TB_PPRAIA.IMAGEURL;";

            try
            {
                connection.Open();
                using (SqlCommand _command = new SqlCommand(sql_ListaCidades, connection))
                {
                    _command.Parameters.AddWithValue("@ID", id);
                    using (SqlDataReader _reader = _command.ExecuteReader())
                    {
                        while (_reader.Read())
                        {
                            Cidade c = new Cidade();
                            c.ID = (int)_reader.GetValue(0);
                            c.Nome = (String)_reader.GetValue(1);
                            c.Estado = (String)_reader.GetValue(2);
                            c.ImageURL = (String)_reader.GetValue(3);
                            c.Qtd = (int)_reader.GetValue(4);

                            cidades.Add(c);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                context.Response.Write(e.Message);
                return cidades;
            }
            finally
            {
                connection.Close();
            }
            return cidades;
        }

        public List<Praia> ListaPraias(int id)
        {
            List<Praia> praias = new List<Praia>();
            String sql_ListaPraias = "SELECT *, COUNT(IDQUIOSQUE) FROM TB_PPRAIA, TB_PQUIOSQUES WHERE IDCIDADE = @IDCIDADE GROUP BY TB_PPRAIA.IDPRAIA, TB_PPRAIA.IDCIDADE, TB_PPRAIA.NOME, TB_PPRAIA.IMAGEURL, TB_PQUIOSQUES.IDQUIOSQUE, TB_PQUIOSQUES.IDPRAIA, TB_PQUIOSQUES.NOME, TB_PQUIOSQUES.NUMEROQUIOSQUE, TB_PQUIOSQUES.RUA, TB_PQUIOSQUES.BAIRRO, TB_PQUIOSQUES.LAT, TB_PQUIOSQUES.LNG, TB_PQUIOSQUES.IMAGEURL;";

            try
            {
                connection.Open();
                using (SqlCommand _command = new SqlCommand(sql_ListaPraias, connection))
                {
                    _command.Parameters.AddWithValue("@IDCIDADE", id);
                    using (SqlDataReader _reader = _command.ExecuteReader())
                    {
                        while (_reader.Read())
                        {
                            Praia p = new Praia();
                            p.ID = (int)_reader.GetValue(0);
                            p.IDCidade = (int)_reader.GetValue(1);
                            p.Nome = (String)_reader.GetValue(2);
                            p.ImageURL = (String)_reader.GetValue(3);
                            p.Qtd = (int)_reader.GetValue(4);

                            praias.Add(p);
                        }
                    }
                }
            }
            catch (Exception)
            {
                return praias;
            }
            finally
            {
                connection.Close();
            }
            return praias;
        }

        public List<Quiosque> ListaQuiosques(int id)
        {
            List<Quiosque> quiosques = new List<Quiosque>();
            String sql_ListaQuiosques = "SELECT TB_PQUIOSQUES.IDQUIOSQUE, TB_PQUIOSQUES.NOME, TB_PQUIOSQUES.NUMEROQUIOSQUE, TB_PQUIOSQUES.RUA, TB_PQUIOSQUES.BAIRRO, TB_PQUIOSQUES.IMAGEURL FROM TB_PQUIOSQUES WHERE IDPRAIA = @IDPRAIA;";

            try
            {
                connection.Open();
                using (SqlCommand _command = new SqlCommand(sql_ListaQuiosques, connection))
                {
                    _command.Parameters.AddWithValue("@IDPRAIA", id);
                    using (SqlDataReader _reader = _command.ExecuteReader())
                    {
                        while (_reader.Read())
                        {
                            Quiosque q = new Quiosque();
                            q.ID = (int)_reader.GetValue(0);
                            q.Nome = (String)_reader.GetValue(1);
                            q.NumeroQuiosque = (int)_reader.GetValue(2);
                            q.Rua= (String)_reader.GetValue(3);
                            q.Bairro = (String)_reader.GetValue(4);
                            q.ImageURL = (String)_reader.GetValue(5);

                            quiosques.Add(q);
                        }
                    }
                }
            }
            catch (Exception)
            {
                return quiosques;
            }
            finally
            {
                connection.Close();
            }
            return quiosques;
        }

        public String RetornaImageURL(int id)
        {
            String ImageURL = String.Empty;
            String sql_RetornaImageURL = "SELECT IMAGEURL FROM TB_PQUIOSQUES WHERE IDQUIOSQUE = @IDQUIOSQUE;";

            try
            {
                connection.Open();
                using (SqlCommand _command = new SqlCommand(sql_RetornaImageURL, connection))
                {
                    _command.Parameters.AddWithValue("@IDQUIOSQUE", id);
                    ImageURL = (String)_command.ExecuteScalar();
                }
            }
            catch (Exception)
            {
                return ImageURL;
            }
            finally
            {
                connection.Close();
            }
            return ImageURL;
        }

        #endregion
        #endregion
    }
}
