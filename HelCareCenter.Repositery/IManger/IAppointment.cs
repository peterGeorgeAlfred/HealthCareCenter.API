using HelCareCenter.Models.Entity;
using HelCareCenter.Repositery.IGeneral;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelCareCenter.Repositery.IManger
{
    public interface IAppointment :IGeneral<Appointment>
    {
        Task<bool> IsAvilable(int doctorId, DateTime from, DateTime to);

        Task<Appointment> Accept(Appointment appointment);
    }
}
