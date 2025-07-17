using System;
using System.Collections.Generic;
using System.Text;

namespace LC.ENTITIES.Custom
{
    public class LawyerDirectoryTemp
    {
        public IEnumerable<LawyerTemp> Lawyers { get; set; }
        public int RecordsTotal { get; set; }
        public int Active { get; set; }
    }
    public class LawyerTemp
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Surnames { get; set; }
        public List<LawyerSpecialtiesThemesTemp> Specialties { get; set; }
        public List<LawyerSpecialtiesThemesTemp> Themes { get; set; }
        public string Department { get; set; }
        public string District { get; set; }
        public decimal Price { get; set; }
        public string LastConnection { get; set; }
        public string UrlImage { get; set; }
        public int Cases { get; set; }
        public string RegisteredAt { get; set; }
        public double Qualification { get; set; }
        public string AboutLawyer { get; set; }
        public bool IsFreeFee { get; set; }
        public bool HasPlans { get; set; }
    }
    public class LawyerSpecialtiesThemesTemp
    {
        public string Name { get; set; }
    }
}
