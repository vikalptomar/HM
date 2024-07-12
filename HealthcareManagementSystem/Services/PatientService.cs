// Patient Management: Add, view, edit, and delete patient information.
using HealthcareManagementSystem.Models;
using HealthcareManagementSystem.DB;
using Microsoft.EntityFrameworkCore;

namespace HealthcareManagementSystem.Services
{
    public interface IPatientService
    {
        //view
        Task<List<PatientModel>> GetAllPatients();
        Task<PatientModel> GetPatientById(int _id);

        //add
        Task<PatientModel> InsertPatient(PatientModel patient);

        //edit
        Task<PatientModel> UpdatePatientById(int _id,PatientModel patient);

        //delete
        Task<bool> DeletePatientById(int _id);

    }
    public class PatientService : IPatientService
    {
        public HM_dbContext _ObjDBContext;
        public PatientService(HM_dbContext objDBContext)
        {
            _ObjDBContext = objDBContext;
        }

        public async Task<List<PatientModel>> GetAllPatients()
        {
            return await _ObjDBContext.Patients.ToListAsync();
        }
        public async Task<PatientModel> GetPatientById(int _id)
        {   
            return await _ObjDBContext.Patients.Where(p => p.PatientId == _id).FirstOrDefaultAsync();
            //return await _ObjDBContext.Patients.FirstOrDefaultAsync(p => p.PatientId == _id);
            //return await _ObjDBContext.Patients.FindAsync(_id);
        }
        public async Task<PatientModel> InsertPatient(PatientModel patient)
        {
            await _ObjDBContext.Patients.AddAsync(patient);
            await _ObjDBContext.SaveChangesAsync();
            return patient;
        }
        public async Task<PatientModel> UpdatePatientById(int _id,PatientModel patient)
        {
            var existingPatient = await _ObjDBContext.Patients.FindAsync(_id);
            if (existingPatient != null)
            {
                existingPatient.PatientId = _id;
                existingPatient.FirstName = patient.FirstName;
                existingPatient.LastName = patient.LastName;
                existingPatient.Email = patient.Email;
                existingPatient.DOB = patient.DOB;
                existingPatient.Gender = patient.Gender;
                existingPatient.PhoneNumber = patient.PhoneNumber;
                existingPatient.Address = patient.Address;
                existingPatient.DateRegistered = patient.DateRegistered;
                await _ObjDBContext.SaveChangesAsync();
            }
            return existingPatient;

        }
        public async Task<bool> DeletePatientById(int _id)
        {
            var existingPatient = await _ObjDBContext.Patients.FindAsync(_id);
            if (existingPatient != null)
            {
                _ObjDBContext.Remove(existingPatient);
                await _ObjDBContext.SaveChangesAsync();
                return true;

            }
          return false;
        }
    }
}