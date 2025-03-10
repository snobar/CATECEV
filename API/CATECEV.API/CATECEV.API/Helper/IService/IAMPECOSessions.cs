﻿using CATECEV.API.Models;
using CATECEV.API.Models.AMPECO.resource.Session;

namespace CATECEV.API.Helper.IService
{
    public interface IAMPECOSessions
    {
        Task<AMPECOResponseModel<IEnumerable<ChargingSession>>> GetChargingSession(int pageNumber, int pageSize = 100);
    }
}
