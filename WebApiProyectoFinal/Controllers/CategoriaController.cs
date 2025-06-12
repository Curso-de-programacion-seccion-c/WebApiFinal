using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using WebApiProyectoFinal.Repository;

namespace WebApiProyectoFinal.Controllers
{
    [System.Web.Http.RoutePrefix("api/categoria")]
    public class CategoriaController : ApiController
    {
        private readonly CategoriaRepository _categoriaRepository = new CategoriaRepository();

        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("")]
        public IHttpActionResult GetCategorias()
        {
            try
            {
                var categorias = _categoriaRepository.GetCategorias();
                if (categorias == null || !categorias.Any())
                {
                    return NotFound();
                }
                return Ok(categorias);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("")]
        public IHttpActionResult InsertCategoria([FromBody] Models.CategoriaModel categoria)
        {
            if (categoria == null || string.IsNullOrEmpty(categoria.NombreCategoria))
            {
                return BadRequest("Body no valido");
            }

            try
            {
                bool isInserted = _categoriaRepository.InsertCategoria(categoria);
                if (isInserted)
                {
                    return Ok("Category inserted successfully.");
                }
                else
                {
                    return BadRequest("Failed to insert category.");
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [System.Web.Http.HttpPut]
        [System.Web.Http.Route("{id}")]
        public IHttpActionResult UpdateCategoria(byte id, [FromBody] Models.CategoriaModel categoria)
        {
            if (categoria == null || string.IsNullOrEmpty(categoria.NombreCategoria))
            {
                return BadRequest("Body no valido");
            }

            var body = new Models.CategoriaModel()
            {
                IdCategoria = id,
                NombreCategoria = categoria.NombreCategoria
            };

            try
            {
                bool isUpdated = _categoriaRepository.UpdateCategoria(body);
                if (isUpdated)
                {
                    return Ok("Category updated successfully.");
                }
                else
                {
                    return BadRequest("Failed to update category.");
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [System.Web.Http.HttpDelete]
        [System.Web.Http.Route("{id}")]
        public IHttpActionResult DeleteCategoria(byte id)
        {
            try
            {
                bool isDeleted = _categoriaRepository.DeleteCategoria(id);
                if (isDeleted)
                {
                    return Ok("Category deleted successfully.");
                }
                else
                {
                    return BadRequest("Failed to delete category.");
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("{id}")]
        public IHttpActionResult GetCategoriaById(byte id)
        {
            try
            {
                var categoria = _categoriaRepository.GetCategoriaById(id);
                if (categoria == null)
                {
                    return NotFound();
                }
                return Ok(categoria);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

    }
}