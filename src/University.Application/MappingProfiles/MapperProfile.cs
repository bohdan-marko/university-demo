using AutoMapper;
using University.Application.DTO;
using University.Application.Models.Create;
using University.Application.Models.Update;
using University.Application.Models;
using University.DAL.Domain;

namespace University.Application.MappingProfiles;

public class MapperProfile : Profile
{
	public MapperProfile()
	{
		CreateMap<Worker, WorkerDto>().ReverseMap();
        CreateMap<Worker, WorkerCreateRequest>().ReverseMap();
        CreateMap<Worker, WorkerUpdateRequest>().ReverseMap();

        CreateMap<User, UserResponse>();
    }
}
