
using Hospital.BAL.Configurations;
using HospitalManagementSystem.Common.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace HospitalManagementSystem.BAL.Services.PatientRepo
{
    public class PatientRegistrationService : IPatientRegistrationService, IDisposable
    {
        readonly AppDbContext _context;
        public PatientRegistrationService(AppDbContext appDbContext)
        {
            _context = appDbContext;
        }
        public void Dispose()
        {
            _context.Dispose();
        }

        public async Task<bool> Delete(int? id, CancellationToken ct = default)
        {
            try
            {
                PatientRegistration patientregistration = (PatientRegistration)await Get(id);

                _context.PatientRegistration.Remove(patientregistration);
                var result = await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        public async Task<bool> Edit(int? id, PatientRegistration patientregistration, CancellationToken ct = default)
        {
            PatientRegistration data = (PatientRegistration)await Get(id);

            try
            {
                data.Firstname = patientregistration.Firstname;
                data.Middlename = patientregistration.Middlename;
                data.Lastname = patientregistration.Lastname;
                data.Gender = patientregistration.Gender;
                data.DOB = patientregistration.DOB;
                data.Bloodgroup = patientregistration.Bloodgroup;
                data.Maritalstatus = patientregistration.Maritalstatus;
                data.Phoneno = patientregistration.Phoneno;
                data.Address = patientregistration.Address;
                data.Complaints = patientregistration.Complaints;
                data.Diagnosis = patientregistration.Diagnosis;

                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<object> Get(CancellationToken ct = default)
        {
            try
            {
                return await _context.PatientRegistration.ToListAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<object> Get(int? id, CancellationToken ct = default)
        {
            try
            {
                var result = await _context.PatientRegistration.FindAsync(id);

                if (result == null)
                {
                    throw new Exception("No records found.");
                }
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> Post(PatientRegistration patientregistration, CancellationToken ct = default)
        {
            try
            {
                await _context.PatientRegistration.AddAsync(patientregistration, ct);
                await _context.SaveChangesAsync(ct);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        
    }
}
