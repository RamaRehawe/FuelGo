﻿namespace FuelGo.Dto
{
    public class ReqCenterAddingDto
    {
        public int NeighborhoodId { get; set; }
        public int CityId { get; set; }
        public string Phone { get; set; }
        public string Name { get; set; }
        public double Lat { get; set; }
        public double Long { get; set; }
        public string LocationDescription { get; set; }
    }
}
