using System.Collections.Generic;
using CommandProject.Models;

namespace CommandProject.Data{
    public class MockCommanderRepository : ICommanderRepository
    {
        public void CreateCommand(Command cmd)
        {
            throw new System.NotImplementedException();
        }

        public void DeleteCommand(Command cmd)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<Command> GetAllAsync()
        {
            var commands = new List<Command>{
                new Command{
                    Id = 0,
                    HowTo = "Boil an Egg",
                    Line = "Boli an Egg",
                    Platform = "Kettle and Pan"
                },
                new Command{
                    Id = 1,
                    HowTo = "Make pasta",
                    Line = "Make pasta",
                    Platform = "Pan and water"
                },
                new Command{
                    Id = 0,
                    HowTo = "Make sandwiche",
                    Line = "Make sandwiche",
                    Platform = "Plate, Bread and Mayo"
                }
            };
            
            return commands;
        }

        public Command GetById(int id)
        {
            return new Command{
                Id = 0,
                HowTo = "Boil an Egg",
                Line = "Boli water",
                Platform = "Kettle and Pan"
            };
        }

        public bool SaveChanges()
        {
            throw new System.NotImplementedException();
        }

        public void UpdateCommand(Command cmd)
        {
            throw new System.NotImplementedException();
        }
    }
}