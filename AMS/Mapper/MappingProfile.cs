using AMS.Data;
using AMS.Models;
using AutoMapper;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace AMS.Mapper
{
    public class MappingProfile : Profile
    {

        public MappingProfile() 
        {
            CreateMap<UserMasterModel, UserMaster>();
            CreateMap<UserMaster, UserMasterModel>();

            CreateMap<ProductModel, Product>();
            CreateMap<Product, ProductModel>();


        }
     
    }
}
