using CaseControl.Api.Interfaces;
using CaseControl.Domain.DTOs;
using CaseControl.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Org.BouncyCastle.Asn1.Tsp;

namespace CaseControl.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EvidenceController : ControllerBase
    {
        //private readonly string _targetFilePath = Path.Combine(Directory.GetCurrentDirectory(), "UploadedFiles");
        private readonly string _targetFilePath = @"\\s460-aud06\CaseControlFiles";
        
        private readonly IEvidence _evidence;

        public EvidenceController(IEvidence type)
        {
            _evidence = type;

            if (!Directory.Exists(_targetFilePath))
            {
                Directory.CreateDirectory(_targetFilePath);
            }
        }


        [HttpGet]
        public async Task<IActionResult> GetAllEvidenceAsync([FromQuery] PaginationDTO pagination)
        {
            try
            {
                return Ok(await _evidence.GetAllEvidenceAsync(pagination));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("totalPages")]
        public async Task<ActionResult> GetPagesAsync([FromQuery] PaginationDTO pagination)
        {
            try
            {
                return Ok(await _evidence.GetTotalPagesAsync(pagination));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

                

        [HttpPost("uploadfile")]
        public async Task<IActionResult> UploadFile([FromForm]IFormFile file, [FromForm]string evidence)
        {
            try
            {
                if (file == null || file.Length == 0)
                {
                    return NotFound("No hay ningún archivo cargado.");
                }

                var evidenceModel = JsonConvert.DeserializeObject<Evidence>(evidence);
                if (evidenceModel == null || string.IsNullOrEmpty(evidenceModel.Name))
                {
                    return NotFound("No hay datos para el archivo cargado.");
                }

                return Ok(await _evidence.UploadEvidenceAsync(file, evidenceModel, _targetFilePath));
            }
            catch (Exception ex)
            {
                return BadRequest("Se ha presentado un error: /n" + ex.Message);
            }
        }


        [HttpGet("downloadfile/{hash}")]
        public async Task<IActionResult> DownloadFile(string hash)
        {
            try
            {
                var evidence = await _evidence.GetEvidenceByHashAsync(hash);
                if(evidence == null)
                {
                    return NotFound();
                }

                if (!System.IO.File.Exists(evidence.Path))
                {
                    return NotFound("El archivo no existe");
                }

                var fileBytes = System.IO.File.ReadAllBytes(evidence.Path);
                return File(fileBytes, "application/octet-stream", $"{evidence.Name}{evidence.Extension}");
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error descargando el archivo", error = ex.Message });
            }
        }

        [HttpDelete("{hash}")]
        public async Task<IActionResult> DeleteBinnacleAsync(string hash)
        {
            var exists = _evidence.EvidenceExists(hash);
            if (!exists)
            {
                return NotFound();
            }

            await _evidence.DeleteEvidenceAsync(hash);

            return NoContent();
        }

        //[HttpGet("getAllFiles")]
        //public IActionResult GetAllFiles()
        //{
        //    try
        //    {
        //        // Simula la obtención de los archivos desde una base de datos o sistema de archivos
        //        var files = _dbContext.EvidenceRecords.ToList(); // Obtén todos los objetos evidenceRecord de la base de datos

        //        // Simula la creación de una lista con la información de los archivos y sus objetos
        //        var fileInfoList = files.Select(file => new
        //        {
        //            file.Id,
        //            file.Filename,
        //            file.Description,
        //            file.CaseId,
        //            file.UploadDate,
        //            // Agregar cualquier otro dato relevante del evidenceRecord
        //        }).ToList();

        //        return Ok(fileInfoList); // Devuelve la lista de archivos
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, new { message = "Error fetching files", error = ex.Message });
        //    }
        //}

    }
}
