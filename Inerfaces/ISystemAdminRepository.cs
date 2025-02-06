﻿using FuelGo.Models;

namespace FuelGo.Inerfaces
{
    public interface ISystemAdminRepository : IBaseRepository
    {
        bool AddAdmin(User user);
        Admin GetAdminById(int id);
        ICollection<Center> GetCenters();
        bool AddCenter(Center center);
    }
}
