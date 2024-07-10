using Microsoft.AspNetCore.Mvc;
using HealthcareManagementSystem.Services;
using HealthcareManagementSystem.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HealthcareManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientController : ControllerBase
    {

        IPatientService _patientService;

        public PatientController(IPatientService patientService)
        {
            _patientService = patientService;
        }

        // GET: api/<PatientController>
        [HttpGet("GetPatient")]
        public async Task<IActionResult> GetPatient()
        {
            var patients = await _patientService.GetAllPatients();
            return Ok(patients);

        }


        // GET api/<PatientController>/5
        [HttpGet("GetPatientById")]
        public async Task<IActionResult> GetPatientById(int id)
        {
            var patient = _patientService.GetPatientById(id);
            if (patient == null)
            {
                return NotFound();
            }
            return Ok(patient);
        }

        [HttpPost("InsertPatient")]
        public async Task<IActionResult> InsertPatient(PatientModel patient)
        {
            var currentPatient = await _patientService.InsertPatient(patient);
            if (currentPatient == null)
            {
                return NotFound();
            }
            return Ok(new { currentPatient });
        }

        [HttpPut("UpdatePatientById")]
        public async Task<IActionResult> UpdatePatientById(int id, PatientModel patient)
        {
            if (id != patient.PatientId)
            {
                return BadRequest();
            }
            var updatePatient = await _patientService.UpdatePatientById(id, patient);
            if (updatePatient == null)
            {
                return NotFound();
            }
            return Ok(updatePatient);
        }

        [HttpDelete("DeletePatientById")]
        public async Task<IActionResult> DeletePatientById(int id)
        {
            var result = await _patientService.DeletePatientById(id);
            if (!result)
            {
                return NotFound(); 
            }
            return Ok(result);

        }
    }
}
