using CommandProject.Models;
using System.Collections.Generic;

namespace CommandProject.Data{
    public interface ICommanderRepository{
        
        IEnumerable<Command> GetAllAsync();

        Command GetById(int id);
        void CreateCommand(Command cmd);
        bool SaveChanges();
        void UpdateCommand(Command cmd);
        void DeleteCommand(Command cmd);
    }
}