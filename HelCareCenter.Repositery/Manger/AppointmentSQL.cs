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
           var result =  await IsAvilable(addedItem.DoctorID, addedItem.From, addedItem.To);
            if (result)                
            return await base.ADD(addedItem);

            return new Appointment(); 
        }
       

        public async Task<bool> IsAvilable(int doctorId, DateTime from, DateTime to)
        {
            bool result = true;

            var periods = GetAllDates(from, to, 3600);

            foreach (var period in periods)
            {
                var appointmentres = await db.Appointments.Where(i => i.DoctorID == doctorId &&  i.From < period && period < i.To).ToListAsync();
                if (appointmentres.Count > 0) return false;
               
            }
            return result;
        }

        public static List<DateTime> GetAllDates(DateTime startDate, DateTime endDate, int slotWithSecond)
        {
            List<DateTime> allDates = new List<DateTime>();
            for (DateTime date = startDate; date <= endDate; date = date.AddSeconds(slotWithSecond))
                allDates.Add(date);
            return allDates;
        }
    }

}

