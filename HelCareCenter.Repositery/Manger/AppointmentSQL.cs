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
    public class AppointmentSQL : General<Appointment>, IAppointment
    {

        public AppointmentSQL(DBContext _db) : base(_db)
        {

        }

        public async Task<Appointment> Accept(Appointment appointment)
        {
            appointment.isAccepted = true;
            await Update(appointment); 

            
            return appointment;
        }

        public override async Task<Appointment> ADD(Appointment addedItem)
        {
           var result =  await CheckAvilability(addedItem.DoctorID, addedItem.From, addedItem.To);
            if (result)                
            return await base.ADD(addedItem);

            return new Appointment(); 
        }
        public async Task<bool> CheckAvilability(int doctorId, DateTime from, DateTime to)
        {
            bool result = false;

           var appointmentres =  await db.Appointments.Where(i => i.DoctorID == doctorId && to < i.From).ToListAsync();
            if (appointmentres.Count == 0) result = true;    

            return result; 
        }
    }
}

