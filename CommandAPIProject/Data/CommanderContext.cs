using CommandProject.Models;
using Microsoft.EntityFrameworkCore;

namespace CommandProject.Data{

    public class CommanderContext : DbContext{
        public CommanderContext(DbContextOptions<CommanderContext> opt): base(opt)
        {
            
        }

        //create a representation of the command object to be created on the database
        //If you have other models just create more representations
        public DbSet<Command> Commands {get; set;}
    }

}