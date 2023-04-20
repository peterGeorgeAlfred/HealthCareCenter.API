﻿using HelCareCenter.Models.Entity;
using HelCareCenter.Repositery.IGeneral;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelCareCenter.Repositery.IManger
{
    public interface  IDoctor : IGeneral<Doctor>
    {
        Task<List<Doctor>> GetAllDoctorByClinicID(int id);
    }
}
