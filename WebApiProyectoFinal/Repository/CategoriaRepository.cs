using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using WebApiProyectoFinal.Models;

namespace WebApiProyectoFinal.Repository
{
    public class CategoriaRepository
    {
        private readonly string _connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

        public List<CategoriaModel> GetCategorias()
        {
            try
            {
                using(var conn = new SqlConnection(_connectionString))
                using(var cmd = new SqlCommand("SELECT IdCategoria, NombreCategoria FROM Categoria", conn))
                {
                    conn.Open();
                    using (var reader = cmd.ExecuteReader())
                    {
                        var categorias = new List<CategoriaModel>();
                        while (reader.Read())
                        {
                            var categoria = new CategoriaModel
                            {
                                IdCategoria = reader.GetByte(0),
                                NombreCategoria = reader.GetString(1)
                            };
                            categorias.Add(categoria);
                        }
                        return categorias;
                    }
                }
            }
            catch (Exception)
            {
                return new List<CategoriaModel>();
            }
        }

        // INSERTAR
        public bool InsertCategoria(CategoriaModel categoria)
        {
            try
            {
                using (var conn = new SqlConnection(_connectionString))
                using (var cmd = new SqlCommand("INSERT INTO Categoria (NombreCategoria) VALUES (@NombreCategoria)", conn))
                {
                    cmd.Parameters.AddWithValue("@NombreCategoria", categoria.NombreCategoria);
                    conn.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();

                    return rowsAffected > 0;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        // ACTUALIZAR
        public bool UpdateCategoria(CategoriaModel categoria)
        {
            try
            {
                using (var conn = new SqlConnection(_connectionString))
                using (var cmd = new SqlCommand("UPDATE Categoria SET NombreCategoria = @NombreCategoria WHERE IdCategoria = @IdCategoria", conn))
                {
                    cmd.Parameters.AddWithValue("@IdCategoria", categoria.IdCategoria);
                    cmd.Parameters.AddWithValue("@NombreCategoria", categoria.NombreCategoria);
                    conn.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        // ELIMINAR
        public bool DeleteCategoria(byte idCategoria)
        {
            try
            {
                using (var conn = new SqlConnection(_connectionString))
                using (var cmd = new SqlCommand("DELETE FROM Categoria WHERE IdCategoria = @IdCategoria", conn))
                {
                    cmd.Parameters.AddWithValue("@IdCategoria", idCategoria);
                    conn.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        // OBTENER POR ID
        public CategoriaModel GetCategoriaById(byte idCategoria)
        {
            try
            {
                using (var conn = new SqlConnection(_connectionString))
                using (var cmd = new SqlCommand("SELECT IdCategoria, NombreCategoria FROM Categoria WHERE IdCategoria = @IdCategoria", conn))
                {
                    cmd.Parameters.AddWithValue("@IdCategoria", idCategoria);
                    conn.Open();
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new CategoriaModel
                            {
                                IdCategoria = reader.GetByte(0),
                                NombreCategoria = reader.GetString(1)
                            };
                        }
                        return null;
                    }
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}