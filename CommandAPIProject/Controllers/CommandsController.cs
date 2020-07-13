using System.Collections.Generic;
using AutoMapper;
using CommandProject.Data;
using CommandProject.DTO;
using CommandProject.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace CommandProject.Controllers
{
    //inheritance from controller base will bring some methods that will be used
    //There is a controller class on aspnetcore but to inherit from that would bring some unecessary things to the application

    //by decorating with this tag will bring some interesting behaviors to the class
    //by defining the main route you will not need to keep repeting the same one to every endpoint [Route("api/[controller]")]
    //[controller] on the route will substitute the [controller] with the name of your controller  api/commands
    //The way to define the verb to each endpoint is using [HttpGet] or [HttpPost] over the endpoint
    //You can also define the especific route to each endpoint on the verb especification  [HttpGet("{id}")] and it will be incremented on the original route
    [Route("api/commands")]
    [ApiController]
    public class CommandsController : ControllerBase {
        private readonly ICommanderRepository _repository;
        private readonly IMapper _mapper;

        public CommandsController(ICommanderRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }


        //GET api/commands
        [HttpGet]
        public ActionResult <IEnumerable<CommandReadDTO>> GetAll(){
            
            var commandItems = _repository.GetAllAsync();

            return Ok(_mapper.Map<IEnumerable<CommandReadDTO>>(commandItems));
        }

        //GET api/commands/{id}
        [HttpGet("{id}", Name="GetById")]
        public ActionResult <CommandReadDTO> GetById(int id){

            var commandItem = _repository.GetById(id);

            if(commandItem == null)
                return NotFound();

            return Ok(_mapper.Map<CommandReadDTO>(commandItem));
        }

        //POST api/commands
        [HttpPost]
        public ActionResult <CommandReadDTO> CreateCommand(CommandCreateDTO cmd){

            var command = _mapper.Map<Command>(cmd);

            _repository.CreateCommand(command);

            _repository.SaveChanges();

            var commandReadDTO = _mapper.Map<CommandReadDTO>(command);

            //Return as a result the url that can be used to call the object creates
            //return as well a 201 result
            //For this to work is necessary to name your endpoint that you want to return, in this case the GET by Id
            return CreatedAtRoute(nameof(GetById), new {Id = commandReadDTO.Id}, commandReadDTO);
            //return Ok(commandReadDTO);
        }

        //PUT api/commands/{id}
        [HttpPut]
        public ActionResult <CommandReadDTO> UpdateCommand(int id,CommandCreateDTO cmd){

            var commandModelfromRepo = _repository.GetById(id);

            if(commandModelfromRepo == null)
                return NotFound();

            _mapper.Map(cmd,commandModelfromRepo);

            _repository.UpdateCommand(commandModelfromRepo);
            _repository.SaveChanges();

            return NoContent();
        }

        //PUT api/commands/{id}
        [HttpPatch("{id}")]
        public ActionResult <CommandReadDTO> PartialCommandUpdate(int id,JsonPatchDocument<CommandUpdateDTO> patchDoc){

            var commandModelfromRepo = _repository.GetById(id);

            if(commandModelfromRepo == null)
                return NotFound();

            var commandToPatch = _mapper.Map<CommandUpdateDTO> (commandModelfromRepo);

            patchDoc.ApplyTo(commandToPatch, ModelState);

            if(!TryValidateModel(commandToPatch))
                return ValidationProblem(ModelState);

            _mapper.Map(commandToPatch,commandModelfromRepo);
            _repository.UpdateCommand(commandModelfromRepo);
            _repository.SaveChanges();

            return NoContent();
        }

        //DELETE api/commands/{id}
        [HttpDelete("{id}")]
        public ActionResult DeleteCommand(int id){

            var commandModelfromRepo = _repository.GetById(id);

            if(commandModelfromRepo == null)
                return NotFound();

            
            _repository.DeleteCommand(commandModelfromRepo);
            _repository.SaveChanges();

            return NoContent();
        }
    }
}