using AutoMapper;
using CommandProject.DTO;
using CommandProject.Models;

namespace CommandProject.Profiles{
    //Profile comes from the automapper extension
    public class CommandsProfile : Profile
    {
        public CommandsProfile(){
            CreateMap<Command, CommandReadDTO>();
            CreateMap<Command, CommandCreateDTO>();
            CreateMap<CommandCreateDTO, Command>();
            CreateMap<Command, CommandUpdateDTO>();
            CreateMap<CommandUpdateDTO, Command>();
        }
    }

}