using HelCareCenter.Context;
using HelCareCenter.Models.Entity;
using HelCareCenter.Repositery.IGeneral;
using HelCareCenter.Repositery.IManger;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelCareCenter.Repositery.Manger
{
    public class DoctorSQL : General<Doctor> ,  IDoctor
    {

        public DoctorSQL(DBContext _db) : base(_db)
        {

        }
        public async Task<List<Doctor>> GetAllDoctorByClinicID(int id)
        {
            return await db.Doctors.Where(i => i.ClinicId == id).ToListAsync(); 
        }
    }
}
